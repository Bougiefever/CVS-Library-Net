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
      public Entry CvsEntry { get; set; }

      public override int LineCount { get { return 5; } }
      public override void ProcessResponse(IList<string> lines)
      {
         string line1 = lines[0];
         string line2 = lines[1];
         string line3 = lines[2];
         string line4 = lines[3];
         string line5 = lines[4];

         string[] folderNames = line1.Split(new char[] {'/'}, StringSplitOptions.None);
         //CvsPath = lines[1];
         //CvsEntry = new Entry();
         //CvsEntry.FileName = lines[2];
         //CvsEntry.Properties = lines[3];
         //CvsEntry.FileLength = Convert.ToInt32(lines[4]);
         base.ProcessResponse(lines);
      }
   }
}
