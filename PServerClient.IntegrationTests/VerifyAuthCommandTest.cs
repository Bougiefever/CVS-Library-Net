using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Requests;
using PServerClient.Connection;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class VerifyAuthCommandTest
   {
      private CvsRoot _root;
      private string _username;
      private string _password;
      private string _cvsRootPath;
      //private string _workingDirectory;
      private string _host;
      private int _port;
      [SetUp]
      public void SetUp()
      {
         _host = "gb-aix-q";
         _port = 2401;
         _username = "abougie";
         _password = "AB4%o=wSobI4w";
         _cvsRootPath = "/usr/local/cvsroot/sandbox";
         //_workingDirectory = "";

         _root = new CvsRoot(_host, _port, _username, _password.UnscramblePassword(), _cvsRootPath);
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
