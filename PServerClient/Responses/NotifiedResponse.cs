using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Notified pathname \n
   //Indicate to the client that the notification for pathname has been done. There
   //should be one such response for every Notify request; if there are several Notify
   //requests for a single file, the requests should be processed in order; the first
   //Notified response pertains to the first Notify request, etc.
   /// </summary>
   public class NotifiedResponse : ResponseBase
   {
      public string RepositoryPath { get; private set; }
      public override ResponseType Type { get { return ResponseType.Notified; } }

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