using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Clear-sticky pathname \n
   ///Clear any sticky tag or date set by Set-sticky.
   /// </summary>
   public class ClearStickyResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.ClearSticky; } }
      public string ModuleName { get; set; }
      public string RepositoryPath { get; set; }

      public override int LineCount { get { return 2; } }

      public override string DisplayResponse()
      {
         return ModuleName + Environment.NewLine + RepositoryPath;
      }

      public override void ProcessResponse(IList<string> lines)
      {
         ModuleName = lines[0];
         RepositoryPath = lines[1];
         base.ProcessResponse(lines);
      }
   }
}