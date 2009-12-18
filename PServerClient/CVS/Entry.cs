using System;
using System.Collections.Generic;
using System.IO;
using PServerClient.LocalFileSystem;

namespace PServerClient.CVS
{
   public class Entry : CVSItemBase
   {
      public Entry(FileSystemInfo info) : base(info)
      {
      }

      public override void Read()
      {
         FileContents = ReaderWriter.Current.ReadFile((FileInfo)Item);
      }

      public override IList<ICVSItem> ChildItems 
      { 
         get { throw new NotSupportedException(); }
      }

      public override CVSFolder CvsFolder 
      { 
         get { throw new NotSupportedException(); }
      }

      public override void Write()
      {
         ReaderWriter.Current.WriteFile((FileInfo)Item, FileContents);
      }
   }
}