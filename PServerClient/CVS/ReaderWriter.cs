using System.Collections.Generic;
using System.IO;

namespace PServerClient.CVS
{
   /// <summary>
   /// Reads and writes to files on the local file system.
   /// Performs all the file system actions.
   /// </summary>
   public class ReaderWriter : IReaderWriter
   {
      private static IReaderWriter _readerWriter;

      /// <summary>
      /// Gets or sets the current static instance of the ReaderWriter class.
      /// Singleton design pattern is used.
      /// </summary>
      /// <value>The current.</value>
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

      /// <summary>
      /// Reads the file into a byte array.
      /// </summary>
      /// <param name="file">The FileInfo instance containing a reference to the file.</param>
      /// <returns>The file contents in a byte array.</returns>
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
      /// <returns>
      /// The list of strings containing the file lines
      /// </returns>
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

      /// <summary>
      /// Writes each line in the list as a line in a text file
      /// </summary>
      /// <param name="file">The FileInfo instance for the file</param>
      /// <param name="lines">The file contents in a list of strings.</param>
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

      /// <summary>
      /// Writes a file whose contents are in a byte array
      /// </summary>
      /// <param name="file">The FileInfo instance for the file</param>
      /// <param name="buffer">The file contents.</param>
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

      /// <summary>
      /// Determines if a file or directory exists.
      /// </summary>
      /// <param name="info">FileInfo or DirectoryInfo instance</param>
      /// <returns>
      /// true if item exists, false if it does not
      /// </returns>
      public bool Exists(FileSystemInfo info)
      {
         return info.Exists;
      }

      /// <summary>
      /// Creates the directory if it does not exist.
      /// </summary>
      /// <param name="dir">The DirectoryInfo for the directory</param>
      public void CreateDirectory(DirectoryInfo dir)
      {
         if (!dir.Exists)
            dir.Create();
         dir.Refresh();
      }

      /// <summary>
      /// Deletes the file or directory if it exists.
      /// </summary>
      /// <param name="info">FileInfo or DirectoryInfo instance</param>
      public void Delete(FileSystemInfo info)
      {
         if (info.Exists)
            info.Delete();
         info.Refresh();
      }
   }
}