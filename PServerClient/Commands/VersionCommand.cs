using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public class VersionCommand : CommandBase
   {
      public VersionCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
         RequiredRequests.Clear();
         RequiredRequests.Add(new AuthRequest(root));
         Requests.Add(new VersionRequest());
      }

      public override CommandType Type
      {
         get
         {
            return CommandType.Version;
         }
      }
   }
}