using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   public class SetStaticDirectoryResponse : ResponseBase
   {
      private string _path;
      public override int LineCount { get { return 2; } }
      public override ResponseType ResponseType { get { return ResponseType.SetStaticDirectory; } }

      public override void ProcessResponse(IList<string> lines)
      {
         _path = lines[1];
      }

      public override string DisplayResponse()
      {
         return _path;
      }
   }
}