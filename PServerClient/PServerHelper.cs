﻿using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace PServerClient
{
   public enum MonthName
   {
      Jan = 1,
      Feb = 2,
      Mar = 3,
      Apr = 4,
      May = 5,
      Jun = 6,
      Jul = 7,
      Aug = 8,
      Sep = 9,
      Oct = 10,
      Nov = 11,
      Dec = 12
   }

   public enum Day
   {
      Sun = 0,
      Mon = 1,
      Tue = 2,
      Wed = 3,
      Thu = 4,
      Fri = 5,
      Sat = 6
   }

   public static class PServerHelper
   {
      private static readonly byte[] _code;

      static PServerHelper()
      {
         _code = new byte[]
                    {
                       0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15,
                       16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31,
                       114, 120, 53, 79, 96, 109, 72, 108, 70, 64, 76, 67, 116, 74, 68, 87,
                       111, 52, 75, 119, 49, 34, 82, 81, 95, 65, 112, 86, 118, 110, 122, 105,
                       41, 57, 83, 43, 46, 102, 40, 89, 38, 103, 45, 50, 42, 123, 91, 35,
                       125, 55, 54, 66, 124, 126, 59, 47, 92, 71, 115, 78, 88, 107, 106, 56,
                       36, 121, 117, 104, 101, 100, 69, 73, 99, 63, 94, 93, 39, 37, 61, 48,
                       58, 113, 32, 90, 44, 98, 60, 51, 33, 97, 62, 77, 84, 80, 85, 223,
                       225, 216, 187, 166, 229, 189, 222, 188, 141, 249, 148, 200, 184, 136, 248, 190,
                       199, 170, 181, 204, 138, 232, 218, 183, 255, 234, 220, 247, 213, 203, 226, 193,
                       174, 172, 228, 252, 217, 201, 131, 230, 197, 211, 145, 238, 161, 179, 160, 212,
                       207, 221, 254, 173, 202, 146, 224, 151, 140, 196, 205, 130, 135, 133, 143, 246,
                       192, 159, 244, 239, 185, 168, 215, 144, 139, 165, 180, 157, 147, 186, 214, 176,
                       227, 231, 219, 169, 175, 156, 206, 198, 129, 164, 150, 210, 154, 177, 134, 127,
                       182, 128, 158, 208, 162, 132, 167, 209, 149, 241, 153, 251, 237, 236, 171, 195,
                       243, 233, 253, 240, 194, 250, 191, 155, 142, 137, 245, 235, 163, 242, 178, 152
                    };
      }

      public static string UnixLineEnd
      {
         get
         {
            return "\n";
         }
      }

      public static string WindowsLineEnd
      {
         get
         {
            return "\r\n";
         }
      }

      public static string ScramblePassword(this string password)
      {
         StringBuilder sb = new StringBuilder(password.Length + 1);
         sb.Append('A');

         for (int i = 0; i < password.Length; i++)
         {
            char c = password[i];
            sb.Append((char) _code[c]);
         }

         return sb.ToString();
      }

      public static string UnscramblePassword(this string scrambled)
      {
         StringBuilder sb = new StringBuilder(scrambled.Length - 1);
         for (int i = 1; i < scrambled.Length; i++)
         {
            char x = scrambled[i];
            char y = (char) _code[x];
            sb.Append(y);
         }

         return sb.ToString();
      }

      public static byte[] Encode(this string message)
      {
         return Encoding.ASCII.GetBytes(message);
      }

      public static string Decode(this byte[] buffer)
      {
         int i = 0;
         int newEnd = 0;
         byte test = buffer[0];
         while (i < buffer.Length - 1)
         {
            byte b = buffer[++i];
            if (test == 10 && b == 0)
               newEnd = i;
            test = b;
         }

         if (newEnd == 0)
            newEnd = buffer.Length;
         byte[] decode = new byte[newEnd];
         Array.Copy(buffer, decode, newEnd);
         return Encoding.ASCII.GetString(decode);
      }

      /// <summary>
      /// RFC822s to date time.
      /// </summary>
      /// <param name="rfcDate">The RFC date.</param>
      /// <returns>DateTime of date string</returns>
      public static DateTime Rfc822ToDateTime(this string rfcDate)
      {
         string dateTimeRegex = @"(\d{1,2})\s(\w{3})\s(\d{4})\s(\d{2}):(\d{2}):(\d{2})\s-(\d{4})";
         Match m = Regex.Match(rfcDate, dateTimeRegex);
         if (!m.Success)
            throw new ArgumentException("RFC822 date string is not formatted properly");
         int day = Convert.ToInt32(m.Groups[1].ToString());
         int month = DateTime.ParseExact(m.Groups[2].ToString(), "MMM", CultureInfo.CurrentCulture).Month;
         int year = Convert.ToInt32(m.Groups[3].ToString());
         int hour = Convert.ToInt32(m.Groups[4].ToString());
         int minute = Convert.ToInt32(m.Groups[5].ToString());
         int second = Convert.ToInt32(m.Groups[6].ToString());
         int offset = Convert.ToInt32(m.Groups[7].ToString());

         DateTime date = new DateTime(year, month, day, hour + offset, minute, second);
         return date;
      }

      public static string ToRfc822(this DateTime date)
      {
         return date.ToString("dd MMM yyyy HH:mm:ss -0000");
      }

      public static string ToEntryString(this DateTime date)
      {
         string entrydate = string.Empty;
         if (date.Year > 1)
            entrydate = date.Day < 10 ? date.ToString("ddd MMM  d HH:mm:ss yyyy") : date.ToString("ddd MMM d HH:mm:ss yyyy");
         return entrydate;
      }

      public static DateTime EntryToDateTime(this string date)
      {
         string dateTimeRegex = @"(.{3})\s(.{3})\s+(\d{1,2})\s(\d{2}):(\d{2}):(\d{2})\s(\d{4})";
         Match m = Regex.Match(date, dateTimeRegex);
         if (!m.Success)
            throw new ArgumentException("Date string is not formatted correctly");

         string mon = m.Groups[2].ToString();
         int day = Convert.ToInt32(m.Groups[3].ToString());
         int hour = Convert.ToInt32(m.Groups[4].ToString());
         int minute = Convert.ToInt32(m.Groups[5].ToString());
         int second = Convert.ToInt32(m.Groups[6].ToString());
         int year = Convert.ToInt32(m.Groups[7].ToString());
         int month = StringToEnum(typeof(MonthName), mon);
         DateTime dt = new DateTime(year, month, day, hour, minute, second);

         return dt;
      }

      public static int StringToEnum(Type t, string value)
      {
         var fis = t.GetFields();
         object result = null;
         foreach (FieldInfo fi in fis)
         {
            if (fi.Name == value)
               result = fi.GetValue(null);
         }

         if (result == null)
            throw new FormatException("Cannot convert string to " + t);
         return Convert.ToInt32(result);
      }

      /// <summary>
      /// Gets the updated fname path file.
      /// </summary>
      /// <param name="fname">The fname.</param>
      /// <returns>the module and file information</returns>
      public static string[] GetUpdatedFnamePathFile(string fname)
      {
         string fnameRegex = @"fname (.+)/(.+)$";
         Match m = Regex.Match(fname, fnameRegex);
         string[] names = new string[2];
         if (m.Success)
         {
            names[0] = m.Groups[1].ToString();
            names[1] = m.Groups[2].ToString();
         }

         return names;
      }

      /// <summary>
      /// Gets the root module folder path.
      /// </summary>
      /// <param name="workingDirectory">The local working directory.</param>
      /// <param name="moduleName">Name of the CVS module.</param>
      /// <returns>Returns the directory for the root module folder</returns>
      public static DirectoryInfo GetRootModuleFolderPath(DirectoryInfo workingDirectory, string moduleName)
      {
         string[] folders = moduleName.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
         string path = workingDirectory.FullName;
         for (int i = 0; i < folders.Length; i++)
         {
            path = Path.Combine(path, folders[i]);
         }

         DirectoryInfo di = new DirectoryInfo(path);
         return di;
      }

      /// <summary>
      /// Determines whether [is test mode].
      /// </summary>
      /// <returns>If config is set up for test mode, returns true</returns>
      public static bool IsTestMode()
      {
         string mode = ConfigurationManager.AppSettings["mode"];
         return mode == "test";
      }

      /// <summary>
      /// Gets the entry filename regex.
      /// </summary>
      /// <param name="fileName">Name of the file.</param>
      /// <returns>regex string</returns>
      public static string GetEntryFilenameRegex(string fileName)
      {
         string name = fileName.Replace(".", @"\.");
         string regex = string.Format("D?{0}.", name);
         return regex;
      }
   }
}