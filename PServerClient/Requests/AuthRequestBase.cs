using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.CVS;
using PServerClient.Responses;

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
         //Lines[1] = _root.RepositoryPath;
         Lines[2] = _root.Username;
         Lines[3] = _root.Password;
         Lines[4] = string.Format("END {0} REQUEST", requestName);
         Responses = new List<IResponse>();
      }

      protected AuthRequestBase(string[] lines)
      {
         Lines = lines;
      }

      public override bool ResponseExpected { get { return true; } }

      public override string GetRequestString()
      {
         StringBuilder sb = new StringBuilder();
         for (int i = 0; i < Lines.Length; i++)
         {
            sb.Append(Lines[i]).Append(LineEnd);
         }
         string request = sb.ToString();
         return request;
      }

      public AuthStatus Status
      {
         get
         {
            try
            {
               IAuthResponse authResponse = Responses.OfType<IAuthResponse>().First();
               return authResponse.Status;
            }
            catch (InvalidOperationException)
            {
               return AuthStatus.Error;
            }
         }
      }
   }
}