using System.Collections.Generic;

namespace PServerClient.Requests
{
   public class RepositoryRequest : RequestBase
   {
      public RepositoryRequest(string repository)
      {
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1}", RequestName, repository);
      }

      public RepositoryRequest(IList<string> lines)
         : base(lines)
      {
      }

      public override bool ResponseExpected
      {
         get
         {
            return false;
         }
      }

      public override RequestType Type
      {
         get
         {
            return RequestType.Repository;
         }
      }
   }
}