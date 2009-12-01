
namespace PServerClient.LocalFileSystem
{
   public interface IEntry
   {
      string FileName { get; set; }
      string Properties { get; set; }
      int FileLength { get; set; }
      byte[] FileContents { get; set; }
   }

   public class Entry : IEntry
   {
      public string FileName { get; set; }
      public string Properties { get; set; }
      public int FileLength { get; set; }
      public byte[] FileContents { get; set; }
   }
}
