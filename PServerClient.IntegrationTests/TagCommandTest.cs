using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Tests.TestSetup;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class TagCommandTest
   {
      private IRoot _root;
      private IConnection _connection;

      [SetUp]
      public void SetUp()
      {
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.PasswordScrambled.UnscramblePassword());
         _root.WorkingDirectory = TestConfig.WorkingDirectory;
         _connection = new PServerConnection();
      }

      [Test]
      public void TagCommandExecuteTest()
      {
         TagCommand cmd = new TagCommand(_root, _connection, "mytesttag");
         _root.Module = "abougie/cvstest";
         cmd.Execute();
      }
   }
}