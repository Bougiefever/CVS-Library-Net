namespace PServerClient.Requests
{
   public class LostRequest : RequestBase
   {
      public LostRequest(string fileName)
      {
         RequestLines = new string[1];
         RequestLines[0] = string.Format("{0} {1}", RequestName, fileName);
      }
      public LostRequest(string[] lines):base(lines){}
      public override bool ResponseExpected { get { return false; } }
      public override RequestType Type { get { return RequestType.Lost; } }
   }
}