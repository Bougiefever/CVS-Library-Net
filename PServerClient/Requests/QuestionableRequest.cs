namespace PServerClient.Requests
{
   /// <summary>
   /// Questionable filename \n
   //Response expected: no. Additional data: no. Tell the server to check whether
   //filename should be ignored, and if not, next time the server sends responses,
   //send (in a M response) ‘?’ followed by the directory and filename. filename must
   //not contain ‘/’; it needs to be a file in the directory named by the most recent
   //Directory request.
   /// </summary>
   public class QuestionableRequest : OneArgRequestBase
   {
      public QuestionableRequest(string fileName) : base(fileName)
      {
      }

      public override RequestType RequestType { get { return RequestType.Questionable; } }
   }
}