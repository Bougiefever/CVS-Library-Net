using NUnit.Framework;
using PServerClient.Responses;

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
         IResponse response = _factory.CreateResponse(test);
         Assert.IsInstanceOf<NullResponse>(response);
      }

      [Test]
      public void CreateAuthResponse()
      {
         string test = "I LOVE YOU\n";
         IResponse response = _factory.CreateResponse(test);
         Assert.IsInstanceOf<AuthResponse>(response);
         test = "I HATE YOU blah blah \n";
         response = _factory.CreateResponse(test);
         Assert.IsInstanceOf<AuthResponse>(response);
         test = "a different string \n";
         response = _factory.CreateResponse(test);
         Assert.IsNotInstanceOf<AuthResponse>(response);
      }

      [Test]
      public void CreateMessageResponse()
      {
         string test = "M my message\n";
         IResponse response = _factory.CreateResponse(test);
         Assert.IsInstanceOf<MessageResponse>(response);
         test = "Not M first \n";
         response = _factory.CreateResponse(test);
         Assert.IsNotInstanceOf<MessageResponse>(response);
      }
   }
}
