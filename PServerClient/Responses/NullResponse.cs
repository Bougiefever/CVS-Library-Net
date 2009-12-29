using System.Collections.Generic;

namespace PServerClient.Responses
{
   public class NullResponse : ResponseBase
   {
      public override int LineCount { get { return 1; } }
      public override ResponseType Type { get { return ResponseType.Null; } }

      public override void Process(IList<string> lines)
      {
         Lines = new string[lines.Count];
         for (int i = 0; i < lines.Count; i++)
         {
            Lines[i] = lines[i];
         }
      }
   }
}