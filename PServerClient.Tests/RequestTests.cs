using System;
using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Tests.TestSetup;

namespace PServerClient.Tests
{
   /// <summary>
   /// Test of Requests classes
   /// </summary>
   [TestFixture]
   public class RequestTests
   {
      // ReSharper disable PossibleNullReferenceException

      // ReSharper disable ConvertToConstant.Local
      private readonly IRoot _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);

      /// <summary>
      /// Sets up the test data
      /// </summary>
      [SetUp]
      public void SetUp()
      {
         _root.Module = "mod1";
      }

      /// <summary>
      /// Tests the add request.
      /// </summary>
      [Test]
      public void TestAddRequest()
      {
         RequestType type = RequestType.Add;
         IRequest request = new AddRequest();
          string expected = "add\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the admin request.
      /// </summary>
      [Test]
      public void TestAdminRequest()
      {
         RequestType type = RequestType.Admin;
         IRequest request = new AdminRequest();
          string expected = "admin\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the annotate request.
      /// </summary>
      [Test]
      public void TestAnnotateRequest()
      {
         RequestType type = RequestType.Annotate;
         IRequest request = new AnnotateRequest();
          string expected = "annotate\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the argument request.
      /// </summary>
      [Test]
      public void TestArgumentRequest()
      {
         RequestType type = RequestType.Argument;
         IRequest request = new ArgumentRequest("-a");
          string expected = "Argument -a\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the argumentx request.
      /// </summary>
      [Test]
      public void TestArgumentxRequest()
      {
         RequestType type = RequestType.Argumentx;
         IRequest request = new ArgumentxRequest("-a");
          string expected = "Argumentx -a\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the auth request.
      /// </summary>
      [Test]
      public void TestAuthRequest()
      {
         RequestType type = RequestType.Auth;
         IAuthRequest request = new AuthRequest(_root);
          string expected = "BEGIN AUTH REQUEST\n/f1/f2/f3\nusername\nA:yZZ30 e\nEND AUTH REQUEST\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the case request.
      /// </summary>
      [Test]
      public void TestCaseRequest()
      {
         RequestType type = RequestType.Case;
         IRequest request = new CaseRequest();
          string expected = "Case\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the check in request.
      /// </summary>
      [Test]
      public void TestCheckInRequest()
      {
         RequestType type = RequestType.CheckIn;
         IRequest request = new CheckInRequest();
          string expected = "ci\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the checkin time request.
      /// </summary>
      [Test]
      public void TestCheckinTimeRequest()
      {
         RequestType type = RequestType.CheckinTime;
         var checkinTime = new DateTime(2009, 11, 6, 14, 21, 8);
         IRequest request = new CheckinTimeRequest(checkinTime);
          string expected = "Checkin-time 06 Nov 2009 14:21:08 -0000\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the checkout request.
      /// </summary>
      [Test]
      public void TestCheckoutRequest()
      {
         RequestType type = RequestType.CheckOut;
         IRequest request = new CheckOutRequest();
          string expected = "co\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the diff request.
      /// </summary>
      [Test]
      public void TestDiffRequest()
      {
         RequestType type = RequestType.Diff;
         IRequest request = new DiffRequest();
          string expected = "diff\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the directory request.
      /// </summary>
      [Test]
      public void TestDirectoryRequest()
      {
         RequestType type = RequestType.Directory;
         IRequest request = new DirectoryRequest(".", _root.Repository + "/" + _root.Module);
          string expected = "Directory .\n/f1/f2/f3/mod1\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the editors request.
      /// </summary>
      [Test]
      public void TestEditorsRequest()
      {
         RequestType type = RequestType.Editors;
         IRequest request = new EditorsRequest();
          string expected = "editors\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the empty conflicts request.
      /// </summary>
      [Test]
      public void TestEmptyConflictsRequest()
      {
         RequestType type = RequestType.EmptyConflicts;
         IRequest request = new EmptyConflictsRequest();
          string expected = "Empty-conflicts\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the entry request.
      /// </summary>
      [Test]
      public void TestEntryRequest()
      {
         RequestType type = RequestType.Entry;
         IRequest request = new EntryRequest("file.cs", "1.1.1", "a", "b", "c");
          string expected = "Entry /file.cs/1.1.1/a/b/c\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the expand modules request.
      /// </summary>
      [Test]
      public void TestExpandModulesRequest()
      {
         RequestType type = RequestType.ExpandModules;
         IRequest request = new ExpandModulesRequest();
          string expected = "expand-modules\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the export request.
      /// </summary>
      [Test]
      public void TestExportRequest()
      {
         RequestType type = RequestType.Export;
         IRequest request = new ExportRequest();
          string expected = "export\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the global option request.
      /// </summary>
      [Test]
      public void TestGlobalOptionRequest()
      {
         RequestType type = RequestType.GlobalOption;
         IRequest request = new GlobalOptionRequest(GlobalOption.Quiet);
          string expected = "Global_option -q\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the gssapi authenticate request.
      /// </summary>
      [Test]
      public void TestGssapiAuthenticateRequest()
      {
         RequestType type = RequestType.GssapiAuthenticate;
         IRequest request = new GssapiAuthenticateRequest();
          string expected = "Gssapi-authenticate\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the gssapi encrypt request.
      /// </summary>
      [Test]
      public void TestGssapiEncryptRequest()
      {
         RequestType type = RequestType.GssapiEncrypt;
         IRequest request = new GssapiEncryptRequest();
          string expected = "Gssapi-encrypt\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the gzip file contents request.
      /// </summary>
      [Test]
      public void TestGzipFileContentsRequest()
      {
         RequestType type = RequestType.GzipFileContents;
         IRequest request = new GzipFileContentsRequest("1");
          string expected = "gzip-file-contents 1\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the gzip stream request.
      /// </summary>
      [Test]
      public void TestGzipStreamRequest()
      {
         RequestType type = RequestType.GzipStream;
         IRequest request = new GzipStreamRequest("1");
         string expected = "Gzip-stream 1\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the history request.
      /// </summary>
      [Test]
      public void TestHistoryRequest()
      {
         RequestType type = RequestType.History;
         IRequest request = new HistoryRequest();
         string expected = "history\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the import request.
      /// </summary>
      [Test]
      public void TestImportRequest()
      {
         RequestType type = RequestType.Import;
         IRequest request = new ImportRequest();
         string expected = "import\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the init request.
      /// </summary>
      [Test]
      public void TestInitRequest()
      {
         RequestType type = RequestType.Init;
         IRequest request = new InitRequest("sandbox");
         string expected = "init sandbox\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the is modified request.
      /// </summary>
      [Test]
      public void TestIsModifiedRequest()
      {
         RequestType type = RequestType.IsModified;
         IRequest request = new IsModifiedRequest("file.cs");
         string expected = "Is-modified file.cs\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the kerberos encrypt request.
      /// </summary>
      [Test]
      public void TestKerberosEncryptRequest()
      {
         RequestType type = RequestType.KerberosEncrypt;
         IRequest request = new KerberosEncryptRequest();
         string expected = "Kerberos-encrypt\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the kopt request.
      /// </summary>
      [Test]
      public void TestKoptRequest()
      {
         RequestType type = RequestType.Kopt;
         IRequest request = new KoptRequest("-kb");
         string expected = "Kopt -kb\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the log request.
      /// </summary>
      [Test]
      public void TestLogRequest()
      {
         RequestType type = RequestType.Log;
         IRequest request = new LogRequest();
         string expected = "log\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the lost request.
      /// </summary>
      [Test]
      public void TestLostRequest()
      {
         RequestType type = RequestType.Lost;
         IRequest request = new LostRequest("file.cs");
         string expected = "Lost file.cs\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the max dot request.
      /// </summary>
      [Test]
      public void TestMaxDotRequest()
      {
         RequestType type = RequestType.MaxDot;
         IRequest request = new MaxDotRequest("one");
         string expected = "Max-dotdot one\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the modified request.
      /// </summary>
      [Test]
      public void TestModifiedRequest()
      {
         RequestType type = RequestType.Modified;
         IRequest request = new ModifiedRequest("file.cs", "u=rw,g=rw,o=rw", 6);
         string expected = "Modified file.cs\nu=rw,g=rw,o=rw\n6\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the noop request.
      /// </summary>
      [Test]
      public void TestNoopRequest()
      {
         RequestType type = RequestType.Noop;
         IRequest request = new NoopRequest();
         string expected = "noop\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the notify request.
      /// </summary>
      [Test]
      public void TestNotifyRequest()
      {
         RequestType type = RequestType.Notify;
         IRequest request = new NotifyRequest("file.cs");
         string expected = "Notify file.cs\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the questionable request.
      /// </summary>
      [Test]
      public void TestQuestionableRequest()
      {
         RequestType type = RequestType.Questionable;
         IRequest request = new QuestionableRequest("file.cs");
         string expected = "Questionable file.cs\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the R annotate request.
      /// </summary>
      [Test]
      public void TestRAnnotateRequest()
      {
         RequestType type = RequestType.RAnnotate;
         IRequest request = new RAnnotateRequest();
         string expected = "rannotate\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the R diff request.
      /// </summary>
      [Test]
      public void TestRDiffRequest()
      {
         RequestType type = RequestType.RDiff;
         IRequest request = new RDiffRequest();
         string expected = "rdiff\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the release request.
      /// </summary>
      [Test]
      public void TestReleaseRequest()
      {
         RequestType type = RequestType.Release;
         IRequest request = new ReleaseRequest();
         string expected = "release\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the remove request.
      /// </summary>
      [Test]
      public void TestRemoveRequest()
      {
         RequestType type = RequestType.Remove;
         IRequest request = new RemoveRequest();
         string expected = "remove\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the repository request.
      /// </summary>
      [Test]
      public void TestRepositoryRequest()
      {
         RequestType type = RequestType.Repository;
         IRequest request = new RepositoryRequest(_root.Repository);
         string expected = "Repository /f1/f2/f3\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the requests to types.
      /// </summary>
      [Test]
      public void TestRequestsToTypes()
      {
         string requests = "ci Global_option Is-modified";
         IList<RequestType> types = RequestHelper.RequestsToRequestTypes(requests);
         Assert.AreEqual(RequestType.CheckIn, types[0]);
         Assert.AreEqual(RequestType.GlobalOption, types[1]);
         Assert.AreEqual(RequestType.IsModified, types[2]);
      }

      /// <summary>
      /// Tests the R log request.
      /// </summary>
      [Test]
      public void TestRLogRequest()
      {
         RequestType type = RequestType.RLog;
         IRequest request = new RLogRequest();
         string expected = "rlog\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the root request.
      /// </summary>
      [Test]
      public void TestRootRequest()
      {
         RequestType type = RequestType.Root;
         IRequest request = new RootRequest(_root.Repository);
         string expected = "Root /f1/f2/f3\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the R tag request.
      /// </summary>
      [Test]
      public void TestRTagRequest()
      {
         RequestType type = RequestType.RTag;
         IRequest request = new RTagRequest();
         string expected = "rtag\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the set request.
      /// </summary>
      [Test]
      public void TestSetRequest()
      {
         RequestType type = RequestType.Set;
         IRequest request = new SetRequest("rabbit", "Peter");
         string expected = "Set rabbit=Peter\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the static directory request.
      /// </summary>
      [Test]
      public void TestStaticDirectoryRequest()
      {
         RequestType type = RequestType.StaticDirectory;
         IRequest request = new StaticDirectoryRequest();
         string expected = "Static-directory\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the status request.
      /// </summary>
      [Test]
      public void TestStatusRequest()
      {
         RequestType type = RequestType.Status;
         IRequest request = new StatusRequest();
         string expected = "status\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the sticky request.
      /// </summary>
      [Test]
      public void TestStickyRequest()
      {
         RequestType type = RequestType.Sticky;
         IRequest request = new StickyRequest("idk");
         string expected = "Sticky idk\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the tag request.
      /// </summary>
      [Test]
      public void TestTagRequest()
      {
         RequestType type = RequestType.Tag;
         IRequest request = new TagRequest();
         string expected = "tag\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the unchanged request.
      /// </summary>
      [Test]
      public void TestUnchangedRequest()
      {
         RequestType type = RequestType.Unchanged;
         IRequest request = new UnchangedRequest("file.cs");
         string expected = "Unchanged file.cs\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the update patches.
      /// </summary>
      [Test]
      public void TestUpdatePatches()
      {
         RequestType type = RequestType.UpdatePatches;
         IRequest request = new UpdatePatchesRequest();
         string expected = "update-patches\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the update request.
      /// </summary>
      [Test]
      public void TestUpdateRequest()
      {
         RequestType type = RequestType.Update;
         IRequest request = new UpdateRequest();
         string expected = "update\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the use unchanged request.
      /// </summary>
      [Test]
      public void TestUseUnchangedRequest()
      {
         RequestType type = RequestType.UseUnchanged;
         IRequest request = new UseUnchangedRequest();
         string expected = "UseUnchanged\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the valid requests request.
      /// </summary>
      [Test]
      public void TestValidRequestsRequest()
      {
         RequestType type = RequestType.ValidRequests;
         IRequest request = new ValidRequestsRequest();
         string expected = "valid-requests\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the valid responses request.
      /// </summary>
      [Test]
      public void TestValidResponsesRequest()
      {
         RequestType type = RequestType.ValidResponses;
         IRequest request = new ValidResponsesRequest(new[] { ResponseType.Ok, ResponseType.MTMessage, ResponseType.EMessage });
         string expected = "Valid-responses ok MT E\n";
         RequestTest(type, request, expected, false);
      }

      /// <summary>
      /// Tests the verify auth request.
      /// </summary>
      [Test]
      public void TestVerifyAuthRequest()
      {
         RequestType type = RequestType.VerifyAuth;
         IRequest request = new VerifyAuthRequest(_root);
         string expected = "BEGIN VERIFICATION REQUEST\n/f1/f2/f3\nusername\nA:yZZ30 e\nEND VERIFICATION REQUEST\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the version request.
      /// </summary>
      [Test]
      public void TestVersionRequest()
      {
         RequestType type = RequestType.Version;
         IRequest request = new VersionRequest();
         string expected = "version\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the watch add request.
      /// </summary>
      [Test]
      public void TestWatchAddRequest()
      {
         RequestType type = RequestType.WatchAdd;
         IRequest request = new WatchAddRequest();
         string expected = "watch-add\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the watchers request.
      /// </summary>
      [Test]
      public void TestWatchersRequest()
      {
         RequestType type = RequestType.Watchers;
         IRequest request = new WatchersRequest();
         string expected = "watchers\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the watch off request.
      /// </summary>
      [Test]
      public void TestWatchOffRequest()
      {
         RequestType type = RequestType.WatchOff;
         IRequest request = new WatchOffRequest();
         string expected = "watch-off\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the watch on request.
      /// </summary>
      [Test]
      public void TestWatchOnRequest()
      {
         RequestType type = RequestType.WatchOn;
         IRequest request = new WatchOnRequest();
         string expected = "watch-on\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the watch remove request.
      /// </summary>
      [Test]
      public void TestWatchRemoveRequest()
      {
         RequestType type = RequestType.WatchRemove;
         IRequest request = new WatchRemoveRequest();
         string expected = "watch-remove\n";
         RequestTest(type, request, expected, true);
      }

      /// <summary>
      /// Tests the wrapper sendme RCS options request.
      /// </summary>
      [Test]
      public void TestWrapperSendmeRcsOptionsRequest()
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