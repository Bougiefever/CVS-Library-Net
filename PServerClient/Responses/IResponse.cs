using System.Collections.Generic;

namespace PServerClient.Responses
{
   public interface IResponse
   {
      //string ResponseString { get; set; }
      //bool Success { get; set; }
      //string ErrorMessage { get; set; }
      void ProcessResponse(IList<string> lines);
      int LineCount { get; }
      IList<string> ResponseLines { get; set; }
   }

   public interface IAuthResponse : IResponse
   {
      AuthStatus Status { get; }
   }
}
