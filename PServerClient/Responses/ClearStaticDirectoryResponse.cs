using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public class ClearStaticDirectoryResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.ClearStaticDirectory; } }
      public string ModuleName { get; set; }
      public string CvsDirectory { get; set; }
      public override int LineCount { get { return 2; } }
      public override void ProcessResponse(IList<string> lines)
      {
         ModuleName = lines[0];
         CvsDirectory = lines[1];
         base.ProcessResponse(lines);
      }
   }
}
