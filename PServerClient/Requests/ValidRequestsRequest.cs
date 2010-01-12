using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// valid-requests \n
   /// Response expected: yes. Ask the server to send back a Valid-requests re-
   /// sponse. The Root request need not have been previously sent.
   /// </summary>
   public class ValidRequestsRequest : NoArgRequestBase
   {
      public ValidRequestsRequest()
      {
      }

      public ValidRequestsRequest(IList<string> lines)
         : base(lines)
      {
      }

      public override bool ResponseExpected
      {
         get
         {
            return true;
         }
      }

      public override RequestType Type
      {
         get
         {
            return RequestType.ValidRequests;
         }
      }
   }
}