using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace PServerClient.Responses
{
   public abstract class ResponseBase : IResponse
   {
      public string[] ResponseLines { get; internal set; }
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
   }
}