using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// update-patches \n
   /// Response expected: yes. This request does not actually do anything. It is used
   /// as a signal that the server is able to generate patches when given an update
   /// request. The client must issue the -u argument to update in order to receive
   /// patches.
   /// </summary>
   public class UpdatePatchesRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="UpdatePatchesRequest"/> class.
      /// </summary>
      public UpdatePatchesRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="UpdatePatchesRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public UpdatePatchesRequest(IList<string> lines)
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
            return RequestType.UpdatePatches;
         }
      }
   }
}