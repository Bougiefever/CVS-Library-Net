using PServerClient.Connection;
using PServerClient.CVS;

namespace PServerClient.Commands
{
   public class TagCommand : CommandBase
   {
      public TagCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
      }

      public override CommandType Type
      {
         get
         {
            return CommandType.Tag;
         }
      }
   }
}