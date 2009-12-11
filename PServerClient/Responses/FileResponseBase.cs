using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   public abstract class FileResponseBase : IFileResponse
   {
      public abstract ResponseType ResponseType { get; }
      public virtual int LineCount { get { return 5; } }
      public virtual void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }

      public string ResponseText { get; set; }
      public ReceiveFile File { get; set; }
   }
}
