using System;
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
      public static bool ValidateResponseXML(XElement response)
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\Schemas\Response.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());
         var validateSchema = XmlSchema.Read(reader, (o, e) => Assert.Fail(e.Message));
         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add(validateSchema);
         bool isValid = true;

         XDocument xdoc = new XDocument(response);
         xdoc.Validate(schemas, (o, e) => { isValid = false; });
         return isValid;
      }

      public static bool ValidateCommandXML(XDocument command)
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\Schemas\Command.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());
         var validateSchema = XmlSchema.Read(reader, (o, e) => Assert.Fail(e.Message));
         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add(validateSchema);
         //bool isValid = true;

         command.Validate(schemas, (o, e) => Assert.Fail(e.Message));
         //isValid = true;
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
         ResponseType rtype = (ResponseType)Convert.ToInt32(responseElement.Element("ResponseType").Value);
         PServerFactory factory = new PServerFactory();
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

      public static XElement ResponseToXML(IResponse response)
      {
         XElement responseElement = new XElement("Response",
                                                 new XElement("Name", response.ResponseType.ToString()),
                                                 new XElement("Type", (int)response.ResponseType),
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

      public static void SaveCommandConversation(ICommand command, string path)
      {
         FileInfo fi = new FileInfo(path);
         XDocument xdoc = CommandToXML(command);
         StreamWriter writer = fi.CreateText();
         xdoc.Save(writer);
      }

      public static XDocument CommandToXML(ICommand command)
      {
         XElement commandElement = new XElement("Command",
                                                new XElement("Name", command.CommandType.ToString()),
                                                new XElement("Type", (int)command.CommandType));
         XElement requestsElement = new XElement("RequiredRequests");
         foreach (IRequest request in command.RequiredRequests)
         {
            requestsElement.Add(RequestToXML(request));
         }
         commandElement.Add(requestsElement);
         requestsElement = new XElement("Requests");
         foreach (IRequest request in command.Requests)
         {
            requestsElement.Add(RequestToXML(request));
         }
         commandElement.Add(requestsElement);
         XDocument xdoc = new XDocument(commandElement);
         return xdoc;
      }

      public static XElement RequestToXML(IRequest request)
      {
         XElement requestElement = new XElement("Request",
                                                new XElement("Name", request.RequestType.ToString()),
                                                new XElement("Type", (int)request.RequestType),
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

      private static IList<IResponse> GetMockMTResponseGroup(string fname)
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

      public static ICommand CommandXMLToCommandObject(XDocument xdoc, Root root)
      {
         XElement commandElement = (XElement) xdoc.FirstNode;
         CommandType type = (CommandType) Convert.ToInt32(commandElement.Element("Type").Value);
         ICommand command = GetCommandByType(type, root);

         return command;
      }

      public static ICommand GetCommandByType(CommandType type, Root root)
      {
         ICommand command;
         switch (type)
         {
            case CommandType.CheckOut:
               command = new CheckOutCommand(root);
               break;
            case CommandType.Import:
               command = new ImportCommand(root);
               break;
            case CommandType.Log:
               command = new LogCommand(root);
               break;
            case CommandType.ValidRequestsList:
               command = new ValidRequestsListCommand(root);
               break;
            case CommandType.VerifyAuth:
               command = new VerifyAuthCommand(root);
               break;
            case CommandType.Version:
               command = new VersionCommand(root);
               break;
            default:
               command = null;
               break;
         }
         // reset command requests so the xml request can be added instead
         command.RequiredRequests.Clear();
         command.Requests.Clear();
         command.ExitCode = ExitCode.Succeeded;
         return command;         
      }

      public static IRequest GetRequestByType(RequestType type)
      {
         IRequest request;
         switch (type)
         {
            case RequestType.Add:
               request = new AddRequest();
               break;
            case RequestType.Admin:
               request = new AdminRequest();
               break;
            case RequestType.Annotate:
               request = new AnnotateRequest();
               break;
            case RequestType.Argument:
               request = new ArgumentRequest("");
               break;
            case RequestType.Argumentx:
               request = new ArgumentxRequest("");
               break;
            case RequestType.Auth:
               request = new AuthRequest(null);
               break;
            case RequestType.Case:
               request = new CaseRequest();
               break;
            case RequestType.CheckIn:
               request = new CheckInRequest();
               break;
            case RequestType.CheckinTime:
               request = new CheckinTimeRequest(new DateTime());
               break;
            case RequestType.CheckOut:
               request = new CheckOutRequest();
               break;
            case RequestType.Diff:
               request = new DiffRequest();
               break;
            case RequestType.Directory:
               request = new DirectoryRequest(null);
               break;
            case RequestType.Editors:
               request = new EditorsRequest();
               break;
            case RequestType.EmptyConflicts:
               request = new EmptyConflictsRequest();
               break;
            case RequestType.Entry:
               request = new EntryRequest("", "", "", "", "");
               break;
            case RequestType.ExpandModules:
               request = new ExpandModulesRequest();
               break;
            case RequestType.Export:
               request = new ExportRequest();
               break;
            case RequestType.GlobalOption:
               request = new GlobalOptionRequest("");
               break;
            case RequestType.GssapiAuthenticate:
               request = new GssapiAuthenticateRequest();
               break;
            case RequestType.GssapiEncrypt:
               request = new GssapiEncryptRequest();
               break;
            case RequestType.GzipFileContents:
               request = new GzipFileContentsRequest("");
               break;
            case RequestType.GzipStream:
               request = new GzipStreamRequest("");
               break;
            case RequestType.History:
               request = new HistoryRequest();
               break;
            case RequestType.Import:
               request = new ImportRequest();
               break;
            case RequestType.Init:
               request = new InitRequest("");
               break;
            case RequestType.IsModified:
               request = new IsModifiedRequest("");
               break;
            case RequestType.KerberosEncrypt:
               request = new KerberosEncryptRequest();
               break;
            case RequestType.Kopt:
               request = new KoptRequest("");
               break;
            case RequestType.Log:
               request = new LogRequest();
               break;
            case RequestType.Lost:
               request = new LostRequest("");
               break;
            case RequestType.MaxDot:
               request = new MaxDotRequest("");
               break;
            case RequestType.Modified:
               request = new ModifiedRequest("", "", 0);
               break;
            case RequestType.Noop:
               request = new NoopRequest();
               break;
            case RequestType.Notify:
               request = new NotifyRequest("");
               break;
            case RequestType.Questionable:
               request = new QuestionableRequest("");
               break;
            case RequestType.RAnnotate:
               request = new RAnnotateRequest();
               break;
            case RequestType.RDiff:
               request = new RDiffRequest();
               break;
            case RequestType.Release:
               request = new ReleaseRequest();
               break;
            case RequestType.Remove:
               request = new RemoveRequest();
               break;
            case RequestType.Repository:
               request = new RepositoryRequest(null);
               break;
            case RequestType.RLog:
               request = new RLogRequest();
               break;
            case RequestType.Root:
               request = new RootRequest(null);
               break;
            case RequestType.RTag:
               request = new RTagRequest();
               break;
            case RequestType.Set:
               request = new SetRequest("", "");
               break;
            case RequestType.StaticDirectory:
               request = new StaticDirectoryRequest();
               break;
            case RequestType.Status:
               request = new StatusRequest();
               break;
            case RequestType.Sticky:
               request = new StickyRequest("");
               break;
            case RequestType.Tag:
               request = new TagRequest();
               break;
            case RequestType.Unchanged:
               request = new UnchangedRequest("");
               break;
            case RequestType.UpdatePatches:
               request = new UpdatePatchesRequest();
               break;
            case RequestType.Update:
               request = new UpdateRequest();
               break;
            case RequestType.UseUnchanged:
               request = new UseUnchangedRequest();
               break;
            case RequestType.ValidRequests:
               request = new ValidRequestsRequest();
               break;
            case RequestType.ValidResponses:
               request = new ValidResponsesRequest(null);
               break;
            case RequestType.VerifyAuth:
               request = new VerifyAuthRequest(null);
               break;
            case RequestType.Version:
               request = new VersionRequest();
               break;
            case RequestType.WatchAdd:
               request = new WatchAddRequest();
               break;
            case RequestType.Watchers:
               request = new WatchersRequest();
               break;
            case RequestType.WatchOff:
               request = new WatchOffRequest();
               break;
            case RequestType.WatchOn:
               request = new WatchOnRequest();
               break;
            case RequestType.WatchRemove:
               request = new WatchRemoveRequest();
               break;
            case RequestType.WrapperSendmeRcsOptions:
               request = new WrapperSendmeRcsOptionsRequest();
               break;
            default:
               request = null;
               break;
         }
         return request;
      }
   }
}