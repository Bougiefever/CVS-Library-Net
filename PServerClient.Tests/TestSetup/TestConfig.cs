using System;
using System.Configuration;
using System.IO;

namespace PServerClient.Tests.TestSetup
{
   public static class TestConfig
   {
      public static string CVSHost
      {
         get
         {
            string host = ConfigurationManager.AppSettings["CVS Host"];
            host = host ?? "host-name";
            return host;
         }
      }

      public static int CVSPort
      {
         get
         {
            string port = ConfigurationManager.AppSettings["CVS Port"];
            port = port ?? "1";
            return Convert.ToInt32(port);
         }
      }

      public static string Username
      {
         get
         {
            string username = ConfigurationManager.AppSettings["CVS Username"];
            username = username ?? "username";
            return username;
         }
      }

      public static string Password
      {
         get
         {
            string pwd = ConfigurationManager.AppSettings["Password scrambled"];
            pwd = pwd ?? "A:yZZ30 e";
            return pwd.UnscramblePassword();
         }
      }

      public static string PasswordScrambled
      {
         get
         {
            string pwd = ConfigurationManager.AppSettings["Password scrambled"];
            pwd = pwd ?? "A:yZZ30 e";
            return pwd;
         }
      }

      public static DirectoryInfo WorkingDirectory
      {
         get
         {
            string path = ConfigurationManager.AppSettings["Working Directory Path"];
            path = path ?? @"c:\_temp";
            DirectoryInfo di = new DirectoryInfo(path);
            return di;
         }
      }

      public static string RepositoryPath
      {
         get
         {
            string path = ConfigurationManager.AppSettings["Repository Path"];
            path = path ?? "/usr/local/cvsroot/sandbox";
            return path;
         }
      }

      public static string ModuleName
      {
         get
         {
            string mod = ConfigurationManager.AppSettings["Module Name"];
            mod = mod ?? "mymod";
            return mod;
         }
      }

      public static string LocalModuleDirectoryName
      {
         get
         {
            string dir = ConfigurationManager.AppSettings["Local Module Directory Name"];
            dir = dir ?? string.Empty;
            return dir;
         }
      }
   }
}