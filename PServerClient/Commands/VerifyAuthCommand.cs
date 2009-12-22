using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public class VerifyAuthCommand : CommandBase
   {
      public VerifyAuthCommand(Root root) : base(root)
      {
         RequiredRequests.Clear();
         RequiredRequests.Add(new VerifyAuthRequest(root));
      }

      public override CommandType CommandType { get { return CommandType.VerifyAuth; } }
   }
}