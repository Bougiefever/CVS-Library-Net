using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   /// <summary>
   /// Tags files in CVS
   /// </summary>
   public class TagCommand : CommandBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="TagCommand"/> class.
      /// </summary>
      /// <param name="root">The CVS root.</param>
      /// <param name="connection">The connection.</param>
      /// <param name="tag">The tag string</param>
      public TagCommand(IRoot root, IConnection connection, string tag)
         : base(root, connection)
      {
         Requests.Add(new RootRequest(root.Repository));
         Requests.Add(new GlobalOptionRequest("-q")); // somewhat quiet
         Requests.Add(new ArgumentRequest(tag));
         Requests.Add(new DirectoryRequest(".", root.Repository + "/" + root.Module));
         Requests.Add(new TagRequest());
      }

      /// <summary>
      /// Gets the command type. 
      /// </summary>
      /// <value></value>
      public override CommandType Type
      {
         get
         {
            return CommandType.Tag;
         }
      }
   }
}