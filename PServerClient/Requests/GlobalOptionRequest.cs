using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Global_option option \n
   /// Response expected: no. Transmit one of the global options ‘-q’, ‘-Q’, ‘-l’,
   /// ‘-t’, ‘-r’, or ‘-n’. option must be one of those strings, no variations (such as
   /// combining of options) are allowed. For graceful handling of valid-requests,
   /// it is probably better to make new global options separate requests, rather than
   /// trying to add them to this request. The Root request need not have been
   /// previously sent.
   /// </summary>
   public class GlobalOptionRequest : OneArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="GlobalOptionRequest"/> class.
      /// </summary>
      /// <param name="arg">The argument string.</param>
      public GlobalOptionRequest(string arg)
         : base(arg)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="GlobalOptionRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public GlobalOptionRequest(IList<string> lines)
         : base(lines)
      {
      }

      /// <summary>
      /// Gets the RequestType of the request
      /// </summary>
      /// <value>The RequestType value</value>
      public override RequestType Type
      {
         get
         {
            return RequestType.GlobalOption;
         }
      }
   }
}