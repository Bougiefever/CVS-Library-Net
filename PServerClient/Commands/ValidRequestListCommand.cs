using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.Connection;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Commands
{
   public class ValidRequestsListCommand : CommandBase
   {
      public ValidRequestsListCommand(CvsRoot root) : base(root)
      {
         RequiredRequests.Clear();
         RequiredRequests.Add(new AuthRequest(root));
         RequiredRequests.Add(new ValidRequestsRequest());
      }

      //public IList<RequestType> RequestList
      //{
      //   get
      //   {
      //      IRequest request = Requests.OfType<ValidRequestsRequest>().First();
      //      ValidRequestResponse response = request.Responses.OfType<ValidRequestResponse>().First();
      //      return response.ValidRequestTypes;
      //   }
      //}
   }
}
