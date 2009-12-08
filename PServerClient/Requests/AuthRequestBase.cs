using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.Responses;

namespace PServerClient.Requests
{
   public abstract class AuthRequestBase : IAuthRequest
   {
      private CvsRoot _root;
      private string _authString;
      private string _lineEnd = "\n";

      protected AuthRequestBase(CvsRoot root, string authString)
      {
         _root = root;
         _authString = authString;
      }

      public bool ResponseExpected
      {
         get { return true; }
      }

      public virtual string GetRequestString()
      {
         return string.Format("BEGIN {0} REQUEST{1}{2}{1}{3}{1}{4}{1}END {0} REQUEST{1}",
                                              _authString, _lineEnd, _root.Root, _root.Username, _root.Password);
      }

      public IList<IResponse> Responses { get; set;}

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
