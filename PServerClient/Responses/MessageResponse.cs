using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class MessageResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.Message; } }
      public IList<string> MessageLines { get; set; }
      public override int LineCount { get { return 0; } }
      public override void ProcessResponse(IList<string> lines)
      {
         MessageLines = lines;
         base.ProcessResponse(lines);
      }
   }
}
