namespace PServerClient.Requests
{
   public class LostRequest : RequestBase
   {
      private readonly string _fileName;

      public LostRequest(string fileName)
      {
         _fileName = fileName;
      }

      public override bool ResponseExpected { get { return false; } }
      public override RequestType RequestType { get { return RequestType.Lost; } }

      public override string GetRequestString()
      {
         return string.Format("{2} {0}{1}", _fileName, LineEnd, RequestName);
      }
   }
}