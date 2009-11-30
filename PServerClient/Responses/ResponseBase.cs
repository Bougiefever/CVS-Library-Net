using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public abstract class ResponseBase : IResponse
   {
      //public virtual string ResponseString { get; set; }
      //public bool Success { get; set; }
      //ublic string ErrorMessage { get; set; }

      public ResponseBase()
      {
         ResponseLines = new List<string>();
      }

      public abstract void ProcessResponse(IList<string> lines);
      public virtual int LineCount { get { return 1; } }
      public IList<string> ResponseLines { get; set; }
   }
}
