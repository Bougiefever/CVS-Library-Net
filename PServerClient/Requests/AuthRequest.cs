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
      }

      public override bool ResponseExpected
      {
         get { return true; }
      }

      public override string GetRequestString()
      {
         StringBuilder sb = new StringBuilder();
         sb.Append("BEGIN AUTH REQUEST");
         sb.Append(lineEnd);
         sb.Append(_root.CvsRootPath);
         sb.Append(lineEnd);
         sb.Append(_root.Username);
         sb.Append(lineEnd);
         sb.Append(_root.Password);
         sb.Append(lineEnd);
         sb.Append("END AUTH REQUEST");
         sb.Append(lineEnd);
         return sb.ToString();
      }
   }
}
