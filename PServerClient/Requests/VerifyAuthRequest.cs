using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using PServerClient.Connection;
using PServerClient.Responses;

namespace PServerClient.Requests
{
   public class VerifyAuthRequest : RequestBase, IAuthRequest
   {
      private CvsRoot _root;


      public VerifyAuthRequest(CvsRoot root)
      {
         _root = root;
      }

      public override bool ResponseExpected { get { return true; } }

      public AuthStatus Status
      {
         get
         {
            IAuthResponse authResponse = Responses.OfType<IAuthResponse>().First();
            return authResponse.Status;
         }
      }

      public override string GetRequestString()
      {
         StringBuilder sb = new StringBuilder();
         sb.Append("BEGIN VERIFICATION REQUEST");
         sb.Append(lineEnd);
         sb.Append(_root.Root);
         sb.Append(lineEnd);
         sb.Append(_root.Username);
         sb.Append(lineEnd);
         sb.Append(_root.Password);
         sb.Append(lineEnd);
         sb.Append("END VERIFICATION REQUEST");
         sb.Append(lineEnd);
         return sb.ToString();
      }
   }
}
