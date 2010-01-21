using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// remove \n 
   /// Response expected: yes. Remove a file. This uses any previous Argument,
   /// Directory, Entry, or Modified requests, if they have been sent. The last
   /// Directory sent specifies the working directory at the time of the operation.
   /// Note that this request does not actually do anything to the repository; the only
   /// effect of a successful remove request is to supply the client with a new entries
   /// line containing ‘-’ to indicate a removed file. In fact, the client probably could
   /// perform this operation without contacting the server, although using remove
   /// may cause the server to perform a few more checks.
   /// The client sends a subsequent ci request to actually record the removal in the
   /// repository.
   /// </summary>
   public class RemoveRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="RemoveRequest"/> class.
      /// </summary>
      public RemoveRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="RemoveRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public RemoveRequest(IList<string> lines)
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
            return RequestType.Remove;
         }
      }
   }
}