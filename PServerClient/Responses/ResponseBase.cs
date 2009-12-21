using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PServerClient.Responses
{
   public abstract class ResponseBase : IResponse
   {
      internal string[] ResponseLines;
      public virtual void ProcessResponse(IList<string> lines)
      {
         ResponseLines = new string[LineCount];
         ResponseLines[0] = ResponseHelper.ResponseNames[(int)ResponseType] + " " + lines[0];
         for (int i = 1; i < LineCount; i++)
            ResponseLines[i] = lines[i];
      }
      public abstract ResponseType ResponseType { get; }
      public virtual string DisplayResponse()
      {
         string response = String.Join("\n", ResponseLines);
         return response;
      }
      public virtual int LineCount { get { return 1; } }

      public virtual XDocument ToXML()
      {
         XDocument xdoc = new XDocument(new XElement("Responses",
                                                     new XElement("Response",
                                                                  new XElement("Name", ResponseType.ToString()),
                                                                  new XElement("ResponseType", (int)ResponseType),
                                                                  new XElement("ProcessLines"))));
         XElement xLines = xdoc.Descendants("ProcessLines").First();

         foreach (string s in ResponseLines)
         {
            XElement line = new XElement("Line", s);
            xLines.Add(line);
         }
         return xdoc;
      }
   }
}