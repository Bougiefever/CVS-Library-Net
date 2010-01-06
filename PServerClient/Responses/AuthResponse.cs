using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   public class AuthResponse : ResponseBase, IAuthResponse
   {
      private const string AuthenticateFail = "I HATE YOU";
      private const string AuthenticatePass = "I LOVE YOU";

      public AuthStatus Status { get; private set; }
      public override int LineCount { get { return 1; } }
      public override void Process()
      {
         if (Lines[0].Contains(AuthenticatePass))
         {
            Status = AuthStatus.Authenticated;
         }
         if (Lines[0].Contains(AuthenticateFail))
         {
            Status = AuthStatus.NotAuthenticated;
         }
         base.Process();
      }

      public override ResponseType Type { get { return ResponseType.Auth; } }

      public override void Initialize(IList<string> lines)
      {
         Lines = new List<string>(1) {lines[0]};
      }

      public override string Display()
      {
         return Lines[0];
      }
   }
}