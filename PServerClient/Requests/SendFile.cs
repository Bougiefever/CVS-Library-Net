namespace PServerClient.Requests
{
   /// <summary>
   /// A file to send to CVS
   /// </summary>
   public class SendFile
   {
      /// <summary>
      /// Gets or sets the name of the file.
      /// </summary>
      /// <value>The name of the file.</value>
      public string FileName { get; set; }

      /// <summary>
      /// Gets or sets the file contents.
      /// </summary>
      /// <value>The file contents.</value>
      public byte[] FileContents { get; set; }

      /// <summary>
      /// Gets or sets the type of the file.
      /// </summary>
      /// <value>The type of the file.</value>
      public FileType FileType { get; set; }
   }
}