using PServerClient.CVS;

namespace PServerClient.Requests
{
   /// <summary>
   /// Root pathname \n
   ///Response expected: no. Tell the server which CVSROOT to use. Note that
   ///pathname is not a fully qualified CVSROOT variable, but only the local directory
   ///part of it. pathname must already exist on the server; if creating a new root, use
   ///the init request, not Root. Again, pathname does not include the hostname
   ///of the server, how to access the server, etc.; by the time the CVS protocol is in
   ///use, connection, authentication, etc., are already taken care of.
   ///The Root request must be sent only once, and it must be sent before any
   ///requests other than Valid-responses, valid-requests, UseUnchanged, Set,
   ///Global_option, init, noop, or version.
   /// </summary>
   public class RootRequest : RequestBase
   {
      private readonly Root _root;

      public RootRequest(Root root)
      {
         _root = root;
      }

      public override bool ResponseExpected { get { return false; } }
      public override RequestType RequestType { get { return RequestType.Root; } }

      public override string GetRequestString()
      {
         return string.Format("{2} {0}{1}", _root.CVSRoot, LineEnd, RequestName);
      }
   }
}