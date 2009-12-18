using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   public abstract class FileResponseBase : IFileResponse
   {

      public abstract ResponseType ResponseType { get; }
      public virtual string DisplayResponse()
      {
         return File.CvsPath;
      }

      public virtual int LineCount { get { return 5; } }

      public virtual void ProcessResponse(IList<string> lines)
      {
         string module = lines[0];
         string cvsPath = lines[1];
         string fileNameRevision = lines[2];
         string fileProperties = lines[3];
         string fileLength = lines[4];

         string fileName = ResponseHelper.GetFileNameFromUpdatedLine(fileNameRevision);
         string revision = ResponseHelper.GetRevisionFromUpdatedLine(fileNameRevision);

         File = new ReceiveFile
                   {
                      FileName = fileName,
                      Revision = revision,
                      CvsPath = cvsPath,
                      Path = module.Split(new[] {"/"}, StringSplitOptions.RemoveEmptyEntries),
                      Properties = fileProperties,
                      FileLength = Convert.ToInt64(fileLength)
                   };
      }
      public ReceiveFile File { get; set; }
   }
}