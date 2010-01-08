using System.Collections.Generic;
using System.IO;

namespace PServerClient.LocalFileSystem
{
   public interface IReaderWriter
   {
      byte[] ReadFile(FileInfo file);

      IList<string> ReadFileLines(FileInfo file);

      void WriteFile(FileInfo file, byte[] buffer);

      bool Exists(FileSystemInfo info);

      void CreateDirectory(DirectoryInfo dir);

      void Delete(FileSystemInfo info);

      void WriteFileLines(FileInfo file, IList<string> lines);
   }
}