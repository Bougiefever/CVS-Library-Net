using PServerClient.CVS;

namespace PServerClient.Requests
{
   public class AuthRequest : AuthRequestBase
   {
      public AuthRequest(Root root) : base(root, RequestType.Auth)
      {
      }

      public override RequestType RequestType { get { return RequestType.Auth; } }
   }
}