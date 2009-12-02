﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PServerClient.LocalFileSystem;

namespace PServerClient.Responses
{
   public class TemplateResponse : ResponseBase, IFileResponse
   {
      public override ResponseType ResponseType { get { return ResponseType.Template; } }
      public long FileLength { get; set; }
      public Entry CvsEntry { get; set; }

      public override void ProcessResponse(IList<string> lines)
      {
         throw new NotImplementedException();
      }
   }
}