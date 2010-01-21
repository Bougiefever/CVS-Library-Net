using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using PServerClient.Commands;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient
{
   /// <summary>
   /// Creates instances of classes
   /// </summary>
   public class PServerFactory
   {
      // ReSharper disable PossibleNullReferenceException

      // ReSharper disable MemberCanBeMadeStatic.Local

      /// <summary>
      /// Creates the response from the reponse type value
      /// </summary>
      /// <param name="type">The response type.</param>
      /// <returns>the response instance</returns>
      public IResponse CreateResponse(ResponseType type)
      {
         string responseName = GetResponseClassNameFromType(type);
         ObjectHandle handle = Activator.CreateInstance("PServerClient", responseName);
         IResponse response = (IResponse) handle.Unwrap();
         return response;
      }

      /// <summary>
      /// Creates the response from the class name string
      /// </summary>
      /// <param name="className">The fully-qualified name of the class.</param>
      /// <returns>the response instance</returns>
      public IResponse CreateResponse(string className)
      {
         Type type = Type.GetType(className);
         IResponse response = (IResponse) Activator.CreateInstance(type);
         return response;
      }

      /// <summary>
      /// Gets the type of the response from the string sent from CVS.
      /// </summary>
      /// <param name="rawResponse">The response string from CVS.</param>
      /// <returns>the response type</returns>
      public ResponseType GetResponseType(string rawResponse)
      {
         ResponseType responseType = ResponseType.Unknown;
         for (int i = 0; i < ResponseHelper.ResponsePatterns.Length; i++)
         {
            string pattern = ResponseHelper.ResponsePatterns[i];
            Match m = Regex.Match(rawResponse, pattern);
            if (m.Success)
            {
               responseType = (ResponseType) i;
               break;
            }
         }

         return responseType;
      }

      /// <summary>
      /// Creates the request of the specified type.
      /// </summary>
      /// <param name="requestType">Type of the request.</param>
      /// <param name="args">The args needed to create the request.</param>
      /// <returns>the request instance</returns>
      public IRequest CreateRequest(RequestType requestType, object[] args)
      {
         string requestName = GetRequestClassNameFromType(requestType);
         Type type = Type.GetType(requestName);
         IRequest request = (IRequest) Activator.CreateInstance(type, args);
         return request;
      }

      /// <summary>
      /// Creates the request from the fully-qualified name.
      /// </summary>
      /// <param name="className">Name of the class.</param>
      /// <param name="lines">The lines of the request.</param>
      /// <returns>the request instance</returns>
      public IRequest CreateRequest(string className, IList<string> lines)
      {
         Type type = Type.GetType(className);
         IRequest request = (IRequest) Activator.CreateInstance(type, new object[] { lines });
         return request;
      }

      /// <summary>
      /// Creates the command from the fully-qualified class name
      /// </summary>
      /// <param name="className">Name of the class.</param>
      /// <param name="args">The args needed to create the command.</param>
      /// <returns>the command instance</returns>
      public ICommand CreateCommand(string className, object[] args)
      {
         Type type = Type.GetType(className);
         ICommand cmd = (ICommand) Activator.CreateInstance(type, args);
         return cmd;
      }

      /// <summary>
      /// Creates the command from XML
      /// </summary>
      /// <param name="commandXML">The command XML.</param>
      /// <param name="args">The args needed to create the command.</param>
      /// <returns>the command instance</returns>
      public ICommand CreateCommand(XDocument commandXML, object[] args)
      {
         XElement commandElement = (XElement) commandXML.FirstNode;
         string className = commandElement.Element("ClassName").Value;
         ICommand command = CreateCommand(className, args);
         command.RequiredRequests.Clear();
         command.Requests.Clear();
         XElement requestsElement = commandElement.Element("RequiredRequests");
         command.RequiredRequests = IRequestListFromRequestsXElement(requestsElement);
         requestsElement = commandElement.Element("Requests");
         command.Requests = IRequestListFromRequestsXElement(requestsElement);
         XElement itemsElement = commandElement.Element("CommandItems");
         command.Items = ICommandItemListFromCommandItemsXElement(itemsElement);
         return command;
      }

      /// <summary>
      /// Gets the list of requests from XML
      /// </summary>
      /// <param name="requestsElement">The requests element.</param>
      /// <returns>list of requests</returns>
      public IList<IRequest> IRequestListFromRequestsXElement(XElement requestsElement)
      {
         IList<IRequest> requests = new List<IRequest>();
         foreach (XElement requestElement in requestsElement.Nodes())
         {
            IRequest request = RequestXElementToIRequest(requestElement);
            requests.Add(request);
         }

         return requests;
      }

      /// <summary>
      /// Converts a single request represented by XML to a request instance
      /// </summary>
      /// <param name="requestElement">The request element.</param>
      /// <returns>the request instance</returns>
      public IRequest RequestXElementToIRequest(XElement requestElement)
      {
         string className = requestElement.Element("ClassName").Value;
         IEnumerable<XElement> linesElements = requestElement.Element("Lines").Elements();
         string[] lines = linesElements.Select(le => le.Value).ToArray();
         PServerFactory factory = new PServerFactory();
         IRequest request = factory.CreateRequest(className, lines);
         return request;
      }

      /// <summary>
      /// Creates a command item list from the XElement
      /// </summary>
      /// <param name="itemsElement">The items element.</param>
      /// <returns>the list of command items</returns>
      public IList<ICommandItem> ICommandItemListFromCommandItemsXElement(XElement itemsElement)
      {
         IList<ICommandItem> items = new List<ICommandItem>();
         foreach (XElement itemElement in itemsElement.Nodes())
         {
            ICommandItem item;
            if (itemElement.Name == "Request")
               item = RequestXElementToIRequest(itemElement);
            else
               item = ResponseXElementToIResponse(itemElement);

            items.Add(item);
         }

         return items;
      }

      /// <summary>
      /// Creates a response instance from the XML for a single response
      /// </summary>
      /// <param name="responseElement">The response element.</param>
      /// <returns>the response instance</returns>
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

         if (response is IMessageResponse)
            response.Lines = lines;
         else
            response.Initialize(lines);
         response.Process();
         if (response is IFileResponse)
         {
            IFileResponse fileResponse = (IFileResponse) response;
            XElement fileElement = responseElement.Descendants("File").First();
            long len = Convert.ToInt64(fileElement.Element("Length").Value);
            string byteString = fileElement.Element("Contents").Value;
            byte[] buffer = new byte[len];
            string[] bytes = byteString.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < bytes.Length; i++)
            {
               buffer[i] = Convert.ToByte(bytes[i]);
            }

            fileResponse.Length = len;
            fileResponse.Contents = buffer;
         }

         return response;
      }

      private string GetRequestClassNameFromType(RequestType type)
      {
         string requestName = "PServerClient.Requests." + type + "Request";
         return requestName;
      }

      private string GetResponseClassNameFromType(ResponseType type)
      {
         string responseName = "PServerClient.Responses." + type + "Response";
         return responseName;
      }
   }
}