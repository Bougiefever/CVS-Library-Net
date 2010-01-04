using System;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Connection;
using PServerClient.Tests.TestSetup;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class VerifyAuthCommandTest
   {
      private IRoot _root;

      [SetUp]
      public void SetUp()
      {
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
      }

      [Test]
      public void AuthenticateSuccessTest()
      {
         VerifyAuthCommand command = new VerifyAuthCommand(_root);
         command.Execute();
         AuthStatus status = command.AuthStatus;
         Assert.AreEqual(AuthStatus.Authenticated, status);
      }

      [Test]
      public void AuthenticateErrorTest()
      {
         _root.Username = "no-such-user";
         VerifyAuthCommand command = new VerifyAuthCommand(_root);
         command.Execute();
         Assert.AreEqual(AuthStatus.Error, command.AuthStatus);
      }

      [Test]
      public void AuthenticateNotAuthenticatedTest()
      {
         _root.Password = "A:yZZ30 e";
         VerifyAuthCommand command = new VerifyAuthCommand(_root);
         command.Execute();
         Assert.AreEqual(AuthStatus.NotAuthenticated, command.AuthStatus);
      }

      [Test]
      public void AuthTest()
      {
         AuthRequest auth = new AuthRequest(_root);
         string s = auth.GetRequestString();
         Console.WriteLine(s);
         CvsTcpClient client = new CvsTcpClient();
         client.Connect(_root.Host, _root.Port);
         byte[] bbb = s.Encode();
         client.Write(bbb);
         byte[] rrr = client.Read();
         string s2 = rrr.Decode();
         Console.WriteLine(s2);
         client.Close();
      }

      [Test]
      public void AuthRequestTest()
      {
         AuthRequest auth = new AuthRequest(_root);
         PServerConnection connection = new PServerConnection();
         connection.Connect(_root);
         var result = connection.DoRequest(auth);
         connection.Close();
      }
   }
}
