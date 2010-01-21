using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Modified filename \n
   /// Response expected: no. Additional data: mode, \n, file transmission. Send
   /// the server a copy of one locally modified file. filename is a file within the most
   /// recent directory sent with Directory; it must not contain ‘/’. If the user is
   /// operating on only some files in a directory, only those files need to be included.
   /// This can also be sent without Entry, if there is no entry for the file.
   /// </summary>
   public class ModifiedRequest : RequestBase, ISubmitFileRequest
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="ModifiedRequest"/> class.
      /// </summary>
      /// <param name="fileName">Name of the file.</param>
      /// <param name="mode">The mode value.</param>
      /// <param name="fileLength">Length of the file.</param>
      public ModifiedRequest(string fileName, string mode, long fileLength)
      {
         FileLength = fileLength;
         Lines = new string[3];
         Lines[0] = string.Format("{0} {1}", RequestName, fileName);
         Lines[1] = mode;
         Lines[2] = fileLength.ToString();
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ModifiedRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public ModifiedRequest(IList<string> lines)
         : base(lines)
      {
      }

      /// <summary>
      /// Gets a value indicating whether a response is expected from CVS after sending the request.
      /// </summary>
      /// <value><c>true</c> if [response expected]; otherwise, <c>false</c>.</value>
      public override bool ResponseExpected
      {
         get
         {
            return false;
         }
      }

      /// <summary>
      /// Gets the RequestType of the request
      /// </summary>
      /// <value>The RequestType value</value>
      public override RequestType Type
      {
         get
         {
            return RequestType.Modified;
         }
      }

      /// <summary>
      /// Gets or sets the length of the file.
      /// </summary>
      /// <value>The length of the file.</value>
      public long FileLength { get; set; }

      /// <summary>
      /// Gets or sets the file contents.
      /// </summary>
      /// <value>The file contents.</value>
      public byte[] FileContents { get; set; }
   }
}