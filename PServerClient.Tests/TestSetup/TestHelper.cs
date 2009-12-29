using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Tests.TestSetup
{
   public static class TestHelper
   {
      #region XML Validation

      public static bool ValidateResponseXML(XElement response)
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\Schemas\Response.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());
         var validateSchema = XmlSchema.Read(reader, (o, e) => Assert.Fail(e.Message));
         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add(validateSchema);
         XDocument xdoc = new XDocument(response);
         xdoc.Validate(schemas, (o, e) => Assert.Fail(e.Message));
         return true;
      }

      public static bool ValidateCommandXML(XDocument command)
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\Schemas\Command.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());
         var validateSchema = XmlSchema.Read(reader, (o, e) => Assert.Fail(e.Message));
         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add(validateSchema);
         command.Validate(schemas, (o, e) => Assert.Fail(e.Message));
         return true;
      }

      public static bool ValidateRequestXML(XElement request)
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\Schemas\Request.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());
         var validateSchema = XmlSchema.Read(reader, (o, e) => Assert.Fail(e.Message));
         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add(validateSchema);
         bool isValid = true;

         XDocument xdoc = new XDocument(request);
         xdoc.Validate(schemas, (o, e) => Assert.Fail(e.Message));
         return true;
      }

      #endregion

      #region Objects to XML

      public static void SaveCommandConversation(ICommand command, string path)
      {
         FileInfo fi = new FileInfo(path);
         XDocument xdoc = ICommandToXDocument(command);
         StreamWriter writer = fi.CreateText();
         xdoc.Save(writer);
      }

      public static XDocument ICommandToXDocument(ICommand command)
      {
         XElement commandElement = new XElement("Command",
                                                new XElement("ClassName", command.GetType().FullName)
                                                //new XElement("Type", (int)command.Type)
                                                );
         XElement requestsElement = new XElement("RequiredRequests");
         foreach (IRequest request in command.RequiredRequests)
         {
            requestsElement.Add(IRequestToXElement(request));
         }
         commandElement.Add(requestsElement);
         requestsElement = new XElement("Requests");
         foreach (IRequest request in command.Requests)
         {
            requestsElement.Add(IRequestToXElement(request));
         }
         commandElement.Add(requestsElement);
         XDocument xdoc = new XDocument(commandElement);
         return xdoc;
      }

      public static IList<IRequest> IRequestListFromRequestsXElement(XElement requestsElement)
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

      public static XElement IRequestToXElement(IRequest request)
      {
         //var classname = request.GetType();
         XElement requestElement = new XElement("Request",
                                                new XElement("ClassName", request.GetType().FullName),
                                                //new XElement("Type", (int)request.Type),
                                                new XElement("Lines"));
         XElement linesElement = requestElement.Descendants("Lines").First();
         foreach (string s in request.RequestLines)
         {
            XElement line = new XElement("Line", s);
            linesElement.Add(line);
         }
         XElement responsesElement = new XElement("Responses");
         requestElement.Add(responsesElement);
         foreach (IResponse response in request.Responses)
         {
            XElement responseElement = IResponseToXElement(response);
            responsesElement.Add(responseElement);
         }
         return requestElement;
      }

      public static IList<IResponse> IResponseListFromResponsesXElement(XElement responsesElement)
      {
         IList<IResponse> responses = new List<IResponse>();
         foreach (XElement responseElement in responsesElement.Nodes())
         {
            IResponse response = ResponseXElementToIResponse(responseElement);
            responses.Add(response);
         }
         return responses;
      }

      public static XElement IResponseToXElement(IResponse response)
      {
         XElement responseElement = new XElement("Response",
                                                 new XElement("ClassName", response.GetType().FullName),
                                                 //new XElement("Type", (int)response.Type),
                                                 new XElement("Lines"));
         XElement linesElement = responseElement.Descendants("Lines").First();

         foreach (string s in response.ResponseLines)
         {
            XElement line = new XElement("Line", s);
            linesElement.Add(line);
         }
         if (response is IFileResponse)
         {
            IFileResponse fileResponse = (IFileResponse)response;
            string length = fileResponse.File.Length.ToString();
            string contents = ResponseHelper.FileContentsToByteArrayString(fileResponse.File.Contents);
            XElement responseFile = new XElement("File",
                                                 new XElement("Length", length),
                                                 new XElement("Contents", contents));
            responseElement.Add(responseFile);
         }
         return responseElement;
      }

      public static string FileContentsToByteArrayString(byte[] fileContents)
      {
         StringBuilder sb = new StringBuilder();
         sb.Append(fileContents[0]);
         for (int i = 1; i < fileContents.Length; i++)
         {
            sb.Append(",").Append(fileContents[i]);
         }
         return sb.ToString();
      }

      #endregion

      #region XML to Objects

      public static ICommand CommandXElementToICommand(XDocument xdoc, Root root)
      {
         XElement commandElement = (XElement)xdoc.FirstNode;
         string className = commandElement.Element("ClassName").Value;
         PServerFactory factory = new PServerFactory();
         ICommand command = factory.CreateCommand(className, new object[] {root});
         command.RequiredRequests.Clear();
         command.Requests.Clear();
         XElement requestsElement = commandElement.Element("RequiredRequests");
         command.RequiredRequests = IRequestListFromRequestsXElement(requestsElement);
         requestsElement = commandElement.Element("Requests");
         command.Requests = IRequestListFromRequestsXElement(requestsElement);
         return command;
      }

      public static IRequest RequestXElementToIRequest(XElement requestElement)
      {
         string className = requestElement.Element("ClassName").Value;
         IEnumerable<XElement> linesElements = requestElement.Element("Lines").Elements();
         string[] lines = linesElements.Select(le => le.Value).ToArray();
         PServerFactory factory = new PServerFactory();
         IRequest request = factory.CreateRequest(className, lines);
         return request;
      }

      public static IResponse ResponseXElementToIResponse(XElement responseElement)
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
         response.ProcessResponse(lines);
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

      #endregion

      #region Mocking

      public static IList<IResponse> GetMockCheckoutResponses(string time, string path, string file)
      {
         IList<IResponse> responses = new List<IResponse>();
         IResponse r = new ModTimeResponse();
         r.ProcessResponse(new List<string> { time });
         responses.Add(r);
         var list = (GetMockMTResponseGroup(path + file));
         foreach (IResponse response in list)
         {
            responses.Add(response);
         }
         responses.Add(GetMockUpdatedResponse(path, file));

         return responses;
      }

      public static IList<IResponse> GetMockMTResponseGroup(string fname)
      {
         IList<IResponse> responses = new List<IResponse>();
         string[] messages = new[] { "+updated", "text U", "fname " + fname, "newline", "-updated" };
         foreach (string s in messages)
         {
            MessageTagResponse m = new MessageTagResponse();
            m.ProcessResponse(new List<string> { s });
            responses.Add(m);
         }
         return responses;
      }

      public static UpdatedResponse GetMockUpdatedResponse(string path, string name)
      {
         UpdatedResponse res = new UpdatedResponse();
         IList<string> lines = new List<string>
                                  {
                                     path,
                                     "/usr/local/cvsroot/sandbox/" + path + name,
                                     "/" + name + "/1.1.1.1///",
                                     "u=rw,g=rw,o=rw",
                                     "5"
                                  };
         res.ProcessResponse(lines);
         string text = "abcde";
         res.File.Contents = text.Encode();
         return res;
      }

      #endregion
   }
}