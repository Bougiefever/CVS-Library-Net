namespace PServerClient.Responses
{
   /// <summary>
   /// Interface for the Auth response, which is required to succeed for all commands to 
   /// continue processing
   /// </summary>
   public interface IAuthResponse : IResponse
   {
      /// <summary>
      /// Gets the CVS authentication status.
      /// </summary>
      /// <value>The authentication status.</value>
      AuthStatus Status { get; }
   }
}