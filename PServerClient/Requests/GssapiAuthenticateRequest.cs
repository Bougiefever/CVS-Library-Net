namespace PServerClient.Requests
{
   /// <summary>
   /// Gssapi-authenticate \n
   //Response expected: no. Use GSSAPI authentication to authenticate all fur-
   //ther communication between the client and the server. This will only work if
   //the connection was made over GSSAPI in the first place. Encrypted data is
   //automatically authenticated, so using both Gssapi-authenticate and Gssapi-
   //encrypt has no effect beyond that of Gssapi-encrypt. Unlike encrypted data,
   //it is reasonable to compress authenticated data.
   //Note that this request does not fully prevent an attacker from hijacking the con-
   //nection, in the sense that it does not prevent hijacking the connection between
   //the initial authentication and the Gssapi-authenticate request.
   /// </summary>
   public class GssapiAuthenticateRequest : NoArgRequestBase
   {
      public GssapiAuthenticateRequest(){}
      public GssapiAuthenticateRequest(string[] lines) : base(lines){}
      public override RequestType Type { get { return RequestType.GssapiAuthenticate; } }
   }
}