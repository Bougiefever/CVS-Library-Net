using System.Collections.Generic;
using System.IO;

namespace PServerClient.LocalFileSystem
{
   public class FileFolder : PServerClient.LocalFileSystem.IFileFolder
   {
      private const string CVSFolderName = "CVS";
      private CvsRoot _cvsRoot;
      DirectoryInfo _localFolder;
      DirectoryInfo _cvsFolder;
      private FileInfo _repositoryFile;
      private FileInfo _entriesFile;
      private FileInfo _root;

      public FileFolder(CvsRoot cvsRoot, string relativePath)
      {
         _cvsRoot = cvsRoot;
         RelativePath = relativePath;
         string path = Path.Combine(_cvsRoot.WorkingDirectory, relativePath);
         _localFolder = new DirectoryInfo(path);
      }

      public string RelativePath { get; set; }
      public IList<IEntry> Entries { get; set; }

      public DirectoryInfo CVSFolder
      {
         get
         {
            if (_cvsFolder == null)
            {
               string path = Path.Combine(_localFolder.FullName, CVSFolderName);
               _cvsFolder = new DirectoryInfo(path);
            }
            return _cvsFolder;
         }
      }
      public FileInfo RepositoryFile
      {
         get
         {
            if (_repositoryFile == null)
            {
               string path = Path.Combine(CVSFolder.FullName, "Repository");
               FileInfo fi = new FileInfo(path);
               _repositoryFile = fi;
            }
            return _repositoryFile;
         }
      }
      public FileInfo EntriesFile
      {
         get
         {
            if (_entriesFile == null)
            {
               string path = Path.Combine(CVSFolder.FullName, "Entries");
               FileInfo fi = new FileInfo(path);
               _entriesFile = fi;
            }
            return _entriesFile;
         }
      }

      public FileInfo RootFile
      {
         get
         {
            if (_root == null)
            {
               string path = Path.Combine(CVSFolder.FullName, "Root");
               FileInfo fi = new FileInfo(path);
               _root = fi;
            }
            return _root;
         }
      }
   }
}
