using PServerClient.Connection;
using PServerClient.CVS;

namespace PServerClient.Commands
{
   /// <summary>
   /// Adds files to CVS. Must be followed by a commit command.
   /// </summary>
   public class AddCommand : CommandBase
   {
      public AddCommand(IRoot root, IConnection connection) : base(root, connection)
      {
      }

      public override CommandType Type { get { return CommandType.Add; } }
   }
}