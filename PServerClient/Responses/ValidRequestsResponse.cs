using System.Collections.Generic;
using System.Text;

namespace PServerClient.Responses
{
   /// <summary>
   /// Valid-requests request-list \n
   //Indicate what requests the server will accept. request-list is a space sepa-
   //rated list of tokens. If the server supports sending patches, it will include
   //‘update-patches’ in this list. The ‘update-patches’ request does not actually
   //do anything.
   /// </summary>
   public class ValidRequestsResponse : ResponseBase
   {
      public override ResponseType Type { get { return ResponseType.ValidRequests; } }
      public IList<RequestType> ValidRequestTypes { get; internal set; }

      public override void Process(IList<string> lines)
      {
         ValidRequestTypes = RequestHelper.RequestsToRequestTypes(lines[0]);
         base.Process(lines);
      }

      public override string Display()
      {
         StringBuilder sb = new StringBuilder();
         foreach (RequestType t in ValidRequestTypes)
         {
            sb.AppendLine(RequestHelper.RequestNames[(int) t]);
         }
         return sb.ToString();
      }
   }
}