using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Questionable filename \n
   /// Response expected: no. Additional data: no. Tell the server to check whether
   /// filename should be ignored, and if not, next time the server sends responses,
   /// send (in a M response) ‘?’ followed by the directory and filename. filename must
   /// not contain ‘/’; it needs to be a file in the directory named by the most recent
   /// Directory request.
   /// </summary>
   public class QuestionableRequest : OneArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="QuestionableRequest"/> class.
      /// </summary>
      /// <param name="fileName">Name of the file.</param>
      public QuestionableRequest(string fileName)
         : base(fileName)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="QuestionableRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public QuestionableRequest(IList<string> lines)
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
            return RequestType.Questionable;
         }
      }
   }
}