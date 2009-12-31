using System;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Commands
{
   public class ExportCommand : CommandBase
   {
      public ExportCommand(Root root, DateTime exportDate) : base(root)
      {
         Requests.Add(new RootRequest(root));
         //Requests.Add(new GlobalOptionRequest("-q")); // somewhat quiet
         string dateArg = GetExportDate(exportDate);
         Requests.Add(new ArgumentRequest(dateArg));
         Requests.Add(new ArgumentRequest("-R"));
         Requests.Add(new ArgumentRequest(root.Module));
         Requests.Add(new DirectoryRequest(root));

         Requests.Add(new ExportRequest());
      }

      public ExportCommand(Root root, string tag) : base(root)
      {
         Requests.Add(new RootRequest(root));
         //Requests.Add(new GlobalOptionRequest("-q")); // somewhat quiet
         string tagArg = "-r " + tag;
         Requests.Add(new ArgumentRequest(tagArg));
         Requests.Add(new ArgumentRequest("-R"));
         Requests.Add(new ArgumentRequest(root.Module));
         Requests.Add(new DirectoryRequest(root));

         Requests.Add(new ExportRequest());
      }

      public override CommandType Type
      {
         get { return CommandType.Export; }
      }

      internal string GetExportDate(DateTime exportDate)
      {
         string mydate = exportDate.ToRfc822();
         return string.Format("-D {0}", mydate);
      }
   }
}