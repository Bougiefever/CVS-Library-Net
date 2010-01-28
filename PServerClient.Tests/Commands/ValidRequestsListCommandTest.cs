using System.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Tests.TestSetup;
using Rhino.Mocks;

namespace PServerClient.Tests.Commands
{
   /// <summary>
   /// Test of ValidRequestsListCommand class
   /// </summary>
   [TestFixture]
   public class ValidRequestsListCommandTest
   {
      private IRoot _root;
      private MockRepository _mocks;
      private IConnection _connection;

      /// <summary>
      /// Sets up for testing
      /// </summary>
      [TestFixtureSetUp]
      public void TestFixtureSetUp()
      {
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
         _mocks = new MockRepository();
         _connection = _mocks.Stub<IConnection>();
      }

      /// <summary>
      /// Tests the constructor.
      /// </summary>
      [Test]
      public void TestConstructor()
      {
         ValidRequestsListCommand command = new ValidRequestsListCommand(_root, _connection);
         int requestCount = command.RequiredRequests.OfType<IAuthRequest>().Count();
         Assert.IsTrue(requestCount == 1);
         requestCount = command.RequiredRequests.OfType<ValidRequestsRequest>().Count();
         Assert.IsTrue(requestCount == 1);
      }
   }
}