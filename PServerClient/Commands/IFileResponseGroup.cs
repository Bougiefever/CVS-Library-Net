using PServerClient.Responses;

namespace PServerClient.Commands
{
   public interface IFileResponseGroup
   {
      ModTimeResponse ModTime { get; set; }
      IMessageResponse MT { get; set; }
      IFileResponse FileResponse { get; set; }
   }

   class FileResponseGroup : IFileResponseGroup
   {
      public ModTimeResponse ModTime { get; set; }
      public IMessageResponse MT { get; set; }
      public IFileResponse FileResponse { get; set; }
   }
}