﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class WrapperSendmeRcsOptionsRequest : NoArgRequestBase
   {
      public override bool ResponseExpected { get { return true; } }
      public override string RequestName { get { return "wrapper-sendme-rcsOptions"; } }
   }
}