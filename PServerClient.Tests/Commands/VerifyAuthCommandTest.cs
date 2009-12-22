using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;
using Rhino.Mocks;

namespace PServerClient.Tests.Commands
{
   [TestFixture]
   public class VerifyAuthCommandTest
   {
      #region Setup/Teardown

      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
         _connection = _mocks.DynamicMock<IConnection>();
      }

      #endregion

      private MockRepository _mocks;
      private IConnection _connection;
      private Root _root;

      [TestFixtureSetUp]
      public void TestFixtureSetUp()
      {
         _root = new Root("host-name", 1, "username", "password", "/f1/f2/f3");
      }

      [Test]
      public void CommandBaseStatusTest()
      {
         VerifyAuthCommand command = new VerifyAuthCommand(_root);
         IAuthRequest request = (IAuthRequest) command.RequiredRequests[0];
         IAuthResponse response = _mocks.DynamicMock<IAuthResponse>();
         request.Responses = new List<IResponse> {response};

         Expect.Call(response.Status).Return(AuthStatus.NotAuthenticated);
         _mocks.ReplayAll();

         AuthStatus result = command.AuthStatus;
         _mocks.VerifyAll();
         Assert.AreEqual(AuthStatus.NotAuthenticated, result);
      }

      [Test]
      public void VerifyAuthCommandConstructorTest()
      {
         VerifyAuthCommand command = new VerifyAuthCommand(_root);
         int count = command.RequiredRequests.OfType<IAuthRequest>().Count();
         Assert.IsTrue(count == 1);
         count = command.Requests.Count();
         Assert.IsTrue(count == 0);
      }
   }
}