using System;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   /// <summary>
   /// The RLog command 
   /// </summary>
   public class RLogCommand : CommandBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="RLogCommand"/> class.
      /// </summary>
      /// <param name="root">The CVS root.</param>
      /// <param name="connection">The connection.</param>
      public RLogCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
      }

      /// <summary>
      /// Gets the command type.
      /// </summary>
      /// <value>The command type.</value>
      public override CommandType Type
      {
         get
         {
            return CommandType.RLog;
         }
      }

      /// <summary>
      /// Gets or sets a value indicating whether to use the -h option
      /// -h 
      /// Print only the name of the RCS file, name of the file in the working directory, 
      /// head, default branch, access list, locks, symbolic names, and suffix. 
      /// </summary>
      /// <value><c>true</c> if [name only option]; otherwise, <c>false</c>.</value>
      public bool NameOnlyOption { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether to use the -l option
      /// </summary>
      /// <value><c>true</c> if [local option]; otherwise, <c>false</c>.</value>
      public bool LocalOption { get; set; }

      /// <summary>
      /// Gets or sets the repository.
      /// </summary>
      /// <value>The cvs repository path.</value>
      public string Repository { get; set; }

      /// <summary>
      /// Gets or sets the file.
      /// </summary>
      /// <value>The file name.</value>
      public string File { get; set; }

      /// <summary>
      /// Prepares the requests for the command after all the properties
      /// have been set.
      /// </summary>
      public override void Initialize()
      {
         Requests.Add(new RootRequest(Root.Repository));
         Requests.Add(new GlobalOptionRequest(GlobalOption.Quiet));
         if (NameOnlyOption)
            Requests.Add(new ArgumentRequest("-h"));
         if (LocalOption)
            Requests.Add(new ArgumentRequest(CommandOption.Local));
         Requests.Add(new ArgumentRequest(GetPathRequestString()));
         Requests.Add(new RLogRequest());
      }

      private string GetPathRequestString()
      {
         string path = Repository ?? Root.Module;
         if (File != null)
            path += "/" + File;
         return path;
      }
   }
}