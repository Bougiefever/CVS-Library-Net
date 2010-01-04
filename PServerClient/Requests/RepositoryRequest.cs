using PServerClient.CVS;

namespace PServerClient.Requests
{
   public class RepositoryRequest : RequestBase
   {
      public RepositoryRequest(IRoot root)
      {
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1}", RequestName, "");
      }
      public RepositoryRequest(string[] lines):base(lines){}
      public override bool ResponseExpected { get { return false; } }
      public override RequestType Type { get { return RequestType.Repository; } }
   }
}