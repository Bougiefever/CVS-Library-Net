using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// co \n
   /// Response expected: yes. Get files from the repository. This uses any previous
   /// Argument, Directory, Entry, or Modified requests, if they have been sent.
   /// Arguments to this command are module names; the client cannot know what
   /// directories they correspond to except by (1) just sending the co request, and
   /// then seeing what directory names the server sends back in its responses, and
   /// (2) the expand-modules request.
   /// </summary>
   public class CheckOutRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="CheckOutRequest"/> class.
      /// </summary>
      public CheckOutRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="CheckOutRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public CheckOutRequest(IList<string> lines)
         : base(lines)
      {
      }

      /// <summary>
      /// Gets a value indicating whether [response expected].
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
            return RequestType.CheckOut;
         }
      }
   }
}