using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// rdiff \n
   /// Response expected: yes. Actually do a cvs command. This uses any previ-
   /// ous Argument requests, if they have been sent. The client should not send
   /// Directory, Entry, or Modified requests for these commands; they are not
   /// used. Arguments to these commands are module names, as described for co.
   /// </summary>
   public class RDiffRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="RDiffRequest"/> class.
      /// </summary>
      public RDiffRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="RDiffRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public RDiffRequest(IList<string> lines)
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
            return RequestType.RDiff;
         }
      }
   }
}