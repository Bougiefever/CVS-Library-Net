using System.Collections.Generic;

namespace PServerClient.Responses
{
   public class CheckedInResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.CheckedIn; } }
      public override int LineCount { get { return 0; } }
      public override void ProcessResponse(IList<string> lines)
      {
         base.ProcessResponse(lines);
      }

   }
}
