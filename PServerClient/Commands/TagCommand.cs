using System;
using PServerClient.CVS;

namespace PServerClient.Commands
{
   public class TagCommand : CommandBase
   {
      public TagCommand(Root root) : base(root)
      {

      }

      public override CommandType Type
      {
         get { return CommandType.Tag; }
      }
   }
}