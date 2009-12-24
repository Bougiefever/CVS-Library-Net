namespace PServerClient.Requests
{
   /// <summary>
   /// remove \n 
   //Response expected: yes. Remove a file. This uses any previous Argument,
   //Directory, Entry, or Modified requests, if they have been sent. The last
   //Directory sent specifies the working directory at the time of the operation.
   //Note that this request does not actually do anything to the repository; the only
   //effect of a successful remove request is to supply the client with a new entries
   //line containing ‘-’ to indicate a removed file. In fact, the client probably could
   //perform this operation without contacting the server, although using remove
   //may cause the server to perform a few more checks.
   //The client sends a subsequent ci request to actually record the removal in the
   //repository.
   /// </summary>
   public class RemoveRequest : NoArgRequestBase
   {
      public RemoveRequest(){}
      public RemoveRequest(string[] lines):base(lines){}
      public override bool ResponseExpected { get { return true; } }
      public override RequestType Type { get { return RequestType.Remove; } }
   }
}