using NUnit.Framework;
using PServerClient.Requests;
using PServerClient.Responses;
using System.Collections.Generic;
using System;

namespace PServerClient.Tests
{
   [TestFixture]
   public class RequestTests
   {
      private CvsRoot _root = new CvsRoot("host-name", 1, "username", "password", "/f1/f2/f3");
      private string _working = @"c:\f1\f2\f3";
      public RequestTests()
      {
         _root.Module = "mod1";
      }

      [Test]
      public void AddRequestTest()
      {
         IRequest request = new AddRequest();
         string actual = request.GetRequestString();
         const string expected = "add\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void AdminRequestTest()
      {
         IRequest request = new AdminRequest();
         string actual = request.GetRequestString();
         const string expected = "admin\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void AnnotateRequestTest()
      {
         IRequest request = new AnnotateRequest();
         string actual = request.GetRequestString();
         const string expected = "annotate\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void ArgumentRequestTest()
      {
         IRequest request = new ArgumentRequest("-a");
         string actual = request.GetRequestString();
         const string expected = "Argument -a\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void ArgumentxRequestTest()
      {
         IRequest request = new ArgumentxRequest("-a");
         string actual = request.GetRequestString();
         const string expected = "Argumentx -a\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void AuthRequestTest()
      {
         IAuthRequest request = new AuthRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "BEGIN AUTH REQUEST\n/f1/f2/f3\nusername\nA:yZZ30 e\nEND AUTH REQUEST\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void CaseRequestTest()
      {
         IRequest request = new CaseRequest();
         string actual = request.GetRequestString();
         const string expected = "Case\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void CheckinRequestTest()
      {
         IRequest request = new CheckInRequest();
         string actual = request.GetRequestString();
         const string expected = "ci\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }
      
      [Test]
      public void CheckinTimeRequestTest()
      {
         IRequest request = new CheckinTimeRequest(new DateTime(2009, 11, 6, 14, 21, 8));
         string actual = request.GetRequestString();
         const string expected = "Checkin-time 06 Nov 2009 14:21:08 -0000\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void CheckoutRequestTest()
      {
         IRequest request = new CheckOutRequest();
         string actual = request.GetRequestString();
         const string expected = "co\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void DiffRequestTest()
      {
         IRequest request = new DiffRequest();
         string actual = request.GetRequestString();
         const string expected = "diff\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void DirectoryRequestTest()
      {
         IRequest request = new DirectoryRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "Directory .\n/f1/f2/f3/mod1\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void EditorsRequestTest()
      {
         IRequest request = new EditorsRequest();
         string actual = request.GetRequestString();
         const string expected = "editors\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void EmptyConflictsRequestTest()
      {
         IRequest request = new EmptyConflictsRequest();
         string actual = request.GetRequestString();
         const string expected = "Empty-conflicts\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void EntryRequestTest()
      {
         IRequest request = new EntryRequest("file.cs", "1.1.1", "a", "b", "c");
         string actual = request.GetRequestString();
         const string expected = "Entry /file.cs/1.1.1/a/b/c\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void ExpandModulesRequestTest()
      {
         IRequest request = new ExpandModulesRequest();
         string actual = request.GetRequestString();
         const string expected = "expand-modules\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void ExportRequestTest()
      {
         IRequest request = new ExportRequest();
         string actual = request.GetRequestString();
         const string expected = "export\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void GlobalOptionRequestTest()
      {
         IRequest request = new GlobalOptionRequest("-o");
         string actual = request.GetRequestString();
         const string expected = "Global_option -o\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void GssapiAuthenticateRequestTest()
      {
         IRequest request = new GssapiAuthenticateRequest();
         string actual = request.GetRequestString();
         const string expected = "Gssapi-authenticate\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void GssapiEncryptRequestTest()
      {
         IRequest request = new GssapiEncryptRequest();
         string actual = request.GetRequestString();
         const string expected = "Gssapi-encrypt\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void GzipFileContentsRequestTest()
      {
         IRequest request = new GzipFileContentsRequest("1");
         string actual = request.GetRequestString();
         const string expected = "gzip-file-contents 1\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void GzipStreamRequestTest()
      {
         IRequest request = new GzipStreamRequest("1");
         string actual = request.GetRequestString();
         const string expected = "Gzip-stream 1\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void HistoryRequestTest()
      {
         IRequest request = new HistoryRequest();
         string actual = request.GetRequestString();
         const string expected = "history\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void ImportRequestTest()
      {
         IRequest request = new ImportRequest();
         string actual = request.GetRequestString();
         const string expected = "import\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void InitRequestTest()
      {
         IRequest request = new InitRequest("sandbox");
         string actual = request.GetRequestString();
         const string expected = "init sandbox\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void IsModifiedRequestTest()
      {
         IRequest request = new IsModifiedRequest("file.cs");
         string actual = request.GetRequestString();
         const string expected = "Is-modified file.cs\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void KerberosEncryptRequestTest()
      {
         IRequest request = new KerberosEncryptRequest();
         string actual = request.GetRequestString();
         const string expected = "Kerberos-encrypt\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void KoptRequestTest()
      {
         IRequest request = new KoptRequest("-kb");
         string actual = request.GetRequestString();
         const string expected = "Kopt -kb\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void LogRequestTest()
      {
         IRequest request = new LogRequest();
         string actual = request.GetRequestString();
         const string expected = "log\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void LostRequestTest()
      {
         IRequest request = new LostRequest("file.cs");
         string actual = request.GetRequestString();
         const string expected = "Lost file.cs\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }
      [Test]
      public void MaxDotRequestTest()
      {
         IRequest request = new MaxDotRequest("one");
         string actual = request.GetRequestString();
         const string expected = "Max-dotdot one\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void ModifiedRequestTest()
      {
         IRequest request = new ModifiedRequest("file.cs", "u=rw,g=rw,o=rw", 6);
         string actual = request.GetRequestString();
         const string expected = "Modified file.cs\nu=rw,g=rw,o=rw\n6\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void NoopRequestTest()
      {
         IRequest request = new NoopRequest();
         string actual = request.GetRequestString();
         const string expected = "noop\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void NotifyRequestTest()
      {
         IRequest request = new NotifyRequest("file.cs");
         string actual = request.GetRequestString();
         const string expected = "Notify file.cs\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void QuestionableRequestTest()
      {
         IRequest request = new QuestionableRequest("file.cs");
         string actual = request.GetRequestString();
         const string expected = "Questionable file.cs\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void RAnnotateRequestTest()
      {
         IRequest request = new RannotateRequest();
         string actual = request.GetRequestString();
         const string expected = "rannotate\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void RDiffRequestTest()
      {
         IRequest request = new RDiffRequest();
         string actual = request.GetRequestString();
         const string expected = "rdiff\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void ReleaseRequestTest()
      {
         IRequest request = new ReleaseRequest();
         string actual = request.GetRequestString();
         const string expected = "release\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void RemoveRequestTest()
      {
         IRequest request = new RemoveRequest();
         string actual = request.GetRequestString();
         const string expected = "remove\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void RepositoryRequestTest()
      {
         IRequest request = new RepositoryRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "Repository /f1/f2/f3\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void RLogRequestTest()
      {
         IRequest request = new RLogRequest();
         string actual = request.GetRequestString();
         const string expected = "rlog\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void RootRequestTest()
      {
         IRequest request = new RootRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "Root /f1/f2/f3\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void RTagRequestTest()
      {
         IRequest request = new RTagRequest();
         string actual = request.GetRequestString();
         const string expected = "rtag\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void SetRequestTest()
      {
         IRequest request = new SetRequest("rabbit", "Peter");
         string actual = request.GetRequestString();
         const string expected = "Set rabbit=Peter\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void StaticDirectoryRequestTest()
      {
         IRequest request = new StaticDirectoryRequest();
         string actual = request.GetRequestString();
         const string expected = "Static-directory\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void StatusRequestTest()
      {
         IRequest request = new StatusRequest();
         string actual = request.GetRequestString();
         const string expected = "status\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void StickyRequestTest()
      {
         IRequest request = new StickyRequest("idk");
         string actual = request.GetRequestString();
         const string expected = "Sticky idk\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void TagRequestTest()
      {
         IRequest request = new TagRequest();
         string actual = request.GetRequestString();
         const string expected = "tag\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void UnchangedRequestTest()
      {
         IRequest request = new UnchangedRequest("file.cs");
         string actual = request.GetRequestString();
         const string expected = "Unchanged file.cs\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void UpdateRequestTest()
      {
         IRequest request = new UpdateRequest();
         string actual = request.GetRequestString();
         const string expected = "update\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void UpdatePatchesTest()
      {
         IRequest request = new UpdatePatchesRequest();
         string actual = request.GetRequestString();
         const string expected = "update-patches\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void UseUnchangedRequestTest()
      {
         IRequest request = new UseUnchangedRequest();
         string actual = request.GetRequestString();
         const string expected = "UseUnchanged\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void ValidRequestsRequestTest()
      {
         IRequest request = new ValidRequestsRequest();
         string actual = request.GetRequestString();
         const string expected = "valid-requests\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void ValidResponsesRequestTest()
      {
         IRequest request = new ValidResponsesRequest("res1 res2 res3");
         string actual = request.GetRequestString();
         const string expected = "Valid-responses res1 res2 res3\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void VersionRequestTest()
      {
         IRequest request = new VersionRequest();
         string actual = request.GetRequestString();
         const string expected = "version\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void WatchAddRequestTest()
      {
         IRequest request = new WatchAddRequest();
         string actual = request.GetRequestString();
         const string expected = "watch-add\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void WatchersRequestTest()
      {
         IRequest request = new WatchersRequest();
         string actual = request.GetRequestString();
         const string expected = "watchers\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void WatchOffRequestTest()
      {
         IRequest request = new WatchOffRequest();
         string actual = request.GetRequestString();
         const string expected = "watch-off\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void WatchOnRequestTest()
      {
         IRequest request = new WatchOnRequest();
         string actual = request.GetRequestString();
         const string expected = "watch-on\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void WatchRemoveRequestTest()
      {
         IRequest request = new WatchRemoveRequest();
         string actual = request.GetRequestString();
         const string expected = "watch-remove\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void WrapperSendmeRcsOptionsRequestTest()
      {
         IRequest request = new WrapperSendmeRcsOptionsRequest();
         string actual = request.GetRequestString();
         const string expected = "wrapper-sendme-rcsOptions\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }

      [Test]
      public void VerifyAuthRequestTest()
      {
         IAuthRequest request = new VerifyAuthRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "BEGIN VERIFICATION REQUEST\n/f1/f2/f3\nusername\nA:yZZ30 e\nEND VERIFICATION REQUEST\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         //Console.WriteLine(actual);
      }
   }
}
