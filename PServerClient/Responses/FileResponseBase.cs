using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   public abstract class FileResponseBase : ResponseBase, IFileResponse
   {
      #region IFileResponse Members

      public abstract override ResponseType ResponseType { get; }

      public override string DisplayResponse()
      {
         return File.RepositoryPath;
      }

      public override int LineCount { get { return 5; } }

      public override void ProcessResponse(IList<string> lines)
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
                      Name = fileName,
                      Revision = revision,
                      RepositoryPath = cvsPath,
                      Path = module.Split(new[] {"/"}, StringSplitOptions.RemoveEmptyEntries),
                      Properties = fileProperties,
                      Length = Convert.ToInt64(fileLength)
                   };
         ResponseLines = new string[LineCount];
         ResponseLines[0] = ResponseHelper.ResponseNames[(int) ResponseType] + " " + lines[0];
         for (int i = 1; i < LineCount; i++)
            ResponseLines[i] = lines[i];
      }

      public ReceiveFile File { get; set; }

      #endregion
   }
}