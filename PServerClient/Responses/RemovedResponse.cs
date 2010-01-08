namespace PServerClient.Responses
{
   /// <summary>
   /// Removed pathname \n
   /// The file has been removed from the repository (this is the case where cvs prints
   /// ‘file foobar.c is no longer pertinent’).
   /// </summary>
   public class RemovedResponse : ResponseBase
   {
      public string RepositoryPath { get; private set; }

      public override ResponseType Type
      {
         get
         {
            return ResponseType.Removed;
         }
      }

      public override void Process()
      {
         RepositoryPath = Lines[0];
         base.Process();
      }

      public override string Display()
      {
         return RepositoryPath;
      }
   }
}