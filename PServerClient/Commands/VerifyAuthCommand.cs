using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public class VerifyAuthCommand : CommandBase
   {
      public VerifyAuthCommand(IRoot root) : base(root)
      {
         RequiredRequests.Clear();
         RequiredRequests.Add(new VerifyAuthRequest(root));
      }

      public override CommandType Type { get { return CommandType.VerifyAuth; } }
   }
}