using System;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.Tests.TestSetup;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class ValidRequestsListCommandTest
   {
      private Root _root;
      [SetUp]
      public void SetUp()
      {
         _root = new Root(TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.PasswordScrambled.UnscramblePassword(), TestConfig.RepositoryPath);
      }

      [Test]
      public void GetValidRequestsListTest()
      {
         ValidRequestsListCommand command = new ValidRequestsListCommand(_root);   
         command.Execute();
         foreach (RequestType t in command.ValidRequestTypes)
         {
            Console.WriteLine(RequestHelper.RequestNames[(int)t]);
         }
      }

      [Test]
      public void NotAuthenticatedTest()
      {
         _root.Password = "A:yZZ30 e";
         ValidRequestsListCommand command = new ValidRequestsListCommand(_root);
         command.Execute();
         Assert.AreEqual(AuthStatus.NotAuthenticated, command.AuthStatus);
      }
   }
}
