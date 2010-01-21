using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Valid-responses request-list \n
   /// Response expected: no. Tell the server what responses the client will accept.
   /// request-list is a space separated list of tokens. The Root request need not have
   /// been previously sent.
   /// </summary>
   public class ValidResponsesRequest : RequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="ValidResponsesRequest"/> class.
      /// </summary>
      /// <param name="validResponses">The valid responses.</param>
      public ValidResponsesRequest(ResponseType[] validResponses)
      {
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1}", RequestName, ResponseHelper.GetValidResponsesString(validResponses));
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ValidResponsesRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public ValidResponsesRequest(IList<string> lines)
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
            return false;
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
            return RequestType.ValidResponses;
         }
      }
   }
}