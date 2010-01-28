using System;
using System.Configuration;
using System.IO;

namespace PServerClient.Tests.TestSetup
{
   /// <summary>
   /// Test data to populate the Root instance for testing
   /// </summary>
   public static class TestConfig
   {
      /// <summary>
      /// Gets the CVS host.
      /// </summary>
      /// <value>The CVS host.</value>
      public static string CVSHost
      {
         get
         {
            string host = ConfigurationManager.AppSettings["CVS Host"];
            host = host ?? "host-name";
            return host;
         }
      }

      /// <summary>
      /// Gets the CVS port.
      /// </summary>
      /// <value>The CVS port.</value>
      public static int CVSPort
      {
         get
         {
            string port = ConfigurationManager.AppSettings["CVS Port"];
            port = port ?? "1";
            return Convert.ToInt32(port);
         }
      }

      /// <summary>
      /// Gets the username.
      /// </summary>
      /// <value>The username.</value>
      public static string Username
      {
         get
         {
            string username = ConfigurationManager.AppSettings["CVS Username"];
            username = username ?? "username";
            return username;
         }
      }

      /// <summary>
      /// Gets the password.
      /// </summary>
      /// <value>The password.</value>
      public static string Password
      {
         get
         {
            string pwd = ConfigurationManager.AppSettings["Password scrambled"];
            pwd = pwd ?? "A:yZZ30 e";
            return pwd.UnscramblePassword();
         }
      }

      /// <summary>
      /// Gets the password scrambled.
      /// </summary>
      /// <value>The password scrambled.</value>
      public static string PasswordScrambled
      {
         get
         {
            string pwd = ConfigurationManager.AppSettings["Password scrambled"];
            pwd = pwd ?? "A:yZZ30 e";
            return pwd;
         }
      }

      /// <summary>
      /// Gets the working directory.
      /// </summary>
      /// <value>The working directory.</value>
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

      /// <summary>
      /// Gets the repository path.
      /// </summary>
      /// <value>The repository path.</value>
      public static string RepositoryPath
      {
         get
         {
            string path = ConfigurationManager.AppSettings["Repository Path"];
            path = path ?? "/usr/local/cvsroot/sandbox";
            return path;
         }
      }

      /// <summary>
      /// Gets the name of the module.
      /// </summary>
      /// <value>The name of the module.</value>
      public static string ModuleName
      {
         get
         {
            string mod = ConfigurationManager.AppSettings["Module Name"];
            mod = mod ?? "mymod";
            return mod;
         }
      }

      /// <summary>
      /// Gets the name of the local module directory.
      /// </summary>
      /// <value>The name of the local module directory.</value>
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