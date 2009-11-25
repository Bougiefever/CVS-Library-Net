using System;

namespace PServerClient.Responses
{
   public class AuthResponse : IAuthResponse
   {
      private const string AuthenticatePass = "I LOVE YOU";
      private const string AuthenticateFail = "I HATE YOU";

      public string ResponseString { get; set; }
      public bool Success { get; set; }

      public string ErrorMessage { get; set; }
      public AuthStatus Status { get; private set; }
      public void ProcessResponse()
      {
         if (ResponseString.Contains("I LOVE YOU"))
         {
            Status = AuthStatus.Authenticated;
            Success = true;
         }
         if (ResponseString.Contains("I HATE YOU"))
         {
            Status = AuthStatus.NotAuthenticated;
            ErrorMessage = ResponseString;
            Success = false;
         }
         if (!ResponseString.Contains("I LOVE YOU") && !ResponseString.Contains("I HATE YOU"))
         {
            Status = AuthStatus.Error;
            ErrorMessage = ResponseString;
            Success = false;
         }
      }
   }
}
