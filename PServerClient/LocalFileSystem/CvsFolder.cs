using System;
using System.IO;

namespace PServerClient.LocalFileSystem
{
   public class CvsFolder
   {
      public CvsFolder(ICvsItem parent)
      {
         // create objects
         ParentFolder = parent;
         Directory = new DirectoryInfo(Path.Combine(parent.Item.FullName, "CVS"));
         RepositoryFile = new FileInfo(Path.Combine(Directory.FullName, "Repository"));
         EntriesFile = new FileInfo(Path.Combine(Directory.FullName, "Entries"));
         RootFile = new FileInfo(Path.Combine(Directory.FullName, "Root"));

         // create file system folder for CVS folder if it doesn't exist
         ReaderWriter.Current.CreateDirectory(Directory);
         //if (!Directory.Exists)
         //   Directory.Create();
      }

      public ICvsItem ParentFolder { get; private set; }
      public DirectoryInfo Directory { get; private set; }
      public FileInfo RepositoryFile { get; private set; }
      public FileInfo EntriesFile { get; private set; }
      public FileInfo RootFile { get; private set; }

      public string GetRootString()
      {
         if (!ReaderWriter.Current.FileExists(RootFile))
            throw new Exception(@"The ""Root"" cvs file does not exist");
         string root;
         byte[] buffer = ReaderWriter.Current.ReadFile(RootFile);
         root = PServerHelper.DecodeString(buffer);
         return root;
      }

      public void WriteRootFile(string root)
      {
            if (root[root.Length] != 10)
               root += (char) 10;
            byte[] buffer = PServerHelper.EncodeString(root);

         ReaderWriter.Current.WriteFile(RootFile, buffer);
      }

      public string GetRepositoryString()
      {
         if (!RepositoryFile.Exists)
            throw new Exception(@"The ""Repository"" cvs file does not exist");
         string repository;
         using (FileStream stream = RepositoryFile.Open(FileMode.Open))
         {
            byte[] buffer = new byte[RepositoryFile.Length];
            stream.Read(buffer, 0, (int)RepositoryFile.Length);
            repository = PServerHelper.DecodeString(buffer);
            stream.Close();
         }
         return repository;
      }

      public void WriteRepositoryFile(string repository)
      {
         using (FileStream stream = RepositoryFile.Open(FileMode.OpenOrCreate))
         {
            if (repository[repository.Length] != 10)
               repository += (char)10;
            byte[] buffer = PServerHelper.EncodeString(repository);
            stream.Write(buffer, 0, buffer.Length);
            stream.Flush();
            stream.Close();
         }
      }
   }
}
