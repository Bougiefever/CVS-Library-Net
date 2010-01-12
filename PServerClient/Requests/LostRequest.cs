using System.Collections.Generic;

namespace PServerClient.Requests
{
   public class LostRequest : RequestBase
   {
      public LostRequest(string fileName)
      {
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1}", RequestName, fileName);
      }

      public LostRequest(IList<string> lines)
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
            return RequestType.Lost;
         }
      }
   }
}