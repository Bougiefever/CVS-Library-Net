using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class RemovedResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.Removed; } }
      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }
   }
}
