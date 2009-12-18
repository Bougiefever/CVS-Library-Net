using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class LogCommandTest
   {
      private Root _root;


      [SetUp]
      public void SetUp()
      {
         _root = new Root(TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.PasswordScrambled.UnscramblePassword(), TestConfig.RepositoryPath);
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
