namespace PServerClient.Requests
{
   /// <summary>
   /// Modified filename \n
   //Response expected: no. Additional data: mode, \n, file transmission. Send
   //the server a copy of one locally modified file. filename is a file within the most
   //recent directory sent with Directory; it must not contain ‘/’. If the user is
   //operating on only some files in a directory, only those files need to be included.
   //This can also be sent without Entry, if there is no entry for the file.
   /// </summary>
   public class ModifiedRequest : RequestBase, IFileRequest
   {
      private readonly string _filename;
      private readonly string _mode;

      public ModifiedRequest(string fileName, string mode, long fileLength)
      {
         _filename = fileName;
         _mode = mode;
         FileLength = fileLength;
      }

      public override bool ResponseExpected { get { return false; } }
      public override RequestType RequestType { get { return RequestType.Modified; } }

      public override string GetRequestString()
      {
         string request = string.Format("{4} {0}{1}{2}{1}{3}{1}", _filename, LineEnd, _mode, FileLength, RequestName);
         return request;
      }

      public long FileLength { get; set; }
      public byte[] FileContents { get; set; }

   }
}