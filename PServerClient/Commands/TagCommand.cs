using System;
using PServerClient.Connection;
using PServerClient.CVS;

namespace PServerClient.Commands
{
   /// <summary>
   /// Issues the tag command
   /// </summary>
   public class TagCommand : CommandBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="TagCommand"/> class.
      /// </summary>
      /// <param name="root">The CVS root.</param>
      /// <param name="connection">The connection.</param>
      /// <param name="tag">The name of the tag to create, move or delete</param>
      public TagCommand(IRoot root, IConnection connection, string tag)
         : base(root, connection)
      {
      }

      /// <summary>
      /// Gets the command type.
      /// </summary>
      /// <value>The command type.</value>
      public override CommandType Type
      {
         get
         {
            return CommandType.Tag;
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