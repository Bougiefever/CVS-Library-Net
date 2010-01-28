using System;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Tests.TestSetup;

namespace PServerClient.IntegrationTests
{
   /// <summary>
   /// Test of the ValidRequestsListCommand class
   /// </summary>
   [TestFixture]
   public class ValidRequestsListCommandTest
   {
      private IRoot _root;
      private IConnection _connection;

      /// <summary>
      /// Sets up test data.
      /// </summary>
      [SetUp]
      public void SetUpTestData()
      {
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
         _connection = new PServerConnection();
      }

      /// <summary>
      /// Tests the get valid requests list execute.
      /// </summary>
      [Test]
      public void TestGetValidRequestsListExecute()
      {
         ValidRequestsListCommand command = new ValidRequestsListCommand(_root, _connection);   
         command.Execute();
         Assert.IsTrue(command.ValidRequestTypes.Count > 0, "There are no valid requests");
         foreach (RequestType t in command.ValidRequestTypes)
         {
            Console.WriteLine(RequestHelper.RequestNames[(int)t]);
         }
      }

      /// <summary>
      /// Tests the get valid requests list when not authenticated.
      /// </summary>
      [Test]
      public void TestGetValidRequestsListWhenNotAuthenticated()
      {
         _root.Password = "A:yZZ30 e";
         ValidRequestsListCommand command = new ValidRequestsListCommand(_root, _connection);
         command.Execute();
         Assert.AreEqual(AuthStatus.NotAuthenticated, command.AuthStatus);
      }
   }
}
