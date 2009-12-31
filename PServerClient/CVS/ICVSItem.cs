using System;
using System.Collections;
using System.IO;

namespace PServerClient.CVS
{
   /// <summary>
   /// This the interface for both local Cvs folders and Cvs entry files
   /// </summary>
   public interface ICVSItem : IEnumerable
   {
      /// <summary>
      /// Either FileInfo or DirectoryInfo object depending on
      /// whether instance is an Entry or Folder object
      /// </summary>
      FileSystemInfo Info { get; }

      /// <summary>
      /// The parent folder item
      /// </summary>
      Folder Parent { get; }

      /// <summary>
      /// This is the repository string that goes in the CVS repository file
      /// </summary>
      string Repository { get; }

      /// <summary>
      /// List of Entries or Folders contained by the current item
      /// Entry does not support this because files do not have 
      /// child items
      /// </summary>
      ICVSItem this[int idx] { get; }

      /// <summary>
      /// Count of child items
      /// </summary>
      int Count { get; }

      /// <summary>
      /// Name of folder or file
      /// </summary>
      string Name { get; }

      /// <summary>
      /// for Entry type, Cvs modification time
      /// </summary>
      DateTime ModTime { get; set; }

      /// <summary>
      /// For Entry type, Cvs revision string
      /// </summary>
      string Revision { get; set; }

      /// <summary>
      /// For Entry type, the cvs properties string
      /// </summary>
      string Properties { get; set; }

      /// <summary>
      /// For Entry type
      /// </summary>
      string StickyOption { get; set; }

      /// <summary>
      /// For entry type, length in bytes of file contents
      /// </summary>
      long Length { get; set; }

      /// <summary>
      /// byte array of contents that is Length long
      /// </summary>
      byte[] FileContents { get; set; }

      /// <summary>
      /// CVS hidden folder for repository information
      /// </summary>
      CVSFolder CvsFolder { get; }

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
      /// Add a child item
      /// </summary>
      /// <param name="item"></param>
      ///void AddItem(ICVSItem item);

      /// <summary>
      /// Remove a child item
      /// </summary>
      /// <param name="item"></param>
      void RemoveItem(ICVSItem item);
   }
}