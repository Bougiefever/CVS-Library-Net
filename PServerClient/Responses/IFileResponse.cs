namespace PServerClient.Responses
{
   /// <summary>
   /// Interface for response types that contain files
   /// </summary>
   public interface IFileResponse : IResponse
   {
      /// <summary>
      /// Gets or sets the module.
      /// </summary>
      /// <value>The module.</value>
      string Module { get; set; }

      /// <summary>
      /// Gets or sets the repository path.
      /// </summary>
      /// <value>The repository path.</value>
      string RepositoryPath { get; set; }

      /// <summary>
      /// Gets or sets the entry line.
      /// </summary>
      /// <value>The entry line.</value>
      string EntryLine { get; set; }

      /// <summary>
      /// Gets the file name.
      /// </summary>
      /// <value>The file name.</value>
      string Name { get; }

      /// <summary>
      /// Gets the revision.
      /// </summary>
      /// <value>The revision.</value>
      string Revision { get; }

      /// <summary>
      /// Gets the properties.
      /// </summary>
      /// <value>The properties.</value>
      string Properties { get; }

      /// <summary>
      /// Gets or sets the length of the content byte array.
      /// CVS sends this prior to sending the file contents.
      /// </summary>
      /// <value>The length of the content byte array.</value>
      long Length { get; set; }

      /// <summary>
      /// Gets or sets the contents.
      /// </summary>
      /// <value>The contents.</value>
      byte[] Contents { get; set; }

      /// <summary>
      /// Gets or sets the type of the file. Text or Binary
      /// </summary>
      /// <value>The type of the file.</value>
      FileType FileType { get; set; }
   }
}