namespace PServerClient.Responses
{
   /// <summary>
   /// Provide a message for the user. After this reponse, the authentica-<br/>
   /// tion protocol continues with another response. Typically the server<br/>
   /// will provide a series of ‘E’ responses followed by ‘error’. Compat-<br/>
   /// ibility note: cvs 1.9.10 and older clients will print unrecognized<br/>
   /// auth response and text, and then exit, upon receiving this response
   /// </summary>
   public class EMessageResponse : MessageResponseBase
   {
      /// <summary>
      /// Gets the ResponseType.
      /// </summary>
      /// <value>The response type.</value>
      public override ResponseType Type
      {
         get
         {
            return ResponseType.EMessage;
         }
      }
   }
}