using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   /// <summary>
   /// Verifies that the authentication information is valid
   /// </summary>
   public class VerifyAuthCommand : CommandBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="VerifyAuthCommand"/> class.
      /// </summary>
      /// <param name="root">The CVS root.</param>
      /// <param name="connection">The connection.</param>
      public VerifyAuthCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
         RequiredRequests.Clear();
         RequiredRequests.Add(new VerifyAuthRequest(root));
      }

      /// <summary>
      /// Gets the command type. 
      /// </summary>
      /// <value></value>
      public override CommandType Type
      {
         get
         {
            return CommandType.VerifyAuth;
         }
      }
   }
}