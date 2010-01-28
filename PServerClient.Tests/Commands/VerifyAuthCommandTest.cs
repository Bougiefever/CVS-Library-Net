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
   /// <summary>
   /// Tests for the VerifyAuthCommand class
   /// </summary>
   [TestFixture]
   public class VerifyAuthCommandTest
   {
      private MockRepository _mocks;
      private IConnection _connection;
      private IRoot _root;

      /// <summary>
      /// Sets up mocking
      /// </summary>
      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
         _connection = _mocks.DynamicMock<IConnection>();
      }

      /// <summary>
      /// Sets up the test root
      /// </summary>
      [TestFixtureSetUp]
      public void TestFixtureSetUp()
      {
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
      }

      /// <summary>
      /// Verifies the auth command constructor
      /// </summary>
      [Test]
      public void VerifyAuthCommandInitializeTest()
      {
         VerifyAuthCommand command = new VerifyAuthCommand(_root, _connection);
         command.Initialize();
         int count = command.RequiredRequests.OfType<IAuthRequest>().Count();
         Assert.IsTrue(count == 1);
         count = command.Requests.Count();
         Assert.IsTrue(count == 0);
      }
   }
}