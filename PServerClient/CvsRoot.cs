using System;
using PServerClient.LocalFileSystem;

namespace PServerClient
{
   public class CvsRoot
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
      public WorkingDirectory WorkingDirectory { get; set; }
      public string Username { get; set; }
      public string Password { get; set; }

      // cvs repository settings
      public string CvsConnectionString { get; set; }
      public string Root { get; set; }
      public string Module { get; set; }
      public string Host { get; set; }
      public int Port { get; set; }
   }
}