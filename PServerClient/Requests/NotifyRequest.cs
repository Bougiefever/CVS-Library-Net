namespace PServerClient.Requests
{
   /// <summary>
   /// Notify filename \n
   //Response expected: no. Tell the server that an edit or unedit command has
   //taken place. The server needs to send a Notified response, but such response is
   //deferred until the next time that the server is sending responses. The filename
   //is a file within the most recent directory sent with Directory; it must not
   //contain ‘/’. Additional data:
   //notification-type \t time \t clienthost \t
   //working-dir \t watches \n
   //where notification-type is ‘E’ for edit, ‘U’ for unedit, undefined behavior if ‘C’,
   //and all other letters should be silently ignored for future expansion. time is
   //the time at which the edit or unedit took place, in a user-readable format of
   //the client’s choice (the server should treat the time as an opaque string rather
   //than interpreting it). clienthost is the name of the host on which the edit or
   //unedit took place, and working-dir is the pathname of the working directory
   //where the edit or unedit took place. watches are the temporary watches, zero
   //or more of the following characters in the following order: ‘E’ for edit, ‘U’ for
   //unedit, ‘C’ for commit, and all other letters should be silently ignored for future
   //expansion. If notification-type is ‘E’ the temporary watches are set; if it is ‘U’
   //they are cleared. If watches is followed by \t then the \t and the rest of the
   //line should be ignored, for future expansion.
   //The time, clienthost, and working-dir fields may not contain the characters ‘+’,
   //‘,’, ‘>’, ‘;’, or ‘=’.
   //Note that a client may be capable of performing an edit or unedit operation
   //without connecting to the server at that time, and instead connecting to the
   //server when it is convenient (for example, when a laptop is on the net again) to
   //send the Notify requests. Even if a client is capable of deferring notifications,
   //it should attempt to send them immediately (one can send Notify requests
   //together with a noop request, for example), unless perhaps if it can know that
   //a connection would be impossible.
   /// </summary>
   public class NotifyRequest : RequestBase
   {
      public NotifyRequest(string fileName)
      {
         RequestLines = new string[1];
         RequestLines[0] = string.Format("{0} {1}", RequestName, fileName);
      }
      public NotifyRequest(string[] lines):base(lines){}

      public override bool ResponseExpected { get { return false; } }
      public override RequestType Type { get { return RequestType.Notify; } }
   }
}