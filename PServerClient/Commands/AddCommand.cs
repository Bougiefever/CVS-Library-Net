using System;
using PServerClient.CVS;

namespace PServerClient.Commands
{
   public class AddCommand : CommandBase
   {
      public AddCommand(IRoot root) : base(root)
      {
      }

      public override CommandType Type
      {
         get { return CommandType.Add; } }
   }
}