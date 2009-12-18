using System;
using System.Collections.Generic;
using System.Linq;
using PServerClient.CVS;
using PServerClient.Responses;

namespace PServerClient.Requests
{
   public abstract class AuthRequestBase : IAuthRequest
   {
      private const string lineEnd = "\n";
      private readonly Root _root;

      protected AuthRequestBase(Root root)
      {
         _root = root;
      }

      #region IAuthRequest Members

      public bool ResponseExpected { get { return true; } }

      public virtual string GetRequestString()
      {
         return string.Format("BEGIN {0} REQUEST{1}{2}{1}{3}{1}{4}{1}END {0} REQUEST{1}",
                              RequestHelper.RequestNames[(int) RequestType], lineEnd, _root.CVSRoot, _root.Username, _root.Password);
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

      #endregion
   }
}