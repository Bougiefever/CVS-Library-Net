using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class NullResponse : IResponse
   {
      public int LineCount { get { return 0; } }
      public ResponseType ResponseType { get { return ResponseType.Unknown; } }
      public void ProcessResponse(IList<string> lines)
      {
         // do nothing
      }

   }
}
