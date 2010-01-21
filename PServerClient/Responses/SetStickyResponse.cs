namespace PServerClient.Responses
{
   /// <summary>
   /// Set-sticky pathname \n
   /// Additional data: tagspec \n. Tell the client to set a sticky tag or date, which
   /// should be supplied with the Sticky request for future operations. pathname
   /// ends in a slash; its purpose is to specify a directory, not a file within a directory.
   /// The client should store tagspec and pass it back to the server as-is, to allow for
   /// future expansion. The first character of tagspec is ‘T’ for a tag, ‘D’ for a date,
   /// or something else for future expansion. The remainder of tagspec contains the
   /// actual tag or date.
   /// </summary>
   public class SetStickyResponse : ResponseBase
   {
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
      /// Gets the ResponseType.
      /// </summary>
      /// <value>The response type.</value>
      public override ResponseType Type
      {
         get
         {
            return ResponseType.SetSticky;
         }
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