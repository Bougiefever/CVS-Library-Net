using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class ValidRequestsListCommandTest
   {
      private CvsRoot _root;
      private string _username;
      private string _password;
      private string _cvsRootPath;
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
         _root = new CvsRoot(_host, _port, _username, _password.UnscramblePassword(), _cvsRootPath);
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
