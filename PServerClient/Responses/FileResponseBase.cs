using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Base class for responses that contain a file
   /// </summary>
   public abstract class FileResponseBase : ResponseBase, IFileResponse
   {
      /// <summary>
      /// Gets the ResponseType.
      /// </summary>
      /// <value>The response type.</value>
      public abstract override ResponseType Type { get; }

      /// <summary>
      /// Gets the line count expected for the response
      /// so the processor knows how many lines to take and use
      /// for this response
      /// </summary>
      /// <value>The line count.</value>
      public override int LineCount
      {
         get
         {
            return 5;
         }
      }

      /// <summary>
      /// Gets or sets the module.
      /// </summary>
      /// <value>The module.</value>
      public string Module { get; set; }

      /// <summary>
      /// Gets or sets the repository path.
      /// </summary>
      /// <value>The repository path.</value>
      public string RepositoryPath { get; set; }

      /// <summary>
      /// Gets or sets the entry line.
      /// </summary>
      /// <value>The entry line.</value>
      public string EntryLine { get; set; }

      /// <summary>
      /// Gets the file name.
      /// </summary>
      /// <value>The file name.</value>
      public string Name { get; private set; }

      /// <summary>
      /// Gets the revision.
      /// </summary>
      /// <value>The revision.</value>
      public string Revision { get; private set; }

      /// <summary>
      /// Gets the properties.
      /// </summary>
      /// <value>The properties.</value>
      public string Properties { get; private set; }

      /// <summary>
      /// Gets or sets the length.
      /// </summary>
      /// <value>The length.</value>
      public long Length { get; set; }

      /// <summary>
      /// Gets or sets the contents.
      /// </summary>
      /// <value>The contents.</value>
      public byte[] Contents { get; set; }

      /// <summary>
      /// Gets or sets the type of the file.
      /// </summary>
      /// <value>The type of the file.</value>
      public FileType FileType { get; set; }

      /// <summary>
      /// Processes this instance.
      /// </summary>
      public override void Process()
      {
         string module = Lines[0];
         string repositoryPath = Lines[1];
         string entryLine = Lines[2];
         string fileProperties = Lines[3];
         string fileLength = Lines[4];

         Name = ResponseHelper.GetFileNameFromEntryLine(entryLine);
         Revision = ResponseHelper.GetRevisionFromEntryLine(entryLine);
         Properties = fileProperties;
         Length = Convert.ToInt64(fileLength);
         FileType = FileType.Text;
         Module = module;
         RepositoryPath = repositoryPath;
         EntryLine = entryLine;

         base.Process();
      }

      /// <summary>
      /// Displays this instance.
      /// </summary>
      /// <returns>string to display</returns>
      public override string Display()
      {
         return RepositoryPath;
      }
   }
}