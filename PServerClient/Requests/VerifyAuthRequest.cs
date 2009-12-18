using PServerClient.CVS;

namespace PServerClient.Requests
{
   public class VerifyAuthRequest : AuthRequestBase
   {
      public VerifyAuthRequest(Root root) : base(root)
      {
      }

      public override RequestType RequestType { get { return RequestType.VerifyAuth; } }
   }
}