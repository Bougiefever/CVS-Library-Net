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
         RequiredRequests.Clear();
         RequiredRequests.Add(new AuthRequest(root));
         RequiredRequests.Add(new ValidRequestsRequest());
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
   }
}