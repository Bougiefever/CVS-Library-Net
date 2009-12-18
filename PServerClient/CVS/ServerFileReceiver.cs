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

      public void SaveFolder(ICVSItem working)
      {
         working.Write();
         foreach (ICVSItem item in working.ChildItems)
         {
            if (item.ItemType == ItemType.Folder)
               SaveFolder(item);
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
         int i = 0;
         IResponse response = responses[i++];
         while (response.ResponseType != ResponseType.Ok)
         {
            if (response.ResponseType == ResponseType.Message)
            {

            }
         }
      }

      public void AddNewEntry(IList<IResponse> entryResponses)
      {
         Folder workingDir = (Folder)_root.WorkingDirectory;
         IResponse res = entryResponses.Where(r => r.ResponseType == ResponseType.MessageTag).First();
         string[] names = PServerHelper.GetUpdatedFnamePathFile(res.DisplayResponse());
         string[] folders = names[0].Split(new[] { @"/" }, StringSplitOptions.RemoveEmptyEntries);
         Folder current = CreateFolderStructure(workingDir, folders);
         string filename = names[1];
         FileInfo fi = new FileInfo(Path.Combine(current.Item.FullName, filename));
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

      public Folder CreateFolderStructure(Folder workingDir, string[] folders)
      {
         Folder startFolder = workingDir;
         for (int i = 0; i < folders.Length; i++)
         {
            string folderName = folders[i];
            Folder folder = (Folder)startFolder.ChildItems.Where(ci => ci.ItemType == ItemType.Folder && ci.Name == folderName).FirstOrDefault();
            if (folder == null)
            {
               DirectoryInfo di = new DirectoryInfo(Path.Combine(startFolder.Item.FullName, folderName));
               folder = new Folder(di);
               startFolder.AddItem(folder);
            }
            startFolder = folder;
         }
         return startFolder;
      }
   }
}