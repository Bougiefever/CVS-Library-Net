namespace PServerClient.Responses
{
   /// <summary>
   /// This contains all information about a file received from the cvs server
   /// </summary>
   public class ReceiveFile
   {
      public string[] Path { get; set; }
      public string CvsPath { get; set; }
      public string FileName { get; set; }
      public string Revision { get; set; }
      public string Properties { get; set; }
      public long FileLength { get; set; }
      public byte[] FileContents { get; set; }
      public FileType FileType { get; set; }
   }
}