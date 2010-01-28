using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;

namespace PServerClient.IntegrationTests
{
   /// <summary>
   /// Test of the VersionCommand class
   /// </summary>
   [TestFixture]
   public class VersionCommandTest
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
      /// Tests the version command execute.
      /// </summary>
      [Test]
      public void TestVersionCommandExecute()
      {
         VersionCommand command = new VersionCommand(_root, _connection);
         command.Execute();
         Console.WriteLine(command.Version);
      }
   }
}
