using System;
using System.Collections.Generic;
using System.IO;

namespace PServerClient.CVS
{
   /// <summary>
   /// This the interface for both local Cvs folders and Cvs entry files
   /// </summary>
   public interface ICVSItem
   {
      /// <summary>
      /// Either FileInfo or DirectoryInfo object depending on
      /// whether instance is an Entry or Folder object
      /// </summary>
      FileSystemInfo Item { get; }
      /// <summary>
      /// List of Entries or Folders contained by the current item
      /// Entry does not support this because files do not have 
      /// child items
      /// </summary>
      IList<ICVSItem> ChildItems { get;  }
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
      /// Looks at the Item property of itself and returns either Folder or Entry type
      /// </summary>
      ItemType ItemType { get; }
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
      void AddItem(ICVSItem item);
      /// <summary>
      /// Remove a child item
      /// </summary>
      /// <param name="item"></param>
      void RemoveItem(ICVSItem item);

      CVSFolder CvsFolder { get; }
   }
}