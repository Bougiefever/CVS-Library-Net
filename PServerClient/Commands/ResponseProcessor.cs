using System;
using System.Collections.Generic;
using System.Linq;
using PServerClient.CVS;

namespace PServerClient.Commands
{
   /// <summary>
   /// Processes responses from CVS commands
   /// </summary>
   public class ResponseProcessor
   {
      ////public IList<IFileResponseGroup> CreateFileGroupsFromResponses(IList<IResponse> responses)
      ////{
      ////   IList<IFileResponseGroup> files = new List<IFileResponseGroup>();
      ////   IFileResponseGroup file = null;
      ////   foreach (IResponse response in responses)
      ////   {
      ////      if (response.Type == ResponseType.ModTime)
      ////      {
      ////         file = new FileResponseGroup { ModTime = (ModTimeResponse)response };
      ////      }

      ////      if (response is IMessageResponse)
      ////      {
      ////         if (file != null) file.MT = (IMessageResponse)response;
      ////      }

      ////      if (response is IFileResponse)
      ////      {
      ////         if (file != null)
      ////         {
      ////            file.FileResponse = (IFileResponse)response;
      ////            files.Add(file);
      ////         }
      ////      }
      ////   }

      ////   return files;
      ////}

      ///// <summary>
      ///// Creates the CVS file structure.
      ///// </summary>
      ///// <param name="root">The root.</param>
      ///// <param name="fileGroups">The file groups.</param>
      ///// <returns></returns>
      ////public Folder CreateCVSFileStructure(IRoot root, IList<IFileResponseGroup> fileGroups)
      ////{
      ////   DirectoryInfo di = PServerHelper.GetRootModuleFolderPath(root.WorkingDirectory, root.Module);
      ////   Folder rootFolder = new Folder(di, root.CVSConnectionString, root.Repository, root.Module);
      ////   Folder parent = rootFolder;
      ////   foreach (IFileResponseGroup fileGroup in fileGroups)
      ////   {
      ////      IFileResponse response = fileGroup.FileResponse;
      ////      string module = ResponseHelper.FixResponseModuleSlashes(response.Module);

      ////      // add file to current folder
      ////      if (module != parent.Module)
      ////      {
      ////         string name = ResponseHelper.GetLastModuleName(response.Module);
      ////         Folder folder = new Folder(name, parent);
      ////         parent = folder;
      ////      }

      ////      Entry entry = new Entry(fileGroup.FileResponse.Name, parent);
      ////      entry.Length = response.Length;
      ////      entry.FileContents = response.Contents;
      ////   }

      ////   return rootFolder;
      ////}

      /// <summary>
      /// Adds the file to the Folder tree and returns the folder the entry was added to.
      /// </summary>
      /// <param name="startingFolder">Either the root folder or the last folder an entry was added to</param>
      /// <param name="file">Group of responses needed for one file</param>
      /// <returns>The folder the entry was added to</returns>
      public Entry AddFile(Folder startingFolder, IFileResponseGroup file)
      {
         string module = file.FileResponse.Module;
         Folder parent = GetModuleFolder(startingFolder, module);

         Entry entry = new Entry(file.FileResponse.Name, parent);
         entry.Length = file.FileResponse.Length;
         entry.FileContents = file.FileResponse.Contents;
         entry.EntryLine = file.FileResponse.EntryLine;
         return entry;
      }

      /// <summary>
      /// Gets the module folder.
      /// </summary>
      /// <param name="startingFolder">The starting folder.</param>
      /// <param name="module">The CVS module.</param>
      /// <returns>Folder for the module</returns>
      public Folder GetModuleFolder(Folder startingFolder, string module)
      {
         module = ResponseHelper.FixResponseModuleSlashes(module);
         Folder folder;

         folder = FindModuleFolder(startingFolder, module);
         if (folder == null)
            folder = AddFolderToStructure(startingFolder.GetRootFolder(), module);

         return folder;
      }

      /// <summary>
      /// Adds the folder for the module to the Folder hierarchy in the Root object
      /// </summary>
      /// <param name="rootFolder">The root folder.</param>
      /// <param name="module">The module.</param>
      /// <returns>The new Folder that was created</returns>
      public Folder AddFolderToStructure(Folder rootFolder, string module)
      {
         string[] modules = module.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
         string mod = string.Empty;
         Folder parent = rootFolder;
         Folder folder = null;
         for (int i = 0; i < modules.Length; i++)
         {
            mod += "/" + modules[i];
            mod = ResponseHelper.FixResponseModuleSlashes(mod);
            folder = FindModuleFolder(rootFolder, mod);
            if (folder == null)
            {
               string name = ResponseHelper.GetLastModuleName(mod);
               folder = new Folder(name, parent);
            }

            parent = folder;
         }

         return folder;
      }

      /// <summary>
      /// Finds the module Folder in sub folders.
      /// </summary>
      /// <param name="startingFolder">The starting folder.</param>
      /// <param name="module">The module.</param>
      /// <returns>The Folder if it exists, else null</returns>
      public Folder FindModuleInSubFolders(Folder startingFolder, string module)
      {
         Folder returnFolder = null;
         if (module == startingFolder.Module)
            returnFolder = startingFolder;
         if (returnFolder == null)
         {
            IList<Folder> subFolders = startingFolder.GetSubFolders().ToList();
            foreach (Folder folder in subFolders)
            {
               if (returnFolder == null)
                  returnFolder = FindModuleInSubFolders(folder, module);
            }
         }

         // return the folder that was found or null if it was not found
         return returnFolder;
      }

      /// <summary>
      /// Finds the module folder.
      /// </summary>
      /// <param name="startingFolder">The starting folder.</param>
      /// <param name="module">The module.</param>
      /// <returns>The Folder if it exists, else null</returns>
      public Folder FindModuleFolder(Folder startingFolder, string module)
      {
         Folder returnFolder = FindModuleInSubFolders(startingFolder, module);

         // if the folder was not found, start from the root and find again
         if (returnFolder == null)
         {
            Folder rootFolder = startingFolder.GetRootFolder();
            if (!startingFolder.Equals(rootFolder))
               returnFolder = FindModuleInSubFolders(startingFolder.GetRootFolder(), module);
         }

         return returnFolder;
      }
   }
}