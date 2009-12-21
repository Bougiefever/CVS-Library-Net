using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   /// <summary>
   /// Mode mode \n
   //This mode applies to the next file mentioned in Checked-in. Mode is a file
   //update modifying response as described in Section 5.9 [Response intro], 
   /// </summary>
   public class ModeResponse : ResponseBase
   {
      public string Mode { get; private set; }
      public override ResponseType ResponseType { get { return ResponseType.Mode; } }
      public override void ProcessResponse(IList<string> lines)
      {
         Mode = lines[0];
         base.ProcessResponse(lines);
      }
      public override string DisplayResponse()
      {
         return Mode;
      }
   }
}