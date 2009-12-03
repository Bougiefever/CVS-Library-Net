using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class GssapiEncryptRequest : NoArgRequestBase
   {
      public override string RequestName { get { return "Gssapi-encrypt"; } }
   }
}
