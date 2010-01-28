using System;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   /// <summary>
   /// The RLog command 
   /// </summary>
   public class RLogCommand : CommandBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="RLogCommand"/> class.
      /// </summary>
      /// <param name="root">The CVS root.</param>
      /// <param name="connection">The connection.</param>
      public RLogCommand(IRoot root, IConnection connection)
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
            return CommandType.RLog;
         }
      }

      /// <summary>
      /// Prepares the requests for the command after all the properties
      /// have been set.
      /// </summary>
      public override void Initialize()
      {
         Requests.Add(new RootRequest(Root.Repository));
         Requests.Add(new ArgumentRequest("-q"));
         Requests.Add(new ArgumentRequest("-Q"));

      }
   }
}