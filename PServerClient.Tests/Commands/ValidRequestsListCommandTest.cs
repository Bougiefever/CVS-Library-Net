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

      [Test][Ignore]
      public void CommandBaseExecuteTest()
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
         command.Requests = new List<IRequest>() {authRequest, otherRequest};
         Expect.Call(() => connection.Connect(null, 0))
            .IgnoreArguments()
            .Repeat.Once();
         Expect.Call(connection.Close).Repeat.Once();
         Expect.Call(authRequest.Responses).Return(authResponses);
         Expect.Call(otherRequest.Responses).Return(otherResponses);
         Expect.Call(() => connection.DoRequest(null))
            .IgnoreArguments()
            .Repeat.Twice();
         //Expect.Call(authResponse.ProcessResponse).Repeat.Once();
         //Expect.Call(otherResponse.ProcessResponse).Repeat.Once();
         //Expect.Call(authResponse.Success).Return(true);
         //Expect.Call(otherResponse.Success).Return(true);

         mocks.ReplayAll();
         command.Execute();
         mocks.VerifyAll();
      }

      [Test]
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
         //Expect.Call(authResponse.ProcessResponse).Repeat.Once();
         //Expect.Call(otherResponse.ProcessResponse).Repeat.Once().Repeat.Never();
         //Expect.Call(authResponse.Success).Return(false);
         //Expect.Call(otherResponse.Success).Repeat.Never();

         mocks.ReplayAll();
         command.Execute();
         mocks.VerifyAll();
      }
   }
}
