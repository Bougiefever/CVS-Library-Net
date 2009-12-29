namespace PServerClient.Requests
{
   /// <summary>
   /// update \n Response expected: yes. Actually do a cvs update command. This uses any
   //previous Argument, Directory, Entry, or Modified requests, if they have been
   //sent. The last Directory sent specifies the working directory at the time of the
   //operation. The -I option is not used–files which the client can decide whether
   //to ignore are not mentioned and the client sends the Questionable request for
   //others.
   /// </summary>
   public class UpdateRequest : NoArgRequestBase
   {
      public UpdateRequest(){}
      public UpdateRequest(string[] lines):base(lines){}
      public override bool ResponseExpected { get { return true; } }
      public override RequestType Type { get { return RequestType.Update; } }
   }
}