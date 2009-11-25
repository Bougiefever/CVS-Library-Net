using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public class VerifyAuthCommand : CommandBase
   {
      public VerifyAuthCommand(CvsRoot cvsRoot) : base(cvsRoot)
      {
         Requests.Add(new VerifyAuthRequest(cvsRoot));
      }
   }
}
