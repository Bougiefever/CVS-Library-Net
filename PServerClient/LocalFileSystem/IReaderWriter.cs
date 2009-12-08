using System.Collections.Generic;
using System.IO;

namespace PServerClient.LocalFileSystem
{
   public interface IReaderWriter
   {
      //byte[] ReadFile(string filePath);
      byte[] ReadFile(FileInfo file);
      IList<string> ReadFileLines(FileInfo file);
      //void WriteFile(string filePath, byte[] buffer);
      void WriteFile(FileInfo file, byte[] buffer);
      bool Exists(FileSystemInfo info);
      //void CreateDirectory(string dirPath);
      void CreateDirectory(DirectoryInfo dir);
      //void DeleteDirectory(string dirPath);
      //void DeleteDirectory(DirectoryInfo dir);
      //void DeleteFile(string filePath);
      //void DeleteFile(FileInfo file);
      void Delete(FileSystemInfo info);
      void WriteFileLines(FileInfo file, IList<string> lines);
   }
}