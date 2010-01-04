using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.Tests.TestSetup;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class LogCommandTest
   {
      private IRoot _root;


      [SetUp]
      public void SetUp()
      {
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
      }

      [Test][Ignore]
      public void SimpleLogTest()
      {
         LogCommand command = new LogCommand(_root);
         command.LocalOnly = true;
         command.Execute();

      }


   }
}
