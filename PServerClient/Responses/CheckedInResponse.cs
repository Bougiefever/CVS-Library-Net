using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class CheckedInResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.CheckedIn; } }
      public override int LineCount { get { return 0; } }
      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }

   }
}
