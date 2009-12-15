namespace PServerClient.Requests
{
   /// <summary>
   /// ci \n
   //Response expected: yes. Actually do a cvs command. This uses any previous
   //Argument, Directory, Entry, or Modified requests, if they have been sent.
   //The last Directory sent specifies the working directory at the time of the
   //operation. No provision is made for any input from the user. This means that
   //ci must use a -m argument if it wants to specify a log message.
   /// </summary>
   public class CheckInRequest : NoArgRequestBase
   {
      public override bool ResponseExpected { get { return true; } }
      public override RequestType RequestType { get { return RequestType.CheckIn; } }
   }
}