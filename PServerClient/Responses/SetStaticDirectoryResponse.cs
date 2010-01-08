namespace PServerClient.Responses
{
   public class SetStaticDirectoryResponse : ResponseBase
   {
      public string ModuleName { get; set; }

      public string RepositoryPath { get; set; }

      public override int LineCount
      {
         get
         {
            return 2;
         }
      }

      public override ResponseType Type
      {
         get
         {
            return ResponseType.SetStaticDirectory;
         }
      }

      public override void Process()
      {
         ModuleName = Lines[0];
         RepositoryPath = Lines[1];
         base.Process();
      }

      public override string Display()
      {
         return RepositoryPath;
      }
   }
}