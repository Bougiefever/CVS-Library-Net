using System;
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
      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }

      public override string DisplayResponse()
      {
         throw new NotImplementedException();
      }

      public override ResponseType ResponseType { get { return ResponseType.Notified; } }
   }
}