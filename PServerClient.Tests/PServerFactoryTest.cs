using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;

namespace PServerClient.Tests
{
   [TestFixture]
   public class PServerFactoryTest
   {
      private PServerFactory _factory;
      private IRoot _root;

      [TestFixtureSetUp]
      public void SetUp()
      {
         _factory = new PServerFactory();
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
      }

      [Test]
      public void CreateResponsesFromTypeTest()
      {
         ResponseType type = ResponseType.Auth;
         IResponse response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<AuthResponse>(response);

         type = ResponseType.Ok;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<OkResponse>(response);

         type = ResponseType.Error;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<ErrorResponse>(response);

         type = ResponseType.EMessage;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<EMessageResponse>(response);

         type = ResponseType.Message;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<MessageResponse>(response);

         type = ResponseType.MTMessage;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<MTMessageResponse>(response);

         type = ResponseType.ValidRequests;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<ValidRequestsResponse>(response);

         type = ResponseType.CheckedIn;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<CheckedInResponse>(response);

         type = ResponseType.NewEntry;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<NewEntryResponse>(response);

         type = ResponseType.Updated;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<UpdatedResponse>(response);

         type = ResponseType.Merged;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<MergedResponse>(response);

         type = ResponseType.Patched;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<PatchedResponse>(response);

         type = ResponseType.Checksum;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<ChecksumResponse>(response);

         type = ResponseType.CopyFile;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<CopyFileResponse>(response);

         type = ResponseType.Removed;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<RemovedResponse>(response);

         type = ResponseType.RemoveEntry;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<RemoveEntryResponse>(response);

         type = ResponseType.SetStaticDirectory;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<SetStaticDirectoryResponse>(response);

         type = ResponseType.ClearStaticDirectory;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<ClearStaticDirectoryResponse>(response);

         type = ResponseType.SetSticky;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<SetStickyResponse>(response);

         type = ResponseType.ClearSticky;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<ClearStickyResponse>(response);

         type = ResponseType.Created;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<CreatedResponse>(response);

         type = ResponseType.UpdateExisting;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<UpdateExistingResponse>(response);

         type = ResponseType.RcsDiff;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<RcsDiffResponse>(response);

         type = ResponseType.Mode;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<ModeResponse>(response);

         type = ResponseType.ModTime;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<ModTimeResponse>(response);

         type = ResponseType.Template;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<TemplateResponse>(response);

         type = ResponseType.Notified;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<NotifiedResponse>(response);

         type = ResponseType.ModuleExpansion;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<ModuleExpansionResponse>(response);

         type = ResponseType.Mbinary;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<MbinaryResponse>(response);

         type = ResponseType.Flush;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<FlushResponse>(response);

         type = ResponseType.Unknown;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<UnknownResponse>(response);
      }

      [Test]
      public void ResponseTypeTest()
      {
         string test = "blah there is no Blah response\n";
         ResponseType result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Unknown, result);

         // auth
         test = "I LOVE YOU\n";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Auth, result);
         test = "I HATE YOU blah blah \n";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Auth, result);
         test = "I LOVE YOU";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Auth, result);

         // Ok
         test = "ok \n";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Ok, result);

         // ErrorRegex,
         test = "error error message";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Error, result);

         // MessageRegex,
         test = "M message";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Message, result);

         // ValidRequestsRegex,
         test = "Valid-requests req1 req2";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.ValidRequests, result);

         // CheckedInRegex,
         test = "Checked-in path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.CheckedIn, result);

         // NewEntryRegex,
         test = "New-entry entry";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.NewEntry, result);

         // UpdatedRegex,
         test = "Updated path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Updated, result);

         // MergedRegex,
         test = "Merged path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Merged, result);

         // PatchedRegex,
         test = "Patched path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Patched, result);

         // ChecksumRegex,
         test = "Checksum 2345";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Checksum, result);

         // CopyFileRegex,
         test = "Copy-file path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.CopyFile, result);

         // RemovedRegex,
         test = "Removed path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Removed, result);

         // RemoveEntryRegex,
         test = "Remove-entry path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.RemoveEntry, result);

         // SetStaticDirectoryRegex,
         test = "Set-static-directory path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.SetStaticDirectory, result);

         // ClearStaticDirectoryRegex,
         test = "Clear-static-directory path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.ClearStaticDirectory, result);

         // SetStickyRegex,
         test = "Set-sticky path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.SetSticky, result);

         // ClearStickyRegex,
         test = "Clear-sticky path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.ClearSticky, result);

         // CreatedRegex,
         test = "Created path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Created, result);

         // MessageTagRegex,
         test = "MT message text";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.MTMessage, result);

         // UpdateExisting,
         test = "Update-existing path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.UpdateExisting, result);

         // RcsDiff,
         test = "Rcs-diff path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.RcsDiff, result);

         // Mode,
         test = "Mode mode";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Mode, result);

         // ModTime,
         test = "Mod-time time";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.ModTime, result);

         // Template,
         test = "Template path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Template, result);

         // Notified,
         test = "Notified path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Notified, result);

         // ModuleExpansion,
         test = "Module-expansion path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.ModuleExpansion, result);

         // Mbinary,
         test = "Mbinary \n";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Mbinary, result);

         // EMessage,
         test = "E message text";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.EMessage, result);

         // Flush
         test = "F message text";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Flush, result);

         // Unknown
         test = "D2001.01.01.00.00.00";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Unknown, result);
      }

      [Test]
      public void CreateCommandTest()
      {
         IRoot root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
         IConnection connection = new PServerConnection();

         // CheckOut
         CommandType type = CommandType.CheckOut;
         string className = "PServerClient.Commands.CheckOutCommand";
         ICommand command = _factory.CreateCommand(className, new object[] { root, connection });
         Assert.AreEqual(type, command.Type);

         // Import
         type = CommandType.Import;
         className = "PServerClient.Commands.ImportCommand";
         command = _factory.CreateCommand(className, new object[] { root, connection });
         Assert.AreEqual(type, command.Type);

         // ValidRequestsList
         type = CommandType.ValidRequestsList;
         className = "PServerClient.Commands.ValidRequestsListCommand";
         command = _factory.CreateCommand(className, new object[] { root, connection });
         Assert.AreEqual(type, command.Type);

         // VerifyAuth
         type = CommandType.VerifyAuth;
         className = "PServerClient.Commands.VerifyAuthCommand";
         command = _factory.CreateCommand(className, new object[] { root, connection });
         Assert.AreEqual(type, command.Type);

         // Version
         type = CommandType.Version;
         className = "PServerClient.Commands.VersionCommand";
         command = _factory.CreateCommand(className, new object[] { root, connection });
         Assert.AreEqual(type, command.Type);

         // Add
         type = CommandType.Add;
         className = "PServerClient.Commands.AddCommand";
         command = _factory.CreateCommand(className, new object[] { root, connection });
         Assert.AreEqual(type, command.Type);

         // Export
         type = CommandType.Export;
         className = "PServerClient.Commands.ExportCommand";
         command = _factory.CreateCommand(className, new object[] { root, connection, DateTime.Now });
         Assert.AreEqual(type, command.Type);

         // Log
         type = CommandType.Log;
         className = "PServerClient.Commands.LogCommand";
         command = _factory.CreateCommand(className, new object[] { root, connection });
         Assert.AreEqual(type, command.Type);

         // Diff
         type = CommandType.Diff;
         className = "PServerClient.Commands.DiffCommand";
         command = _factory.CreateCommand(className, new object[] { root, connection });
         Assert.AreEqual(type, command.Type);

         // Tag
         type = CommandType.Tag;
         className = "PServerClient.Commands.TagCommand";
         command = _factory.CreateCommand(className, new object[] { root, connection });
         Assert.AreEqual(type, command.Type);
      }

      [Test]
      public void CreateCommandFromXMLTest()
      {
         string xml = TestStrings.CommandXMLFileWithManyItems;
         XDocument xdoc = XDocument.Parse(xml);
         bool result = TestHelper.ValidateCommandXML(xdoc);
         Assert.IsTrue(result);
         IRoot root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);

         PServerFactory factory = new PServerFactory();
         IConnection connection = new PServerConnection();
         ICommand cmd = factory.CreateCommand(xdoc, new object[] { root, connection });
         Assert.IsInstanceOf<CheckOutCommand>(cmd);

         Assert.AreEqual(2, cmd.RequiredRequests.Count);
         Assert.AreEqual(3, cmd.Requests.Count);

         AuthRequest authRequest = cmd.RequiredRequests.OfType<AuthRequest>().FirstOrDefault();
         Assert.IsNotNull(authRequest);
         AuthResponse authResponse = cmd.Responses.OfType<AuthResponse>().FirstOrDefault();
         Assert.IsNotNull(authResponse);
         Assert.AreEqual(AuthStatus.Authenticated, authResponse.Status);
      }

      [Test]
      public void CreateRequestFromLinesTest()
      {
         PServerFactory factory = new PServerFactory();
         IList<string> lines = new List<string> { "line 1", "line 2" };
         for (int i = 0; i < 62; i++)
         {
            RequestType requestType = (RequestType)i;
            string className = string.Format("PServerClient.Requests.{0}Request", requestType);
            IRequest request = factory.CreateRequest(className, lines);
            Assert.AreEqual(requestType, request.Type);
            Assert.AreEqual(2, request.Lines.Count());
            Assert.AreEqual("line 1", request.Lines[0]);
            Assert.AreEqual("line 2", request.Lines[1]);
         }
      }

      [Test]
      public void CreateResponseByClassNameTest()
      {
         for (int i = 0; i < 32; i++)
         {
            ResponseType type = (ResponseType)i;
            string className = string.Format("PServerClient.Responses.{0}Response", type);
            IResponse response = _factory.CreateResponse(className);
            Assert.AreEqual(type, response.Type);
         }
      }

      [Test]
      public void CreateRequestFromTypeTest()
      {
         // Add
         RequestType type = RequestType.Add;
         IRequest request = _factory.CreateRequest(type, new object[0]);
         Assert.IsInstanceOf<AddRequest>(request);

         // Admin
         type = RequestType.Admin;
         request = _factory.CreateRequest(type, new object[0]);
         Assert.IsInstanceOf<AdminRequest>(request);

         // Annotate
         type = RequestType.Annotate;
         request = _factory.CreateRequest(type, new object[0]);
         Assert.IsInstanceOf<AnnotateRequest>(request);

         // Argument
         type = RequestType.Argument;
         request = _factory.CreateRequest(type, new object[] { "-a" });
         Assert.IsInstanceOf<ArgumentRequest>(request);

         // Argumentx
         type = RequestType.Argumentx;
         request = _factory.CreateRequest(type, new object[] { "-a" });
         Assert.IsInstanceOf<ArgumentxRequest>(request);

         // Auth
         type = RequestType.Auth;
         request = _factory.CreateRequest(type, new object[] { _root });
         Assert.IsInstanceOf<AuthRequest>(request);

         // Case
         type = RequestType.Case;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<CaseRequest>(request);

         // CheckIn
         type = RequestType.CheckIn;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<CheckInRequest>(request);

         // CheckinTime
         type = RequestType.CheckinTime;
         var checkinTime = new DateTime(2009, 11, 6, 14, 21, 8);
         request = _factory.CreateRequest(type, new object[] { checkinTime });
         Assert.IsInstanceOf<CheckinTimeRequest>(request);

         // CheckOut
         type = RequestType.CheckOut;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<CheckOutRequest>(request);

         // Diff
         type = RequestType.Diff;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<DiffRequest>(request);

         // Directory
         type = RequestType.Directory;
         request = _factory.CreateRequest(type, new object[] { ".", _root.Repository + "/" + _root.Module });
         Assert.IsInstanceOf<DirectoryRequest>(request);

         // Editors
         type = RequestType.Editors;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<EditorsRequest>(request);

         // EmptyConflicts
         type = RequestType.EmptyConflicts;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<EmptyConflictsRequest>(request);

         // Entry
         type = RequestType.Entry;
         request = _factory.CreateRequest(type, new object[] { "file.cs", "1.1.1", "a", "b", "c" });
         Assert.IsInstanceOf<EntryRequest>(request);

         // ExpandModules
         type = RequestType.ExpandModules;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<ExpandModulesRequest>(request);

         // Export
         type = RequestType.Export;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<ExportRequest>(request);

         // GlobalOption
         type = RequestType.GlobalOption;
         request = _factory.CreateRequest(type, new object[] { "-o" });
         Assert.IsInstanceOf<GlobalOptionRequest>(request);

         // GssapiAuthenticate
         type = RequestType.GssapiAuthenticate;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<GssapiAuthenticateRequest>(request);

         // GssapiEncrypt
         type = RequestType.GssapiEncrypt;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<GssapiEncryptRequest>(request);

         // GzipFileContents
         type = RequestType.GzipFileContents;
         request = _factory.CreateRequest(type, new object[] { "1" });
         Assert.IsInstanceOf<GzipFileContentsRequest>(request);

         // GzipStream
         type = RequestType.GzipStream;
         request = _factory.CreateRequest(type, new object[] { "1" });
         Assert.IsInstanceOf<GzipStreamRequest>(request);

         // History
         type = RequestType.History;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<HistoryRequest>(request);

         // Import
         type = RequestType.Import;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<ImportRequest>(request);

         // Init
         type = RequestType.Init;
         request = _factory.CreateRequest(type, new object[] { "sandbox" });
         Assert.IsInstanceOf<InitRequest>(request);

         // IsModified
         type = RequestType.IsModified;
         request = _factory.CreateRequest(type, new object[] { "file.cs" });
         Assert.IsInstanceOf<IsModifiedRequest>(request);

         // KerberosEncrypt
         type = RequestType.KerberosEncrypt;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<KerberosEncryptRequest>(request);

         // Kopt
         type = RequestType.Kopt;
         request = _factory.CreateRequest(type, new object[] { "-kb" }); 
         Assert.IsInstanceOf<KoptRequest>(request);

         // Log
         type = RequestType.Log;
         request = _factory.CreateRequest(type, new object[] { }); 
         Assert.IsInstanceOf<LogRequest>(request);

         // Lost
         type = RequestType.Lost;
         request = _factory.CreateRequest(type, new object[] { "file.cs" }); 
         Assert.IsInstanceOf<LostRequest>(request);

         // MaxDot
         type = RequestType.MaxDot;
         request = _factory.CreateRequest(type, new object[] { "one" });
         Assert.IsInstanceOf<MaxDotRequest>(request);

         // Modified
         type = RequestType.Modified;
         request = _factory.CreateRequest(type, new object[] { "file.cs", "u=rw,g=rw,o=rw", 6 });
         Assert.IsInstanceOf<ModifiedRequest>(request);

         // Noop
         type = RequestType.Noop;
         request = _factory.CreateRequest(type, new object[] { }); 
         Assert.IsInstanceOf<NoopRequest>(request);

         // Notify
         type = RequestType.Notify;
         request = _factory.CreateRequest(type, new object[] { "file.cs" }); 
         Assert.IsInstanceOf<NotifyRequest>(request);

         // Questionable 
         type = RequestType.Questionable;
         request = _factory.CreateRequest(type, new object[] { "file.cs" }); 
         Assert.IsInstanceOf<QuestionableRequest>(request);

         // RAnnotate
         type = RequestType.RAnnotate;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<RAnnotateRequest>(request);

         // RDiff
         type = RequestType.RDiff;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<RDiffRequest>(request);

         // Release
         type = RequestType.Release;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<ReleaseRequest>(request);

         // Remove
         type = RequestType.Remove;
         request = _factory.CreateRequest(type, new object[] { }); 
         Assert.IsInstanceOf<RemoveRequest>(request);

         // Repository
         type = RequestType.Repository;
         request = _factory.CreateRequest(type, new object[] { _root.Repository }); 
         Assert.IsInstanceOf<RepositoryRequest>(request);

         // RLog
         type = RequestType.RLog;
         request = _factory.CreateRequest(type, new object[] { }); 
         Assert.IsInstanceOf<RLogRequest>(request);

         // Root
         type = RequestType.Root;
         request = _factory.CreateRequest(type, new object[] { _root.Repository });
         Assert.IsInstanceOf<RootRequest>(request);

         // RTag
         type = RequestType.RTag;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<RTagRequest>(request);

         // Set
         type = RequestType.Set;
         request = _factory.CreateRequest(type, new object[] { "rabbit", "Peter" });
         Assert.IsInstanceOf<SetRequest>(request);

         // StaticDirectory
         type = RequestType.StaticDirectory;
         request = _factory.CreateRequest(type, new object[] { }); 
         Assert.IsInstanceOf<StaticDirectoryRequest>(request);

         // Status
         type = RequestType.Status;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<StatusRequest>(request);

         // Sticky
         type = RequestType.Sticky;
         request = _factory.CreateRequest(type, new object[] { "idk" });
         Assert.IsInstanceOf<StickyRequest>(request);

         // Tag
         type = RequestType.Tag;
         request = _factory.CreateRequest(type, new object[] { }); 
         Assert.IsInstanceOf<TagRequest>(request);

         // Unchanged
         type = RequestType.Unchanged;
         request = _factory.CreateRequest(type, new object[] { "file.cs" }); 
         Assert.IsInstanceOf<UnchangedRequest>(request);

         // UpdatePatches 
         type = RequestType.UpdatePatches;
         request = _factory.CreateRequest(type, new object[] { }); 
         Assert.IsInstanceOf<UpdatePatchesRequest>(request);

         // Update 
         type = RequestType.Update;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<UpdateRequest>(request);

         // UseUnchanged 
         type = RequestType.UseUnchanged;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<UseUnchangedRequest>(request);

         // ValidRequests
         type = RequestType.ValidRequests;
         request = _factory.CreateRequest(type, new object[] { }); 
         Assert.IsInstanceOf<ValidRequestsRequest>(request);

         // ValidResponses
         type = RequestType.ValidResponses;
         request = _factory.CreateRequest(type, new object[] { new[] { ResponseType.Ok, ResponseType.MTMessage, ResponseType.EMessage } });
         Assert.IsInstanceOf<ValidResponsesRequest>(request);

         // VerifyAuth 
         type = RequestType.VerifyAuth;
         request = _factory.CreateRequest(type, new object[] { _root }); 
         Assert.IsInstanceOf<VerifyAuthRequest>(request);

         // Version 
         type = RequestType.Version;
         request = _factory.CreateRequest(type, new object[] { }); 
         Assert.IsInstanceOf<VersionRequest>(request);

         // WatchAdd
         type = RequestType.WatchAdd;
         request = _factory.CreateRequest(type, new object[] { }); 
         Assert.IsInstanceOf<WatchAddRequest>(request);

         // Watchers 
         type = RequestType.Watchers;
         request = _factory.CreateRequest(type, new object[] { });
         Assert.IsInstanceOf<WatchersRequest>(request);

         // WatchOff
         type = RequestType.WatchOff;
         request = _factory.CreateRequest(type, new object[] { }); 
         Assert.IsInstanceOf<WatchOffRequest>(request);

         // WatchOn
         type = RequestType.WatchOn;
         request = _factory.CreateRequest(type, new object[] { }); 
         Assert.IsInstanceOf<WatchOnRequest>(request);

         // WatchRemove
         type = RequestType.WatchRemove;
         request = _factory.CreateRequest(type, new object[] { }); 
         Assert.IsInstanceOf<WatchRemoveRequest>(request);

         // WrapperSendmeRcsOptions = 61
         type = RequestType.WrapperSendmeRcsOptions;
         request = _factory.CreateRequest(type, new object[] { }); 
         Assert.IsInstanceOf<WrapperSendmeRcsOptionsRequest>(request);
      }
   }
}