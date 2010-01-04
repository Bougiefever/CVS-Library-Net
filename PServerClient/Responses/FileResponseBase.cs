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
         string repositoryPath = lines[1];
         string entryLine = lines[2];
         string fileProperties = lines[3];
         string fileLength = lines[4];

         string fileName = ResponseHelper.GetFileNameFromEntryLine(entryLine);
         string revision = ResponseHelper.GetRevisionFromEntryLine(entryLine);

         File = new ReceiveFile
                   {
                      Name = fileName,
                      Revision = revision,
                      RepositoryPath = repositoryPath,
                      //Path = module.Split(new[] {"/"}, StringSplitOptions.RemoveEmptyEntries),
                      EntryLine = entryLine,
                      Module = module,
                      Properties = fileProperties,
                      Length = Convert.ToInt64(fileLength)
                   };
         Lines = new List<string>(LineCount);
         //string line =string.Format("{0} {1}", ResponseHelper.ResponseNames[(int) Type],lines[0]);
         //Lines.Add(line);
         for (int i = 0; i < LineCount; i++)
            Lines.Add(lines[i]);

         ModuleName = module;
         RepositoryPath = repositoryPath;
         EntryLine = entryLine;
      }

      public ReceiveFile File { get; set; }

      #endregion

      #region IFileResponse Members

      public string ModuleName { get; set; }

      public string RepositoryPath { get; set; }

      public string EntryLine { get; set; }

      #endregion
   }
}