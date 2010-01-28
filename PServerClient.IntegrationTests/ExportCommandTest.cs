using System;
using System.IO;
using System.Xml.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Tests.TestSetup;

namespace PServerClient.IntegrationTests
{
   /// <summary>
   /// Test of ExportCommand class
   /// </summary>
   [TestFixture]
   public class ExportCommandTest
   {
      private IRoot _root;
      private IConnection _connection;

      /// <summary>
      /// Sets up test data
      /// </summary>
      [SetUp]
      public void SetUp()
      {
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
         _root.WorkingDirectory = TestConfig.WorkingDirectory;
         _root.Module = TestConfig.ModuleName;
         _connection = new PServerConnection();
      }

      /// <summary>
      /// Tests the export command execute.
      /// </summary>
      [Test]
      public void TestExportCommandExecute()
      {
         DateTime date = DateTime.Now.AddDays(1);
         _root.Module = "abougie/TestApp";
         ExportCommand cmd = new ExportCommand(_root, _connection);
         cmd.ExportDate = date;
         cmd.ExportType = ExportType.Date;
         cmd.Execute();
         TestHelper.SaveCommandConversation(cmd, @"c:\_junk\ExportCommandTestApp.xml");
      }

      /// <summary>
      /// Tests the export date.
      /// </summary>
      [Test]
      public void TestExportDate()
      {
         DateTime date = DateTime.Parse("12/30/2009 16:00:00");
         ExportCommand cmd = new ExportCommand(_root, _connection);
         cmd.ExportDate = date;
         cmd.ExportType = ExportType.Date;

         string mydate = cmd.GetExportDate(date);
         Assert.AreEqual("-D 30 Dec 2009 16:00:00 -0000", mydate);
      }

      /// <summary>
      /// Tests the process files.
      /// </summary>
      [Test][Ignore]
      public void TestProcessFiles()
      {
         DirectoryInfo di = Directory.GetParent(Environment.CurrentDirectory);
         FileInfo fi = new FileInfo(Path.Combine(di.FullName, "TestSetup\\ExportCommand.xml"));
         TextReader reader = fi.OpenText();
         XDocument xdoc = XDocument.Load(reader);
         bool result = TestHelper.ValidateCommandXML(xdoc);
         Assert.IsTrue(result);
         PServerFactory factory = new PServerFactory();
         IRoot root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
         root.WorkingDirectory = TestConfig.WorkingDirectory;
         DateTime date = new DateTime();
         ExportCommand cmd = (ExportCommand)factory.CreateCommand(xdoc, new object[] { root, date });
         cmd.AfterExecute();
         ////Assert.AreEqual(4, cmd.FileGroups.Count);
      }
   }
}