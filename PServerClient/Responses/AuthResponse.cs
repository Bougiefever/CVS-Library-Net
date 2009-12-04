using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   public class AuthResponse : IAuthResponse, IResponse
   {
      private const string AuthenticatePass = "I LOVE YOU";
      private const string AuthenticateFail = "I HATE YOU";

      public AuthStatus Status { get; private set; }
      public int LineCount { get { return 1; } }
      public ResponseType ResponseType { get { return ResponseType.Auth; } }
      public string ResponseText { get; set; }
      public void ProcessResponse(IList<string> lines)
      {
         if (lines[0].Contains("I LOVE YOU"))
         {
            Status = AuthStatus.Authenticated;
         }
         if (lines[0].Contains("I HATE YOU"))
         {
            Status = AuthStatus.NotAuthenticated;
         }
         ResponseText = lines[0] + (char)10;
      }

   }
}
