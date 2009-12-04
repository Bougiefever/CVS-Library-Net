using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PServerClient.Responses
{
   public class ModTimeResponse : ResponseBase
   {
      //private string _dateTimeRegex = @"(\d{2})\s(\w{3})\s(\d{4})\s(\d{2}):(\d{2}):(\d{2})\s-(\d{4})";
      public override ResponseType ResponseType { get { return ResponseType.ModTime; } }
      public DateTime ModTime { get; set; }
      public override void ProcessResponse(IList<string> lines)
      {
         string date = lines[0];
         ModTime = PServerHelper.ConvertRFC822ToDateTime(date);
         //Match m = Regex.Match(date, _dateTimeRegex);

         //int day = Convert.ToInt32(m.Groups[1].ToString());
         //int month = DateTime.ParseExact(m.Groups[2].ToString(), "MMM", CultureInfo.CurrentCulture).Month;
         //int year = Convert.ToInt32(m.Groups[3].ToString());
         //int hour = Convert.ToInt32(m.Groups[4].ToString());
         //int minute = Convert.ToInt32(m.Groups[5].ToString());
         //int second = Convert.ToInt32(m.Groups[6].ToString());
         //int offset = Convert.ToInt32(m.Groups[7].ToString());

         //ModTime = new DateTime(year, month, day, hour + offset, minute, second);
         base.ProcessResponse(lines);
      }
   }
}
