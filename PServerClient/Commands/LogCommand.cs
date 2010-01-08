using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public class LogCommand : CommandBase
   {
      public LogCommand(IRoot root, IConnection connection)
         : base(root, connection)
      {
         Requests.Add(new AuthRequest(root));
         Requests.Add(new RootRequest(root.Repository));
         if (LocalOnly)
            Requests.Add(new ArgumentRequest("-l"));
         Requests.Add(new LogRequest());
      }

      public bool LocalOnly { get; set; }

      public bool DefaultBranch { get; set; }

      public bool Dates { get; set; }

      public override CommandType Type
      {
         get
         {
            return CommandType.Log;
         }
      }
   }
}