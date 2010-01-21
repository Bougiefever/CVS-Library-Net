namespace PServerClient.Responses
{
   /// <summary>
   /// Mode mode \n
   /// This mode applies to the next file mentioned in Checked-in. Mode is a file
   /// update modifying response as described in Section 5.9 [Response intro], 
   /// </summary>
   public class ModeResponse : ResponseBase
   {
      /// <summary>
      /// Gets the mode value.
      /// </summary>
      /// <value>The mode value.</value>
      public string Mode { get; private set; }

      /// <summary>
      /// Gets the ResponseType.
      /// </summary>
      /// <value>The response type.</value>
      public override ResponseType Type
      {
         get
         {
            return ResponseType.Mode;
         }
      }

      /// <summary>
      /// Processes this instance.
      /// </summary>
      public override void Process()
      {
         Mode = Lines[0];
         base.Process();
      }

      /// <summary>
      /// Displays this instance.
      /// </summary>
      /// <returns>string to display</returns>
      public override string Display()
      {
         return Mode;
      }
   }
}