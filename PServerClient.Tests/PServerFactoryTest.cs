using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;

namespace PServerClient.Tests
{
   [TestFixture]
   public class PServerFactoryTest
   {
      private PServerFactory _factory;

      [SetUp]
      public void SetUp()
      {
         _factory = new PServerFactory();
      }

      [Test]
      public void CreateResponsesTest()
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

         type = ResponseType.MessageTag;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<MessageTagResponse>(response);

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

         type = ResponseType.Null;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<NullResponse>(response);
      }

      [Test]
      public void ResponseTypeTest()
      {
         string test = "blah there is no Blah response\n";
         ResponseType result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Null, result);

         // auth
         test = "I LOVE YOU\n";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Auth, result);
         test = "I HATE YOU blah blah \n";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Auth, result);

         //Ok
         test = "ok \n";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Ok, result);

         //ErrorRegex,
         test = "error error message";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Error, result);

         //MessageRegex,
         test = "M message";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Message, result);

         //ValidRequestsRegex,
         test = "Valid-requests req1 req2";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.ValidRequests, result);

         //CheckedInRegex,
         test = "Checked-in path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.CheckedIn, result);

         //NewEntryRegex,
         test = "New-entry entry";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.NewEntry, result);

         //UpdatedRegex,
         test = "Updated path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Updated, result);

         //MergedRegex,
         test = "Merged path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Merged, result);

         //PatchedRegex,
         test = "Patched path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Patched, result);

         //ChecksumRegex,
         test = "Checksum 2345";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Checksum, result);

         //CopyFileRegex,
         test = "Copy-file path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.CopyFile, result);

         //RemovedRegex,
         test = "Removed path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Removed, result);

         //RemoveEntryRegex,
         test = "Remove-entry path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.RemoveEntry, result);

         //SetStaticDirectoryRegex,
         test = "Set-static-directory path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.SetStaticDirectory, result);

         //ClearStaticDirectoryRegex,
         test = "Clear-static-directory path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.ClearStaticDirectory, result);

         //SetStickyRegex,
         test = "Set-sticky path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.SetSticky, result);

         //ClearStickyRegex,
         test = "Clear-sticky path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.ClearSticky, result);

         //CreatedRegex,
         test = "Created path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Created, result);

         //MessageTagRegex,
         test = "MT message text";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.MessageTag, result);

         //UpdateExisting,
         test = "Update-existing path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.UpdateExisting, result);

         //RcsDiff,
         test = "Rcs-diff path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.RcsDiff, result);

         //Mode,
         test = "Mode mode";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Mode, result);

         //ModTime,
         test = "Mod-time time";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.ModTime, result);

         //Template,
         test = "Template path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Template, result);

         //Notified,
         test = "Notified path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Notified, result);

         //ModuleExpansion,
         test = "Module-expansion path";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.ModuleExpansion, result);

         //Mbinary,
         test = "Mbinary \n";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Mbinary, result);

         //EMessage,
         test = "E message text";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.EMessage, result);

         //Flush
         test = "F message text";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.Flush, result);
      }

      [Test]
      public void CreateCommandsTest()
      {
         //Checkout command
         Root root = new Root(TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password, TestConfig.RepositoryPath);
         CommandType type = CommandType.CheckOut;
         ICommand command = _factory.CreateCommand(type, new object[] { root });
         Assert.IsInstanceOf<CheckOutCommand>(command);

         //Import command
         type = CommandType.Import;
         command = _factory.CreateCommand(type, new object[] { root });
         Assert.IsInstanceOf<ImportCommand>(command);

         //Log command
         type = CommandType.Log;
         command = _factory.CreateCommand(type, new object[] { root });
         Assert.IsInstanceOf<LogCommand>(command);

         //ValidRequestList command
         type = CommandType.ValidRequestsList;
         command = _factory.CreateCommand(type, new object[] { root });
         Assert.IsInstanceOf<ValidRequestsListCommand>(command);

         //VerifyAuth command
         type = CommandType.VerifyAuth;
         command = _factory.CreateCommand(type, new object[] { root });
         Assert.IsInstanceOf<VerifyAuthCommand>(command);

         //Version command
         type = CommandType.Version;
         command = _factory.CreateCommand(type, new object[] { root });
         Assert.IsInstanceOf<VersionCommand>(command);
      }
   }
}