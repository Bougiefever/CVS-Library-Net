using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using PServerClient.Connection;
using PServerClient.Responses;

namespace PServerClient.Requests
{
   public class VerifyAuthRequest : AuthRequestBase
   {
      public VerifyAuthRequest(CvsRoot root)
         : base(root, "VERIFICATION")
      {
      }
   }
}
