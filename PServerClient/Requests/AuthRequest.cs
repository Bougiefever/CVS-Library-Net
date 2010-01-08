using PServerClient.CVS;

namespace PServerClient.Requests
{
   public class AuthRequest : AuthRequestBase
   {
      public AuthRequest(IRoot root)
         : base(root, RequestType.Auth)
      {
      }

      public AuthRequest(string[] lines)
         : base(lines)
      {
      }

      public override RequestType Type
      {
         get
         {
            return RequestType.Auth;
         }
      }
   }
}