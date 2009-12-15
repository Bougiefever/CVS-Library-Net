using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Set-sticky pathname \n
   //Additional data: tagspec \n. Tell the client to set a sticky tag or date, which
   //should be supplied with the Sticky request for future operations. pathname
   //ends in a slash; its purpose is to specify a directory, not a file within a directory.
   //The client should store tagspec and pass it back to the server as-is, to allow for
   //future expansion. The first character of tagspec is ‘T’ for a tag, ‘D’ for a date,
   //or something else for future expansion. The remainder of tagspec contains the
   //actual tag or date.
   /// </summary>
   public class SetStickyResponse : ResponseBase
   {
      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }

      public override string DisplayResponse()
      {
         throw new NotImplementedException();
      }

      public override ResponseType ResponseType { get { return ResponseType.SetSticky; } }
   }
}