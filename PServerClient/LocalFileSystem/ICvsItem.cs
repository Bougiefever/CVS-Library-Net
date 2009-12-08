using System;
using System.Collections.Generic;
using System.IO;

namespace PServerClient.LocalFileSystem
{
   /// <summary>
   /// This the interface for both local Cvs folders and Cvs entry files
   /// </summary>
   public interface ICvsItem
   {
      /// <summary>
      /// Either FileInfo or DirectoryInfo object depending on
      /// whether instance is an Entry or Folder object
      /// </summary>
      FileSystemInfo Item { get; set; }
      /// <summary>
      /// List of Entries or sub-Folders for the current item
      /// Entries will return an empty list because they do not have 
      /// child items
      /// </summary>
      IList<ICvsItem> ChildItems { get; set; }
      /// <summary>
      /// for Entry type, Cvs modification time
      /// </summary>
      DateTime ModTime { get; set; }
      /// <summary>
      /// For Entry type, Cvs revision string
      /// </summary>
      string Version { get; set; }
      /// <summary>
      /// For Entry type, the cvs properties string
      /// </summary>
      string Properties { get; set; }
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
      CvsItemType ItemType { get; }
      /// <summary>
      /// Gets an iterator to loop through child items.
      /// Entry types return null
      /// </summary>
      /// <returns></returns>
      IEnumerator<ICvsItem> CreateIterator();
      /// <summary>
      /// Write the FileContents to the Entry file
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
      void AddItem(ICvsItem item);
      /// <summary>
      /// Remove a child item
      /// </summary>
      /// <param name="item"></param>
      void RemoveItem(ICvsItem item);
   }
}