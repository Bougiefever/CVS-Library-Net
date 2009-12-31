using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PServerClient.Responses
{
   public abstract class ResponseBase : IResponse
   {
      public IList<string> Lines { get; set; }

      public virtual void Process(IList<string> lines)
      {
         Lines = new List<string>(LineCount);
         //ResponseLines[0] = ResponseHelper.ResponseNames[(int) Type] + " " + lines[0];
         for (int i = 0; i < LineCount; i++)
            Lines.Add(lines[i]);
      }

      public abstract ResponseType Type { get; }

      public virtual string Display()
      {
         string response = String.Join("\n", Lines.ToArray());
         return response;
      }

      public XElement GetXElement()
      {
         XElement responseElement = new XElement("Response",
                                                 new XElement("ClassName", GetType().FullName),
                                                 new XElement("Name", ResponseHelper.ResponseNames[(int)Type]),
                                                 new XElement("Lines"));
         XElement linesElement = responseElement.Descendants("Lines").First();

         foreach (string s in Lines)
         {
            XElement line = new XElement("Line", s);
            linesElement.Add(line);
         }
         if (this is IFileResponse)
         {
            IFileResponse fileResponse = (IFileResponse)this;
            string length = fileResponse.File.Length.ToString();
            string contents = ResponseHelper.FileContentsToByteArrayString(fileResponse.File.Contents);
            XElement responseFile = new XElement("File",
                                                 new XElement("Length", length),
                                                 new XElement("Contents", contents));
            responseElement.Add(responseFile);
         }
         return responseElement;
      }

      public virtual int LineCount { get { return 1; } }
   }
}