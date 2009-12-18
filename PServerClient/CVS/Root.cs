using System.IO;

namespace PServerClient.CVS
{
   /// <summary>
   /// CVSRoot object contains information about the CVS repository.
   /// Also contains the local working directory and the CVS module folder,
   /// which is the entry point for the hierarchy of the repository module items
   /// </summary>
   public class Root
   {
      private Folder _moduleFolder;
      public Root(string host, int port, string username, string password, string repositoryPath)
      {
         RepositoryPath = repositoryPath;
         CVSConnectionString = string.Format(":pserver:{0}@{1}:{2}", username, host, repositoryPath);
         Host = host;
         Port = port;
         Username = username;
         Password = password.ScramblePassword();
      }

      public DirectoryInfo WorkingDirectory { get; set; }
      public Folder ModuleFolder
      {
         get
         {
            if (_moduleFolder == null)
            {
               _moduleFolder = PServerHelper.CreateModuleFolderStructure(WorkingDirectory, CVSConnectionString, Module);
            }
            return _moduleFolder;
         }
      }
      public string Username { get; set; }
      public string Password { get; set; }

      // cvs repository settings
      /// <summary>
      /// Pserver connection string for Cvs
      /// </summary>
      public string CVSConnectionString { get; set; }
      /// <summary>
      /// Cvs root folder on unix machine
      /// </summary>
      public string RepositoryPath { get; set; }
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