using PServerClient.CVS;

namespace PServerClient.Commands
{
   public class ImportCommand : CommandBase
   {
      public ImportCommand(IRoot root) : base(root)
      {
      }

      public override CommandType Type { get { return CommandType.Import; } }
   }
}