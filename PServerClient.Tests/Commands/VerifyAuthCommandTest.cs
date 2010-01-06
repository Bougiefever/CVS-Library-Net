using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;
using Rhino.Mocks;

namespace PServerClient.Tests.Commands
{
   [TestFixture]
   public class VerifyAuthCommandTest
   {

      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
         _connection = _mocks.DynamicMock<IConnection>();
      }

      private MockRepository _mocks;
      private IConnection _connection;
      private IRoot _root;

      [TestFixtureSetUp]
      public void TestFixtureSetUp()
      {
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
      }

      [Test]
      public void VerifyAuthCommandConstructorTest()
      {
         VerifyAuthCommand command = new VerifyAuthCommand(_root, _connection);
         int count = command.RequiredRequests.OfType<IAuthRequest>().Count();
         Assert.IsTrue(count == 1);
         count = command.Requests.Count();
         Assert.IsTrue(count == 0);
      }
   }
}