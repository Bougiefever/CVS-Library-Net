namespace PServerClient.Requests
{
   /// <summary>
   /// Gssapi-encrypt \n
   //Response expected: no. Use GSSAPI encryption to encrypt all further commu-
   //nication between the client and the server. This will only work if the connection
   //was made over GSSAPI in the first place. See Kerberos-encrypt, above, for
   //the relation between Gssapi-encrypt and Gzip-stream.
   //Note that this request does not fully prevent an attacker from hijacking the con-
   //nection, in the sense that it does not prevent hijacking the connection between
   //the initial authentication and the Gssapi-encrypt request.
   /// </summary>
   public class GssapiEncryptRequest : NoArgRequestBase
   {
      public override RequestType RequestType { get { return RequestType.GssapiEncrypt; } }
   }
}