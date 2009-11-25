﻿using System.Text.RegularExpressions;

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
         for (int i=0; i<CreateResponseHelper.ResponsePatterns.Length - 1; i++)
         {
            Match m = Regex.Match(rawResponse, CreateResponseHelper.ResponsePatterns[i]);
            if (m.Success)
               responseType = (ResponseType) i;
         }
         return responseType;
      }

      public static class CreateResponseHelper
      {
         public static readonly string[] ResponsePatterns;
         private const string AuthRegex = "(I LOVE YOU|I HATE YOU)";
         private const string OkRegex = @"^ok\s?\n";
         private const string ErrorRegex = @"^error";
         private const string MessageRegex = @"^M\s";
         private const string ValidRequestsRegex = @"^Valid-requests\s";


         static CreateResponseHelper()
         {
            ResponsePatterns = new string[5];
            ResponsePatterns[0] = AuthRegex;
            ResponsePatterns[1] = OkRegex;
            ResponsePatterns[2] = ErrorRegex;
            ResponsePatterns[3] = MessageRegex;
            ResponsePatterns[4] = ValidRequestsRegex;
         }

      }
   }
}
