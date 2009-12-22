using PServerClient.CVS;
using PServerClient.Requests;

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

      public override CommandType CommandType { get { return CommandType.ValidRequestsList; } }
   }
}