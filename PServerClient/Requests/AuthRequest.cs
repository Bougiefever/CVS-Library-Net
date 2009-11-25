using System.Text;
using PServerClient.Responses;

namespace PServerClient.Requests
{
   public class AuthRequest : RequestBase, IAuthRequest
   {
      private CvsRoot _root;

      public AuthRequest(CvsRoot root)
      {
         _root = root;
         Response = new AuthResponse();
      }

      public override bool ResponseExpected
      {
         get { return true; }
      }

      public override string GetRequestString()
      {
         StringBuilder sb = new StringBuilder();
         sb.Append("BEGIN AUTH REQUEST\n");
         sb.Append(_root.CvsRootPath);
         sb.Append("\n");
         sb.Append(_root.Username);
         sb.Append("\n");
         sb.Append(_root.Password);
         sb.Append("\n");
         sb.Append("END AUTH REQUEST\n");
         return sb.ToString();
      }
   }
}
