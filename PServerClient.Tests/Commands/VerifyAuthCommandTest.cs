using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.Requests;
using PServerClient.Responses;
using Rhino.Mocks;
using Is = Rhino.Mocks.Constraints.Is;

namespace PServerClient.Tests.Commands
{
   [TestFixture]
   public class VerifyAuthCommandTest
   {
      private MockRepository _mocks;
      private IConnection _connection;
      private IAuthRequest _request;
      private IAuthResponse _response;
      private IList<IResponse> _responses;
      private CvsRoot _root;

      [TestFixtureSetUp]
      public void TestFixtureSetUp()
      {
         _root = new CvsRoot("host-name", 1, "username", "password", "/f1/f2/f3", @"c:\f1\f2\f3");
      }

      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
         _connection = _mocks.DynamicMock<IConnection>();
         _request = _mocks.DynamicMock<IAuthRequest>();
         _response = _mocks.DynamicMock<IAuthResponse>();
         _responses = new List<IResponse>() { _response };
      }

      [Test]
      public void VerifyAuthCommandConstructorTest()
      {
         VerifyAuthCommand command = new VerifyAuthCommand(_root);
         command.Requests = new List<IRequest>() { _request };
         int count = command.Requests.OfType<IAuthRequest>().Count();
         Assert.IsTrue(count == 1);
         count = command.Requests.Count();
         Assert.IsTrue(count == 1);
      }

      [Test][Ignore]
      public void VerifyAuthCommandExecuteTest()
      {
         Expect.Call(() => _connection.Connect(string.Empty, 1)).IgnoreArguments().Repeat.Once();
         Expect.Call(_connection.Close).Repeat.Once();
         Expect.Call(() => _connection.DoRequest(null))
            .IgnoreArguments()
            .Constraints(Is.TypeOf<IAuthRequest>());
         Expect.Call(_request.Responses).Return(_responses);
         //Expect.Call(() => _response.ProcessResponse());
         //Expect.Call(_response.Success).Return(true);

         _mocks.ReplayAll();
         VerifyAuthCommand command = new VerifyAuthCommand(_root);
         command.Connection = _connection;
         command.Requests = new List<IRequest>() { _request };
         command.Execute();
         _mocks.VerifyAll();
      }

      [Test][Ignore]
      public void AuthenticateStatusTest()
      {
         Expect.Call(_request.Responses).Return(_responses);
         Expect.Call(_response.Status).Return(AuthStatus.Error);

         _mocks.ReplayAll();
         VerifyAuthCommand command = new VerifyAuthCommand(_root);
         command.Requests = new List<IRequest>() { _request };
         AuthStatus result = command.AuthStatus;
         _mocks.VerifyAll();
         Assert.AreEqual(AuthStatus.Error, result);
      }

      [Test][Ignore]
      public void CommandBaseErrorMessageTest()
      {
         Expect.Call(_request.Responses).Return(_responses);
         //Expect.Call(_response.ErrorMessage).Return("An error message");

         _mocks.ReplayAll();
         VerifyAuthCommand command = new VerifyAuthCommand(_root);
         command.Requests = new List<IRequest>() { _request };
         //string message = command.ErrorMessage;
         _mocks.VerifyAll();
         //Assert.AreEqual("An error message", message);
      }
   }
}
