namespace PServerClient.Requests
{
   /// <summary>
   /// UseUnchanged \n
   //Response expected: no. To specify the version of the protocol described in this
   //document, servers must support this request (although it need not do anything)
   //and clients must issue it. The Root request need not have been previously sent.
   /// </summary>
   public class UseUnchangedRequest : NoArgRequestBase
   {
      public override RequestType RequestType { get { return RequestType.UseUnchanged; } }
   }
}