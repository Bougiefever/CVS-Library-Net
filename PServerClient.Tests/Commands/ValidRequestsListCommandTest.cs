using System.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.Requests;

namespace PServerClient.Tests.Commands
{
   [TestFixture]
   public class ValidRequestsListCommandTest
   {
      private Root _root;

      [TestFixtureSetUp]
      public void TestFixtureSetUp()
      {
         _root = new Root("host-name", 1, "username", "password", "/f1/f2/f3");
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
