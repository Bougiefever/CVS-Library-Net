using System.IO;

namespace PServerClient.CVS
{
   /// <summary>
   /// The interface for the CVS connection information
   /// </summary>
   public interface IRoot
   {
      /// <summary>
      /// Gets or sets the CVS protocol to use for communicating with the CVS server
      /// </summary>
      /// <value>The CVS protocol.</value>
      string Protocol { get; set; }

      /// <summary>
      /// Gets or sets the username.
      /// </summary>
      /// <value>The username.</value>
      string Username { get; set; }

      /// <summary>
      /// Gets or sets the password.
      /// The password is set in clear text, but is scrambled before being transmitted 
      /// </summary>
      /// <value>The password.</value>
      string Password { get; set; }

      /// <summary>
      /// Gets or sets the name of host machine
      /// </summary>
      string Host { get; set; }

      /// <summary>
      /// Gets or sets the CVS Repository path
      /// </summary>
      string Repository { get; set; }

      /// <summary>
      /// Gets or sets the name of Cvs module being interacted with
      /// </summary>
      string Module { get; set; }

      /// <summary>
      /// Gets or sets the the local file system directory 
      /// </summary>
      DirectoryInfo WorkingDirectory { get; set; }

      /// <summary>
      /// Gets or sets the root folder in the tree
      /// </summary>
      Folder RootFolder { get; set; }

      /// <summary>
      /// Gets the read-only Pserver connection string for Cvs
      /// </summary>
      string CVSConnectionString { get; }

      /// <summary>
      /// Gets or sets the port for Cvs on host machine
      /// </summary>
      int Port { get; set; }
   }
}