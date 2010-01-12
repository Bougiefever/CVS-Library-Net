using System.Collections.Generic;
using PServerClient.CVS;

namespace PServerClient.Requests
{
   public class VerifyAuthRequest : AuthRequestBase
   {
      public VerifyAuthRequest(IRoot root)
         : base(root, RequestType.VerifyAuth)
      {
      }

      public VerifyAuthRequest(IList<string> lines)
         : base(lines)
      {
      }

      public override RequestType Type
      {
         get
         {
            return RequestType.VerifyAuth;
         }
      }
   }
}