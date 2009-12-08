using System.IO;

namespace PServerClient.LocalFileSystem
{
   public interface IReaderWriter
   {
      byte[] ReadFile(string filePath);
      byte[] ReadFile(FileInfo file);
      void WriteFile(string filePath, byte[] buffer);
      void WriteFile(FileInfo file, byte[] buffer);
      bool FileExists(string filePath);
      bool FileExists(FileInfo file);
      bool DirectoryExists(string dirPath);
      bool DirectoryExists(DirectoryInfo dir);
      void CreateDirectory(string dirPath);
      void CreateDirectory(DirectoryInfo dir);
      void DeleteDirectory(string dirPath);
      void DeleteDirectory(DirectoryInfo dir);
      void DeleteFile(string filePath);
      void DeleteFile(FileInfo file);
   }
}