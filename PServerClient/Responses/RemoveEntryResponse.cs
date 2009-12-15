using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Remove-entry pathname \n
   //The file needs its entry removed from CVS/Entries, but the file itself is already
   //gone (this happens in response to a ci request which involves committing the
   //removal of a file).
   /// </summary>
   public class RemoveEntryResponse : ResponseBase
   {
      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }

      public override string DisplayResponse()
      {
         throw new NotImplementedException();
      }

      public override ResponseType ResponseType { get { return ResponseType.RemoveEntry; } }
   }
}