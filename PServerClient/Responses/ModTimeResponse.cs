using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   public class ModTimeResponse : ResponseBase
   {
      public override ResponseType Type { get { return ResponseType.ModTime; } }
      public DateTime ModTime { get; set; }

      public override void ProcessResponse(IList<string> lines)
      {
         string date = lines[0];
         ModTime = date.Rfc822ToDateTime();
         base.ProcessResponse(lines);
      }

      public override string DisplayResponse()
      {
         return ModTime.ToString();
      }
   }
}