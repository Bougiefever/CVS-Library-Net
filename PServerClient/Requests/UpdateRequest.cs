using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// update \n Response expected: yes. Actually do a cvs update command. This uses any
   /// previous Argument, Directory, Entry, or Modified requests, if they have been
   /// sent. The last Directory sent specifies the working directory at the time of the
   /// operation. The -I option is not used–files which the client can decide whether
   /// to ignore are not mentioned and the client sends the Questionable request for
   /// others.
   /// </summary>
   public class UpdateRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="UpdateRequest"/> class.
      /// </summary>
      public UpdateRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="UpdateRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public UpdateRequest(IList<string> lines)
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
            return RequestType.Update;
         }
      }
   }
}