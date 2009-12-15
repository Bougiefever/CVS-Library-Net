using System.Text.RegularExpressions;

namespace PServerClient.Responses
{
   public class ResponseFactory
   {
      public IResponse CreateResponse(ResponseType responseType)
      {
         IResponse response;
         switch (responseType)
         {
            case ResponseType.Auth:
               response = new AuthResponse();
               break;
            case ResponseType.Ok:
               response = new OkResponse();
               break;
            case ResponseType.Error:
            case ResponseType.EMessage:
               response = new ErrorResponse();
               break;
            case ResponseType.Message:
               response = new MessageResponse();
               break;
            case ResponseType.MessageTag:
               response = new MessageTagResponse();
               break;
            case ResponseType.ValidRequests:
               response = new ValidRequestResponse();
               break;
            case ResponseType.CheckedIn:
               response = new CheckedInResponse();
               break;
            case ResponseType.NewEntry:
               response = new NewEntryResponse();
               break;
            case ResponseType.Updated:
               response = new UpdatedResponse();
               break;
            case ResponseType.Merged:
               response = new MergedResponse();
               break;
            case ResponseType.Patched:
               response = new PatchedResponse();
               break;
            case ResponseType.CheckSum:
               response = new ChecksumResponse();
               break;
            case ResponseType.CopyFile:
               response = new CopyFileResponse();
               break;
            case ResponseType.Removed:
               response = new RemovedResponse();
               break;
            case ResponseType.RemoveEntry:
               response = new RemoveEntryResponse();
               break;
            case ResponseType.SetStaticDirectory:
               response = new SetStaticDirectoryResponse();
               break;
            case ResponseType.ClearStaticDirectory:
               response = new ClearStaticDirectoryResponse();
               break;
            case ResponseType.SetSticky:
               response = new SetStickyResponse();
               break;
            case ResponseType.ClearSticky:
               response = new ClearStickyResponse();
               break;
            case ResponseType.Created:
               response = new CreatedResponse();
               break;
            case ResponseType.UpdateExisting:
               response = new UpdateExistingResponse();
               break;
            case ResponseType.RcsDiff:
               response = new RcsDiffResponse();
               break;
            case ResponseType.Mode:
               response = new ModeResponse();
               break;
            case ResponseType.ModTime:
               response = new ModTimeResponse();
               break;
            case ResponseType.Template:
               response = new TemplateResponse();
               break;
            case ResponseType.Notified:
               response = new NotifiedResponse();
               break;
            case ResponseType.ModuleExpansion:
               response = new ModuleExpansionResponse();
               break;
            case ResponseType.Mbinary:
               response = new MbinaryResponse();
               break;
            case ResponseType.Flush:
               response = new FlushResponse();
               break;
            //case ResponseType.Unknown:
            default:
               response = new NullResponse();
               break;
         }
         return response;
      }

      public ResponseType GetResponseType(string rawResponse)
      {
         ResponseType responseType = ResponseType.Unknown;
         for (int i = 0; i < ResponseHelper.ResponsePatterns.Length; i++)
         {
            Match m = Regex.Match(rawResponse, ResponseHelper.ResponsePatterns[i]);
            if (m.Success)
               responseType = (ResponseType) i;
         }
         return responseType;
      }
   }
}