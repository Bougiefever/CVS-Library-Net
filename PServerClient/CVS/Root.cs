using PServerClient.CVS;

namespace PServerClient.CVS
{
   public class Root
   {
      public Root(string host, int port, string username, string password, string cvsroot)
      {
         CVSRoot = cvsroot;
         CvsConnectionString = string.Format(":pserver:{0}@{1}:{2}", username, host, cvsroot);
         Host = host;
         Port = port;
         Username = username;
         Password = password.ScramblePassword();
      }

      // for the current user and machine
      /// <summary>
      /// This is the root folder for the current cvs module
      /// </summary>
      public ICVSItem WorkingDirectory { get; set; }
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
      public string CVSRoot { get; set; }
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