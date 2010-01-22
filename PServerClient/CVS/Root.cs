using System.IO;

namespace PServerClient.CVS
{
   /// <summary>
   /// CVS Root object contains information about the CVS repository.
   /// Also contains the local working directory and the CVS module folder,
   /// which is the entry point for the hierarchy of the repository module items
   /// </summary>
   public class Root : IRoot
   {
      private Folder _rootFolder;

      /// <summary>
      /// Initializes a new instance of the Root class
      /// </summary>
      /// <param name="repository">path of the cvs repository on the cvs server</param>
      /// <param name="module">cvs module name</param>
      /// <param name="host">machine name of host machine</param>
      /// <param name="port">port of cvs server on host</param>
      /// <param name="username">cvs username for login</param>
      /// <param name="password">cvs password for login</param>
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

      /// <summary>
      /// Gets or sets the the local file system directory
      /// </summary>
      /// <value></value>
      public DirectoryInfo WorkingDirectory { get; set; }

      /// <summary>
      /// Gets or sets the root folder in the tree
      /// </summary>
      /// <value></value>
      public Folder RootFolder
      {
         get
         {
            if (_rootFolder == null)
            {
               DirectoryInfo di = PServerHelper.GetRootModuleFolderPath(WorkingDirectory, Module);
               Folder rootFolder = new Folder(di, CVSConnectionString, Repository, Module);
               _rootFolder = rootFolder;
            }

            return _rootFolder;
         }

         set
         {
            _rootFolder = value;
         }
      }

      /// <summary>
      /// Gets or sets the username.
      /// </summary>
      /// <value>The username.</value>
      public string Username { get; set; }

      /// <summary>
      /// Gets or sets the password.
      /// The password is set in clear text, but is scrambled before being transmitted
      /// </summary>
      /// <value>The password.</value>
      public string Password { get; set; }

      /// <summary>
      /// Gets the Pserver connection string for Cvs
      /// </summary>
      public string CVSConnectionString
      {
         get
         {
            string connection = string.Format(":{0}:{1}@{2}:{3}{4}", Protocol, Username, Host, Port, Repository);
            return connection;
         }
      }

      /// <summary>
      /// Gets or sets the name of Cvs module being interacted with
      /// </summary>
      /// <value></value>
      public string Module { get; set; }

      /// <summary>
      /// Gets or sets the name of host machine
      /// </summary>
      /// <value></value>
      public string Host { get; set; }

      /// <summary>
      /// Gets or sets the port for Cvs on host machine
      /// </summary>
      /// <value></value>
      public int Port { get; set; }

      /// <summary>
      /// Gets or sets the CVS protocol to use for communicating with the CVS server
      /// </summary>
      /// <value>The CVS protocol.</value>
      public string Protocol { get; set; }

      /// <summary>
      /// Gets or sets the CVS Repository path
      /// </summary>
      /// <value></value>
      public string Repository { get; set; }
   }
}