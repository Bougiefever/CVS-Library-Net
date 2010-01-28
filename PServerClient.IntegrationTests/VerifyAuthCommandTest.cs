using System;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Tests.TestSetup;

namespace PServerClient.IntegrationTests
{
   /// <summary>
   /// Tests the VerifyAuthCommand class
   /// </summary>
   [TestFixture]
   public class VerifyAuthCommandTest
   {
      private IRoot _root;
      private IConnection _connection;

      /// <summary>
      /// Sets up testing.
      /// </summary>
      [SetUp]
      public void SetUpTesting()
      {
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
         _connection = new PServerConnection();
      }

      /// <summary>
      /// Tests the authenticate success.
      /// </summary>
      [Test]
      public void TestAuthenticateSuccess()
      {
         VerifyAuthCommand command = new VerifyAuthCommand(_root, _connection);
         command.Execute();
         AuthStatus status = command.AuthStatus;
         Assert.AreEqual(AuthStatus.Authenticated, status);
      }

      /// <summary>
      /// Tests the authenticate error.
      /// </summary>
      [Test]
      public void TestAuthenticateError()
      {
         _root.Username = "no-such-user";
         VerifyAuthCommand command = new VerifyAuthCommand(_root, _connection);
         command.Execute();
         Assert.AreEqual(AuthStatus.Error, command.AuthStatus);
      }

      /// <summary>
      /// Tests the authenticate not authenticated.
      /// </summary>
      [Test]
      public void TestAuthenticateNotAuthenticated()
      {
         _root.Password = "A:yZZ30 e";
         VerifyAuthCommand command = new VerifyAuthCommand(_root, _connection);
         command.Execute();
         Assert.AreEqual(AuthStatus.NotAuthenticated, command.AuthStatus);
      }

      /// <summary>
      /// Tests the auth request.
      /// </summary>
      [Test]
      public void TestAuthRequest()
      {
         AuthRequest auth = new AuthRequest(_root);
         PServerConnection connection = new PServerConnection();
         connection.Connect(_root);
         connection.DoRequest(auth);
         var response = connection.GetResponse();
         Console.WriteLine(response.Display());
         connection.Close();
      }
   }
}
