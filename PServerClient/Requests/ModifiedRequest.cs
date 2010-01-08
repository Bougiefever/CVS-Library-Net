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
      public ModifiedRequest(string fileName, string mode, long fileLength)
      {
         FileLength = fileLength;
         Lines = new string[3];
         Lines[0] = string.Format("{0} {1}", RequestName, fileName);
         Lines[1] = mode;
         Lines[2] = fileLength.ToString();
      }

      public ModifiedRequest(string[] lines)
         : base(lines)
      {
      }

      public override bool ResponseExpected
      {
         get
         {
            return false;
         }
      }

      public override RequestType Type
      {
         get
         {
            return RequestType.Modified;
         }
      }

      public long FileLength { get; set; }

      public byte[] FileContents { get; set; }
   }
}