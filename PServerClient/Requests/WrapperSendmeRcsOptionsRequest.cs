namespace PServerClient.Requests
{
   /// <summary>
   /// wrapper-sendme-rcsOptions \n
   //Response expected: yes. Request that the server transmit mappings from file-
   //names to keyword expansion modes in Wrapper-rcsOption responses.
   /// </summary>
   public class WrapperSendmeRcsOptionsRequest : NoArgRequestBase
   {
      public override bool ResponseExpected { get { return true; } }
      public override RequestType RequestType { get { return RequestType.WrapperSendmercsOptions; } }
   }
}