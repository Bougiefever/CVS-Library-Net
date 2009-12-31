using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient
{
   public class PServerFactory
   {
      // ReSharper disable PossibleNullReferenceException
      // ReSharper disable MemberCanBeMadeStatic.Local

      public IResponse CreateResponse(ResponseType type)
      {
         string responseName = GetResponseClassNameFromType(type);
         ObjectHandle handle = Activator.CreateInstance("PServerClient", responseName);
         IResponse response = (IResponse) handle.Unwrap();
         return response;
      }

      public IResponse CreateResponse(string className)
      {
         Type type = Type.GetType(className);
         IResponse response = (IResponse) Activator.CreateInstance(type);
         return response;
      }

      public ResponseType GetResponseType(string rawResponse)
      {
         ResponseType responseType = ResponseType.Unknown;
         for (int i = 0; i < ResponseHelper.ResponsePatterns.Length; i++)
         {
            string pattern = ResponseHelper.ResponsePatterns[i];
            Match m = Regex.Match(rawResponse, pattern);
            if (m.Success)
            {
               responseType = (ResponseType)i;
               break;
            }
         }
         return responseType;
      }

      public IRequest CreateRequest(RequestType requestType, object[] args)
      {
         string requestName = GetRequestClassNameFromType(requestType);
         Type type = Type.GetType(requestName);
         IRequest request = (IRequest) Activator.CreateInstance(type, args);
         return request;
      }

      public IRequest CreateRequest(string className, string[] lines)
      {
         Type type = Type.GetType(className);
         IRequest request = (IRequest)Activator.CreateInstance(type, new object[] { lines });
         return request;
      }

      public ICommand CreateCommand(string className, object[] args)
      {
         Type type = Type.GetType(className);
         ICommand cmd = (ICommand)Activator.CreateInstance(type, args);
         return cmd;
      }

      public ICommand CreateCommand(XDocument commandXML, object[] args)
      {
         XElement commandElement = (XElement)commandXML.FirstNode;
         string className = commandElement.Element("ClassName").Value;
         ICommand command = CreateCommand(className, args);
         command.RequiredRequests.Clear();
         command.Requests.Clear();
         XElement requestsElement = commandElement.Element("RequiredRequests");
         command.RequiredRequests = IRequestListFromRequestsXElement(requestsElement);
         requestsElement = commandElement.Element("Requests");
         command.Requests = IRequestListFromRequestsXElement(requestsElement);
         return command;
      }

      public IList<IRequest> IRequestListFromRequestsXElement(XElement requestsElement)
      {
         IList<IRequest> requests = new List<IRequest>();
         foreach (XElement requestElement in requestsElement.Nodes())
         {
            IRequest request = RequestXElementToIRequest(requestElement);
            requests.Add(request);
            XElement responsesElement = requestElement.Element("Responses");
            request.Responses = IResponseListFromResponsesXElement(responsesElement);
         }
         return requests;
      }

      public IRequest RequestXElementToIRequest(XElement requestElement)
      {
         string className = requestElement.Element("ClassName").Value;
         IEnumerable<XElement> linesElements = requestElement.Element("Lines").Elements();
         string[] lines = linesElements.Select(le => le.Value).ToArray();
         PServerFactory factory = new PServerFactory();
         IRequest request = factory.CreateRequest(className, lines);
         return request;
      }

      public IList<IResponse> IResponseListFromResponsesXElement(XElement responsesElement)
      {
         IList<IResponse> responses = new List<IResponse>();
         foreach (XElement responseElement in responsesElement.Nodes())
         {
            IResponse response = ResponseXElementToIResponse(responseElement);
            responses.Add(response);
         }
         return responses;
      }

      public IResponse ResponseXElementToIResponse(XElement responseElement)
      {
         PServerFactory factory = new PServerFactory();
         string className = responseElement.Element("ClassName").Value;
         IResponse response = factory.CreateResponse(className);
         IList<string> lines = new List<string>();
         XElement linesElement = responseElement.Descendants("Lines").First();
         foreach (XElement lineElement in linesElement.Elements())
         {
            lines.Add(lineElement.Value);
         }
         response.Process(lines);
         if (response is IFileResponse)
         {
            IFileResponse fileResponse = (IFileResponse)response;
            XElement fileElement = responseElement.Descendants("File").First();
            long len = Convert.ToInt64(fileElement.Element("Length").Value);
            string byteString = fileElement.Element("Contents").Value;
            byte[] buffer = new byte[len];
            string[] bytes = byteString.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < bytes.Length; i++)
            {
               buffer[i] = Convert.ToByte(bytes[i]);
            }
            fileResponse.File.Length = len;
            fileResponse.File.Contents = buffer;
         }
         return response;
      }

      
      private string GetResponseClassNameFromType(ResponseType type)
      {
         string responseName = "PServerClient.Responses." + type + "Response";
         return responseName;
      }

      private string GetRequestClassNameFromType(RequestType type)
      {
         string requestName = "PServerClient.Requests." + type + "Request";
         return requestName;
      }
   }
}