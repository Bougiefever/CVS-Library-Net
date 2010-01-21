namespace PServerClient.Responses
{
   /// <summary>
   /// Interface for responses that contain user messages
   /// </summary>
   public interface IMessageResponse : IResponse
   {
      /// <summary>
      /// Gets the message.
      /// </summary>
      /// <value>The message.</value>
      string Message { get; }
   }
}