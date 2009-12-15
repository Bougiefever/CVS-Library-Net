using System;
using PServerClient.LocalFileSystem;

namespace PServerClient
{
   public  class CvsRoot
   {
      public CvsRoot(string host, int port, string username, string password, string cvsroot)
      {
         Root = cvsroot;
         CvsConnectionString = string.Format(":pserver:{0}@{1}:{2}", username, host, cvsroot);
         Host = host;
         Port = port;
         Username = username;
         Password = password.ScramblePassword();
      }

      // for the current user and machine
      /// <summary>
      /// This is the root folder for the cvs module that is being used
      /// </summary>
      public ICvsItem WorkingDirectory { get; set; }
      public string Username { get; set; }
      public string Password { get; set; }

      // cvs repository settings
      /// <summary>
      /// Pserver connection string for Cvs
      /// </summary>
      public string CvsConnectionString { get; set; }
      /// <summary>
      /// Cvs root folder on unix machine
      /// </summary>
      public string Root { get; set; }
      /// <summary>
      /// Name of Cvs module being interacted with
      /// </summary>
      public string Module { get; set; }
      /// <summary>
      /// Name of host machine
      /// </summary>
      public string Host { get; set; }
      /// <summary>
      /// Port for Cvs on host machine
      /// </summary>
      public int Port { get; set; }
   }
}