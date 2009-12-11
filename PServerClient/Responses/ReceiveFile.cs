using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Responses
{
   /// <summary>
   /// This contains all information about a file received from the cvs server
   /// </summary>
   public class ReceiveFile
   {
      public string Module { get; set; }
      public string CvsPath { get; set; }
      public string FileName { get; set; }
      public string Revision { get; set; }
      public long FileLength { get; set; }
      public byte[] FileContents { get; set; }
   }
}
