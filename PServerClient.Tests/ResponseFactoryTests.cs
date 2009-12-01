using NUnit.Framework;
using PServerClient.Responses;
using System.Collections.Generic;

namespace PServerClient.Tests
{
   [TestFixture]
   public class ResponseFactoryTests
   {
      private ResponseFactory _factory = new ResponseFactory();

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
         Assert.AreEqual(ResponseType.CheckSum, result);

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

         //FMessage
         test = "F message text";
         result = _factory.GetResponseType(test);
         Assert.AreEqual(ResponseType.FMessage, result);
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
         Assert.IsInstanceOf<ErrorResponse>(response);

         type = ResponseType.Message;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<MessageResponse>(response);

         type = ResponseType.MessageTag;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<MessageTagResponse>(response);

         type = ResponseType.ValidRequests;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<ValidRequestResponse>(response);

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

         type = ResponseType.CheckSum;
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

         type = ResponseType.FMessage;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<FMessageResponse>(response);

         type = ResponseType.Unknown;
         response = _factory.CreateResponse(type);
         Assert.IsInstanceOf<NullResponse>(response);
      }

      [Test]
      public void GetResponseLinesOneLineTest()
      {
         string test = "M my message";
         IList<string> lines = new List<string>() { "preceding string", test, "another string" };
         IList<string> result = _factory.GetResponseLines(ResponseType.Message, lines, 1, 1);
         Assert.AreEqual(1, result.Count);
         Assert.AreEqual("my message", result[0]);
      }

      [Test]
      public void GetResponseLinesLineCountLinesTest()
      {
         IList<string> lines = new List<string>() { "Updated line 1", "line 2", "line 3", "another command" };
         IList<string> result = _factory.GetResponseLines(ResponseType.Updated, lines, 3, 0);
         Assert.AreEqual(3, result.Count);
         Assert.AreEqual("line 1", result[0]);
         Assert.AreEqual("line 2", result[1]);
         Assert.AreEqual("line 3", result[2]);
      }

      [Test]
      public void GetResponseLinesUnknownLineCountTest()
      {
         IList<string> lines = new List<string>() { "MT line 1", "MT line 2", "MT line 3", "another command" };
         IList<string> result = _factory.GetResponseLines(ResponseType.MessageTag, lines, 0, 0);
         Assert.AreEqual(3, result.Count);
         Assert.AreEqual("line 1", result[0]);
         Assert.AreEqual("line 2", result[1]);
         Assert.AreEqual("line 3", result[2]);
      }

      [Test]
      public void GetResponsesTest()
      {
         IList<string> lines = new List<string>() 
         {
            "I LOVE YOU ",
            "Valid-requests Root Valid-responses valid-requests Repository Directory",
            "ok ",
            "Module-expansion abougie",
            "Clear-sticky abougie/",
            "/usr/local/cvsroot/sandbox/abougie/",
            "Clear-static-directory abougie/",
            "/usr/local/cvsroot/sandbox/abougie/",
            "Mod-time 27 Nov 2009 14:21:06 -0000",
            "MT +updated",
            "MT text U ",
            "MT fname abougie/.cvspass",
            "MT newline",
            "MT -updated",
            "Updated abougie/",
            "/usr/local/cvsroot/sandbox/abougie/.cvspass",
            "/.cvspass/1.1.1.1///",
            "u=rw,g=rw,o=rw",
            "74",
            "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w"
         };
         IList<IResponse> responses = _factory.CreateResponses(lines);

      }
   }
}
