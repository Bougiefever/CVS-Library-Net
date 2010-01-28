using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   /// <summary>
   /// the CVS log command
   /// </summary>
   public class LogCommand : CommandBase
   {
      /// <summary>
      /// Initializes a new instance of the <see cref="LogCommand"/> class.
      /// </summary>
      /// <param name="root">The CVS root.</param>
      /// <param name="connection">The CVS connection.</param>
      public LogCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
      }

      /// <summary>
      /// Gets or sets a value indicating whether [local only].
      /// </summary>
      /// <value><c>true</c> if [local only]; otherwise, <c>false</c>.</value>
      public bool LocalOnly { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether [default branch].
      /// </summary>
      /// <value><c>true</c> if [default branch]; otherwise, <c>false</c>.</value>
      public bool DefaultBranch { get; set; }

      /// <summary>
      /// Gets or sets a value indicating whether this <see cref="LogCommand"/> is dates.
      /// </summary>
      /// <value><c>true</c> if dates; otherwise, <c>false</c>.</value>
      public bool Dates { get; set; }

      /// <summary>
      /// Gets the command type. 
      /// </summary>
      /// <value>the CommandType value</value>
      public override CommandType Type
      {
         get
         {
            return CommandType.Log;
         }
      }

      /// <summary>
      /// Initializes the log command
      /// </summary>
      public override void Initialize()
      {
         Requests.Add(new AuthRequest(Root));
         Requests.Add(new RootRequest(Root.Repository));
         if (LocalOnly)
            Requests.Add(new ArgumentRequest(CommandOption.Local));
         Requests.Add(new LogRequest());
      }
   }
}