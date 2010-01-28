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
   /// Test of CheckoutCommand class
   /// </summary>
   [TestFixture]
   public class CheckoutCommandTest
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
         _root.WorkingDirectory = TestConfig.WorkingDirectory;
         _mocks = new MockRepository();
         _connection = _mocks.Stub<IConnection>();
      }

      /// <summary>
      /// Tests this instance.
      /// </summary>
      [Test]
      public void TestInitialize()
      {
         CheckOutCommand cmd = new CheckOutCommand(_root, _connection);
         cmd.Initialize();
         Assert.IsTrue(cmd.Requests.Count > 0);
         Assert.AreEqual(1, cmd.Requests.OfType<CheckOutRequest>().Count());
         Assert.AreEqual(1, cmd.Requests.OfType<RootRequest>().Count());
         Assert.AreEqual(1, cmd.Requests.OfType<DirectoryRequest>().Count());
      }

      /// <summary>
      /// Tests the CheckoutModule property.
      /// </summary>
      [Test]
      public void TestCheckoutModule()
      {
         CheckOutCommand cmd = new CheckOutCommand(_root, _connection);
         string result = cmd.CheckoutModule;
         Assert.AreEqual(_root.Module, result);
         cmd.CheckoutModule = "mod/source";
         result = cmd.CheckoutModule;
         Assert.AreEqual("mod/source", result);
      }

      /// <summary>
      /// Tests SaveCVSFolder is true.
      /// </summary>
      [Test]
      public void TestSaveCVSFolderIsTrue()
      {
         CheckOutCommand cmd = new CheckOutCommand(_root, _connection);
         Assert.IsTrue(cmd.SaveCVSFolder);
      }
   }
}