using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.CVS;
using PServerClient.Responses;

namespace PServerClient.Requests
{
   public abstract class AuthRequestBase : IAuthRequest
   {
      private const string lineEnd = "\n";
      private readonly Root _root;

      protected AuthRequestBase(Root root, RequestType type)
      {
         _root = root;
         RequestLines = new string[5];
         string requestName = RequestHelper.RequestNames[(int) type];
         RequestLines[0] = string.Format("BEGIN {0} REQUEST", requestName);
         RequestLines[1] = _root.RepositoryPath;
         RequestLines[2] = _root.Username;
         RequestLines[3] = _root.Password;
         RequestLines[4] = string.Format("END {0} REQUEST", requestName);
      }

      public bool ResponseExpected { get { return true; } }
      public string[] RequestLines { get; internal set; }
      public virtual string GetRequestString()
      {
         StringBuilder sb = new StringBuilder();
         for (int i = 0; i < RequestLines.Length; i++)
         {
            sb.Append(RequestLines[i]).Append("\n");
         }
         string request = sb.ToString();
         return request;
      }

      public IList<IResponse> Responses { get; set; }
      public abstract RequestType RequestType { get; }

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