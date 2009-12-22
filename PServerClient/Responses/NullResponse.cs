using System.Collections.Generic;

namespace PServerClient.Responses
{
   public class NullResponse : ResponseBase
   {
      public override int LineCount { get { return 1; } }
      public override ResponseType ResponseType { get { return ResponseType.Unknown; } }

      public override void ProcessResponse(IList<string> lines)
      {
         ResponseLines = new string[lines.Count];
         for (int i = 0; i < lines.Count; i++)
         {
            ResponseLines[i] = lines[i];
         }
      }
   }
}