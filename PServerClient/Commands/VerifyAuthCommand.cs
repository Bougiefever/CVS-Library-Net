using System;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public class VerifyAuthCommand : CommandBase
   {
      public VerifyAuthCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
         RequiredRequests.Clear();
         RequiredRequests.Add(new VerifyAuthRequest(root));
      }

      public override CommandType Type { get { return CommandType.VerifyAuth; } }
   }
}