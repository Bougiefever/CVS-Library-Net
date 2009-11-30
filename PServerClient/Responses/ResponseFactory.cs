using System.Text.RegularExpressions;

namespace PServerClient.Responses
{
   public enum ResponseType
   {
      UnknownResponse = 100,
      AuthResponse = 0,
      OkResponse = 1,
      ErrorResponse = 2,
      MessageResponse = 3,
      ValidRequestsResponse = 4,
      CheckedInResponse = 5,
      NewEntryResponse = 6,
      UpdatedResponse = 7,
      MergedResponse = 8,
      PatchedRespone = 9,
      CheckSumResponse = 10,
      CopyFileResponse = 11,
      RemovedResponse = 12,
      RemoveEntryResponse = 13,
      SetStaticDirectoryResponse = 14,
      ClearStaticDirectoryRepsponse = 15
   }

   public class ResponseFactory
   {

      public IResponse CreateResponse(string rawResponse)
      {
         ResponseType responseType = GetResponseType(rawResponse);
         IResponse response = new NullResponse();
         switch (responseType)
         {
            case ResponseType.AuthResponse:
               response = new AuthResponse();
               break;
            case ResponseType.OkResponse:
               response = new OkResponse();
               break;
            case ResponseType.MessageResponse:
               response = new MessageResponse();
               break;
            case ResponseType.ValidRequestsResponse:
               response = new ValidRequestResponse();
               break;
            case ResponseType.ErrorResponse:
            case ResponseType.CheckedInResponse:
            case ResponseType.NewEntryResponse:
            case ResponseType.UnknownResponse:
               break;
         }
         return response;
      }

      public ResponseType GetResponseType(string rawResponse)
      {
         ResponseType responseType = ResponseType.UnknownResponse;
         for (int i=0; i<CreateResponseHelper.ResponsePatterns.Length; i++)
         {
            Match m = Regex.Match(rawResponse, CreateResponseHelper.ResponsePatterns[i]);
            if (m.Success)
               responseType = (ResponseType) i;
         }
         return responseType;
      }


   }
}
