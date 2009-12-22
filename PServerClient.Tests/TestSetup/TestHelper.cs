using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using PServerClient.Commands;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Tests.TestSetup
{
   public static class TestHelper
   {
      public static bool ValidateResponseXML(XElement response)
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\ResponseSchema.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());

         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add("", reader);
         bool isValid = true;
         XDocument xdoc = new XDocument(new XElement("Requests",
                                                     new XElement("Request",
                                                                  new XElement("Name", "AnyRequest"),
                                                                  new XElement("Type", "100"),
                                                                  new XElement("Lines", new XElement("Line", "my request")),
                                                                  new XElement("Responses", response))));
         //Console.WriteLine(xdoc.ToString());
         xdoc.Validate(schemas, (o, e) => { isValid = false; });
         return isValid;
      }

      public static bool ValidateXML(XDocument response)
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\ResponseSchema.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());

         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add("", reader);
         bool isValid = true;
         response.Validate(schemas, (o, e) => { isValid = false; });
         return isValid;
      }

      public static bool ValidateRequestXML(XElement request)
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\ResponseSchema.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());

         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add("", reader);
         bool isValid = true;
         XDocument xdoc = new XDocument(new XElement("Requests", request));
         //Console.WriteLine(xdoc.ToString());
         xdoc.Validate(schemas, (o, e) => { isValid = false; });
         return isValid;
      }

      public static IList<IResponse> XMLToResponseList(XDocument xdoc)
      {
         IList<IResponse> responses = new List<IResponse>();
         IEnumerable<XElement> responseElements = xdoc.Element("Responses").Elements("Response");
         foreach (XElement element in responseElements)
         {
            IResponse response = XMLToResponse(element);
            responses.Add(response);
         }
         return responses;
      }

      public static IResponse XMLToResponse(XElement responseElement)
      {
         ResponseType rtype = (ResponseType) Convert.ToInt32(responseElement.Element("ResponseType").Value);
         ResponseFactory factory = new ResponseFactory();
         IResponse response = factory.CreateResponse(rtype);
         IList<string> lines = new List<string>();
         XElement linesElement = responseElement.Descendants("ProcessLines").First();
         foreach (XElement lineElement in linesElement.Elements())
         {
            lines.Add(lineElement.Value);
         }
         response.ProcessResponse(lines);
         if (response is IFileResponse)
         {
            IFileResponse fileResponse = (IFileResponse) response;
            XElement fileElement = responseElement.Descendants("ResponseFile").First();
            long len = Convert.ToInt64(fileElement.Element("Length").Value);
            string byteString = fileElement.Element("Contents").Value;
            byte[] buffer = new byte[len];
            string[] bytes = byteString.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < bytes.Length; i++)
            {
               buffer[i] = Convert.ToByte(bytes[i]);
            }
            fileResponse.File.Length = len;
            fileResponse.File.Contents = buffer;
         }
         return response;
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

      public static XElement ResponseToXML(IResponse response)
      {
         XElement responseElement = new XElement("Response",
                                                 new XElement("Name", response.ResponseType.ToString()),
                                                 new XElement("Type", (int) response.ResponseType),
                                                 new XElement("Lines"));
         XElement linesElement = responseElement.Descendants("Lines").First();

         foreach (string s in response.ResponseLines)
         {
            XElement line = new XElement("Line", s);
            linesElement.Add(line);
         }
         if (response is IFileResponse)
         {
            IFileResponse fileResponse = (IFileResponse) response;
            string length = fileResponse.File.Length.ToString();
            string contents = ResponseHelper.FileContentsToByteArrayString(fileResponse.File.Contents);
            XElement responseFile = new XElement("File",
                                                 new XElement("Length", length),
                                                 new XElement("Contents", contents));
            responseElement.Add(responseFile);
         }
         return responseElement;
      }

      public static void SaveCommandConversation(ICommand command, string path)
      {
         FileInfo fi = new FileInfo(path);
         XDocument xdoc = CommandRequestsToXML(command);
         StreamWriter writer = fi.CreateText();
         xdoc.Save(writer);
      }

      public static XDocument CommandRequestsToXML(ICommand command)
      {
         XElement requestsElement = new XElement("Requests");
         XDocument xdoc = new XDocument(requestsElement);
         foreach (IRequest request in command.RequiredRequests)
         {
            requestsElement.Add(RequestToXML(request));
         }
         foreach (IRequest request in command.Requests)
         {
            requestsElement.Add(RequestToXML(request));
         }
         return xdoc;
      }

      public static XElement RequestToXML(IRequest request)
      {
         XElement requestElement = new XElement("Request",
                                                new XElement("Name", request.RequestType.ToString()),
                                                new XElement("Type", (int) request.RequestType),
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
            XElement responseElement = ResponseToXML(response);
            responsesElement.Add(responseElement);
         }
         return requestElement;
      }

      public static IList<IResponse> GetMockCheckoutResponses(string time, string path, string file)
      {
         IList<IResponse> responses = new List<IResponse>();
         IResponse r = new ModTimeResponse();
         r.ProcessResponse(new List<string> {time});
         responses.Add(r);
         var list = (GetMockMTResponseGroup(path + file));
         foreach (IResponse response in list)
         {
            responses.Add(response);
         }
         responses.Add(GetMockUpdatedResponse(path, file));

         return responses;
      }

      private static IList<IResponse> GetMockMTResponseGroup(string fname)
      {
         IList<IResponse> responses = new List<IResponse>();
         string[] messages = new[] {"+updated", "text U", "fname " + fname, "newline", "-updated"};
         foreach (string s in messages)
         {
            MessageTagResponse m = new MessageTagResponse();
            m.ProcessResponse(new List<string> {s});
            responses.Add(m);
         }
         return responses;
      }

      public static UpdatedResponse GetMockUpdatedResponse(string path, string name)
      {
         UpdatedResponse res = new UpdatedResponse();
         IList<string> lines = new List<string>
                                  {
                                     "Updated " + path,
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

      
   }
}