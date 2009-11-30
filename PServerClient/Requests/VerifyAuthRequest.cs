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
         Responses.Add(new AuthResponse());
      }

      public override bool ResponseExpected
      {
         get { return true; }
      }

      public void SetRequestString(string response)
      {
         
      }

      public override string GetRequestString()
      {
         StringBuilder sb = new StringBuilder();
         sb.Append("BEGIN VERIFICATION REQUEST\n");
         sb.Append(_root.CvsRootPath);
         sb.Append("\n");
         sb.Append(_root.Username);
         sb.Append("\n");
         sb.Append(_root.Password);
         sb.Append("\n");
         sb.Append("END VERIFICATION REQUEST\n");
         return sb.ToString();
      }
   }
}
