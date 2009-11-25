using PServerClient.Responses;


namespace PServerClient.Requests
{

   /// <summary>
   /// Request interface to process cvs requests
   /// </summary>
   public interface IRequest 
   {
      bool ResponseExpected { get; }
      string GetRequestString();
      void SetCvsResponse(string response);
      void GetResponse();
      IResponse Response { get; set; }
   }

   /// <summary>
   /// used to differentiate the auth request from all the other requests on a command
   /// </summary>
   public interface IAuthRequest : IRequest
   {
      
   }
}
