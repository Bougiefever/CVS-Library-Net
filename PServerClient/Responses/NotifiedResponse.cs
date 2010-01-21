namespace PServerClient.Responses
{
   /// <summary>
   /// Notified pathname \n
   /// Indicate to the client that the notification for pathname has been done. There
   /// should be one such response for every Notify request; if there are several Notify
   /// requests for a single file, the requests should be processed in order; the first
   /// Notified response pertains to the first Notify request, etc.
   /// </summary>
   public class NotifiedResponse : ResponseBase
   {
      /// <summary>
      /// Gets the repository path.
      /// </summary>
      /// <value>The repository path.</value>
      public string RepositoryPath { get; private set; }

      public override ResponseType Type
      {
         get
         {
            return ResponseType.Notified;
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