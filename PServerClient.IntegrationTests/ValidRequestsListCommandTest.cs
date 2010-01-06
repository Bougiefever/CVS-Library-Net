using System;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Tests.TestSetup;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class ValidRequestsListCommandTest
   {
      private IRoot _root;
      private IConnection _connection;

      [SetUp]
      public void SetUp()
      {
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
         _connection = new PServerConnection();
      }

      [Test]
      public void GetValidRequestsListTest()
      {
         ValidRequestsListCommand command = new ValidRequestsListCommand(_root, _connection);   
         command.Execute();
         Assert.IsTrue(command.ValidRequestTypes.Count > 0, "There are no valid requests");
         foreach (RequestType t in command.ValidRequestTypes)
         {
            Console.WriteLine(RequestHelper.RequestNames[(int)t]);
         }
      }

      [Test]
      public void NotAuthenticatedTest()
      {
         _root.Password = "A:yZZ30 e";
         ValidRequestsListCommand command = new ValidRequestsListCommand(_root, _connection);
         command.Execute();
         Assert.AreEqual(AuthStatus.NotAuthenticated, command.AuthStatus);
      }
   }
}
