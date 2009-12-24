namespace PServerClient.Requests
{
   /// <summary>
   /// release \n
   //Response expected: yes. Note that a cvs release command has taken place
   //and update the history file accordingly.
   /// </summary>
   public class ReleaseRequest : NoArgRequestBase
   {
      public ReleaseRequest(){}
      public ReleaseRequest(string[] lines):base(lines){}
      public override bool ResponseExpected { get { return true; } }
      public override RequestType Type { get { return RequestType.Release; } }
   }
}