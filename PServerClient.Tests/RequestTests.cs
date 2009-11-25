using NUnit.Framework;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.Tests
{
   [TestFixture]
   public class RequestTests
   {
      private CvsRoot _root = new CvsRoot("host-name", 1, "username", "password", "/f1/f2/f3", @"c:\f1\f2\f3");

      [Test]
      public void RequestBaseGetResponseTest()
      {
         AuthRequest request = new AuthRequest(_root);
         string test = "I LOVE YOU\n";
         request.SetCvsResponse(test);
         request.GetResponse();
         string result = request.Response.ResponseString;
         Assert.AreEqual(test, result);
      }

      [Test]
      public void VerifyAuthRequestTest()
      {
         VerifyAuthRequest request = new VerifyAuthRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "BEGIN VERIFICATION REQUEST\n/f1/f2/f3\nusername\nA:yZZ30 e\nEND VERIFICATION REQUEST\n";
         Assert.AreEqual(expected, actual);
         Assert.IsNotNull(request.Response);
      }

      [Test]
      public void AuthRequestTest()
      {
         AuthRequest request = new AuthRequest(_root);
         string actual = request.GetRequestString();
         const string expected = "BEGIN AUTH REQUEST\n/f1/f2/f3\nusername\nA:yZZ30 e\nEND AUTH REQUEST\n";
         Assert.AreEqual(expected, actual);
         Assert.IsNotNull(request.Response);
      }

      [Test]
      public void ValidRequestsRequestTest()
      {
         ValidRequestsRequest request = new ValidRequestsRequest();
         string expected = "valid-requests \n";
         Assert.AreEqual(expected, request.GetRequestString());
         Assert.IsNotNull(request.Response);
      }
   }
}
