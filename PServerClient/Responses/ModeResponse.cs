using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class ModeResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.Mode; } }
      public override void ProcessResponse(IList<string> lines)
      {
         base.ProcessResponse(lines);
      }
   }
}
