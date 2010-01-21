using PServerClient.Connection;
using PServerClient.CVS;

namespace PServerClient.Commands
{
   /// <summary>
   /// The CVS diff command
   /// </summary>
   public class DiffCommand : CommandBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="DiffCommand"/> class.
      /// </summary>
      /// <param name="root">The root. Does not need a working directory</param>
      /// <param name="connection">The connection.</param>
      public DiffCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
      }

      /// <summary>
      /// Gets the command type. 
      /// </summary>
      /// <value></value>
      public override CommandType Type
      {
         get
         {
            return CommandType.Diff;
         }
      }
   }
}