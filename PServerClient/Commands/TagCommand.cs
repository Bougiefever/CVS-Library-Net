using System;
using PServerClient.CVS;

namespace PServerClient.Commands
{
   public class TagCommand : CommandBase
   {
      public TagCommand(IRoot root) : base(root)
      {

      }

      public override CommandType Type
      {
         get { return CommandType.Tag; }
      }
   }
}