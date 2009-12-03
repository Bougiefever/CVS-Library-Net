using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PServerClient.Requests
{
   public class ModifiedRequest : RequestBase, IFileRequest
   {
      private string _filename;
      private string _mode;
      public ModifiedRequest(string fileName, string mode, long fileLength)
      {
         _filename = fileName;
         _mode = mode;
         FileLength = fileLength;
      }
      public override bool ResponseExpected { get { return false; } }
      public override string GetRequestString()
      {
         string request = string.Format("Modified {0}{1}{2}{1}{3}{1}", _filename, lineEnd, _mode, FileLength.ToString());
         return request;
      }
      public long FileLength { get; set; }
      public byte[] FileContents { get; set; }
   }
}
