using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;
using System.Linq;

namespace PServerClient.Tests
{
   [TestFixture]
   public class ResponseTests
   {
      [Test]
      public void AuthResponseAuthenticatedTest()
      {
         AuthResponse response = new AuthResponse();
         IList<string> lines = new List<string> { "blah\n\r blahI LOVE YOUblah" };
         response.Process(lines);
         Assert.AreEqual(AuthStatus.Authenticated, response.Status);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void AuthResponseNotAuthenticatedTest()
      {
         AuthResponse response = new AuthResponse();
         IList<string> lines = new List<string> { "blah\n\r blahI HATE YOUblah" };
         response.Process(lines);
         Assert.AreEqual(AuthStatus.NotAuthenticated, response.Status);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void AuthResponseTest()
      {
         IResponse response = new AuthResponse();
         ResponseTest(response, ResponseType.Auth, 1, "I LOVE YOU", new List<string> {"I LOVE YOU"});
      }

      [Test]
      public void CheckedInResponseTest()
      {
         CheckedInResponse response = new CheckedInResponse();
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         ResponseTest(response, ResponseType.CheckedIn, 5, "/usr/local/cvsroot/sandbox/mod1/file1.cs", new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"}, contents);
      }

      [Test]
      public void ChecksumResponseTest()
      {
         ChecksumResponse response = new ChecksumResponse();
         ResponseTest(response, ResponseType.Checksum, 1, "123", new List<string> { "123" });
         Assert.AreEqual("123", response.CheckSum);
      }

      [Test]
      public void ClearStaticDirectoryResponseTest()
      {
         ClearStaticDirectoryResponse response = new ClearStaticDirectoryResponse();
         IList<string> lines = new List<string> { "mod1/", "/usr/local/cvsroot/sandbox/mod1/" };
         ResponseTest(response, ResponseType.ClearStaticDirectory, 2, "/usr/local/cvsroot/sandbox/mod1/", lines);
         Assert.AreEqual("mod1/", response.ModuleName);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/", response.RepositoryPath);

      }

      [Test]
      public void ClearStickyResponseTest()
      {
         ClearStickyResponse response = new ClearStickyResponse();
         IList<string> lines = new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/"};
         ResponseTest(response, ResponseType.ClearSticky, 2, "/usr/local/cvsroot/sandbox/mod1/", lines);
         Assert.AreEqual("mod1/", response.ModuleName);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/", response.RepositoryPath);
      }

      [Test]
      public void CopyFileResponseTest()
      {
         CopyFileResponse response = new CopyFileResponse();
         IList<string> lines = new List<string> { "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/usr/local/cvsroot/sandbox/mod1/newfile1.cs" };
         ResponseTest(response, ResponseType.CopyFile, 2, "/usr/local/cvsroot/sandbox/mod1/file1.cs\n/usr/local/cvsroot/sandbox/mod1/newfile1.cs", lines);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.OriginalFileName);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/newfile1.cs", response.NewFileName);
      }

      [Test]
      public void CreatedResponseTest()
      {
         CreatedResponse response = new CreatedResponse();
         IList<string> lines = new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"};
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         ResponseTest(response, ResponseType.Created, 5, "/usr/local/cvsroot/sandbox/mod1/file1.cs", lines, contents);
      }

      [Test]
      public void ErrorResponseTest()
      {
         ErrorResponse response = new ErrorResponse();
         IList<string> lines = new List<string> {"My error message"};
         ResponseTest(response, ResponseType.Error, 1, "My error message", lines);
         Assert.AreEqual("My error message", response.Message);
     }

      [Test]
      public void FileResponseBaseProcessMultipleDirectoriesTest()
      {
         UpdatedResponse response = new UpdatedResponse();
         IList<string> lines = new List<string> { "mod1/mod2/mod3/", "/usr/local/cvsroot/sandbox/mod1/mod2/mod3/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74" };
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
         IList<string> lines = new List<string> { "mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74" };
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
         ResponseTest(response, ResponseType.Flush, 1, "", new List<string> { "" });
      }

      [Test]
      public void MbinaryResponseTest()
      {
         MbinaryResponse response = new MbinaryResponse();
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         IList<string> lines = new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"};
         ResponseTest(response, ResponseType.Mbinary, 5, "/usr/local/cvsroot/sandbox/mod1/file1.cs", lines, contents);
      }

      [Test]
      public void MergedResponseTest()
      {
         MergedResponse response = new MergedResponse();
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         IList<string> lines = new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"};
         ResponseTest(response, ResponseType.Merged, 5, "/usr/local/cvsroot/sandbox/mod1/file1.cs", lines, contents);
      }

      [Test]
      public void MessageTagTest()
      {
         MessageTagResponse response = new MessageTagResponse();
         ResponseTest(response, ResponseType.MessageTag, 1, "My message", new List<string> { "My message" });
         Assert.AreEqual("My message", response.Message);
      }

      [Test]
      public void MessageTest()
      {
         MessageResponse response = new MessageResponse();
         ResponseTest(response, ResponseType.Message, 1, "My message", new List<string> { "My message" });
         Assert.AreEqual("My message", response.Message);
      }

      [Test]
      public void ModeResponseTest()
      {
         ModeResponse response = new ModeResponse();
         ResponseTest(response, ResponseType.Mode, 1, "modemode", new List<string> { "modemode" });
         Assert.AreEqual("modemode", response.Mode);
      }

      [Test]
      public void ModTimeResponseTest()
      {
         ModTimeResponse response = new ModTimeResponse();
         ResponseTest(response, ResponseType.ModTime, 1, "11/27/2009 2:21:06 PM", new List<string> { "27 Nov 2009 14:21:06 -0000" });
         DateTime expected = new DateTime(2009, 11, 27, 14, 21, 6);
         Assert.AreEqual(expected, response.ModTime);
      }

      [Test]
      public void ModuleExpansionResponseTest()
      {
         ModuleExpansionResponse response = new ModuleExpansionResponse();
         IList<string> lines = new List<string> { "mod1" };
         ResponseTest(response, ResponseType.ModuleExpansion, 1, "mod1", lines);
         Assert.AreEqual("mod1", response.ModuleName);
      }

      [Test]
      public void NewEntryResponseTest()
      {
         NewEntryResponse response = new NewEntryResponse();
         IList<string> lines = new List<string> {"mod1", "/file1.cs/1.1.1.1///"};
         ResponseTest(response, ResponseType.NewEntry, 2, "mod1\n/file1.cs/1.1.1.1///", lines);
         Assert.AreEqual("file1.cs", response.FileName);
         Assert.AreEqual("1.1.1.1", response.Revision);
      }

      [Test]
      public void NotifiedResponseTest()
      {
         NotifiedResponse response = new NotifiedResponse();
         IList<string> lines = new List<string> {"/usr/local/cvsroot/sandbox/mod1/file1.cs"};
         ResponseTest(response, ResponseType.Notified, 1, "/usr/local/cvsroot/sandbox/mod1/file1.cs", lines);
      }

      [Test]
      public void UnknownResponseTest()
      {
         UnknownResponse response = new UnknownResponse();
         IList<string> lines = new List<string> {"D2009.12.31.13.46.32"};
         ResponseTest(response, ResponseType.Unknown, 1, "D2009.12.31.13.46.32", lines);
      }

      [Test]
      public void OkResponseTest()
      {
         OkResponse response = new OkResponse();
         ResponseTest(response, ResponseType.Ok, 1, "ok", new List<string> { "" });
      }

      [Test]
      public void PatchedResponseTest()
      {
         PatchedResponse response = new PatchedResponse();
         IList<string> lines = new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"};
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         ResponseTest(response, ResponseType.Patched, 5, "/usr/local/cvsroot/sandbox/mod1/file1.cs", lines, contents);
      }

      [Test]
      public void RcsDiffResponseTest()
      {
         RcsDiffResponse response = new RcsDiffResponse();
         IList<string> lines = new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"};
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         ResponseTest(response, ResponseType.RcsDiff, 5, "/usr/local/cvsroot/sandbox/mod1/file1.cs", lines, contents);
     }

      [Test]
      public void RemovedResponseTest()
      {
         RemovedResponse response = new RemovedResponse();
         IList<string> lines = new List<string> {"/usr/local/cvsroot/sandbox/mod1/file1.cs"};
         ResponseTest(response, ResponseType.Removed, 1, "/usr/local/cvsroot/sandbox/mod1/file1.cs", lines);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.RepositoryPath);
      }

      [Test]
      public void RemoveEntryResponseTest()
      {
         RemoveEntryResponse response = new RemoveEntryResponse();
         IList<string> lines = new List<string> {"/usr/local/cvsroot/sandbox/mod1/file1.cs"};
         ResponseTest(response, ResponseType.RemoveEntry, 1, "/usr/local/cvsroot/sandbox/mod1/file1.cs", lines);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.RepositoryPath);
      }

      [Test]
      public void SetStaticDirectoryResponseTest()
      {
         SetStaticDirectoryResponse response = new SetStaticDirectoryResponse();
         IList<string> lines = new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/"};
         ResponseTest(response, ResponseType.SetStaticDirectory, 2, "/usr/local/cvsroot/sandbox/mod1/", lines);
         Assert.AreEqual("mod1/", response.ModuleName);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/", response.RepositoryPath);
     }

      [Test]
      public void SetStickyResponseTest()
      {
         SetStickyResponse response = new SetStickyResponse();
         IList<string> lines = new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/"};
         ResponseTest(response, ResponseType.SetSticky, 2, "/usr/local/cvsroot/sandbox/mod1/", lines);
         Assert.AreEqual("mod1/", response.ModuleName);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/", response.RepositoryPath);
      }

      [Test]
      public void TemplateResponseTest()
      {
         TemplateResponse response = new TemplateResponse();
         IList<string> lines = new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"};
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         ResponseTest(response, ResponseType.Template, 5, "/usr/local/cvsroot/sandbox/mod1/file1.cs", lines, contents);
      }

      [Test]
      public void UpdatedResponseTest()
      {
         UpdatedResponse response = new UpdatedResponse();
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         IList<string> lines = new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"};
         ResponseTest(response, ResponseType.Updated, 5, "/usr/local/cvsroot/sandbox/mod1/file1.cs", lines, contents);
      }

      [Test]
      public void UpdateExistingTest()
      {
         UpdateExistingResponse response = new UpdateExistingResponse();
         string contents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         IList<string> lines = new List<string> {"mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74"};
         ResponseTest(response, ResponseType.UpdateExisting, 5, "/usr/local/cvsroot/sandbox/mod1/file1.cs", lines, contents);
      }

      [Test]
      public void ValidRequestsResponseTest()
      {
         ValidRequestsResponse response = new ValidRequestsResponse();
         string process = "Root Valid-responses valid-requests Repository Directory";
         IList<string> lines = new List<string> {process};
         string display = "Root\r\nValid-responses\r\nvalid-requests\r\nRepository\r\nDirectory\r\n";
         ResponseTest(response, ResponseType.ValidRequests, 1, display, lines);
         Assert.AreEqual(5, response.ValidRequestTypes.Count);
         Assert.AreEqual(RequestType.Root, response.ValidRequestTypes[0]);
         Assert.AreEqual(RequestType.Directory, response.ValidRequestTypes[4]);

      }


      [Test]
      public void WrapperRscOptionResponseTest()
      {
         WrapperRscOptionResponse response = new WrapperRscOptionResponse();
         string process = "*.cs -k 'b'";
         IList<string> lines = new List<string> { process };
         ResponseTest(response, ResponseType.WrapperRscOption, 1, "*.cs -k 'b'", lines);
      }

      [Test]
      public void ResponseHelperPatternTest()
      {
         // test auth pattern
         string test = "I LOVE YOU blah";
         string pattern = ResponseHelper.ResponsePatterns[0];
         Match m = Regex.Match(test, pattern);
         Assert.IsTrue(m.Success);
         string data = m.Groups["data"].Value;
         Assert.AreEqual("I LOVE YOU", data);
         // test other patterns
         for (int i = 1; i < 32; i++)
         {
            string responseName = ResponseHelper.ResponseNames[i];
            //Console.WriteLine(responseName);
            test = string.Format("{0} blah", responseName);
            Console.WriteLine(test);
            pattern = ResponseHelper.ResponsePatterns[i];
            m = Regex.Match(test, pattern);
            Assert.IsTrue(m.Success);
            data = m.Groups["data"].Value;
            Assert.AreEqual("blah", data.TrimStart());
         }
      }

      [Test]
      public void CollapseMessageResponsesTest()
      {
         DirectoryInfo di = Directory.GetParent(Environment.CurrentDirectory);
         FileInfo fi = new FileInfo(Path.Combine(di.FullName, "TestSetup\\ExportCommandWithEMessages.xml"));
         TextReader reader = fi.OpenText();
         XDocument xdoc = XDocument.Load(reader);
         Root root = new Root(TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password, TestConfig.RepositoryPath);
         PServerFactory factory = new PServerFactory();
         ICommand cmd = factory.CreateCommand(xdoc, new object[] { root, DateTime.Now });
         IRequest export = cmd.Requests.OfType<ExportRequest>().First();
         Assert.AreEqual(15, export.Responses.Count);
         IList<IResponse> condensed = ResponseHelper.CollapseMessagesInResponses(export.Responses);
         Assert.AreEqual(4, condensed.Count);
         IMessageResponse message = (IMessageResponse) condensed[2];
         Assert.AreEqual(12, message.Lines.Count);
      }

      private void ResponseTest(IResponse response, ResponseType expectedType, int lineCount, string expectedDisplay, IList<string> lines)
      {
         ResponseTest(response, expectedType, lineCount, expectedDisplay, lines, "");
      }

      private void ResponseTest(IResponse response, ResponseType expectedType, int lineCount, string expectedDisplay, IList<string> lines, string fileContents)
      {
         Assert.AreEqual(lineCount, response.LineCount);
         Assert.AreEqual(expectedType, response.Type);
         response.Process(lines);
         if (response is IFileResponse)
            ((IFileResponse)response).File.Contents = fileContents.Encode();
         string display = response.Display();
         Console.WriteLine(display);
         Assert.AreEqual(expectedDisplay, display);
         XElement el = response.GetXElement(); 
         bool result = TestHelper.ValidateResponseXML(el);
         Assert.IsTrue(result);
         Console.WriteLine(el.ToString());         
      }
   }
}