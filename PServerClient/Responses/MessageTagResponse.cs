using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class MessageTagResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.MessageTag; } }
      public IList<string> MessageLines { get; set; }
      public override int LineCount { get { return 1; } }
      public override void ProcessResponse(IList<string> lines)
      {
         MessageLines = lines;
         base.ProcessResponse(lines);
      }
   }
}
