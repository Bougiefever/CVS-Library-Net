using PServerClient.Responses;

namespace PServerClient.Commands
{
   /// <summary>
   /// A group of responses that make up a file sent from the CVS server
   /// </summary>
   public interface IFileResponseGroup
   {
      /// <summary>
      /// Gets or sets the mod time response 
      /// </summary>
      /// <value>The mod time.</value>
      ModTimeResponse ModTime { get; set; }

      /// <summary>
      /// Gets or sets the MT message response. This contains a line like
      /// fname mymod/TestApp/file1.cs 
      /// that gives information about the module and file that will be sent
      /// </summary>
      /// <value>The MT Message response.</value>
      IMessageResponse MT { get; set; }

      /// <summary>
      /// Gets or sets the response instance that implements the IFileResponse interface
      /// This response will contain the file contents as a byte array
      /// that can be saved to a file.
      /// </summary>
      /// <value>The IFileResponse response instance.</value>
      IFileResponse FileResponse { get; set; }
   }
}