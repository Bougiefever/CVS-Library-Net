using System;
using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Tests.TestSetup;

namespace PServerClient.Tests
{
   [TestFixture]
   public class RequestTests
   {
      // ReSharper disable PossibleNullReferenceException

      // ReSharper disable ConvertToConstant.Local
      private readonly IRoot _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);

      public RequestTests()
      {
         _root.Module = "mod1";
      }

      [Test]
      public void AddRequestTest()
      {
         RequestType type = RequestType.Add;
         IRequest request = new AddRequest();
          string expected = "add\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void AdminRequestTest()
      {
         RequestType type = RequestType.Admin;
         IRequest request = new AdminRequest();
          string expected = "admin\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void AnnotateRequestTest()
      {
         RequestType type = RequestType.Annotate;
         IRequest request = new AnnotateRequest();
          string expected = "annotate\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void ArgumentRequestTest()
      {
         RequestType type = RequestType.Argument;
         IRequest request = new ArgumentRequest("-a");
          string expected = "Argument -a\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void ArgumentxRequestTest()
      {
         RequestType type = RequestType.Argumentx;
         IRequest request = new ArgumentxRequest("-a");
          string expected = "Argumentx -a\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void AuthRequestTest()
      {
         RequestType type = RequestType.Auth;
         IAuthRequest request = new AuthRequest(_root);
          string expected = "BEGIN AUTH REQUEST\n/f1/f2/f3\nusername\nA:yZZ30 e\nEND AUTH REQUEST\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void CaseRequestTest()
      {
         RequestType type = RequestType.Case;
         IRequest request = new CaseRequest();
          string expected = "Case\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void CheckInRequestTest()
      {
         RequestType type = RequestType.CheckIn;
         IRequest request = new CheckInRequest();
          string expected = "ci\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void CheckinTimeRequestTest()
      {
         RequestType type = RequestType.CheckinTime;
         var checkinTime = new DateTime(2009, 11, 6, 14, 21, 8);
         IRequest request = new CheckinTimeRequest(checkinTime);
          string expected = "Checkin-time 06 Nov 2009 14:21:08 -0000\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void CheckoutRequestTest()
      {
         RequestType type = RequestType.CheckOut;
         IRequest request = new CheckOutRequest();
          string expected = "co\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void DiffRequestTest()
      {
         RequestType type = RequestType.Diff;
         IRequest request = new DiffRequest();
          string expected = "diff\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void DirectoryRequestTest()
      {
         RequestType type = RequestType.Directory;
         IRequest request = new DirectoryRequest(".", _root.Repository + "/" + _root.Module);
          string expected = "Directory .\n/f1/f2/f3/mod1\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void EditorsRequestTest()
      {
         RequestType type = RequestType.Editors;
         IRequest request = new EditorsRequest();
          string expected = "editors\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void EmptyConflictsRequestTest()
      {
         RequestType type = RequestType.EmptyConflicts;
         IRequest request = new EmptyConflictsRequest();
          string expected = "Empty-conflicts\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void EntryRequestTest()
      {
         RequestType type = RequestType.Entry;
         IRequest request = new EntryRequest("file.cs", "1.1.1", "a", "b", "c");
          string expected = "Entry /file.cs/1.1.1/a/b/c\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void ExpandModulesRequestTest()
      {
         RequestType type = RequestType.ExpandModules;
         IRequest request = new ExpandModulesRequest();
          string expected = "expand-modules\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void ExportRequestTest()
      {
         RequestType type = RequestType.Export;
         IRequest request = new ExportRequest();
          string expected = "export\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void GlobalOptionRequestTest()
      {
         RequestType type = RequestType.GlobalOption;
         IRequest request = new GlobalOptionRequest("-o");
          string expected = "Global_option -o\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void GssapiAuthenticateRequestTest()
      {
         RequestType type = RequestType.GssapiAuthenticate;
         IRequest request = new GssapiAuthenticateRequest();
          string expected = "Gssapi-authenticate\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void GssapiEncryptRequestTest()
      {
         RequestType type = RequestType.GssapiEncrypt;
         IRequest request = new GssapiEncryptRequest();
          string expected = "Gssapi-encrypt\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void GzipFileContentsRequestTest()
      {
         RequestType type = RequestType.GzipFileContents;
         IRequest request = new GzipFileContentsRequest("1");
          string expected = "gzip-file-contents 1\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void GzipStreamRequestTest()
      {
         RequestType type = RequestType.GzipStream;
         IRequest request = new GzipStreamRequest("1");
         string expected = "Gzip-stream 1\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void HistoryRequestTest()
      {
         RequestType type = RequestType.History;
         IRequest request = new HistoryRequest();
         string expected = "history\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void ImportRequestTest()
      {
         RequestType type = RequestType.Import;
         IRequest request = new ImportRequest();
         string expected = "import\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void InitRequestTest()
      {
         RequestType type = RequestType.Init;
         IRequest request = new InitRequest("sandbox");
         string expected = "init sandbox\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void IsModifiedRequestTest()
      {
         RequestType type = RequestType.IsModified;
         IRequest request = new IsModifiedRequest("file.cs");
         string expected = "Is-modified file.cs\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void KerberosEncryptRequestTest()
      {
         RequestType type = RequestType.KerberosEncrypt;
         IRequest request = new KerberosEncryptRequest();
         string expected = "Kerberos-encrypt\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void KoptRequestTest()
      {
         RequestType type = RequestType.Kopt;
         IRequest request = new KoptRequest("-kb");
         string expected = "Kopt -kb\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void LogRequestTest()
      {
         RequestType type = RequestType.Log;
         IRequest request = new LogRequest();
         string expected = "log\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void LostRequestTest()
      {
         RequestType type = RequestType.Lost;
         IRequest request = new LostRequest("file.cs");
         string expected = "Lost file.cs\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void MaxDotRequestTest()
      {
         RequestType type = RequestType.MaxDot;
         IRequest request = new MaxDotRequest("one");
         string expected = "Max-dotdot one\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void ModifiedRequestTest()
      {
         RequestType type = RequestType.Modified;
         IRequest request = new ModifiedRequest("file.cs", "u=rw,g=rw,o=rw", 6);
         string expected = "Modified file.cs\nu=rw,g=rw,o=rw\n6\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void NoopRequestTest()
      {
         RequestType type = RequestType.Noop;
         IRequest request = new NoopRequest();
         string expected = "noop\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void NotifyRequestTest()
      {
         RequestType type = RequestType.Notify;
         IRequest request = new NotifyRequest("file.cs");
         string expected = "Notify file.cs\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void QuestionableRequestTest()
      {
         RequestType type = RequestType.Questionable;
         IRequest request = new QuestionableRequest("file.cs");
         string expected = "Questionable file.cs\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void RAnnotateRequestTest()
      {
         RequestType type = RequestType.RAnnotate;
         IRequest request = new RAnnotateRequest();
         string expected = "rannotate\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void RDiffRequestTest()
      {
         RequestType type = RequestType.RDiff;
         IRequest request = new RDiffRequest();
         string expected = "rdiff\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void ReleaseRequestTest()
      {
         RequestType type = RequestType.Release;
         IRequest request = new ReleaseRequest();
         string expected = "release\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void RemoveRequestTest()
      {
         RequestType type = RequestType.Remove;
         IRequest request = new RemoveRequest();
         string expected = "remove\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void RepositoryRequestTest()
      {
         RequestType type = RequestType.Repository;
         IRequest request = new RepositoryRequest(_root.Repository);
         string expected = "Repository /f1/f2/f3\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void RequestsToTypesTest()
      {
         string requests = "ci Global_option Is-modified";
         IList<RequestType> types = RequestHelper.RequestsToRequestTypes(requests);
         Assert.AreEqual(RequestType.CheckIn, types[0]);
         Assert.AreEqual(RequestType.GlobalOption, types[1]);
         Assert.AreEqual(RequestType.IsModified, types[2]);
      }

      [Test]
      public void RLogRequestTest()
      {
         RequestType type = RequestType.RLog;
         IRequest request = new RLogRequest();
         string expected = "rlog\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void RootRequestTest()
      {
         RequestType type = RequestType.Root;
         IRequest request = new RootRequest(_root.Repository);
         string expected = "Root /f1/f2/f3\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void RTagRequestTest()
      {
         RequestType type = RequestType.RTag;
         IRequest request = new RTagRequest();
         string expected = "rtag\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void SetRequestTest()
      {
         RequestType type = RequestType.Set;
         IRequest request = new SetRequest("rabbit", "Peter");
         string expected = "Set rabbit=Peter\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void StaticDirectoryRequestTest()
      {
         RequestType type = RequestType.StaticDirectory;
         IRequest request = new StaticDirectoryRequest();
         string expected = "Static-directory\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void StatusRequestTest()
      {
         RequestType type = RequestType.Status;
         IRequest request = new StatusRequest();
         string expected = "status\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void StickyRequestTest()
      {
         RequestType type = RequestType.Sticky;
         IRequest request = new StickyRequest("idk");
         string expected = "Sticky idk\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void TagRequestTest()
      {
         RequestType type = RequestType.Tag;
         IRequest request = new TagRequest();
         string expected = "tag\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void UnchangedRequestTest()
      {
         RequestType type = RequestType.Unchanged;
         IRequest request = new UnchangedRequest("file.cs");
         string expected = "Unchanged file.cs\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void UpdatePatchesTest()
      {
         RequestType type = RequestType.UpdatePatches;
         IRequest request = new UpdatePatchesRequest();
         string expected = "update-patches\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void UpdateRequestTest()
      {
         RequestType type = RequestType.Update;
         IRequest request = new UpdateRequest();
         string expected = "update\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void UseUnchangedRequestTest()
      {
         RequestType type = RequestType.UseUnchanged;
         IRequest request = new UseUnchangedRequest();
         string expected = "UseUnchanged\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void ValidRequestsRequestTest()
      {
         RequestType type = RequestType.ValidRequests;
         IRequest request = new ValidRequestsRequest();
         string expected = "valid-requests\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void ValidResponsesRequestTest()
      {
         RequestType type = RequestType.ValidResponses;
         IRequest request = new ValidResponsesRequest(new[] { ResponseType.Ok, ResponseType.MTMessage, ResponseType.EMessage });
         string expected = "Valid-responses ok MT E\n";
         RequestTest(type, request, expected, false);
      }

      [Test]
      public void VerifyAuthRequestTest()
      {
         RequestType type = RequestType.VerifyAuth;
         IRequest request = new VerifyAuthRequest(_root);
         string expected = "BEGIN VERIFICATION REQUEST\n/f1/f2/f3\nusername\nA:yZZ30 e\nEND VERIFICATION REQUEST\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void VersionRequestTest()
      {
         RequestType type = RequestType.Version;
         IRequest request = new VersionRequest();
         string expected = "version\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void WatchAddRequestTest()
      {
         RequestType type = RequestType.WatchAdd;
         IRequest request = new WatchAddRequest();
         string expected = "watch-add\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void WatchersRequestTest()
      {
         RequestType type = RequestType.Watchers;
         IRequest request = new WatchersRequest();
         string expected = "watchers\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void WatchOffRequestTest()
      {
         RequestType type = RequestType.WatchOff;
         IRequest request = new WatchOffRequest();
         string expected = "watch-off\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void WatchOnRequestTest()
      {
         RequestType type = RequestType.WatchOn;
         IRequest request = new WatchOnRequest();
         string expected = "watch-on\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void WatchRemoveRequestTest()
      {
         RequestType type = RequestType.WatchRemove;
         IRequest request = new WatchRemoveRequest();
         string expected = "watch-remove\n";
         RequestTest(type, request, expected, true);
      }

      [Test]
      public void WrapperSendmeRcsOptionsRequestTest()
      {
         RequestType type = RequestType.WrapperSendmeRcsOptions;
         IRequest request = new WrapperSendmeRcsOptionsRequest();
         string expected = "wrapper-sendme-rcsOptions\n";
         RequestTest(type, request, expected, true);
      }

      private void RequestTest(RequestType type, IRequest request, string requestString, bool responseExpected)
      {
         string actual = request.GetRequestString();
         Assert.AreEqual(requestString, actual);
         Assert.AreEqual(responseExpected, request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = request.GetXElement();
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }
   }
}