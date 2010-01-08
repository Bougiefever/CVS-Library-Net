using PServerClient.Connection;
using PServerClient.CVS;

namespace PServerClient.Commands
{
   public class ImportCommand : CommandBase
   {
      public ImportCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
      }

      public override CommandType Type
      {
         get
         {
            return CommandType.Import;
         }
      }
   }
}