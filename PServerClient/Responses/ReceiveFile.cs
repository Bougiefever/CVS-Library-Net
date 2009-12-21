namespace PServerClient.Responses
{
   /// <summary>
   /// This contains all information about a file received from the cvs server
   /// </summary>
   public class ReceiveFile
   {
      public string[] Path { get; set; }
      public string RepositoryPath { get; set; }
      public string Name { get; set; }
      public string Revision { get; set; }
      public string Properties { get; set; }
      public long Length { get; set; }
      public byte[] Contents { get; set; }
      public FileType Type { get; set; }
   }
}