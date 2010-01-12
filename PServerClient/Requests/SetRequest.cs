using System.Collections.Generic;

namespace PServerClient.Requests
{
   /// <summary>
   /// Set variable=value \n
   /// Response expected: no. Set a user variable variable to value. The Root request
   /// need not have been previously sent.
   /// </summary>
   public class SetRequest : RequestBase
   {
      public SetRequest(string variableName, string value)
      {
         Lines = new string[1];
         Lines[0] = string.Format("{0} {1}={2}", RequestName, variableName, value);
      }

      public SetRequest(IList<string> lines)
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
            return RequestType.Set;
         }
      }
   }
}