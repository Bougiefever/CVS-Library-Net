using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   public class SetStaticDirectoryResponse : ResponseBase
   {
      public string ModuleName { get; set; }
      public string RepositoryPath { get; set; }

      public override int LineCount { get { return 2; } }
      public override ResponseType Type { get { return ResponseType.SetStaticDirectory; } }

      public override void ProcessResponse(IList<string> lines)
      {
         ModuleName = lines[0];
         RepositoryPath = lines[1];
         base.ProcessResponse(lines);
      }

      public override string DisplayResponse()
      {
         return ModuleName + Environment.NewLine + RepositoryPath;
      }
   }
}