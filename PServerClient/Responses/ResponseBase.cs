using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public abstract class ResponseBase : IResponse
   {
      public virtual string ResponseString { get; set; }
      public bool Success { get; set; }
      public string ErrorMessage { get; set; }
      public abstract void ProcessResponse();
   }
}
