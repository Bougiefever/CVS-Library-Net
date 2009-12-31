using System;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.Tests.TestSetup;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class ExportCommandTest
   {
      private Root _root;

      [SetUp]
      public void SetUp()
      {

         _root = new Root(TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.PasswordScrambled.UnscramblePassword(), TestConfig.RepositoryPath);
         _root.WorkingDirectory = TestConfig.WorkingDirectory;
         _root.Module = TestConfig.ModuleName;
      }

      [Test]
      public void ExecuteCommandTest()
      {
         DateTime date = DateTime.Now.AddDays(1);
         ExportCommand cmd = new ExportCommand(_root, date);
         cmd.Execute();
         TestHelper.SaveCommandConversation(cmd, @"c:\_junk\ExportCommand.xml");

      }

      [Test]
      public void ExportDateTest()
      {
         DateTime date = DateTime.Parse("12/30/2009 16:00:00");
         ExportCommand cmd = new ExportCommand(_root, date);
         string mydate = cmd.GetExportDate(date);
      }

      
   }
}