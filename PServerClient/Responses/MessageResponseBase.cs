namespace PServerClient.Responses
{
   /// <summary>
   /// Base classes for message-type responses
   /// </summary>
   public abstract class MessageResponseBase : ResponseBase, IMessageResponse
   {
      /// <summary>
      /// Gets the message.
      /// </summary>
      /// <value>The message.</value>
      public string Message { get; internal set; }

      /// <summary>
      /// Processes this instance.
      /// </summary>
      public override void Process()
      {
         Message = Lines[0];
         base.Process();
      }
   }
}