namespace PServerClient.Requests
{
   public class SendFile
   {
      public string FileName { get; set; }
      public byte[] FileContents { get; set; }
      public FileType FileType { get; set; }
   }
}