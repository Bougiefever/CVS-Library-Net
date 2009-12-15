namespace PServerClient.Requests
{
   /// <summary>
   /// release \n
   //Response expected: yes. Note that a cvs release command has taken place
   //and update the history file accordingly.
   /// </summary>
   public class ReleaseRequest : NoArgRequestBase
   {
      public override bool ResponseExpected { get { return true; } }
      public override RequestType RequestType { get { return RequestType.Release; } }
   }
}