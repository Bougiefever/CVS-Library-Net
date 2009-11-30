using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
         //response.ResponseString = "blah\n\r blahI LOVE YOUblah";
         response.ProcessResponse(lines);
         Assert.AreEqual(AuthStatus.Authenticated, response.Status);
      }

      [Test]
      public void AuthResponseNotAuthenticatedTest()
      {
         AuthResponse response = new AuthResponse();
         IList<string> lines = new List<string>() { "blah\n\r blahI HATE YOUblah" };
         //response.ResponseString = "blah\n\r blahI HATE YOUblah";
         response.ProcessResponse(lines);
         Assert.AreEqual(AuthStatus.NotAuthenticated, response.Status);
         //Assert.AreEqual(response.ResponseString, response.ErrorMessage);
      }

      [Test]
      public void AuthResponseErrorTest()
      {
         AuthResponse response = new AuthResponse();
         IList<string> lines = new List<string>() { "blah\n\r blahblah" };
         //response.ResponseString = "blah\n\r blahblah";
         response.ProcessResponse(lines);
         Assert.AreEqual(AuthStatus.Error, response.Status);
         //Assert.AreEqual(response.ResponseString, response.ErrorMessage);
      }
   }
}
