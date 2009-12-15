namespace PServerClient.Requests
{
   public class VerifyAuthRequest : AuthRequestBase
   {
      public VerifyAuthRequest(CvsRoot root) : base(root)
      {
      }

      public override RequestType RequestType { get { return RequestType.VerifyAuth; } }
   }
}