using System.Collections.Generic;

namespace PServerClient.Responses
{
   public class AuthResponse : ResponseBase, IAuthResponse
   {
      private const string AuthenticateFail = "I HATE YOU";
      private const string AuthenticatePass = "I LOVE YOU";

      #region IAuthResponse Members

      public AuthStatus Status { get; private set; }
      public override int LineCount { get { return 1; } }
      public override ResponseType Type { get { return ResponseType.Auth; } }

      public override void ProcessResponse(IList<string> lines)
      {
         if (lines[0].Contains(AuthenticatePass))
         {
            Status = AuthStatus.Authenticated;
         }
         if (lines[0].Contains(AuthenticateFail))
         {
            Status = AuthStatus.NotAuthenticated;
         }
         ResponseLines = new string[1];
         ResponseLines[0] = lines[0];
      }

      public override string DisplayResponse()
      {
         return ResponseLines[0];
      }

      #endregion
   }
}