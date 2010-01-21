using PServerClient.Connection;
using PServerClient.CVS;

namespace PServerClient.Commands
{
   /// <summary>
   /// CVS import command to add a project to CVS
   /// </summary>
   public class ImportCommand : CommandBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="ImportCommand"/> class.
      /// </summary>
      /// <param name="root">The CVS root.</param>
      /// <param name="connection">The CVS connection.</param>
      public ImportCommand(IRoot root, IConnection connection)
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
            return CommandType.Import;
         }
      }
   }
}