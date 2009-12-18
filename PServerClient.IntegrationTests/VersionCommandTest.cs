using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class VersionCommandTest
   {
      private Root _root;
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

         _root = new Root(_host, _port, _username, _password.UnscramblePassword(), _cvsRootPath);
      }
      [Test]
      public void ExecuteTest()
      {
         ICommand command = new VersionCommand(_root);
         command.Execute();
         // print out the whole conversation, not auth
         IEnumerable<IAuthRequest> auth = command.Requests.OfType<IAuthRequest>();
         IEnumerable<IRequest> reqs = command.Requests.Except(auth.Cast<IRequest>());
         foreach (IRequest req in reqs) 
         {
            Console.Write("C: {0}", req.GetRequestString());
            if (req.ResponseExpected)
               foreach (IResponse res in req.Responses)
               {
                  Console.Write("S: {0}", res.DisplayResponse());
               }
         }
      }
   }
}
