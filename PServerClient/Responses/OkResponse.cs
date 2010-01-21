namespace PServerClient.Responses
{
   /// <summary>
   /// ok \n
   /// The command completed successfully.
   /// </summary>
   public class OkResponse : ResponseBase
   {
      /// <summary>
      /// Gets the ResponseType.
      /// </summary>
      /// <value>The response type.</value>
      public override ResponseType Type
      {
         get
         {
            return ResponseType.Ok;
         }
      }

      /// <summary>
      /// Displays this instance.
      /// </summary>
      /// <returns>string to display</returns>
      public override string Display()
      {
         return "ok";
      }
   }
}