using System.Collections.Generic;
using PServerClient.Responses;

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
   }
}