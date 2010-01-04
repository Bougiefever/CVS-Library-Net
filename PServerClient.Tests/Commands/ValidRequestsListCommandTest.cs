using System.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Tests.TestSetup;

namespace PServerClient.Tests.Commands
{
   [TestFixture]
   public class ValidRequestsListCommandTest
   {
      private IRoot _root;

      [TestFixtureSetUp]
      public void TestFixtureSetUp()
      {
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
      }

      [Test]
      public void ConstructorTest()
      {
         ValidRequestsListCommand command = new ValidRequestsListCommand(_root);
         int requestCount = command.RequiredRequests.OfType<IAuthRequest>().Count();
         Assert.IsTrue(requestCount == 1);
         requestCount = command.RequiredRequests.OfType<ValidRequestsRequest>().Count();
         Assert.IsTrue(requestCount == 1);
      }
   }
}