using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Clear-static-directory pathname \n
   //Like Set-static-directory, but clear, not set, the flag.
   /// </summary>
   public class ClearStaticDirectoryResponse : ResponseBase
   {
      public override ResponseType Type { get { return ResponseType.ClearStaticDirectory; } }
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