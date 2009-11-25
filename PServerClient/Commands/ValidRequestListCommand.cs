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
         Requests.Add(new AuthRequest(root));
         Requests.Add(new ValidRequestsRequest());
      }
      public IList<string> RequestList
      {
         get
         {
            IRequest request = Requests.OfType<ValidRequestsRequest>().First();
            ValidRequestResponse response = (ValidRequestResponse) request.Response;
            return response.ValidRequests;
         }
      }
   }
}
