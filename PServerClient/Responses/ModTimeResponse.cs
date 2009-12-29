using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   public class ModTimeResponse : ResponseBase
   {
      public override ResponseType Type { get { return ResponseType.ModTime; } }
      public DateTime ModTime { get; set; }

      public override void Process(IList<string> lines)
      {
         string date = lines[0];
         ModTime = date.Rfc822ToDateTime();
         base.Process(lines);
      }

      public override string Display()
      {
         return ModTime.ToString();
      }
   }
}