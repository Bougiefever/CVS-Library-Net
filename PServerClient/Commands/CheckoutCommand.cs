﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.Requests;
using System.IO;

namespace PServerClient.Commands
{
   public class CheckoutCommand : CommandBase
   {
      public CheckoutCommand(CvsRoot root) : base(root)
      {
         Requests.Add(new AuthRequest(root));
         Requests.Add(new RootRequest(root));
         Requests.Add(new ArgumentRequest(root.Module));
         Requests.Add(new DirectoryRequest(root));
         Requests.Add(new CheckOutRequest());
      }
   }
}