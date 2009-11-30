using NUnit.Framework;
using PServerClient.Requests;
using PServerClient.Responses;
using System.Collections.Generic;

namespace PServerClient.Tests
{
   [TestFixture]
   public class RequestTests
   {
      private CvsRoot _root = new CvsRoot("host-name", 1, "username", "password", "/f1/f2/f3", @"c:\f1\f2\f3");

      [Test][Ignore]
      public void RequestBaseGetResponseTest()
      {
         IAuthRequest request = new AuthRequest(_root);
         IList<string> test = new List<string>() { "I LOVE YOU\n" };

         request.ProcessResponses(test);
         //request.SetCvsResponse(test);
         //request.GetResponses();
         //string result = request.Responses[0].ResponseString;
         //Assert.AreEqual(test, result);
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

      [Test]
      public void ValidRequestsRequestTest()
      {
         ValidRequestsRequest request = new ValidRequestsRequest();
         string expected = "valid-requests \n";
         Assert.AreEqual(expected, request.GetRequestString());
         Assert.IsNotNull(request.Responses);
      }
   }
}
