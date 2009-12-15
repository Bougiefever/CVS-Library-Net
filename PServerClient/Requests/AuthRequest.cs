namespace PServerClient.Requests
{
   public class AuthRequest : AuthRequestBase
   {
      public AuthRequest(CvsRoot root) : base(root)
      {
      }

      public override RequestType RequestType { get { return RequestType.Auth; } }
   }
}