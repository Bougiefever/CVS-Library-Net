using System;
using System.IO;
using System.Collections.Generic;
using PServerClient.Responses;
using PServerClient.CVS;

namespace PServerClient.Commands
{
   public class ResponseProcessor
   {
      public IList<IFileResponseGroup> CreateFileGroupsFromResponses(IList<IResponse> responses)
      {
         IList<IFileResponseGroup> files = new List<IFileResponseGroup>();
         IFileResponseGroup file = null;
         foreach (IResponse response in responses)
         {
            if (response.Type == ResponseType.ModTime)
            {
               file = new FileResponseGroup {ModTime = (ModTimeResponse) response};
            }
            if (response is IMessageResponse)
            {
               if (file != null) file.MT = (IMessageResponse) response;
            }
            if (response is IFileResponse)
            {
               if (file != null)
               {
                  file.FileResponse = (IFileResponse) response;
                  files.Add(file);
               }
            }
         }
         return files;
      }

      public Folder CreateCVSFileStructure(IRoot root, IList<IFileResponseGroup> fileGroups)
      {
         DirectoryInfo di = PServerHelper.GetRootModuleFolderPath(root.WorkingDirectory, root.Module);
         Folder rootFolder = new Folder(di, root.CVSConnectionString, root.Repository, root.Module);
         Folder parent = rootFolder;
         foreach (IFileResponseGroup fileGroup in fileGroups)
         {
            IFileResponse response = fileGroup.FileResponse;
            string module = ResponseHelper.FixResponseModuleSlashes(response.Module);
            if (module != parent.Module) // add file to current folder
            {
               string name = ResponseHelper.GetLastModuleName(response.Module);
               Folder folder = new Folder(name, parent);
               parent = folder;
            }
            Entry entry = new Entry(fileGroup.FileResponse.Name, parent);
            entry.Length = response.Length;
            entry.FileContents = response.Contents;
         }
         return rootFolder;
      }

      public Entry AddFile(Folder rootFolder, IFileResponseGroup file)
      {
         string module = file.FileResponse.Module;
         module = ResponseHelper.FixResponseModuleSlashes(module);
         Folder parent = GetModuleFolder(rootFolder, module);
         if (parent == null)
         {
            parent = AddFolderToStructure(rootFolder, module);
         }
         Entry entry = new Entry(file.FileResponse.Name, parent);
         entry.Length = file.FileResponse.Length;
         entry.FileContents = file.FileResponse.Contents;
         entry.EntryLine = file.FileResponse.EntryLine;
         return entry;
      }

      public Folder AddFolderToStructure(Folder rootFolder, string module)
      {
         string[] modules = module.Split(new [] {"/"}, StringSplitOptions.RemoveEmptyEntries);
         string mod = string.Empty;
         Folder parent = rootFolder;
         Folder folder = null;
         for (int i = 0; i < modules.Length;i++ )
         {
            mod += "/" + modules[i];
            mod = ResponseHelper.FixResponseModuleSlashes(mod);
            folder = GetModuleFolder(rootFolder, mod);
            if (folder == null)
            {
               string name = ResponseHelper.GetLastModuleName(mod);
               folder = new Folder(name, parent);
               parent = folder;
            }
         }
         return folder;
      }

      public Folder GetModuleFolder(Folder rootFolder, string module)
      {
         Folder folder = rootFolder;
         Folder returnFolder = null;
         if (module == rootFolder.Module)
            returnFolder = folder;
         IList<Folder> subFolders = folder.GetSubFolders();
         foreach (Folder subFolder in subFolders)
         {
            if (returnFolder == null)
               returnFolder = GetModuleFolder(subFolder, module);
         }
         return returnFolder;
      }
   }
}