using System;
using System.Collections.Generic;
using NUnit.Framework;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Tests
{
   [TestFixture]
   public class ResponseTests
   {
      [Test]
      public void GetValidResponsesStringTest()
      {
         ResponseType[] types = new[] { ResponseType.Ok, ResponseType.MessageTag, ResponseType.EMessage };
         string rtypes = ResponseHelper.GetValidResponsesString(types);
         Assert.AreEqual("ok MT E", rtypes);
      }

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
         IList<string> lines = new List<string>() { "Root Valid-responses valid-requests Global_option" };
         response.ProcessResponse(lines);
         Assert.AreEqual(4, response.ValidRequestTypes.Count);
         Assert.AreEqual(RequestType.Root, response.ValidRequestTypes[0]);
         Assert.AreEqual(RequestType.GlobalOption, response.ValidRequestTypes[3]);
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
         string expected = "+updated";
         Assert.AreSame(expected, response.Message);
         Assert.AreEqual(1, response.LineCount);
      }

      [Test]
      public void FileResponseBaseProcessTest()
      {
         UpdatedResponse response = new UpdatedResponse();
         IList<string> lines = new List<string>() { "mod1/", "/usr/local/cvsroot/sandbox/mod1/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74" };
         response.ProcessResponse(lines);
         ReceiveFile file = response.File;
         Assert.AreEqual("mod1", file.Path[0]);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/file1.cs", file.CvsPath);
         Assert.AreEqual("file1.cs", file.FileName);
         Assert.AreEqual("1.2.3.4", file.Revision);
         Assert.AreEqual("u=rw,g=rw,o=rw", file.Properties);
         Assert.AreEqual(74, file.FileLength);
         Assert.AreEqual(5, response.LineCount);
      }

      [Test]
      public void FileResponseBaseProcessMultipleDirectoriesTest()
      {
         UpdatedResponse response = new UpdatedResponse();
         IList<string> lines = new List<string>() { "mod1/mod2/mod3/", "/usr/local/cvsroot/sandbox/mod1/mod2/mod3/file1.cs", "/file1.cs/1.2.3.4///", "u=rw,g=rw,o=rw", "74" };
         response.ProcessResponse(lines);
         ReceiveFile file = response.File;
         Assert.AreEqual("mod1", file.Path[0]);
         Assert.AreEqual("mod2", file.Path[1]);
         Assert.AreEqual("mod3", file.Path[2]);
         Assert.AreEqual("/usr/local/cvsroot/sandbox/mod1/mod2/mod3/file1.cs", file.CvsPath);
         Assert.AreEqual("file1.cs", file.FileName);
         Assert.AreEqual("1.2.3.4", file.Revision);
         Assert.AreEqual("u=rw,g=rw,o=rw", file.Properties);
         Assert.AreEqual(74, file.FileLength);
         Assert.AreEqual(5, response.LineCount);
      }
   }
}
