using System;
using PServerClient.CVS;

namespace PServerClient.Commands
{
   public class ExportCommand : CommandBase
   {
      public ExportCommand(Root root) : base(root)
      {
      }

      public override CommandType Type
      {
         get { return CommandType.Export; }
      }
   }
}