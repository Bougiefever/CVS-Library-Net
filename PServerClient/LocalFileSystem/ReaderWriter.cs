using System;
using System.IO;

namespace PServerClient.LocalFileSystem
{
   public class ReaderWriter : IReaderWriter
   {
      private static IReaderWriter _readerWriter;

      public static IReaderWriter Current
      {
         get
         {
            if (_readerWriter == null)
               _readerWriter = new ReaderWriter();
            return _readerWriter;
         }
         set
         {
            _readerWriter = value;
         }
      }

      public byte[] ReadFile(string filePath)
      {
         FileInfo file = new FileInfo(filePath);
         return ReadFile(file);
      }

      public byte[] ReadFile(FileInfo file)
      {
         if (!file.Exists)
            throw new Exception(string.Format("The specified file does not exist: {0}", file.FullName));
         byte[] buffer;
         using (FileStream stream = file.Open(FileMode.Open))
         {
            buffer = new byte[file.Length];
            stream.Read(buffer, 0, (int)file.Length);
            stream.Close();
         }
         return buffer;
      }

      public void WriteFile(string filePath, byte[] buffer)
      {
         FileInfo file = new FileInfo(filePath);
         WriteFile(file, buffer);

      }

      public void WriteFile(FileInfo file, byte[] buffer)
      {
         using (FileStream stream = file.Open(FileMode.OpenOrCreate))
         {
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
            stream.Close();
         }
      }

      public bool FileExists(string filePath)
      {
         FileInfo file = new FileInfo(filePath);
         return FileExists(file);
      }

      public bool FileExists(FileInfo file)
      {
         return file.Exists;
      }

      public bool DirectoryExists(string dirPath)
      {
         DirectoryInfo dir = new DirectoryInfo(dirPath);
         return DirectoryExists(dir);
      }

      public bool DirectoryExists(DirectoryInfo dir)
      {
         return dir.Exists;
      }

      public void CreateDirectory(string dirPath)
      {
         DirectoryInfo dir = new DirectoryInfo(dirPath);
         CreateDirectory(dir);
      }

      public void CreateDirectory(DirectoryInfo dir)
      {
         if (!dir.Exists)
            dir.Create();
      }

      public void DeleteDirectory(string dirPath)
      {
         DirectoryInfo dir = new DirectoryInfo(dirPath);
         DeleteDirectory(dir);
      }

      public void DeleteDirectory(DirectoryInfo dir)
      {
         if (dir.Exists)
            dir.Delete(true);
      }

      public void DeleteFile(string filePath)
      {
         FileInfo file = new FileInfo(filePath);
         DeleteFile(file);
      }

      public void DeleteFile(FileInfo file)
      {
         if (file.Exists)
            file.Delete();
      }
   }
}
