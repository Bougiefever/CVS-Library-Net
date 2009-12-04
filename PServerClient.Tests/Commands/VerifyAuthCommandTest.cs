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
      private CvsRoot _root;

      [TestFixtureSetUp]
      public void TestFixtureSetUp()
      {
         _root = new CvsRoot("host-name", 1, "username", "password", "/f1/f2/f3");
      }

      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
         _connection = _mocks.DynamicMock<IConnection>();
      }

      [Test]
      public void VerifyAuthCommandConstructorTest()
      {
         VerifyAuthCommand command = new VerifyAuthCommand(_root);
         int count = command.Requests.OfType<IAuthRequest>().Count();
         Assert.IsTrue(count == 1);
         count = command.Requests.Count();
         Assert.IsTrue(count == 1);
      }

      [Test]
      public void CommandBaseStatusTest()
      {
         VerifyAuthCommand command = new VerifyAuthCommand(_root);
         IAuthRequest request = (IAuthRequest)command.Requests[0];
         IAuthResponse response = _mocks.DynamicMock<IAuthResponse>();
         request.Responses = new List<IResponse>() { response };

         Expect.Call(response.Status).Return(AuthStatus.NotAuthenticated);
         _mocks.ReplayAll();

         AuthStatus result = command.AuthStatus;
         _mocks.VerifyAll();
         Assert.AreEqual(AuthStatus.NotAuthenticated, result);
      }
   }
}
