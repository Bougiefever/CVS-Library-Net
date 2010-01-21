using System.Collections.Generic;
using System.IO;

namespace PServerClient.CVS
{
   /// <summary>
   /// Reads and writes to files on the local file system. 
   /// Performs all the file system actions.
   /// </summary>
   public interface IReaderWriter
   {
      /// <summary>
      /// Reads the file into a byte array.
      /// </summary>
      /// <param name="file">The FileInfo instance containing a reference to the file.</param>
      /// <returns>The file contents in a byte array.</returns>
      byte[] ReadFile(FileInfo file);

      /// <summary>
      /// Reads text file contents and puts each line into a string List
      /// </summary>
      /// <param name="file">The FileInfo instance for the file</param>
      /// <returns>The list of strings containing the file lines</returns>
      IList<string> ReadFileLines(FileInfo file);

      /// <summary>
      /// Writes a file whose contents are in a byte array
      /// </summary>
      /// <param name="file">The FileInfo instance for the file</param>
      /// <param name="buffer">The file contents.</param>
      void WriteFile(FileInfo file, byte[] buffer);

      /// <summary>
      /// Determines if a file or directory exists.
      /// </summary>
      /// <param name="info">FileInfo or DirectoryInfo instance</param>
      /// <returns>true if item exists, false if it does not</returns>
      bool Exists(FileSystemInfo info);

      /// <summary>
      /// Creates the directory if it does not exist.
      /// </summary>
      /// <param name="dir">The DirectoryInfo for the directory</param>
      void CreateDirectory(DirectoryInfo dir);

      /// <summary>
      /// Deletes the file or directory if it exists.
      /// </summary>
      /// <param name="info">FileInfo or DirectoryInfo instance</param>
      void Delete(FileSystemInfo info);

      /// <summary>
      /// Writes each line in the list as a line in a text file
      /// </summary>
      /// <param name="file">The FileInfo instance for the file</param>
      /// <param name="lines">The file contents in a list of strings.</param>
      void WriteFileLines(FileInfo file, IList<string> lines);
   }
}