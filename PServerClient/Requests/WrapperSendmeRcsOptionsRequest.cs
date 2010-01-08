namespace PServerClient.Requests
{
   /// <summary>
   /// wrapper-sendme-rcsOptions \n
   /// Response expected: yes. Request that the server transmit mappings from file-
   /// names to keyword expansion modes in Wrapper-rcsOption responses.
   /// </summary>
   public class WrapperSendmeRcsOptionsRequest : NoArgRequestBase
   {
      public WrapperSendmeRcsOptionsRequest()
      {
      }

      public WrapperSendmeRcsOptionsRequest(string[] lines)
         : base(lines)
      {
      }

      public override bool ResponseExpected
      {
         get
         {
            return true;
         }
      }

      public override RequestType Type
      {
         get
         {
            return RequestType.WrapperSendmeRcsOptions;
         }
      }
   }
}