using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PServerClient.Commands;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class ValidRequestsListCommandTest
   {
      private CvsRoot _root;
      private string _username;
      private string _password;
      private string _repositoryPath;
      private string _localPath;
      private string _host;
      private int _port;
      [SetUp]
      public void SetUp()
      {
         _host = "gb-aix-q";
         _port = 2401;
         _username = "abougie";
         _password = "AB4%o=wSobI4w";
         _repositoryPath = "/usr/local/cvsroot/sandbox";
         _localPath = "";

         _root = new CvsRoot(_host, _port, _username, _password.UnscramblePassword(), _repositoryPath, _localPath);
      }

      [Test]
      public void GetValidRequestsListTest()
      {
         ValidRequestsListCommand command = new ValidRequestsListCommand(_root);   
         command.Execute();
         IList<string> requests = command.RequestList;
         Assert.IsNotNull(requests);
         foreach (string s in requests)
         {
            Console.WriteLine(s);
         }
      }

      [Test]
      public void NotAuthenticatedTest()
      {
         _root.Password = "A:yZZ30 e";
         ValidRequestsListCommand command = new ValidRequestsListCommand(_root);
         command.Execute();
         Assert.AreEqual(AuthStatus.NotAuthenticated, command.AuthStatus);
         Assert.AreEqual("I HATE YOU\n", command.ErrorMessage);
      }
   }
}
