using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// export \n 
   /// Response expected: yes. Get files from the repository. This uses any previous
   /// Argument, Directory, Entry, or Modified requests, if they have been sent.
   /// Arguments to this command are module names, as described for the co request.
   /// The intention behind this command is that a client can get sources from a server
   /// without storing CVS information about those sources. That is, a client probably
   /// should not count on being able to take the entries line returned in the Created
   /// response from an export request and send it in a future Entry request. Note
   /// that the entries line in the Created response must indicate whether the file is
   /// binary or text, so the client can create it correctly.
   /// </summary>
   public class ExportRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="ExportRequest"/> class.
      /// </summary>
      public ExportRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="ExportRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public ExportRequest(IList<string> lines)
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
            return RequestType.Export;
         }
      }
   }
}