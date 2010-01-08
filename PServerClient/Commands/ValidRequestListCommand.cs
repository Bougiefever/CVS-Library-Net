using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public class ValidRequestsListCommand : CommandBase
   {
      public ValidRequestsListCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
         RequiredRequests.Clear();
         RequiredRequests.Add(new AuthRequest(root));
         RequiredRequests.Add(new ValidRequestsRequest());
      }

      public override CommandType Type
      {
         get
         {
            return CommandType.ValidRequestsList;
         }
      }
   }
}