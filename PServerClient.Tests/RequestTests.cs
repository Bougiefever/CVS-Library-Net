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
      private readonly Root _root = new Root(TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password, TestConfig.RepositoryPath);
      private readonly PServerFactory _factory = new PServerFactory();

      public RequestTests()
      {
         _root.Module = "mod1";
      }

      [Test]
      public void AddRequestTest()
      {
         RequestType type = RequestType.Add;
         IRequest request = _factory.CreateRequest(type, new object[0]); // new AddRequest();
         string actual = request.GetRequestString();
         const string expected = "add\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Assert.AreEqual("PServerClient.Requests.AddRequest", el.Element("ClassName").Value);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void AdminRequestTest()
      {
         RequestType type = RequestType.Admin;
         IRequest request = _factory.CreateRequest(type, new object[0]);// new AdminRequest();
         string actual = request.GetRequestString();
         const string expected = "admin\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void AnnotateRequestTest()
      {
         RequestType type = RequestType.Annotate;
         IRequest request = _factory.CreateRequest(type, new object[0]); //new AnnotateRequest();
         string actual = request.GetRequestString();
         const string expected = "annotate\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ArgumentRequestTest()
      {
         RequestType type = RequestType.Argument;
         IRequest request = _factory.CreateRequest(type, new object[] { "-a" }); //new ArgumentRequest("-a");
         string actual = request.GetRequestString();
         const string expected = "Argument -a\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ArgumentxRequestTest()
      {
         RequestType type = RequestType.Argumentx;
         IRequest request = _factory.CreateRequest(type, new object[] { "-a" }); //new ArgumentxRequest("-a");
         string actual = request.GetRequestString();
         const string expected = "Argumentx -a\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void AuthRequestTest()
      {
         RequestType type = RequestType.Auth;
         IAuthRequest request = (IAuthRequest) _factory.CreateRequest(type, new object[] {_root }); //new AuthRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "BEGIN AUTH REQUEST\n/f1/f2/f3\nusername\nA:yZZ30 e\nEND AUTH REQUEST\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void CaseRequestTest()
      {
         RequestType type = RequestType.Case;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new CaseRequest();
         string actual = request.GetRequestString();
         const string expected = "Case\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void CheckInRequestTest()
      {
         RequestType type = RequestType.CheckIn;
         IRequest request = _factory.CreateRequest(type, new object[] { });  //new CheckInRequest();
         string actual = request.GetRequestString();
         const string expected = "ci\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void CheckinTimeRequestTest()
      {
         RequestType type = RequestType.CheckinTime;
         DateTime checkinTime = new DateTime(2009, 11, 6, 14, 21, 8);
         IRequest request = _factory.CreateRequest(type, new object[] { checkinTime }); //new CheckinTimeRequest(new DateTime(2009, 11, 6, 14, 21, 8));
         string actual = request.GetRequestString();
         const string expected = "Checkin-time 06 Nov 2009 14:21:08 -0000\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void CheckoutRequestTest()
      {
         RequestType type = RequestType.CheckOut;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new CheckOutRequest();
         string actual = request.GetRequestString();
         const string expected = "co\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void DiffRequestTest()
      {
         RequestType type = RequestType.Diff;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new DiffRequest();
         string actual = request.GetRequestString();
         const string expected = "diff\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void DirectoryRequestTest()
      {
         RequestType type = RequestType.Directory;
         IRequest request = _factory.CreateRequest(type, new object[] { _root }); //new DirectoryRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "Directory .\n/f1/f2/f3/mod1\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void EditorsRequestTest()
      {
         RequestType type = RequestType.Editors;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new EditorsRequest();
         string actual = request.GetRequestString();
         const string expected = "editors\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void EmptyConflictsRequestTest()
      {
         RequestType type = RequestType.EmptyConflicts;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new EmptyConflictsRequest();
         string actual = request.GetRequestString();
         const string expected = "Empty-conflicts\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void EntryRequestTest()
      {
         RequestType type = RequestType.Entry;
         IRequest request = _factory.CreateRequest(type, new object[] { "file.cs", "1.1.1", "a", "b", "c" }); //new EntryRequest("file.cs", "1.1.1", "a", "b", "c");
         string actual = request.GetRequestString();
         const string expected = "Entry /file.cs/1.1.1/a/b/c\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ExpandModulesRequestTest()
      {
         RequestType type = RequestType.ExpandModules;
         IRequest request = _factory.CreateRequest(type, new object[] {}); //new ExpandModulesRequest();
         string actual = request.GetRequestString();
         const string expected = "expand-modules\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ExportRequestTest()
      {
         RequestType type = RequestType.Export;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new ExportRequest();
         string actual = request.GetRequestString();
         const string expected = "export\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void GlobalOptionRequestTest()
      {
         RequestType type = RequestType.GlobalOption;
         IRequest request = _factory.CreateRequest(type, new object[] { "-o" }); //new GlobalOptionRequest("-o");
         string actual = request.GetRequestString();
         const string expected = "Global_option -o\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void GssapiAuthenticateRequestTest()
      {
         RequestType type = RequestType.GssapiAuthenticate;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new GssapiAuthenticateRequest();
         string actual = request.GetRequestString();
         const string expected = "Gssapi-authenticate\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void GssapiEncryptRequestTest()
      {
         RequestType type = RequestType.GssapiEncrypt;
         IRequest request = _factory.CreateRequest(type, new object[] { });  //new GssapiEncryptRequest();
         string actual = request.GetRequestString();
         const string expected = "Gssapi-encrypt\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void GzipFileContentsRequestTest()
      {
         RequestType type = RequestType.GzipFileContents;
         IRequest request = _factory.CreateRequest(type, new object[] { "1" });  //new GzipFileContentsRequest("1");
         string actual = request.GetRequestString();
         const string expected = "gzip-file-contents 1\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void GzipStreamRequestTest()
      {
         RequestType type = RequestType.GzipStream;
         IRequest request = _factory.CreateRequest(type, new object[] { "1" });//new GzipStreamRequest("1");
         string actual = request.GetRequestString();
         const string expected = "Gzip-stream 1\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void HistoryRequestTest()
      {
         RequestType type = RequestType.History;
         IRequest request = _factory.CreateRequest(type, new object[] { });//new HistoryRequest();
         string actual = request.GetRequestString();
         const string expected = "history\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ImportRequestTest()
      {
         RequestType type = RequestType.Import;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new ImportRequest();
         string actual = request.GetRequestString();
         const string expected = "import\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void InitRequestTest()
      {
         RequestType type = RequestType.Init;
         IRequest request = _factory.CreateRequest(type, new object[] { "sandbox" }); //new InitRequest("sandbox");
         string actual = request.GetRequestString();
         const string expected = "init sandbox\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void IsModifiedRequestTest()
      {
         RequestType type = RequestType.IsModified;
         IRequest request = _factory.CreateRequest(type, new object[] { "file.cs" }); //new IsModifiedRequest("file.cs");
         string actual = request.GetRequestString();
         const string expected = "Is-modified file.cs\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void KerberosEncryptRequestTest()
      {
         RequestType type = RequestType.KerberosEncrypt;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new KerberosEncryptRequest();
         string actual = request.GetRequestString();
         const string expected = "Kerberos-encrypt\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void KoptRequestTest()
      {
         RequestType type = RequestType.Kopt;
         IRequest request = _factory.CreateRequest(type, new object[] { "-kb" }); //new KoptRequest("-kb");
         string actual = request.GetRequestString();
         const string expected = "Kopt -kb\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void LogRequestTest()
      {
         RequestType type = RequestType.Log;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new LogRequest();
         string actual = request.GetRequestString();
         const string expected = "log\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void LostRequestTest()
      {
         RequestType type = RequestType.Lost;
         IRequest request = _factory.CreateRequest(type, new object[] { "file.cs" }); //new LostRequest("file.cs");
         string actual = request.GetRequestString();
         const string expected = "Lost file.cs\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void MaxDotRequestTest()
      {
         RequestType type = RequestType.MaxDot;
         IRequest request = _factory.CreateRequest(type, new object[] { "one" }); //new MaxDotRequest("one");
         string actual = request.GetRequestString();
         const string expected = "Max-dotdot one\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ModifiedRequestTest()
      {
         RequestType type = RequestType.Modified;
         IRequest request = _factory.CreateRequest(type, new object[] { "file.cs", "u=rw,g=rw,o=rw", 6 }); //new ModifiedRequest("file.cs", "u=rw,g=rw,o=rw", 6);
         string actual = request.GetRequestString();
         const string expected = "Modified file.cs\nu=rw,g=rw,o=rw\n6\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void NoopRequestTest()
      {
         RequestType type = RequestType.Noop;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new NoopRequest();
         string actual = request.GetRequestString();
         const string expected = "noop\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void NotifyRequestTest()
      {
         RequestType type = RequestType.Notify;
         IRequest request = _factory.CreateRequest(type, new object[] { "file.cs" }); //new NotifyRequest("file.cs");
         string actual = request.GetRequestString();
         const string expected = "Notify file.cs\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void QuestionableRequestTest()
      {
         RequestType type = RequestType.Questionable;
         IRequest request = _factory.CreateRequest(type, new object[] { "file.cs" }); new QuestionableRequest("file.cs");
         string actual = request.GetRequestString();
         const string expected = "Questionable file.cs\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void RAnnotateRequestTest()
      {
         RequestType type = RequestType.RAnnotate;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new RAnnotateRequest();
         string actual = request.GetRequestString();
         const string expected = "rannotate\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void RDiffRequestTest()
      {
         RequestType type = RequestType.RDiff;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new RDiffRequest();
         string actual = request.GetRequestString();
         const string expected = "rdiff\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ReleaseRequestTest()
      {
         RequestType type = RequestType.Release;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new ReleaseRequest();
         string actual = request.GetRequestString();
         const string expected = "release\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void RemoveRequestTest()
      {
         RequestType type = RequestType.Remove;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new RemoveRequest();
         string actual = request.GetRequestString();
         const string expected = "remove\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void RepositoryRequestTest()
      {
         RequestType type = RequestType.Repository;
         IRequest request = _factory.CreateRequest(type, new object[] { _root }); //new RepositoryRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "Repository /f1/f2/f3\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
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
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new RLogRequest();
         string actual = request.GetRequestString();
         const string expected = "rlog\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void RootRequestTest()
      {
         RequestType type = RequestType.Root;
         IRequest request = _factory.CreateRequest(type, new object[] { _root }); //new RootRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "Root /f1/f2/f3\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void RTagRequestTest()
      {
         RequestType type = RequestType.RTag;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new RTagRequest();
         string actual = request.GetRequestString();
         const string expected = "rtag\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void SetRequestTest()
      {
         RequestType type = RequestType.Set;
         IRequest request = _factory.CreateRequest(type, new object[] { "rabbit", "Peter" }); //new SetRequest("rabbit", "Peter");
         string actual = request.GetRequestString();
         const string expected = "Set rabbit=Peter\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void StaticDirectoryRequestTest()
      {
         RequestType type = RequestType.StaticDirectory;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new StaticDirectoryRequest();
         string actual = request.GetRequestString();
         const string expected = "Static-directory\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void StatusRequestTest()
      {
         RequestType type = RequestType.Status;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new StatusRequest();
         string actual = request.GetRequestString();
         const string expected = "status\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void StickyRequestTest()
      {
         RequestType type = RequestType.Sticky;
         IRequest request = _factory.CreateRequest(type, new object[] { "idk" }); //new StickyRequest("idk");
         string actual = request.GetRequestString();
         const string expected = "Sticky idk\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void TagRequestTest()
      {
         RequestType type = RequestType.Tag;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new TagRequest();
         string actual = request.GetRequestString();
         const string expected = "tag\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void UnchangedRequestTest()
      {
         RequestType type = RequestType.Unchanged;
         IRequest request = _factory.CreateRequest(type, new object[] { "file.cs" }); //new UnchangedRequest("file.cs");
         string actual = request.GetRequestString();
         const string expected = "Unchanged file.cs\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void UpdatePatchesTest()
      {
         RequestType type = RequestType.UpdatePatches;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new UpdatePatchesRequest();
         string actual = request.GetRequestString();
         const string expected = "update-patches\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void UpdateRequestTest()
      {
         RequestType type = RequestType.Update;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new UpdateRequest();
         string actual = request.GetRequestString();
         const string expected = "update\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void UseUnchangedRequestTest()
      {
         RequestType type = RequestType.UseUnchanged;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new UseUnchangedRequest();
         string actual = request.GetRequestString();
         const string expected = "UseUnchanged\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ValidRequestsRequestTest()
      {
         RequestType type = RequestType.ValidRequests;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new ValidRequestsRequest();
         string actual = request.GetRequestString();
         const string expected = "valid-requests\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ValidResponsesRequestTest()
      {
         RequestType type = RequestType.ValidResponses;
         IRequest request = _factory.CreateRequest(type, new object[] { new ResponseType[] {ResponseType.Ok, ResponseType.MessageTag, ResponseType.EMessage }}); //new ValidResponsesRequest(new[] { ResponseType.Ok, ResponseType.MessageTag, ResponseType.EMessage });
         string actual = request.GetRequestString();
         const string expected = "Valid-responses ok MT E\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void VerifyAuthRequestTest()
      {
         RequestType type = RequestType.VerifyAuth;
         IRequest request = _factory.CreateRequest(type, new object[] {_root }); //new VerifyAuthRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "BEGIN VERIFICATION REQUEST\n/f1/f2/f3\nusername\nA:yZZ30 e\nEND VERIFICATION REQUEST\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void VersionRequestTest()
      {
         RequestType type = RequestType.Version;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new VersionRequest();
         string actual = request.GetRequestString();
         const string expected = "version\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void WatchAddRequestTest()
      {
         RequestType type = RequestType.WatchAdd;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new WatchAddRequest();
         string actual = request.GetRequestString();
         const string expected = "watch-add\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void WatchersRequestTest()
      {
         RequestType type = RequestType.Watchers;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new WatchersRequest();
         string actual = request.GetRequestString();
         const string expected = "watchers\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void WatchOffRequestTest()
      {
         RequestType type = RequestType.WatchOff;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new WatchOffRequest();
         string actual = request.GetRequestString();
         const string expected = "watch-off\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void WatchOnRequestTest()
      {
         RequestType type = RequestType.WatchOn;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new WatchOnRequest();
         string actual = request.GetRequestString();
         const string expected = "watch-on\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void WatchRemoveRequestTest()
      {
         RequestType type = RequestType.WatchRemove;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new WatchRemoveRequest();
         string actual = request.GetRequestString();
         const string expected = "watch-remove\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void WrapperSendmeRcsOptionsRequestTest()
      {
         RequestType type = RequestType.WrapperSendmeRcsOptions;
         IRequest request = _factory.CreateRequest(type, new object[] { }); //new WrapperSendmeRcsOptionsRequest();
         string actual = request.GetRequestString();
         const string expected = "wrapper-sendme-rcsOptions\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
         Assert.AreEqual(type, request.Type);
         XElement el = TestHelper.IRequestToXElement(request);
         bool result = TestHelper.ValidateRequestXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }
   }
}