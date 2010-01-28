using System;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   /// <summary>
   /// Executes the CVS version command
   /// </summary>
   public class VersionCommand : CommandBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="VersionCommand"/> class.
      /// </summary>
      /// <param name="root">The CVS root.</param>
      /// <param name="connection">The CVS connection.</param>
      public VersionCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
      }

      /// <summary>
      /// Gets the command type.
      /// </summary>
      /// <value>The CommandType value</value>
      public override CommandType Type
      {
         get
         {
            return CommandType.Version;
         }
      }

      /// <summary>
      /// Gets the version information that was sent from CVS.
      /// </summary>
      /// <value>The version.</value>
      public string Version
      {
         get; private set;
      }

      /// <summary>
      /// Prepares the requests for the command after all the properties
      /// have been set.
      /// </summary>
      public override void Initialize()
      {
         RequiredRequests.Clear();
         RequiredRequests.Add(new AuthRequest(Root));
         Requests.Add(new VersionRequest());
      }

      /// <summary>
      /// After execute, gets the version from the responses and sets the Version property.
      /// </summary>
      protected internal override void AfterExecute()
      {
         base.AfterExecute();
         Version = ExitCode == ExitCode.Succeeded ? UserMessages[0] : "Error in command";
      }
   }
}