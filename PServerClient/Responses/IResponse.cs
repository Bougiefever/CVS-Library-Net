namespace PServerClient.Responses
{
   public interface IResponse
   {
      string ResponseString { get; set; }
      bool Success { get; set; }
      string ErrorMessage { get; set; }
      void ProcessResponse();
   }

   public interface IAuthResponse : IResponse
   {
      AuthStatus Status { get; }
   }
}
