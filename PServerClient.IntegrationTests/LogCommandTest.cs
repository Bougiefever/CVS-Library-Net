using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PServerClient.Commands;

namespace PServerClient.Tests.Commands
{
   [TestFixture]
   public class LogCommandTest
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

      [Test][Ignore]
      public void SimpleLogTest()
      {
         LogCommand command = new LogCommand(_root);
         command.LocalOnly = true;
         command.Execute();

      }


   }
}
