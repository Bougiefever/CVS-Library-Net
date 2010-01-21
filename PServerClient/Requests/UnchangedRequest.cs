using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Unchanged filename \n
   /// Response expected: no. Tell the server that filename has not been modified in
   /// the checked out directory. The filename is a file within the most recent directory
   /// sent with Directory; it must not contain ‘/’.
   /// </summary>
   public class UnchangedRequest : OneArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="UnchangedRequest"/> class.
      /// </summary>
      /// <param name="fileName">Name of the file.</param>
      public UnchangedRequest(string fileName)
         : base(fileName)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="UnchangedRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public UnchangedRequest(IList<string> lines)
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
            return RequestType.Unchanged;
         }
      }
   }
}