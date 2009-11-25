using PServerClient.Requests;

namespace PServerClient.Commands
{
   public class LogCommand : CommandBase
   {
      public bool LocalOnly { get; set; }
      public bool DefaultBranch { get; set; }
      public bool Dates { get; set; }
      public LogCommand(CvsRoot root) : base(root)
      {
         Requests.Add(new AuthRequest(root));
         Requests.Add(new RootRequest(root));
         if (LocalOnly)
            Requests.Add(new ArgumentRequest("-l"));
         Requests.Add(new LogRequest());
      }
   }
}
