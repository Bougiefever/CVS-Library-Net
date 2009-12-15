using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PServerClient.Responses.Messages
{
   public static class MessageHelper
   {
      private const string _fnameRegex = @"fname (.+)/(.+)$";
      private const string _muRegex = @"M U (.+)/(.+)$";
      public static string[] GetUpdatedFnamePathFile(string fname)
      {
         Match m = Regex.Match(fname, _fnameRegex);
         string[] names = new string[2];
         if (m.Success)
         {
            names[0] = m.Groups[1].ToString();
            names[1] = m.Groups[2].ToString();
         }
         return names;
      }

      public static string[] GetMUPathFile(string mu)
      {
         Match m = Regex.Match(mu, _muRegex);
         string[] names = new string[2];
         if (m.Success)
         {
            names[0] = m.Groups[1].ToString();
            names[1] = m.Groups[2].ToString();
         }
         return names;
      }
   }
}
