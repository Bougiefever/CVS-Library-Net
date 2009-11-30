using NUnit.Framework;
using PServerClient.Responses;
using System.Collections.Generic;

namespace PServerClient.Tests
{
   [TestFixture]
   public class ResponseFactoryTests
   {
      private ResponseFactory _factory = new ResponseFactory();
      [Test]
      public void UnknownResponseReturnsNullResponseObject()
      {
         string test = "blah there is no Blah response\n";
         IResponse response = _factory.CreateResponse(ResponseType.Unknown, test);
         Assert.IsInstanceOf<NullResponse>(response);
      }

      [Test]
      public void CreateAuthResponse()
      {
         string test = "I LOVE YOU\n";
         IResponse response = _factory.CreateResponse(ResponseType.Auth, test);
         Assert.IsInstanceOf<AuthResponse>(response);
         test = "I HATE YOU blah blah \n";
         response = _factory.CreateResponse(ResponseType.Auth, test);
         Assert.IsInstanceOf<AuthResponse>(response);
         test = "a different string \n";
         response = _factory.CreateResponse(ResponseType.Auth, test);
         Assert.IsNotInstanceOf<AuthResponse>(response);
      }

      [Test]
      public void CreateMessageResponse()
      {
         string test = "M my message\n";
         IResponse response = _factory.CreateResponse(ResponseType.Message, test);
         Assert.IsInstanceOf<MessageResponse>(response);
         test = "Not M first \n";
         response = _factory.CreateResponse(ResponseType.Unknown, test);
         Assert.IsNotInstanceOf<MessageResponse>(response);
      }

      [Test]
      public void CreateValidRequestsResponse()
      {
          string test = "Valid-requests my-first-request my-second-request\n";
          IResponse response = _factory.CreateResponse(ResponseType.ValidRequests, test);
          Assert.IsInstanceOf<ValidRequestResponse>(response);
      }

      [Test]
      public void CreateResponsesTest()
      {
         IList<string> test = new List<string>() { "I LOVE YOU\n" };
         ResponseFactory factory = new ResponseFactory();
         IList<IResponse> responses = factory.CreateResponses(test);
         Assert.AreEqual(1, responses.Count);
         IResponse response = responses[0];
         Assert.IsInstanceOf<IAuthResponse>(response);
      }

      [Test]
      public void GetResponseLinesOneLineTest()
      {
         string test = "M my message\n";
         IList<string> lines = new List<string>() {"preceding string", test, "another string" };
         ResponseFactory factory = new ResponseFactory();
         IList<string> result = factory.GetResponseLines(ResponseType.Message, lines, 1, 1);

      }

      [Test]
      public void GetResponseLinesLineCountLinesTest()
      { 
      }

      [Test]
      public void GetResponseLinesUnknownLineCountTest()
      { 
      }
   }
}
