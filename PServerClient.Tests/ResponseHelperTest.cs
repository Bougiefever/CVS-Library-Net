using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;

namespace PServerClient.Tests
{
   [TestFixture]
   public class ResponseHelperTest
   {
      [Test]
      public void FixResponseModuleSlashesTest()
      {
         string mod = "mymod/";
         string result = ResponseHelper.FixResponseModuleSlashes(mod);
         Assert.AreEqual("mymod", result);

         mod = "mymod/Properties/";
         result = ResponseHelper.FixResponseModuleSlashes(mod);
         Assert.AreEqual("mymod/Properties", result);

         mod = "mymod/cvstest/CVSROOT";
         result = ResponseHelper.FixResponseModuleSlashes(mod);
         Assert.AreEqual(mod, result);

         mod = "/mymod/";
         result = ResponseHelper.FixResponseModuleSlashes(mod);
         Assert.AreEqual("mymod", result);

         mod = "/mymod/cvstest/CVSROOT/";
         result = ResponseHelper.FixResponseModuleSlashes(mod);
         Assert.AreEqual("mymod/cvstest/CVSROOT", result);
      }

      [Test]
      public void GetLastModuleNameTest()
      {
         string mod = "mymod/";
         string result = ResponseHelper.GetLastModuleName(mod);
         Assert.AreEqual("mymod", result);

         mod = "mymod/Properties/";
         result = ResponseHelper.GetLastModuleName(mod);
         Assert.AreEqual("Properties", result);

         mod = "mymod/cvstest/CVSROOT/";
         result = ResponseHelper.GetLastModuleName(mod);
         Assert.AreEqual("CVSROOT", result);
      }

      [Test]
      public void CollapseMessageResponsesTest()
      {
         DirectoryInfo di = Directory.GetParent(Environment.CurrentDirectory);
         FileInfo fi = new FileInfo(Path.Combine(di.FullName, "TestSetup\\ExportCommandWithEMessages.xml"));
         TextReader reader = fi.OpenText();
         XDocument xdoc = XDocument.Load(reader);
         ////bool result = TestHelper.ValidateCommandXML(xdoc);
         ////Assert.IsTrue(result);
         IRoot root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
         root.WorkingDirectory = TestConfig.WorkingDirectory;
         PServerFactory factory = new PServerFactory();
         IConnection connection = new PServerConnection();
         ICommand cmd = factory.CreateCommand(xdoc, new object[] { root, connection, DateTime.Now });
         Assert.AreEqual(18, cmd.Responses.Count);
         Assert.AreEqual(12, cmd.Responses.OfType<EMessageResponse>().Count());
         IList<IResponse> condensed = ResponseHelper.CollapseMessagesInResponses(cmd.Responses);
         Assert.AreEqual(7, condensed.Count);
         IMessageResponse message = (IMessageResponse)condensed[5];
         Assert.AreEqual(12, message.Lines.Count);
         Console.WriteLine(message.Display());
      }

      [Test]
      public void GetInfoFromUpdatedTest()
      {
         string test = "/.cvspass/1.1.1.1///";
         string name = ResponseHelper.GetFileNameFromEntryLine(test);
         string revision = ResponseHelper.GetRevisionFromEntryLine(test);
         Assert.AreEqual(".cvspass", name);
         Assert.AreEqual("1.1.1.1", revision);
      }

      [Test]
      [ExpectedException(typeof(ArgumentException))]
      public void GetInfoFromUpdatedExceptionTest()
      {
         string test = "abcd";
         string revision = ResponseHelper.GetRevisionFromEntryLine(test);
      }
   }
}
