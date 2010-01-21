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
         RequiredRequests.Clear();
         RequiredRequests.Add(new AuthRequest(root));
         Requests.Add(new VersionRequest());
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

      protected internal override void AfterExecute()
      {
         base.AfterExecute();
         Version = ExitCode == ExitCode.Succeeded ? UserMessages[0] : "Error in command";
      }
   }
}