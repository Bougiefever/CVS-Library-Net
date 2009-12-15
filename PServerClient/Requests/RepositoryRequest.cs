namespace PServerClient.Requests
{
   public class RepositoryRequest : RequestBase
   {
      private readonly CvsRoot _root;

      public RepositoryRequest(CvsRoot root)
      {
         _root = root;
      }

      public override bool ResponseExpected { get { return false; } }
      public override RequestType RequestType { get { return RequestType.Repository; } }

      public override string GetRequestString()
      {
         return string.Format("{2} {0}{1}", _root.Root, LineEnd, RequestName);
      }
   }
}