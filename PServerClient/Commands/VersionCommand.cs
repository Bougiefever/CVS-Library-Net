using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public class VersionCommand : CommandBase
   {
      public VersionCommand(Root root) : base(root)
      {
         RequiredRequests.Clear();
         RequiredRequests.Add(new AuthRequest(root));
         Requests.Add(new VersionRequest());
      }
   }
}
