using System;

namespace PServerClient
{
   public class CvsRoot
   {
      public CvsRoot(string host, int port, string username, string password, string cvsroot, string localPath)
      {
         CvsRootPath = cvsroot;
         Root = string.Format(":pserver:{0}@{1}:{2}", username, host, cvsroot);
         Host = host;
         Port = port;
         Username = username;
         Password = password.ScramblePassword();
         LocalPath = localPath;
      }

      public string LocalPath { get; set; }
      public string Username { get; set; }
      public string Root { get; set; }
      public string CvsRootPath { get; set; }
      public string Host { get; set; }
      public int Port { get; set; }
      public string Password { get; set; }
   }
}