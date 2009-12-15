using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// ok \n
   ///The command completed successfully.
   /// </summary>
   public class OkResponse : ResponseBase
   {
      public override void ProcessResponse(IList<string> lines)
      {
         
      }

      public override string DisplayResponse()
      {
         return "ok";
      }

      public override ResponseType ResponseType { get { return ResponseType.Ok; } }
   }
}