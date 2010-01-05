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
   }
}