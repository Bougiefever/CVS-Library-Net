using System;
using PServerClient.CVS;

namespace PServerClient.Commands
{
   public class DiffCommand : CommandBase
   {
      public DiffCommand(Root root) : base(root)
      {
      }

      public override CommandType Type
      {
         get { return CommandType.Diff; }
      }
   }
}