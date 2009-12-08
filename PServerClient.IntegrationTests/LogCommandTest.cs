using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PServerClient.Commands;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class LogCommandTest
   {
      private CvsRoot _root;
      private string _username;
      private string _password;
      private string _cvsRootPath;
      private string _workingDirectory;
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
         _workingDirectory = "";

         _root = new CvsRoot(_host, _port, _username, _password.UnscramblePassword(), _cvsRootPath);
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
