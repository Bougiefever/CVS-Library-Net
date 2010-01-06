using System;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public class DiffCommand : CommandBase
   {
      public DiffCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
      }

      public override CommandType Type
      {
         get { return CommandType.Diff; }
      }


   }
}