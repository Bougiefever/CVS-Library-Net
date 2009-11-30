using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace PServerClient.Responses
{
   public enum ResponseType
   {
      Unknown = 100,
      Auth = 0,
      Ok = 1,
      Error = 2,
      Message = 3,
      ValidRequests = 4,
      CheckedIn = 5,
      NewEntry = 6,
      Updated = 7,
      Merged = 8,
      Patched = 9,
      CheckSum = 10,
      CopyFile = 11,
      Removed = 12,
      RemoveEntry = 13,
      SetStaticDirectory = 14,
      ClearStaticDirectory = 15,
      SetSticky = 16,
      ClearSticky = 17,
      Created = 18,
      MessageTag = 19,
      UpdateExisting = 20,
      RcsDiff = 21,
      Mode = 22,
      ModTime = 23,
      Template = 24,
      Notified = 25,
      ModuleExpansion = 26,
      Mbinary = 27,
      EMessage = 28,
      FMessage = 29
   }

   public class ResponseFactory
   {
      public IList<IResponse> CreateResponses(IList<string> responseLines)
      {
         IList<IResponse> responses = new List<IResponse>();
         ResponseType responseType;
         int index = 0;
         while (index < responses.Count)
         {
            string line = responseLines[index++];
            responseType = GetResponseType(line);
            IResponse response = CreateResponse(responseType, line);
            // get rest of entry on first line of response
            Match m = Regex.Match(line, CreateResponseHelper.ResponsePatterns[(int)responseType]);
            string text = m.Groups[1].ToString();
            response.ResponseLines.Add(text);
            if (response.LineCount > 1)
            {
               for (int i = 1; i < response.LineCount; i++)
               {
                  response.ResponseLines.Add(responseLines[index++]);
               }
            }
         }

         return responses;
      }

      public IResponse CreateResponse(ResponseType responseType, string rawResponse)
      {
         //ResponseType responseType = GetResponseType(rawResponse);
         IResponse response = new NullResponse();
         switch (responseType)
         {
            case ResponseType.Auth:
               response = new AuthResponse();
               break;
            case ResponseType.Ok:
               response = new OkResponse();
               break;
            case ResponseType.Message:
               response = new MessageResponse();
               break;
            case ResponseType.ValidRequests:
               response = new ValidRequestResponse();
               break;
            case ResponseType.Error:
            case ResponseType.CheckedIn:
            case ResponseType.NewEntry:
            case ResponseType.Unknown:
               break;
         }
         return response;
      }

      public ResponseType GetResponseType(string rawResponse)
      {
         ResponseType responseType = ResponseType.Unknown;
         for (int i = 0; i < CreateResponseHelper.ResponsePatterns.Length; i++)
         {
            Match m = Regex.Match(rawResponse, CreateResponseHelper.ResponsePatterns[i]);
            if (m.Success)
               responseType = (ResponseType)i;
         }
         return responseType;
      }

      public IList<string> GetResponseLines(ResponseType responseType, IList<string> lines, int lineCount, int index)
      {
         IList<string> responseLines = new List<string>();
         string line = lines[index++];
         string pattern = CreateResponseHelper.ResponsePatterns[(int)responseType];
         Match m = Regex.Match(line, pattern);
         responseLines.Add(m.Groups[1].ToString());
         if (lineCount != 1)
         {
            int i = 1;
            bool adding = true;
            do
            {
               line = lines[index++];
               if (lineCount == 0)
               {
                  m = Regex.Match(line, pattern);
                  if (m.Success)
                  {
                     line = m.Groups[1].ToString();
                     responseLines.Add(line);
                  }
                  else
                     adding = false;
               }
               else
               {
                  responseLines.Add(line);
                  i++;
                  if (i == lineCount)
                     adding = false;
               }
            } while (adding == true);
         }

         return responseLines;
      }

   }
}
