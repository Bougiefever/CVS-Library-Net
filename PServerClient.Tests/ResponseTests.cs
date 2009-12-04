using System;
using System.Collections.Generic;
using NUnit.Framework;
using PServerClient.Responses;

namespace PServerClient.Tests
{
   [TestFixture]
   public class ResponseTests
   {
      [Test]
      public void AuthResponseAuthenticatedTest()
      {
         AuthResponse response = new AuthResponse();
         IList<string> lines = new List<string>() { "blah\n\r blahI LOVE YOUblah" };
         response.ProcessResponse(lines);
         Assert.AreEqual(AuthStatus.Authenticated, response.Status);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void AuthResponseNotAuthenticatedTest()
      {
         AuthResponse response = new AuthResponse();
         IList<string> lines = new List<string>() { "blah\n\r blahI HATE YOUblah" };
         response.ProcessResponse(lines);
         Assert.AreEqual(AuthStatus.NotAuthenticated, response.Status);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void ValidRequestsResponseProcessTest()
      {
         ValidRequestResponse response = new ValidRequestResponse();
         IList<string> lines = new List<string>() { "Root Valid-responses valid-requests Repository Directory" };
         response.ProcessResponse(lines);
         Assert.AreEqual(5, response.ValidRequests.Count);
         Assert.AreEqual("Root", response.ValidRequests[0]);
         Assert.AreEqual("Directory", response.ValidRequests[4]);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void OkProcessResponseTest()
      {
         OkResponse response = new OkResponse();
         IList<string> lines = new List<string>() { "" };
         response.ProcessResponse(lines);
         Assert.AreEqual(lines.Count, response.LineCount);
         // no error means passing test
      }

      [Test]
      public void ModuleExpansionProcessResponseTest()
      {
         ModuleExpansionResponse response = new ModuleExpansionResponse();
         IList<string> lines = new List<string>() { "module name" };
         response.ProcessResponse(lines);
         Assert.AreEqual("module name", response.ModuleName);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void ClearStickyProcessResponseTest()
      {
         ClearStickyResponse response = new ClearStickyResponse();
         IList<string> lines = new List<string>() { "module/", "/f1/f2/f3/" };
         response.ProcessResponse(lines);
         Assert.AreEqual("module/", response.ModuleName);
         Assert.AreEqual("/f1/f2/f3/", response.CvsDirectory);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void ClearStaticDirectoryProcessResponseTest()
      {
         ClearStaticDirectoryResponse response = new ClearStaticDirectoryResponse();
         IList<string> lines = new List<string>() { "module/", "/f1/f2/f3/" };
         response.ProcessResponse(lines);
         Assert.AreEqual("module/", response.ModuleName);
         Assert.AreEqual("/f1/f2/f3/", response.CvsDirectory);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void ModTimeProcessResponseTest()
      {
         ModTimeResponse response = new ModTimeResponse();
         IList<string> lines = new List<string>() { "27 Nov 2009 14:21:06 -0000" };
         response.ProcessResponse(lines);
         DateTime expected = new DateTime(2009, 11, 27, 14, 21, 6);
         Assert.AreEqual(expected, response.ModTime);
         Assert.AreEqual(lines.Count, response.LineCount);
      }

      [Test]
      public void MessageTagProcessResponseTest()
      {
         MessageTagResponse response = new MessageTagResponse();
         IList<string> lines = new List<string>() { "+updated" };
         response.ProcessResponse(lines);
         Assert.AreSame(lines, response.MessageLines);
         Assert.AreEqual(1, response.LineCount);
      }

      [Test][Ignore]
      public void UpdatedProcessResponseTest()
      {
         UpdatedResponse response = new UpdatedResponse();
         IList<string> lines = new List<string>() { "mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/.cvspass/1.1.1.1///", "u=rw,g=rw,o=rw","74", "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w"};
         response.ProcessResponse(lines);
         //Assert.AreEqual("mod1/", response.ModuleName);
         //Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", response.CvsPath);
         //Entry entry = response.CvsEntry;
         //Assert.AreEqual("/.cvspass/1.1.1.1///", entry.FileName);
         //Assert.AreEqual("u=rw,g=rw,o=rw", entry.Properties);
         //Assert.AreEqual(74, entry.FileLength);
         Assert.AreEqual(5, response.LineCount);
      }
   }
}
