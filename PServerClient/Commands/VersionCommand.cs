using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public class VersionCommand : CommandBase
   {
      public VersionCommand(CvsRoot root) : base(root)
      {
         Requests.Add(new AuthRequest(root));
         Requests.Add(new VersionRequest());
      }
   }
}
