using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
   }
}
