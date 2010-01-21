using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// 	The client connects, and sends the following:<br/>
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
   ///     using ‘E’ and/or ‘error’.
   /// </summary>
   public class AuthResponse : ResponseBase, IAuthResponse
   {
      private const string AuthenticateFail = "I HATE YOU";
      private const string AuthenticatePass = "I LOVE YOU";

      /// <summary>
      /// Gets the authentication status.
      /// </summary>
      /// <value>The status.</value>
      public AuthStatus Status { get; private set; }

      /// <summary>
      /// Gets the line count expected for the response
      /// so the processor knows how many lines to take and use
      /// for this response
      /// </summary>
      /// <value>The line count.</value>
      public override int LineCount
      {
         get
         {
            return 1;
         }
      }

      /// <summary>
      /// Gets the ResponseType.
      /// </summary>
      /// <value>The response type.</value>
      public override ResponseType Type
      {
         get
         {
            return ResponseType.Auth;
         }
      }

      /// <summary>
      /// Processes this instance.
      /// </summary>
      public override void Process()
      {
         if (Lines[0].Contains(AuthenticatePass))
         {
            Status = AuthStatus.Authenticated;
         }

         if (Lines[0].Contains(AuthenticateFail))
         {
            Status = AuthStatus.NotAuthenticated;
         }

         base.Process();
      }

      /// <summary>
      /// Initializes the response with the lines from CVS
      /// </summary>
      /// <param name="lines">The response lines.</param>
      public override void Initialize(IList<string> lines)
      {
         Lines = new List<string>(1) { lines[0] };
      }

      /// <summary>
      /// Displays this instance.
      /// </summary>
      /// <returns>string to display</returns>
      public override string Display()
      {
         return Lines[0];
      }
   }
}