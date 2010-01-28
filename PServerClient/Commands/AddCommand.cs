using System;
using PServerClient.Connection;
using PServerClient.CVS;

namespace PServerClient.Commands
{
   /// <summary>
   /// Adds files to CVS. Must be followed by a commit command.
   /// </summary>
   public class AddCommand : CommandBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="AddCommand"/> class.
      /// </summary>
      /// <param name="root">The Root instance. Must have a working directory</param>
      /// <param name="connection">The connection.</param>
      public AddCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
      }

      /// <summary>
      /// Gets the command type. 
      /// </summary>
      /// <value></value>
      public override CommandType Type
      {
         get
         {
            return CommandType.Add;
         }
      }

      /// <summary>
      /// Prepares the requests for the command after all the properties
      /// have been set.
      /// </summary>
      public override void Initialize()
      {
         throw new NotImplementedException();
      }
   }
}