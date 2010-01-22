namespace PServerClient.Responses
{
   /// <summary>
   /// Remove-entry pathname \n
   /// The file needs its entry removed from CVS/Entries, but the file itself is already
   /// gone (this happens in response to a ci request which involves committing the
   /// removal of a file).
   /// </summary>
   public class RemoveEntryResponse : ResponseBase
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
            return ResponseType.RemoveEntry;
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