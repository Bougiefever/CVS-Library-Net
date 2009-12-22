namespace PServerClient.Requests
{
   public class LostRequest : RequestBase
   {
      public LostRequest(string fileName)
      {
         RequestLines  = new string[1];
         RequestLines[0] = string.Format("{0} {1}", RequestName, fileName);
      }

      public override bool ResponseExpected { get { return false; } }
      public override RequestType RequestType { get { return RequestType.Lost; } }
   }
}