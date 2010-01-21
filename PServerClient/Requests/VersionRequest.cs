using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// version \n
   /// Response expected: yes. Request that the server transmit its version message.
   /// The Root request need not have been previously sent.
   /// </summary>
   public class VersionRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="VersionRequest"/> class.
      /// </summary>
      public VersionRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="VersionRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public VersionRequest(IList<string> lines)
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
            return RequestType.Version;
         }
      }
   }
}