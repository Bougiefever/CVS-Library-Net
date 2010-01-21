namespace PServerClient.Responses
{
   /// <summary>
   /// Response from CVS that does not match any known responses
   /// </summary>
   public class UnknownResponse : ResponseBase
   {
      /// <summary>
      /// Gets the line count expected for the response
      /// so the processor knows how many lines to take and use
      /// for this response
      /// </summary>
      /// <value>The line count.</value>
      public override int LineCount
      {
         get
         {
            return 1;
         }
      }

      /// <summary>
      /// Gets the ResponseType.
      /// </summary>
      /// <value>The response type.</value>
      public override ResponseType Type
      {
         get
         {
            return ResponseType.Unknown;
         }
      }
   }
}