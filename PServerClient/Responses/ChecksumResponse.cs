using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class ChecksumResponse : ResponseBase
   {
      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }

      public override int LineCount { get { return 0; } }
   }
}
