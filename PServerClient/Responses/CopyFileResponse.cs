using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class CopyFileResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.CopyFile; } }
      public override void ProcessResponse(IList<string> lines)
      {
         base.ProcessResponse(lines);
      }

   }
}
