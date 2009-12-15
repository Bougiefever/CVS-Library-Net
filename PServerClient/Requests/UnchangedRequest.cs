namespace PServerClient.Requests
{
   /// <summary>
   /// Unchanged filename \n
   //Response expected: no. Tell the server that filename has not been modified in
   //the checked out directory. The filename is a file within the most recent directory
   //sent with Directory; it must not contain ‘/’.
   /// </summary>
   public class UnchangedRequest : OneArgRequestBase
   {
      public UnchangedRequest(string fileName) : base(fileName)
      {
      }

      public override RequestType RequestType { get { return RequestType.Unchanged; } }
   }
}