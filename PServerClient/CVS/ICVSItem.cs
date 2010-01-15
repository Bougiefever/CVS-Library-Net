using System.IO;

namespace PServerClient.CVS
{
   /// <summary>
   /// This the interface for both folders and entry files
   /// </summary>
   public interface ICVSItem
   {
      /// <summary>
      /// Gets either FileInfo or DirectoryInfo object depending on
      /// whether instance is an Entry or Folder object
      /// </summary>
      FileSystemInfo Info { get; }

      /// <summary>
      /// Gets the parent folder item
      /// </summary>
      Folder Parent { get; }

      /// <summary>
      /// Gets the CVS hidden folder for repository information
      /// </summary>
      CVSFolder CVSFolder { get; }

      /// <summary>
      /// Write the FileContents to the Entry file if item is Entry type
      /// Create a directory if item is Folder type
      /// </summary>
      void Write();

      /// <summary>
      /// read contents of the Entry file into FileContents
      /// </summary>
      void Read();

      /// <summary>
      /// Save to disk
      /// </summary>
      void Save();

      /// <summary>
      /// Save to disk
      /// </summary>
      /// <param name="recursive">Determines whether or not to save all children</param>
      void Save(bool recursive);
   }
}