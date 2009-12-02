using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class ErrorResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.Error; } }
      public override int LineCount { get { return 0; } }
      public string ErrorMessage { get; set; }
      public override void ProcessResponse(IList<string> lines)
      {
         ErrorMessage = lines[0];
      }

 
   }
}
