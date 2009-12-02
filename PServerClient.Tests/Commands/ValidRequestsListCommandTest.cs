using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.Requests;
using PServerClient.Responses;
using Rhino.Mocks;

namespace PServerClient.Tests.Commands
{
   [TestFixture]
   public class ValidRequestsListCommandTest
   {
      private CvsRoot _root;

      [TestFixtureSetUp]
      public void TestFixtureSetUp()
      {
         _root = new CvsRoot("host-name", 1, "username", "password", "/f1/f2/f3", @"c:\f1\f2\f3");
      }

      [Test]
      public void ConstructorTest()
      {
         ValidRequestsListCommand command = new ValidRequestsListCommand(_root);
         int requestCount = command.Requests.OfType<IAuthRequest>().Count();
         Assert.IsTrue(requestCount == 1);
         requestCount = command.Requests.OfType<ValidRequestsRequest>().Count();
         Assert.IsTrue(requestCount == 1);
      }

      [Test]
      public void RequestListTest()
      {
         ValidRequestsListCommand command = new ValidRequestsListCommand(_root);
         ValidRequestResponse response = new ValidRequestResponse();
         response.ProcessResponse(new List<string>() {"req1 req1 req3"});
         command.Requests[1].Responses = new List<IResponse>() { response };
         var result = command.RequestList;
         Assert.AreEqual(3, result.Count);
      }

      [Test]
      public void CommandBaseExecuteTest()
      {
         MockRepository mocks = new MockRepository();
         IConnection connection = mocks.DynamicMock<IConnection>();
         IRequest authRequest = mocks.Stub<IAuthRequest>();
         IResponse authResponse = mocks.DynamicMock<IAuthResponse>();
         IList<IResponse> authResponses = new List<IResponse>() { authResponse };
         IRequest otherRequest = mocks.Stub<IRequest>();
         IResponse otherResponse = mocks.DynamicMock<IResponse>();
         IList<IResponse> otherResponses = new List<IResponse>() { otherResponse };

         ValidRequestsListCommand command = new ValidRequestsListCommand(_root);
         command.Connection = connection;
         command.Requests = new List<IRequest>() {authRequest, otherRequest};

         Expect.Call(() => connection.Connect(null, 0))
            .IgnoreArguments()
            .Repeat.Once();
         Expect.Call(connection.DoRequest(authRequest)).Return(authResponses);
         Expect.Call(connection.DoRequest(otherRequest)).Return(otherResponses);
         Expect.Call(connection.Close).Repeat.Once();

         mocks.ReplayAll();
         command.Execute();
         mocks.VerifyAll();
      }

      //[Test][Ignore]
      public void CommandBaseExecuteWhenAuthFailedTest()
      {
         MockRepository mocks = new MockRepository();
         IConnection connection = mocks.DynamicMock<IConnection>();
         IRequest authRequest = mocks.DynamicMock<IAuthRequest>();
         IResponse authResponse = mocks.DynamicMock<IAuthResponse>();
         IList<IResponse> authResponses = new List<IResponse>() { authResponse };
         IRequest otherRequest = mocks.DynamicMock<IRequest>();
         IResponse otherResponse = mocks.DynamicMock<IResponse>();
         IList<IResponse> otherResponses = new List<IResponse>() { otherResponse };

         ValidRequestsListCommand command = new ValidRequestsListCommand(_root);
         command.Connection = connection;
         command.Requests = new List<IRequest>() { authRequest, otherRequest };
         Expect.Call(() => connection.Connect(null, 0))
            .IgnoreArguments()
            .Repeat.Once();
         Expect.Call(connection.Close).Repeat.Once();
         Expect.Call(authRequest.Responses).Return(authResponses);
         Expect.Call(otherRequest.Responses).Return(otherResponses).Repeat.Never();
         Expect.Call(() => connection.DoRequest(null))
            .IgnoreArguments()
            .Repeat.Once();

         mocks.ReplayAll();
         command.Execute();
         mocks.VerifyAll();
      }
   }
}
