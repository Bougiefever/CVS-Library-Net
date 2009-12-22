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

namespace PServerClient.Tests
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
               new XElement("Name", "CheckOut"),
               new XElement("RequestType", "17"),
            new XElement("Responses", response))));
         Console.WriteLine(xdoc.ToString());
         xdoc.Validate(schemas, (o, e) =>
         {
            isValid = false;
         });
         return isValid;
      }

      public static bool ValidateResponseXML(XDocument response)
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\ResponseSchema.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());

         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add("", reader);
         bool isValid = true;
         response.Validate(schemas, (o, e) =>
         {
            isValid = false;
         });
         return isValid;
      }

      public static IList<IResponse> ResponsesFromXML(XDocument xdoc)
      {
         IList<IResponse> responses = new List<IResponse>();
         IEnumerable<XElement> responseElements = xdoc.Element("Responses").Elements("Response");
         foreach (XElement element in responseElements)
         {
            IResponse response = ResponseFromXElement(element);
            responses.Add(response);
         }
         return responses;
      }

      public static IResponse ResponseFromXElement(XElement responseElement)
      {
         ResponseType rtype = (ResponseType)Convert.ToInt32(responseElement.Element("ResponseType").Value);
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
            IFileResponse fileResponse = (IFileResponse)response;
            XElement fileElement = responseElement.Descendants("ResponseFile").First();
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

      public static XElement ResponseXML(IResponse response)
      {
         XElement responseElement = new XElement("Response",
                                             new XElement("Name", response.ResponseType.ToString()),
                                             new XElement("ResponseType", (int)response.ResponseType),
                                             new XElement("ProcessLines"));
         XElement xLines = responseElement.Descendants("ProcessLines").First();

         foreach (string s in response.ResponseLines)
         {
            XElement line = new XElement("Line", s);
            xLines.Add(line);
         }
         if (response is IFileResponse)
         {
            IFileResponse fileResponse = (IFileResponse) response;
            string length = fileResponse.File.Length.ToString();
            string contents = ResponseHelper.FileContentsToByteArrayString(fileResponse.File.Contents);
            XElement responseFile = new XElement("ResponseFile",
               new XElement("Length", length),
               new XElement("Contents", contents));
            responseElement.Add(responseFile);
         }
         return responseElement;
      }

      public static void SaveCommandConversation(ICommand command)
      {
         
      }

      public static XElement RequestsXML(IList<IRequest> requests)
      {
         XElement requestsElement = new XElement("Requests");
         foreach (IRequest request in requests)
         {
            requestsElement.Add(RequestXML(request));
         }
         return requestsElement;
      }

      public static XElement RequestXML(IRequest request)
      {
         XElement requestElement = new XElement("Request",
            new XElement("Name", request.RequestType.ToString()),
            new XElement("RequestType", (int)request.RequestType));
         return requestElement;
      }
   }
}