using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Copy-file pathname \n
   //Additional data: newname \n. Copy file pathname to newname in the same
   //directory where it already is. This does not affect CVS/Entries.
   //This can optionally be implemented as a rename instead of a copy. The only
   //use for it which currently has been identified is prior to a Merged response as
   //described under Merged. Clients can probably assume that is how it is being
   //used, if they want to worry about things like how long to keep the newname
   //file around.
   /// </summary>
   public class CopyFileResponse : ResponseBase
   {
      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }

      public override string DisplayResponse()
      {
         throw new NotImplementedException();
      }

      public override ResponseType ResponseType { get { return ResponseType.CopyFile; } }
   }
}