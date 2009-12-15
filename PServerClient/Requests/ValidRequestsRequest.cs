namespace PServerClient.Requests
{
   /// <summary>
   /// valid-requests \n
   /// Response expected: yes. Ask the server to send back a Valid-requests re-
   ///sponse. The Root request need not have been previously sent.
   /// </summary>
   public class ValidRequestsRequest : NoArgRequestBase
   {
      public override bool ResponseExpected { get { return true; } }
      public override RequestType RequestType { get { return RequestType.ValidRequests; } }
   }
}