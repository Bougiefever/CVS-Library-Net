using System.Text;
using PServerClient.CVS;

namespace PServerClient.Requests
{
   public abstract class AuthRequestBase : RequestBase, IAuthRequest
   {
      private readonly IRoot _root;

      protected AuthRequestBase(IRoot root, RequestType type)
      {
         _root = root;
         Lines = new string[5];
         string requestName = RequestHelper.RequestNames[(int) type];
         Lines[0] = string.Format("BEGIN {0} REQUEST", requestName);
         Lines[1] = _root.Repository;
         Lines[2] = _root.Username;
         Lines[3] = _root.Password;
         Lines[4] = string.Format("END {0} REQUEST", requestName);
      }

      protected AuthRequestBase(string[] lines)
      {
         Lines = lines;
      }

      public override bool ResponseExpected
      {
         get
         {
            return true;
         }
      }

      public override string GetRequestString()
      {
         StringBuilder sb = new StringBuilder();
         for (int i = 0; i < Lines.Length; i++)
         {
            sb.Append(Lines[i]).Append(PServerHelper.UnixLineEnd);
         }

         string request = sb.ToString();
         return request;
      }
   }
}