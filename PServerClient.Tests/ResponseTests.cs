using System;
using System.Collections.Generic;
using System.Xml.Linq;
using NUnit.Framework;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;

namespace PServerClient.Tests
{
   [TestFixture]
   public class ResponseTests
   {
      [Test]
      public void AuthResponseAuthenticatedTest()
      {
         AuthResponse response = new AuthResponse();
         IList<string> lines = new List<string> {"blah\n\r blahI LOVE YOUblah"};
         response.Process(lines);
         Assert.AreEqual(AuthStatus.Authenticated, response.Status);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void AuthResponseNotAuthenticatedTest()
      {
         AuthResponse response = new AuthResponse();
         IList<string> lines = new List<string> {"blah\n\r blahI HATE YOUblah"};
         response.Process(lines);
         Assert.AreEqual(AuthStatus.NotAuthenticated, response.Status);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void AuthResponseTest()
      {
         IResponse response = new AuthResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.Auth, response.Type);
         response.Process(new List<string> {"I LOVE YOU"});
         Assert.AreEqual("I LOVE YOU", response.Display());
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void CheckedInResponseTest()
      {
         CheckedInResponse response = new CheckedInResponse();
         Assert.AreEqual(5, response.LineCount);
         Assert.AreEqual(ResponseType.CheckedIn, response.Type);
         response.Process(new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"});
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         response.File.Contents = contents.Encode();
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.Display());
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         Assert.IsTrue(TestHelper.ValidateResponseXML(el));
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ChecksumResponseTest()
      {
         ChecksumResponse response = new ChecksumResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.Checksum, response.Type);
         response.Process(new List<string> {"123"});
         Assert.AreEqual("123", response.Display());
         Assert.AreEqual("123", response.CheckSum);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ClearStaticDirectoryProcessResponseTest()
      {
         ClearStaticDirectoryResponse response = new ClearStaticDirectoryResponse();
         IList<string> lines = new List<string> {"module/", "/f1/f2/f3/"};
         response.Process(lines);
         Assert.AreEqual("module/", response.ModuleName);
         Assert.AreEqual("/f1/f2/f3/", response.RepositoryPath);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void ClearStaticDirectoryResponseTest()
      {
         ClearStaticDirectoryResponse response = new ClearStaticDirectoryResponse();
         Assert.AreEqual(2, response.LineCount);
         Assert.AreEqual(ResponseType.ClearStaticDirectory, response.Type);
         response.Process(new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/"});
         Assert.AreEqual("mod1/", response.ModuleName);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/", response.RepositoryPath);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ClearStickyProcessResponseTest()
      {
         ClearStickyResponse response = new ClearStickyResponse();
         IList<string> lines = new List<string> {"module/", "/f1/f2/f3/"};
         response.Process(lines);
         Assert.AreEqual("module/", response.ModuleName);
         Assert.AreEqual("/f1/f2/f3/", response.RepositoryPath);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void ClearStickyResponseTest()
      {
         ClearStickyResponse response = new ClearStickyResponse();
         Assert.AreEqual(2, response.LineCount);
         Assert.AreEqual(ResponseType.ClearSticky, response.Type);
         response.Process(new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/"});
         Assert.AreEqual("mod1/", response.ModuleName);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/", response.RepositoryPath);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void CopyFileResponseTest()
      {
         CopyFileResponse response = new CopyFileResponse();
         Assert.AreEqual(2, response.LineCount);
         Assert.AreEqual(ResponseType.CopyFile, response.Type);
         response.Process(new List<string> {"/usr/local/cvsroot/sandbox/mod1/file1.cs", "/usr/local/cvsroot/sandbox/mod1/newfile1.cs"});
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.OriginalFileName);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/newfile1.cs", response.NewFileName);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void CreatedResponseTest()
      {
         CreatedResponse response = new CreatedResponse();
         Assert.AreEqual(5, response.LineCount);
         Assert.AreEqual(ResponseType.Created, response.Type);
         response.Process(new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"});
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         response.File.Contents = contents.Encode();
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.Display());
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         Assert.IsTrue(TestHelper.ValidateResponseXML(el));
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ErrorResponseTest()
      {
         ErrorResponse response = new ErrorResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.Error, response.Type);
         response.Process(new List<string> {"My error message"});
         Assert.AreEqual("My error message", response.Message);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void FileResponseBaseProcessMultipleDirectoriesTest()
      {
         UpdatedResponse response = new UpdatedResponse();
         IList<string> lines = new List<string> {"mod1/mod2/mod3/", "/usr/local/cvsroot/sandbox/mod1/mod2/mod3/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"};
         response.Process(lines);
         ReceiveFile file = response.File;
         Assert.AreEqual("mod1", file.Path[0]);
         Assert.AreEqual("mod2", file.Path[1]);
         Assert.AreEqual("mod3", file.Path[2]);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/mod2/mod3/file1.cs", file.RepositoryPath);
         Assert.AreEqual("file1.cs", file.Name);
         Assert.AreEqual("1.2.3.4", file.Revision);
         Assert.AreEqual("u=rw,g=rw,o=rw", file.Properties);
         Assert.AreEqual(74, file.Length);
         Assert.AreEqual(5, response.LineCount);
      }

      [Test]
      public void FileResponseBaseProcessTest()
      {
         UpdatedResponse response = new UpdatedResponse();
         IList<string> lines = new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"};
         response.Process(lines);
         ReceiveFile file = response.File;
         Assert.AreEqual("mod1", file.Path[0]);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", file.RepositoryPath);
         Assert.AreEqual("file1.cs", file.Name);
         Assert.AreEqual("1.2.3.4", file.Revision);
         Assert.AreEqual("u=rw,g=rw,o=rw", file.Properties);
         Assert.AreEqual(74, file.Length);
         Assert.AreEqual(5, response.LineCount);
      }

      [Test]
      public void FlushResponseTest()
      {
         FlushResponse response = new FlushResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.Flush, response.Type);
         response.Process(new List<string> {""});
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void MbinaryResponseTest()
      {
         MbinaryResponse response = new MbinaryResponse();
         Assert.AreEqual(5, response.LineCount);
         Assert.AreEqual(ResponseType.Mbinary, response.Type);
         response.Process(new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"});
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         response.File.Contents = contents.Encode();
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.Display());
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         Assert.IsTrue(TestHelper.ValidateResponseXML(el));
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void MergedResponseTest()
      {
         MergedResponse response = new MergedResponse();
         Assert.AreEqual(5, response.LineCount);
         Assert.AreEqual(ResponseType.Merged, response.Type);
         response.Process(new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"});
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         response.File.Contents = contents.Encode();
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.Display());
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         Assert.IsTrue(TestHelper.ValidateResponseXML(el));
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void MessageTagProcessResponseTest()
      {
         MessageTagResponse response = new MessageTagResponse();
         IList<string> lines = new List<string> {"+updated"};
         response.Process(lines);
         string expected = "+updated";
         Assert.AreSame(expected, response.Message);
         Assert.AreEqual(1, response.LineCount);
      }

      [Test]
      public void MessageTagTest()
      {
         MessageTagResponse response = new MessageTagResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.MessageTag, response.Type);
         response.Process(new List<string> {"My message"});
         Assert.AreEqual("My message", response.Message);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void MessageTest()
      {
         MessageResponse response = new MessageResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.Message, response.Type);
         response.Process(new List<string> {"My message"});
         Assert.AreEqual("My message", response.Message);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ModeResponseTest()
      {
         ModeResponse response = new ModeResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.Mode, response.Type);
         response.Process(new List<string> {"modemode"});
         Assert.AreEqual("modemode", response.Mode);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ModTimeProcessResponseTest()
      {
         ModTimeResponse response = new ModTimeResponse();
         IList<string> lines = new List<string> {"27 Nov 2009 14:21:06 -0000"};
         response.Process(lines);
         DateTime expected = new DateTime(2009, 11, 27, 14, 21, 6);
         Assert.AreEqual(expected, response.ModTime);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void ModTimeTest()
      {
         ModTimeResponse response = new ModTimeResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.ModTime, response.Type);
         response.Process(new List<string> {"27 Nov 2009 14:21:06 -0000"});
         DateTime expected = DateTime.Parse("11/27/2009 2:21:06 PM");
         Assert.AreEqual(expected, response.ModTime);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ModuleExpansionProcessResponseTest()
      {
         ModuleExpansionResponse response = new ModuleExpansionResponse();
         IList<string> lines = new List<string> {"module name"};
         response.Process(lines);
         Assert.AreEqual("module name", response.ModuleName);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void ModuleExpansionResponseTest()
      {
         ModuleExpansionResponse response = new ModuleExpansionResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.ModuleExpansion, response.Type);
         response.Process(new List<string> {"mod1"});
         Assert.AreEqual("mod1", response.ModuleName);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void NewEntryResponseTest()
      {
         NewEntryResponse response = new NewEntryResponse();
         Assert.AreEqual(2, response.LineCount);
         Assert.AreEqual(ResponseType.NewEntry, response.Type);
         response.Process(new List<string> {"mod1", "/file1.cs/1.1.1.1///"});
         Assert.AreEqual("file1.cs", response.FileName);
         Assert.AreEqual("1.1.1.1", response.Revision);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void NotifiedResponseTest()
      {
         NotifiedResponse response = new NotifiedResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.Notified, response.Type);
         response.Process(new List<string> {"/usr/local/cvsroot/sandbox/mod1/file1.cs"});
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.RepositoryPath);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void NullResponseTest()
      {
         NullResponse response = new NullResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.Null, response.Type);
         response.Process(new List<string> {""});
         Assert.AreEqual("", response.Display());
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void OkProcessResponseTest()
      {
         OkResponse response = new OkResponse();
         IList<string> lines = new List<string> {""};
         response.Process(lines);
         Assert.AreEqual(lines.Count, response.LineCount);
         // no error means passing test
      }

      [Test]
      public void OkResponseTest()
      {
         OkResponse response = new OkResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.Ok, response.Type);
         response.Process(new List<string> {""});
         Assert.AreEqual("ok", response.Display());
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void PatchedResponseTest()
      {
         PatchedResponse response = new PatchedResponse();
         Assert.AreEqual(5, response.LineCount);
         Assert.AreEqual(ResponseType.Patched, response.Type);
         response.Process(new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"});
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         response.File.Contents = contents.Encode();
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.Display());
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         Assert.IsTrue(TestHelper.ValidateResponseXML(el));
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void RcsDiffResponseTest()
      {
         RcsDiffResponse response = new RcsDiffResponse();
         Assert.AreEqual(5, response.LineCount);
         Assert.AreEqual(ResponseType.RcsDiff, response.Type);
         response.Process(new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"});
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         response.File.Contents = contents.Encode();
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.Display());
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         Assert.IsTrue(TestHelper.ValidateResponseXML(el));
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void RemovedResponseTest()
      {
         RemovedResponse response = new RemovedResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.Removed, response.Type);
         response.Process(new List<string> {"/usr/local/cvsroot/sandbox/mod1/file1.cs"});
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.RepositoryPath);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void RemoveEntryResponseTest()
      {
         RemoveEntryResponse response = new RemoveEntryResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.RemoveEntry, response.Type);
         response.Process(new List<string> {"/usr/local/cvsroot/sandbox/mod1/file1.cs"});
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.RepositoryPath);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void SetStaticDirectoryResponseTest()
      {
         SetStaticDirectoryResponse response = new SetStaticDirectoryResponse();
         Assert.AreEqual(2, response.LineCount);
         Assert.AreEqual(ResponseType.SetStaticDirectory, response.Type);
         response.Process(new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/"});
         Assert.AreEqual("mod1/", response.ModuleName);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/", response.RepositoryPath);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void SetStickyResponseTest()
      {
         SetStickyResponse response = new SetStickyResponse();
         Assert.AreEqual(2, response.LineCount);
         Assert.AreEqual(ResponseType.SetSticky, response.Type);
         response.Process(new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/"});
         Assert.AreEqual("mod1/", response.ModuleName);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/", response.RepositoryPath);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void TemplateResponseTest()
      {
         TemplateResponse response = new TemplateResponse();
         Assert.AreEqual(5, response.LineCount);
         Assert.AreEqual(ResponseType.Template, response.Type);
         response.Process(new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"});
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         response.File.Contents = contents.Encode();
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.Display());
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         Assert.IsTrue(TestHelper.ValidateResponseXML(el));
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void UpdatedResponseTest()
      {
         UpdatedResponse response = new UpdatedResponse();
         Assert.AreEqual(5, response.LineCount);
         Assert.AreEqual(ResponseType.Updated, response.Type);
         response.Process(new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"});
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         response.File.Contents = contents.Encode();
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.Display());
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         Assert.IsTrue(TestHelper.ValidateResponseXML(el));
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void UpdateExistingTest()
      {
         UpdateExistingResponse response = new UpdateExistingResponse();
         Assert.AreEqual(5, response.LineCount);
         Assert.AreEqual(ResponseType.UpdateExisting, response.Type);
         response.Process(new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"});
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         response.File.Contents = contents.Encode();
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.Display());
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         Assert.IsTrue(TestHelper.ValidateResponseXML(el));
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ValidRequestResponseTest()
      {
         ValidRequestsResponse response = new ValidRequestsResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.ValidRequests, response.Type);
         string process = "Root Valid-responses valid-requests Repository Directory";
         response.Process(new List<string> {process});
         var result = response.ValidRequestTypes;
         Assert.IsInstanceOf<IList<RequestType>>(result);
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         Assert.IsTrue(TestHelper.ValidateResponseXML(el));
         Console.WriteLine(el.ToString());
      }

      [Test]
      public void ValidRequestsResponseProcessTest()
      {
         ValidRequestsResponse response = new ValidRequestsResponse();
         IList<string> lines = new List<string> {"Root Valid-responses valid-requests Global_option"};
         response.Process(lines);
         Assert.AreEqual(4, response.ValidRequestTypes.Count);
         Assert.AreEqual(RequestType.Root, response.ValidRequestTypes[0]);
         Assert.AreEqual(RequestType.GlobalOption, response.ValidRequestTypes[3]);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void WrapperRscOptionResponseTest()
      {
         WrapperRscOptionResponse response = new WrapperRscOptionResponse();
         Assert.AreEqual(1, response.LineCount);
         Assert.AreEqual(ResponseType.WrapperRscOption, response.Type);
         string process = "*.cs -k 'b'";
         response.Process(new List<string> {process});
         XElement el = response.GetXElement(); //TestHelper.IResponseToXElement(response);
         Assert.IsTrue(TestHelper.ValidateResponseXML(el));
         Console.WriteLine(el.ToString());
      }
   }
}