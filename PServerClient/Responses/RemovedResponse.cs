using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Removed pathname \n
   //The file has been removed from the repository (this is the case where cvs prints
   //‘file foobar.c is no longer pertinent’).
   /// </summary>
   public class RemovedResponse : ResponseBase
   {
      public string RepositoryPath { get; private set; }
      public override ResponseType ResponseType { get { return ResponseType.Removed; } }

      public override void ProcessResponse(IList<string> lines)
      {
         RepositoryPath = lines[0];
         base.ProcessResponse(lines);
      }

      public override string DisplayResponse()
      {
         return RepositoryPath;
      }
   }
}