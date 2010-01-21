using System.Collections.Generic;
using PServerClient.CVS;

namespace PServerClient.Requests
{
   /// <summary>
   /// 	<para>The client connects, and sends the following:<br/>
   ///     • the string ‘BEGIN AUTH REQUEST’, a linefeed,<br/>
   ///     • the cvs root, a linefeed,<br/>
   ///     • the username, a linefeed,<br/>
   ///     • the password trivially encoded (see Chapter 4 [Password scrambling],<br/>
   ///     page 6), a linefeed,<br/>
   ///     • the string ‘END AUTH REQUEST’, and a linefeed.<br/>
   ///     The client must send the identical string for cvs root both here and later in<br/>
   ///     the Root request of the cvs protocol itself. Servers are encouraged to
   ///     enforce<br/>
   ///     this restriction. The possible server responses (each of which is followed by<br/>
   ///     a linefeed) are the following. Note that although there is a small similarity<br/>
   ///     between this authentication protocol and the cvs protocol, they are separate.<br/>
   ///     I LOVE YOU<br/>
   ///     The authentication is successful. The client proceeds with the cvs<br/>
   ///     protocol itself.<br/>
   ///     I HATE YOU<br/>
   ///     The authentication fails. After sending this response, the server<br/>
   ///     may close the connection. It is up to the server to decide whether<br/>
   ///     to give this response, which is generic, or a more specific response<br/>
   ///     using ‘E’ and/or ‘error’.</para>
   /// </summary>
   public class AuthRequest : AuthRequestBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="AuthRequest"/> class.
      /// </summary>
      /// <param name="root">The CVS root.</param>
      public AuthRequest(IRoot root)
         : base(root, RequestType.Auth)
      {
      }

      /// <summary>
      /// Initializes a new instance of the <see cref="AuthRequest"/> class.
      /// </summary>
      /// <param name="lines">The request string.</param>
      public AuthRequest(IList<string> lines)
         : base(lines)
      {
      }

      /// <summary>
      /// Gets the RequestType.
      /// </summary>
      /// <value>The type of this request.</value>
      public override RequestType Type
      {
         get
         {
            return RequestType.Auth;
         }
      }
   }
}