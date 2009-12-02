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
      private CvsRoot _root = new CvsRoot("host-name", 1, "username", "password", "/f1/f2/f3", @"c:\f1\f2\f3");
      public RequestTests()
      {
         _root.Module = "mod1";
      }

      [Test]
      public void RootRequestTest()
      {
         IRequest request = new RootRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "Root /f1/f2/f3\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
      }

      [Test]
      public void ValidResponsesRequestTest()
      {
         IRequest request = new ValidResponsesRequest("res1 res2 res3");
         string actual = request.GetRequestString();
         const string expected = "Valid-responses res1 res2 res3\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
      }

      [Test]
      public void ValidRequestsRequestTest()
      {
         IRequest request = new ValidRequestsRequest();
         string actual = request.GetRequestString();
         const string expected = "valid-requests\n";
         Assert.AreEqual(expected, actual);
         Assert.IsTrue(request.ResponseExpected);
      }

      [Test]
      public void DirectoryRequestTest()
      {
         IRequest request = new DirectoryRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "Directory .\n/f1/f2/f3/mod1\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
      }

      [Test]
      public void MaxDotRequestTest()
      {
         IRequest request = new MaxDotRequest("one");
         string actual = request.GetRequestString();
         const string expected = "Max-dotdot one\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
      }

      [Test]
      public void StaticDirectoryRequestTest()
      {
         IRequest request = new StaticDirectoryRequest();
         string actual = request.GetRequestString();
         const string expected = "Static-directory\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
      }

      [Test]
      public void EntryRequestTest()
      {
         IRequest request = new EntryRequest("file.cs", "1.1.1", "", "", "");
         string actual = request.GetRequestString();
         const string expected = "Entry /file.cs/1.1.1///\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
      }

      [Test]
      public void KoptRequestTest()
      {
         IRequest request = new KoptRequest("-kb");
         string actual = request.GetRequestString();
         const string expected = "Kopt -kb\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
      }

      [Test]
      public void CheckinTimeRequestTest()
      {
         IRequest request = new CheckinTimeRequest(new DateTime(2009, 11, 6, 14, 21, 8));
         string actual = request.GetRequestString();
         const string expected = "Checkin-time 06 Nov 2009 14:21:08 -0000\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
      }

      [Test]
      public void ModifiedRequestTest()
      {
         IRequest request = new ModifiedRequest("file.cs");
         string actual = request.GetRequestString();
         const string expected = "Modified file.cs\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
      }

      [Test]
      public void IsModifiedRequestTest()
      {
         IRequest request = new IsModifiedRequest("file.cs");
         string actual = request.GetRequestString();
         const string expected = "Is-modified file.cs\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
      }

      [Test]
      public void UnchangedRequestTest()
      {
         IRequest request = new UnchangedRequest("file.cs");
         string actual = request.GetRequestString();
         const string expected = "Unchanged file.cs\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
      }

      [Test]
      public void UseUnchangedRequestTest()
      {
         IRequest request = new UseUnchangedRequest();
         string actual = request.GetRequestString();
         const string expected = "UseUnchanged\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
      }

      [Test]
      public void NotifyRequestTest()
      {
         IRequest request = new NotifyRequest("file.cs");
         string actual = request.GetRequestString();
         const string expected = "Notify file.cs\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
      }

      [Test]
      public void QuestionableRequestTest()
      {
         IRequest request = new NotifyRequest("file.cs");
         string actual = request.GetRequestString();
         const string expected = "Notify file.cs\n";
         Assert.AreEqual(expected, actual);
         Assert.IsFalse(request.ResponseExpected);
      }

      [Test]
      public void VerifyAuthRequestTest()
      {
         IAuthRequest request = new VerifyAuthRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "BEGIN VERIFICATION REQUEST\n/f1/f2/f3\nusername\nA:yZZ30 e\nEND VERIFICATION REQUEST\n";
         Assert.AreEqual(expected, actual);
         Assert.IsNotNull(request.Responses);
      }

      [Test]
      public void AuthRequestTest()
      {
         IAuthRequest request = new AuthRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "BEGIN AUTH REQUEST\n/f1/f2/f3\nusername\nA:yZZ30 e\nEND AUTH REQUEST\n";
         Assert.AreEqual(expected, actual);
         Assert.IsNotNull(request.Responses);
      }

      //[Test]
      //public void ValidRequestsRequestTest()
      //{
      //   ValidRequestsRequest request = new ValidRequestsRequest();
      //   string expected = "valid-requests \n";
      //   Assert.AreEqual(expected, request.GetRequestString());
      //   Assert.IsNotNull(request.Responses);
      //}
   }
}
