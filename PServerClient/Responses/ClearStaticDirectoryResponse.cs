namespace PServerClient.Responses
{
   /// <summary>
   /// Clear-static-directory pathname \n
   /// Like Set-static-directory, but clear, not set, the flag.
   /// </summary>
   public class ClearStaticDirectoryResponse : ResponseBase
   {
      /// <summary>
      /// Gets the ResponseType.
      /// </summary>
      /// <value>The response type.</value>
      public override ResponseType Type
      {
         get
         {
            return ResponseType.ClearStaticDirectory;
         }
      }

      /// <summary>
      /// Gets or sets the name of the module.
      /// </summary>
      /// <value>The name of the module.</value>
      public string ModuleName { get; set; }

      /// <summary>
      /// Gets or sets the repository path.
      /// </summary>
      /// <value>The repository path.</value>
      public string RepositoryPath { get; set; }

      /// <summary>
      /// Gets the line count expected for the response
      /// so the processor knows how many lines to take and use
      /// for this response
      /// </summary>
      /// <value>The line count.</value>
      public override int LineCount
      {
         get
         {
            return 2;
         }
      }

      /// <summary>
      /// Displays this instance.
      /// </summary>
      /// <returns>string to display</returns>
      public override string Display()
      {
         return RepositoryPath;
      }

      /// <summary>
      /// Processes this instance.
      /// </summary>
      public override void Process()
      {
         ModuleName = Lines[0];
         RepositoryPath = Lines[1];
         base.Process();
      }
   }
}