using System;
using System.Configuration;
using System.IO;

namespace PServerClient.IntegrationTests
{
   public static class TestConfig
   {
      public static string CVSHost
      {
         get { return ConfigurationManager.AppSettings["CVS Host"]; }
      }

      public static int CVSPort
      {
         get { return Convert.ToInt32(ConfigurationManager.AppSettings["CVS Port"]); }
      }

      public static string Username
      {
         get { return ConfigurationManager.AppSettings["CVS Username"]; }
      }

      public static string PasswordScrambled
      {
         get { return ConfigurationManager.AppSettings["Password scrambled"]; }
      }

      public static DirectoryInfo WorkingDirectory
      {
         get { return new DirectoryInfo(ConfigurationManager.AppSettings["Working Directory Path"]); }
      }

      public static string RepositoryPath
      {
         get { return ConfigurationManager.AppSettings["Repository Path"]; }
      }

      public static string ModuleName
      {
         get { return ConfigurationManager.AppSettings["Module Name"];}
      }

      public static string LocalModuleDirectoryName
      {
         get { return ConfigurationManager.AppSettings["Local Module Directory Name"];}
      }
   }
}