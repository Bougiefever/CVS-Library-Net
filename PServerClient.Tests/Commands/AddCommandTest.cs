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
   /// Tests for the AddCommand class
   /// </summary>
   [TestFixture]
   public class AddCommandTest
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
      /// Tests this instance.
      /// </summary>
      [Test]
      public void TestInitialize()
      {
         AddCommand cmd = new AddCommand(_root, _connection);
         cmd.Initialize();
         Assert.IsTrue(cmd.Requests.Count > 0);
         Assert.AreEqual(1, cmd.Requests.OfType<AddRequest>().Count(), "Add request must be in the request list");
      }
   }
}