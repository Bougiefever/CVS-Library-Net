using PServerClient.CVS;

namespace PServerClient.Requests
{
   public class RepositoryRequest : RequestBase
   {
      public RepositoryRequest(Root root)
      {
         RequestLines = new string[1];
         RequestLines[0] = string.Format("{0} {1}", RequestName, root.RepositoryPath);
      }
      public override bool ResponseExpected { get { return false; } }
      public override RequestType RequestType { get { return RequestType.Repository; } }
   }
}