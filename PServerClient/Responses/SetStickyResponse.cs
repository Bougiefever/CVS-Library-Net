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
      public string ModuleName { get; set; }
      public string RepositoryPath { get; set; }

      public override int LineCount { get { return 2; } }
      public override ResponseType Type { get { return ResponseType.SetSticky; } }

      public override void Process(IList<string> lines)
      {
         ModuleName = lines[0];
         RepositoryPath = lines[1];
         base.Process(lines);
      }

      public override string Display()
      {
         return RepositoryPath;
      }
   }
}