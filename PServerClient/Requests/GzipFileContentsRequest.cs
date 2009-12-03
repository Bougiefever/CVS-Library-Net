using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class GzipFileContentsRequest : OneArgRequestBase
   {
      public GzipFileContentsRequest(string level) : base(level) { }
      public override string RequestName { get { return "gzip-file-contents"; } }
   }
}
