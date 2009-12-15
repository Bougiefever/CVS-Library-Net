using System;
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
      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }

      public override string DisplayResponse()
      {
         throw new NotImplementedException();
      }

      public override ResponseType ResponseType { get { return ResponseType.Removed; } }
   }
}