using PServerClient.Connection;
using PServerClient.CVS;

namespace PServerClient.Commands
{
   /// <summary>
   /// Tags files in CVS
   /// </summary>
   public class TagCommand : CommandBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="TagCommand"/> class.
      /// </summary>
      /// <param name="root">The CVS root.</param>
      /// <param name="connection">The connection.</param>
      public TagCommand(IRoot root, IConnection connection)
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
            return CommandType.Tag;
         }
      }
   }
}