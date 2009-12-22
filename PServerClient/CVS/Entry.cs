using System.IO;
using PServerClient.LocalFileSystem;

namespace PServerClient.CVS
{
   /// <summary>
   /// Represents a cvs file in the repository
   /// </summary>
   public class Entry : CVSItemBase
   {
      public Entry(FileSystemInfo info) : base(info)
      {
      }

      public override void Read()
      {
         FileContents = ReaderWriter.Current.ReadFile((FileInfo) Info);
      }

      public override void Write()
      {
         ReaderWriter.Current.WriteFile((FileInfo) Info, FileContents);
      }
   }
}