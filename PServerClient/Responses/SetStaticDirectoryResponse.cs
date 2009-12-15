using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   public class SetStaticDirectoryResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.SetStaticDirectory; } }

      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }

      public override string DisplayResponse()
      {
         throw new NotImplementedException();
      }
   }
}