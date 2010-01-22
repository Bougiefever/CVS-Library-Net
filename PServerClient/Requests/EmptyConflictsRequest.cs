using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Empty-conflicts \n
   /// Response expected: yes. This request is an alias for noop. Its presence in the
   /// list of valid-requests is intended to be used as a placeholder to alert the client
   /// that the server does not require the contents of files with conflicts that have
   /// not been modified since the merge, for operations other than diff. It was a bug
   /// in pre 1.11.22 + pre 1.12.14 servers that the contents of files with conflicts was
   /// required for the server to acknowledge the existence of the conflicts.
   /// </summary>
   public class EmptyConflictsRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="EmptyConflictsRequest"/> class.
      /// </summary>
      public EmptyConflictsRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="EmptyConflictsRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public EmptyConflictsRequest(IList<string> lines)
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
            return RequestType.EmptyConflicts;
         }
      }
   }
}