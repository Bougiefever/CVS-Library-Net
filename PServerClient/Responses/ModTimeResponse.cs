using System;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   public class ModTimeResponse : ResponseBase
   {
      public override void Process()
      {
         string date = Lines[0];
         ModTime = date.Rfc822ToDateTime();
         base.Process();
      }

      public override ResponseType Type { get { return ResponseType.ModTime; } }
      public DateTime ModTime { get; set; }

      public override string Display()
      {
         return ModTime.ToString();
      }
   }
}