using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.LocalFileSystem;

namespace PServerClient.Responses
{
   public class RcsDiffResponse : ResponseBase, IFileResponse
   {
      public override ResponseType ResponseType { get { return ResponseType.RcsDiff; } }
      public ReceiveFile File { get; set; }

      public override void ProcessResponse(IList<string> lines)
      {
         base.ProcessResponse(lines);
      }
   }
}
