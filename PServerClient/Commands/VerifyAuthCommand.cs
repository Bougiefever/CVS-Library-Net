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

      /// <summary>
      /// Prepares the requests for the command after all the properties
      /// have been set.
      /// </summary>
      public override void Initialize()
      {
         RequiredRequests.Clear();
         RequiredRequests.Add(new VerifyAuthRequest(Root));
      }
   }
}