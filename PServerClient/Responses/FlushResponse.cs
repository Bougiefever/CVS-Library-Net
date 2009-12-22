namespace PServerClient.Responses
{
   /// <summary>
   /// F \n
   //Flush stderr. That is, make it possible for the user to see what has been written
   //to stderr (it is up to the implementation to decide exactly how far it should go
   //to ensure this).
   /// </summary>
   public class FlushResponse : ResponseBase
   {
      public override ResponseType ResponseType { get { return ResponseType.Flush; } }
   }
}