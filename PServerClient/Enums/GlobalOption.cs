namespace PServerClient
{
   /// <summary>
   /// CVS Global options. These are available for every command
   /// </summary>
   public enum GlobalOption
   {
      /// <summary>
      /// -d cvs_root_directory 
      /// Use cvs_root_directory as the root directory pathname of the repository. 
      /// Overrides the setting of the $CVSROOT environment variable. 
      /// </summary>
      CVSRootDir = 0,

      /// <summary>
      /// -l 
      /// Do not log the `cvs_command' in the command history (but execute it anyway). 
      /// </summary>
      DoNotLog = 1,

      /// <summary>
      /// -n 
      /// Do not change any files. Attempt to execute the `cvs_command', but only to issue reports; 
      /// do not remove, update, or merge any existing files, or create any new files. 
      /// Note that CVS will not necessarily produce exactly the same output as without `-n'. 
      /// In some cases the output will be the same, but in other cases CVS will skip some 
      /// of the processing that would have been required to produce the exact same output. 
      /// </summary>
      DoNotChangeFiles = 2,

      /// <summary>
      /// -Q 
      /// Cause the command to be really quiet; the command will only generate output for serious problems. 
      /// </summary>
      ReallyQuite = 3,

      /// <summary>
      /// -q 
      /// Cause the command to be somewhat quiet; informational messages, 
      /// such as reports of recursion through subdirectories, are suppressed. 
      /// </summary>
      Quiet = 4,

      /// <summary>
      /// -s variable=value 
      /// Set a user variable 
      /// </summary>
      SetVariable = 5,

      /// <summary>
      /// -t 
      /// Trace program execution; display messages showing the steps of CVS activity. 
      /// Particularly useful with `-n' to explore the potential impact of an unfamiliar command. 
      /// </summary>
      Trace = 6,

      /// <summary>
      /// -v 
      /// --version 
      /// Display version and copyright information for CVS. 
      /// </summary>
      Version = 7,

      /// <summary>
      /// -x 
      /// Encrypt all communication between the client and the server. 
      /// As of this writing, this is only implemented when using a GSSAPI connection 
      /// or a Kerberos connection. Enabling encryption implies that message traffic 
      /// is also authenticated. Encryption support is not available by default; 
      /// it must be enabled using a special configure option, `--enable-encryption', 
      /// when you build CVS. 
      /// </summary>
      Encrypt = 8,

      /// <summary>
      /// -z gzip-level 
      /// Set the compression level. Only has an effect on the CVS client. 
      /// </summary>
      GZipLevel = 9
   }
}