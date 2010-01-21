using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// noop \n
   /// Response expected: yes. This request is a null command in the sense that
   /// it doesn’t do anything, but merely (as with any other requests expecting a
   /// response) sends back any responses pertaining to pending errors, pending
   /// Notified responses, etc. The Root request need not have been previously
   /// sent.
   /// </summary>
   public class NoopRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="NoopRequest"/> class.
      /// </summary>
      public NoopRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="NoopRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public NoopRequest(IList<string> lines)
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
            return RequestType.Noop;
         }
      }
   }
}