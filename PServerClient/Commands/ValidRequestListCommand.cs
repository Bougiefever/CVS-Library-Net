using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   /// <summary>
   /// Gets the list of valid requests from CVS
   /// </summary>
   public class ValidRequestsListCommand : CommandBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="ValidRequestsListCommand"/> class.
      /// </summary>
      /// <param name="root">The CVS root.</param>
      /// <param name="connection">The connection.</param>
      public ValidRequestsListCommand(IRoot root, IConnection connection)
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
            return CommandType.ValidRequestsList;
         }
      }

      /// <summary>
      /// Prepares the requests for the command after all the properties
      /// have been set.
      /// </summary>
      public override void Initialize()
      {
         RequiredRequests.Clear();
         RequiredRequests.Add(new AuthRequest(Root));
         RequiredRequests.Add(new ValidRequestsRequest());
      }
   }
}