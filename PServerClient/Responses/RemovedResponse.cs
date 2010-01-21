namespace PServerClient.Responses
{
   /// <summary>
   /// Removed pathname \n
   /// The file has been removed from the repository (this is the case where cvs prints
   /// ‘file foobar.c is no longer pertinent’).
   /// </summary>
   public class RemovedResponse : ResponseBase
   {
      /// <summary>
      /// Gets the repository path.
      /// </summary>
      /// <value>The repository path.</value>
      public string RepositoryPath { get; private set; }

      /// <summary>
      /// Gets the ResponseType.
      /// </summary>
      /// <value>The response type.</value>
      public override ResponseType Type
      {
         get
         {
            return ResponseType.Removed;
         }
      }

      /// <summary>
      /// Processes this instance.
      /// </summary>
      public override void Process()
      {
         RepositoryPath = Lines[0];
         base.Process();
      }

      /// <summary>
      /// Displays this instance.
      /// </summary>
      /// <returns>string to display</returns>
      public override string Display()
      {
         return RepositoryPath;
      }
   }
}