using System;
using System.IO;
using System.Collections.Generic;

namespace PServerClient.LocalFileSystem
{
   interface IFileFolder
   {
      DirectoryInfo CVSFolder { get; }
      IList<IEntry> Entries { get; set; }
      FileInfo EntriesFile { get; }
      string RelativePath { get; set; }
      FileInfo RepositoryFile { get; }
      FileInfo RootFile { get; }
   }
}
