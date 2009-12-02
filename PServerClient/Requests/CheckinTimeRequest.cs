using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class CheckinTimeRequest : RequestBase
   {
      private DateTime _checkin;
      public CheckinTimeRequest(DateTime time)
      {
         _checkin = time;
      }
      public override bool ResponseExpected { get { return false; } }
      public override string GetRequestString()
      {
         string time = _checkin.ToString("dd MMM yyyy HH:mm:ss -0000");
         return string.Format("Checkin-time {0}{1}", time, lineEnd);
      }
   }
}
