namespace PServerClient.Responses
{
   public class EMessageResponse : MessageResponseBase
   {
      public override ResponseType Type
      {
         get
         {
            return ResponseType.EMessage;
         }
      }
   }
}