using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Root pathname \n
   /// Response expected: no. Tell the server which CVSROOT to use. Note that
   /// pathname is not a fully qualified CVSROOT variable, but only the local directory
   /// part of it. pathname must already exist on the server; if creating a new root, use
   /// the init request, not Root. Again, pathname does not include the hostname
   /// of the server, how to access the server, etc.; by the time the CVS protocol is in
   /// use, connection, authentication, etc., are already taken care of.
   /// The Root request must be sent only once, and it must be sent before any
   /// requests other than Valid-responses, valid-requests, UseUnchanged, Set,
   /// Global_option, init, noop, or version.
   /// </summary>
   public class RootRequest : OneArgRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="RootRequest"/> class.
      /// </summary>
      /// <param name="root">The CVS root string.</param>
      public RootRequest(string root)
         : base(root)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="RootRequest"/> class.
      /// </summary>
      /// <param name="lines">The lines.</param>
      public RootRequest(IList<string> lines)
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
            return RequestType.Root;
         }
      }
   }
}