using System.Collections.Generic;
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

      public byte[] ReadFile(FileInfo file)
      {
         if (!file.Exists)
            throw new IOException(string.Format("The specified file does not exist: {0}", file.FullName));
         byte[] buffer;
         using (FileStream stream = file.Open(FileMode.Open))
         {
            buffer = new byte[file.Length];
            stream.Read(buffer, 0, (int) file.Length);
            stream.Close();
         }

         return buffer;
      }

      /// <summary>
      /// Reads text file contents and puts each line into a string List
      /// </summary>
      /// <param name="file">The FileInfo instance for the file</param>
      /// <returns>The list of strings containing the file lines</returns>
      public IList<string> ReadFileLines(FileInfo file)
      {
         if (!file.Exists)
            throw new IOException(string.Format("The specified file does not exist: {0}", file.FullName));
         TextReader reader = file.OpenText();
         IList<string> lines = new List<string>();
         string line;
         while ((line = reader.ReadLine()) != null)
         {
            lines.Add(line);
         }

         reader.Close();
         return lines;
      }

      public void WriteFileLines(FileInfo file, IList<string> lines)
      {
         using (TextWriter tw = file.CreateText())
         {
            foreach (string s in lines)
            {
               tw.WriteLine(s);
            }

            tw.Flush();
            tw.Close();
         }
      }

      public void WriteFile(FileInfo file, byte[] buffer)
      {
         if (file.Exists)
            file.Delete();

         using (FileStream stream = file.Open(FileMode.OpenOrCreate, FileAccess.Write))
         {
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
            stream.Close();
         }

         file.Refresh();
      }

      public bool Exists(FileSystemInfo info)
      {
         return info.Exists;
      }

      public void CreateDirectory(DirectoryInfo dir)
      {
         if (!dir.Exists)
            dir.Create();
         dir.Refresh();
      }

      public void Delete(FileSystemInfo info)
      {
         if (info.Exists)
            info.Delete();
         info.Refresh();
      }
   }
}