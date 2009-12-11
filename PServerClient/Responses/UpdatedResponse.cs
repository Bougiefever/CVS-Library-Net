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

      public override int LineCount { get { return 5; } }
      public ReceiveFile File { get; set; }

      public override void ProcessResponse(IList<string> lines)
      {
         string module = lines[0];
         string cvsPath = lines[1];
         string fileNameRevision = lines[2];
         string fileProperties = lines[3];
         string fileLength = lines[4];

         string fileName = ResponseHelper.GetFileNameFromUpdatedLine(fileNameRevision);
         string revision = ResponseHelper.GetRevisionFromUpdatedLine(fileNameRevision);

         File = new ReceiveFile()
                   {
                      FileName = fileName,
                      Revision = revision,
                      Module = module, 
                      CvsPath = cvsPath, 
                      FileLength = Convert.ToInt64(fileLength)
                   };
         base.ProcessResponse(lines);
      }
   }
}
