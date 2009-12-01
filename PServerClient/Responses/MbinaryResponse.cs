using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.LocalFileSystem;

namespace PServerClient.Responses
{
   public class MbinaryResponse : ResponseBase, IFileResponse
   {
      public long FileLength { get; set; }
      public Entry CvsEntry { get; set; }

      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }
   }
}
