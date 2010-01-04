using System.IO;

namespace PServerClient.CVS
{
   public interface IRoot
   {
      string Protocol { get; set; }
      string Username { get; set; }
      string Password { get; set; }

      /// <summary>
      /// Name of host machine
      /// </summary>
      string Host { get; set; }

      /// <summary>
      /// CVS Repository 
      /// </summary>
      string Repository { get; set; }

      /// <summary>
      /// Name of Cvs module being interacted with
      /// </summary>
      string Module { get; set; }   
   
      DirectoryInfo WorkingDirectory { get; set; }
      Folder RootFolder { get; set; }

      /// <summary>
      /// Pserver connection string for Cvs
      /// </summary>
      string CVSConnectionString { get; }

      /// <summary>
      /// Port for Cvs on host machine
      /// </summary>
      int Port { get; set; }
   }

   /// <summary>
   /// CVS Root object contains information about the CVS repository.
   /// Also contains the local working directory and the CVS module folder,
   /// which is the entry point for the hierarchy of the repository module items
   /// </summary>
   public class Root : IRoot
   {
      /// <summary>
      /// Root constructor for commands that don't interact with the local file system
      /// </summary>
      /// <param name="host">machine name of host machine</param>
      /// <param name="port">port of cvs server on host</param>
      /// <param name="username">cvs login credentials</param>
      /// <param name="password">cvs login credentials</param>
      /// <param name="repositoryPath"></param>
      public Root(string repository, string module, string host, int port, string username, string password)
      {
         Protocol = "pserver";
         Repository = repository;
         Module = module;
         Host = host;
         Port = port;
         Username = username;
         Password = password.ScramblePassword();
      }

      public DirectoryInfo WorkingDirectory { get; set; }
      public Folder RootFolder { get; set; }
      public string Username { get; set; }
      public string Password { get; set; }

      // cvs repository settings
      /// <summary>
      /// Pserver connection string for Cvs
      /// </summary>
      public string CVSConnectionString 
      {
         get
         {
            string connection = string.Format(":{0}:{1}@{2}:{3}", Protocol, Username, Host, Repository);
            return connection;
         }
      }

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
      public string Protocol { get; set; }

      public string Repository { get; set; }
   }
}