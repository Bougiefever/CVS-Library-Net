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
      /// <summary>
      /// Initializes a new instance of the <see cref="ValidRequestsRequest"/> class.
      /// </summary>
      public ValidRequestsRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ValidRequestsRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public ValidRequestsRequest(IList<string> lines)
         : base(lines)
      {
      }

      /// <summary>
      /// Gets a value indicating whether a response is expected from CVS after sending the request.
      /// </summary>
      /// <value><c>true</c> if [response expected]; otherwise, <c>false</c>.</value>
      public override bool ResponseExpected
      {
         get
         {
            return true;
         }
      }

      /// <summary>
      /// Gets the RequestType of the request
      /// </summary>
      /// <value>The RequestType value</value>
      public override RequestType Type
      {
         get
         {
            return RequestType.ValidRequests;
         }
      }
   }
}