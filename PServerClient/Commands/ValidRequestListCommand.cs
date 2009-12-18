using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Commands
{
   public class ValidRequestsListCommand : CommandBase
   {
      public ValidRequestsListCommand(Root root) : base(root)
      {
         RequiredRequests.Clear();
         RequiredRequests.Add(new AuthRequest(root));
         RequiredRequests.Add(new ValidRequestsRequest());
      }
   }
}
