using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.LocalFileSystem;

namespace PServerClient.Responses
{
   public class UpdateExistingResponse : ResponseBase, IFileResponse
   {
      public override ResponseType ResponseType { get { return ResponseType.UpdateExisting; } }
      public string ModuleName { get; set; }
      public string CvsPath { get; set; }
      public long FileLength { get; set; }
      public Entry CvsEntry { get; set; }

      public override void ProcessResponse(IList<string> lines)
      {
         ModuleName = lines[0];
         CvsPath = lines[1];
         CvsEntry = new Entry();
         CvsEntry.FileName = lines[2];
         CvsEntry.FileLength = Convert.ToInt32(lines[4]);
         CvsEntry.FileContents = Encoding.ASCII.GetBytes(lines[5]);
      }
   }
}
