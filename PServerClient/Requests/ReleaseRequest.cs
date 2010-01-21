using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// release \n
   /// Response expected: yes. Note that a cvs release command has taken place
   /// and update the history file accordingly.
   /// </summary>
   public class ReleaseRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="ReleaseRequest"/> class.
      /// </summary>
      public ReleaseRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ReleaseRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public ReleaseRequest(IList<string> lines)
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
            return RequestType.Release;
         }
      }
   }
}