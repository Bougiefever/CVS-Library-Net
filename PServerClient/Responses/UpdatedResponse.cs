using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.LocalFileSystem;

namespace PServerClient.Responses
{
   public class UpdatedResponse : ResponseBase, IFileResponse
   {
      public override ResponseType ResponseType { get { return ResponseType.Updated; } }
      public string ModuleName { get; set; }
      public string CvsPath { get; set; }
      public long FileLength { get; set; }
      public Entry CvsEntry { get; set; }

      public override int LineCount { get { return 5; } }
      public override void ProcessResponse(IList<string> lines)
      {
         ModuleName = lines[0];
         CvsPath = lines[1];
         CvsEntry = new Entry();
         CvsEntry.FileName = lines[2];
         CvsEntry.Properties = lines[3];
         CvsEntry.FileLength = Convert.ToInt32(lines[4]);
         base.ProcessResponse(lines);
      }
   }
}
