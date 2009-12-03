using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class GzipStreamRequest : OneArgRequestBase
   {
      public GzipStreamRequest(string level) : base(level) { }
      public override string RequestName { get { return "Gzip-stream"; } }
   }
}
