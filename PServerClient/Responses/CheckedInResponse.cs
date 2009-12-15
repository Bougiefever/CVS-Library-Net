using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Checked-in pathname \n
   //Additional data: New Entries line, \n. This means a file pathname has been
   //successfully operated on (checked in, added, etc.). name in the Entries line is
   //the same as the last component of pathname.
   /// </summary>
   public class CheckedInResponse : ResponseBase
   {
      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }

      public override string DisplayResponse()
      {
         throw new NotImplementedException();
      }

      public override ResponseType ResponseType { get { return ResponseType.CheckedIn; } }
      public override int LineCount { get { return 0; } }
   }
}