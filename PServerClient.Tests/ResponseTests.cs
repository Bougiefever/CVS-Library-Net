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
         response.ResponseString = "blah\n\r blahI LOVE YOUblah";
         response.ProcessResponse();
         Assert.AreEqual(AuthStatus.Authenticated, response.Status);
      }

      [Test]
      public void AuthResponseNotAuthenticatedTest()
      {
         AuthResponse response = new AuthResponse();
         response.ResponseString = "blah\n\r blahI HATE YOUblah";
         response.ProcessResponse();
         Assert.AreEqual(AuthStatus.NotAuthenticated, response.Status);
         Assert.AreEqual(response.ResponseString, response.ErrorMessage);
      }

      [Test]
      public void AuthResponseErrorTest()
      {
         AuthResponse response = new AuthResponse();
         response.ResponseString = "blah\n\r blahblah";
         response.ProcessResponse();
         Assert.AreEqual(AuthStatus.Error, response.Status);
         Assert.AreEqual(response.ResponseString, response.ErrorMessage);
      }
   }
}
