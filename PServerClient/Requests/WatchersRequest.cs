using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// watchers \n
   /// Response expected: yes. Actually do a cvs command. This uses any previous
   /// Argument, Directory, Entry, or Modified requests, if they have been sent.
   /// The last Directory sent specifies the working directory at the time of the
   /// operation. No provision is made for any input from the user. This means that
   /// ci must use a -m argument if it wants to specify a log message.
   /// </summary>
   public class WatchersRequest : NoArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="WatchersRequest"/> class.
      /// </summary>
      public WatchersRequest()
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="WatchersRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public WatchersRequest(IList<string> lines)
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
            return RequestType.Watchers;
         }
      }
   }
}