using PServerClient.CVS;

namespace PServerClient.Commands
{
   public class ImportCommand : CommandBase
   {
      public ImportCommand(Root root) : base(root)
      {
      }

      public override CommandType CommandType { get { return CommandType.Import; } }
   }
}