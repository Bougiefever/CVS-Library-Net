using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// wrapper-sendme-rcsOptions \n
   /// Response expected: yes. Request that the server transmit mappings from file-
   /// names to keyword expansion modes in Wrapper-rcsOption responses.
   /// </summary>
   public class WrapperSendmeRcsOptionsRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="WrapperSendmeRcsOptionsRequest"/> class.
      /// </summary>
      public WrapperSendmeRcsOptionsRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="WrapperSendmeRcsOptionsRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public WrapperSendmeRcsOptionsRequest(IList<string> lines)
         : base(lines)
      {
      }

      /// <summary>
      /// Gets a value indicating whether a response is expected from CVS after sending the request.
      /// </summary>
      /// <value><c>true</c> if [response expected]; otherwise, <c>false</c>.</value>
      public override bool ResponseExpected
      {
         get
         {
            return true;
         }
      }

      /// <summary>
      /// Gets the RequestType of the request
      /// </summary>
      /// <value>The RequestType value</value>
      public override RequestType Type
      {
         get
         {
            return RequestType.WrapperSendmeRcsOptions;
         }
      }
   }
}