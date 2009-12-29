using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   public abstract class FileResponseBase : ResponseBase, IFileResponse
   {
      #region IFileResponse Members

      public abstract override ResponseType Type { get; }

      public override string Display()
      {
         return File.RepositoryPath;
      }

      public override int LineCount { get { return 5; } }

      public override void Process(IList<string> lines)
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
         Lines = new string[LineCount];
         Lines[0] = ResponseHelper.ResponseNames[(int) Type] + " " + lines[0];
         for (int i = 1; i < LineCount; i++)
            Lines[i] = lines[i];
      }

      public ReceiveFile File { get; set; }

      #endregion
   }
}