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

      public string Version
      {
         get; private set;
      }

      protected internal override void AfterExecute()
      {
         base.AfterExecute();
         Version = ExitCode == ExitCode.Succeeded ? UserMessages[0] : "Error in command";
      }
   }
}