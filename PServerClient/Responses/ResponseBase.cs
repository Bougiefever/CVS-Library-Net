using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public abstract class ResponseBase : IResponse
   {
      public virtual void ProcessResponse(IList<string> lines)
      {
         ResponseText = ResponseHelper.ResponseNames[(int)ResponseType];
         foreach(string l in lines)
            ResponseText += l + (char)10;
      }
      public abstract ResponseType ResponseType { get; }
      public virtual int LineCount { get { return 1; } }
      public string ResponseText { get; set; }
   }
}
