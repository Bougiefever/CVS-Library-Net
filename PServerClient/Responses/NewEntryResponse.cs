using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// New-entry pathname \n
   //Additional data: New Entries line, \n. Like Checked-in, but the file is not up
   //to date.
   /// </summary>
   public class NewEntryResponse : ResponseBase
   {
      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }

      public override string DisplayResponse()
      {
         throw new NotImplementedException();
      }

      public override ResponseType ResponseType { get { return ResponseType.NewEntry; } }
   }
}