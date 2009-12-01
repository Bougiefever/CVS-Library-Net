using System.Text.RegularExpressions;
using System.Collections.Generic;
using PServerClient.Connection;
using System.Text;

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
      private ICvsTcpClient _tcpClient;
      public IList<IResponse> CreateMyResponses(ICvsTcpClient tcpClient)
      {
         _tcpClient = tcpClient;
         IList<IResponse> responses = new List<IResponse>();
         bool hasData = true;
         while (hasData)
         {
            string line = ReadLine();
            ResponseType responseType = GetResponseType(line);
            IResponse response = CreateResponse(responseType);
            if (response is IFileResponse)
            {

            }
         }

         return responses;
      }

      public IList<IResponse> CreateResponses(IList<string> lines)
      {
         IList<IResponse> responses = new List<IResponse>();
         ResponseType responseType;
         int index = 0;
         while (index < lines.Count)
         {
            string line = lines[index];
            responseType = GetResponseType(line);
            IResponse response = CreateResponse(responseType);
            IList<string> responseLines = GetResponseLines(responseType, lines, response.LineCount, index);
            index += responseLines.Count;
            response.ProcessResponse(responseLines);
         }
         return responses;
      }

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
            case ResponseType.FMessage:
               response = new FMessageResponse();
               break;
            case ResponseType.Unknown:
            default:
               response = new NullResponse();
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

      public string ReadLine()
      {
         int i = _tcpClient.ReadByte();
         StringBuilder sb = new StringBuilder();
         while (i != 10)
            sb.Append((char)i);
         return sb.ToString();
      }

   }
}
