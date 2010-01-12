using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Static-directory \n
   /// Response expected: no. Tell the server that the directory most recently specified
   /// with Directory should not have additional files checked out unless explicitly
   /// requested. The client sends this if the Entries.Static flag is set, which is
   /// controlled by the Set-static-directory and Clear-static-directory re-
   /// sponses.
   /// </summary>
   public class StaticDirectoryRequest : NoArgRequestBase
   {
      public StaticDirectoryRequest()
      {
      }

      public StaticDirectoryRequest(IList<string> lines)
         : base(lines)
      {
      }

      public override RequestType Type
      {
         get
         {
            return RequestType.StaticDirectory;
         }
      }
   }
}