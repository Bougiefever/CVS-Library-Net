using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PServerClient.Responses;

namespace PServerClient.CVS
{
   public class ServerFileReceiver
   {
      private readonly Root _root;
      public ServerFileReceiver(Root root)
      {
         _root = root;
      }

      public void ProcessCheckoutResponses(IList<IResponse> checkOutResponses)
      {
         if (checkOutResponses.Where(r => r.ResponseType == ResponseType.MessageTag).Count() > 1)
         {
            ReceiveMTUpdatedResponses(checkOutResponses);
         }
         else
         {
            ReceiveMUStyleResponses(checkOutResponses);
         }
      }

      public void WriteToDisk(Folder module)
      {
         module.Write();
         foreach (ICVSItem item in module)
         {
            if (item is Folder)
               WriteToDisk((Folder)item);
            else
               item.Write();
         }
      }

      internal void ReceiveMTUpdatedResponses(IList<IResponse> responses)
      {
         int i = 0;
         IResponse response = responses[i++];
         while (response.ResponseType != ResponseType.Ok)
         {
            IList<IResponse> entryResponses;
            if (response.ResponseType == ResponseType.ModTime)
            {
               entryResponses = new List<IResponse> { response };
               response = responses[i++];
               while (response.ResponseType == ResponseType.MessageTag)
               {
                  MessageTagResponse r = (MessageTagResponse)response;
                  if (r.Message.StartsWith("fname"))
                     entryResponses.Add(response);
                  response = responses[i++];
               }
               entryResponses.Add(response);
               AddNewEntry(entryResponses);
            }
            response = responses[i++];
         }
      }

      internal void ReceiveMUStyleResponses(IList<IResponse> responses)
      {
         //int i = 0;
         //IResponse response = responses[i++];
         //while (response.ResponseType != ResponseType.Ok)
         //{
         //   if (response.ResponseType == ResponseType.Message)
         //   {

         //   }
         //}
      }

      public void AddNewEntry(IList<IResponse> entryResponses)
      {
         IResponse res = entryResponses.Where(r => r.ResponseType == ResponseType.MessageTag).First();
         string[] names = PServerHelper.GetUpdatedFnamePathFile(res.DisplayResponse());
         string[] folders = names[0].Split(new[] { @"/" }, StringSplitOptions.RemoveEmptyEntries);
         Folder current = CreateFolderStructure(folders);

         string filename = names[1];
         FileInfo fi = new FileInfo(Path.Combine(current.Info.FullName, filename));
         Entry entry = new Entry(fi);
         res = entryResponses.Where(r => r.ResponseType == ResponseType.ModTime).First();
         entry.ModTime = ((ModTimeResponse)res).ModTime;
         UpdatedResponse ur = (UpdatedResponse)entryResponses.Where(r => r.ResponseType == ResponseType.Updated).First();
         if (entry.Name == ur.File.FileName)
         {
            entry.Revision = ur.File.Revision;
            entry.StickyOption = "";
            entry.Length = ur.File.FileLength;
            entry.Properties = ur.File.Properties;
            entry.FileContents = ur.File.FileContents;
         }
         current.AddItem(entry);
      }

      public Folder CreateFolderStructure(string[] folders)
      {
         Folder current = _root.ModuleFolder;
         string repository = _root.Module;
         for (int i = 1; i < folders.Length; i++)
         {
            string folderName = folders[i];
            Folder folder = null;
            foreach (ICVSItem item in current)
            {
               if ((item is Folder) && item.Name == folderName)
                  folder = (Folder)item;
            }
            if (folder == null)
            {
               repository += "/" + folderName;
               DirectoryInfo di = new DirectoryInfo(Path.Combine(current.Info.FullName, folderName));
               folder = new Folder(di, _root.CvsConnectionString, repository);
               current.AddItem(folder);
            }
            current = folder;
         }
         return current;
      }
   }
}