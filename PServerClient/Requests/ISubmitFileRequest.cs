namespace PServerClient.Requests
{
   /// <summary>
   /// Interface for requests that contain a file to send to CVS
   /// </summary>
   public interface ISubmitFileRequest : IRequest
   {
      /// <summary>
      /// Gets the length of the file.
      /// </summary>
      /// <value>The length of the file.</value>
      long FileLength { get; }

      /// <summary>
      /// Gets or sets the file contents.
      /// </summary>
      /// <value>The file contents.</value>
      byte[] FileContents { get; set; }
   }
}