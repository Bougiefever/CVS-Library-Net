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
      public override ResponseType ResponseType { get { return ResponseType.ClearStaticDirectory; } }
      public string ModuleName { get; set; }
      public string CvsDirectory { get; set; }
      public override string DisplayResponse()
      {
         return ModuleName + Environment.NewLine + CvsDirectory;
      }

      public override int LineCount { get { return 2; } }

      public override void ProcessResponse(IList<string> lines)
      {
         ModuleName = lines[0];
         CvsDirectory = lines[1];
      }
   }
}