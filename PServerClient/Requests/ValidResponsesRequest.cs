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
      public ValidResponsesRequest(ResponseType[] validResponses)
      {
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1}", RequestName, ResponseHelper.GetValidResponsesString(validResponses));
      }

      public ValidResponsesRequest(IList<string> lines)
         : base(lines)
      {
      }

      public override bool ResponseExpected
      {
         get
         {
            return false;
         }
      }

      public override RequestType Type
      {
         get
         {
            return RequestType.ValidResponses;
         }
      }
   }
}