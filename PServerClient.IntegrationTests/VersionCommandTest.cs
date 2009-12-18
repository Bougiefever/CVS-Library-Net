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

      [SetUp]
      public void SetUp()
      {
         _root = new Root(TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.PasswordScrambled.UnscramblePassword(), TestConfig.RepositoryPath);
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
