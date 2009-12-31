using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;

namespace PServerClient.Tests
{
   [TestFixture]
   public class ResponseProcessorTest
   {
      [Test]
      public void CreateFileGroupsFromResponsesTest()
      {
         ResponseProcessor processor = new ResponseProcessor();
         DirectoryInfo di = Directory.GetParent(Environment.CurrentDirectory);
         FileInfo fi = new FileInfo(Path.Combine(di.FullName, "TestSetup\\ExportCommand.xml"));
         TextReader reader = fi.OpenText();
         XDocument xdoc = XDocument.Load(reader);
         bool result = TestHelper.ValidateCommandXML(xdoc);
         Assert.IsTrue(result);
         PServerFactory factory = new PServerFactory();
         Root root = new Root(TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password, TestConfig.RepositoryPath);
         DateTime date = new DateTime();
         ICommand cmd = factory.CreateCommand(xdoc, new object[] {root, date});
         IRequest request = cmd.Requests.OfType<ExportRequest>().First();
         IList<IResponse> responses = request.Responses;
         IList<IFileResponseGroup> files = processor.CreateFileGroupsFromResponses(responses);
         Assert.AreEqual(4, files.Count);
         IFileResponseGroup file = files[0];
         Assert.AreEqual(DateTime.Parse("12/8/2009 3:26:27 PM"), file.ModTime.ModTime);
         Assert.AreEqual("fname abougie/cvstest/AssemblyInfo.cs", file.MT.Lines[2]);
         Assert.AreEqual("AssemblyInfo.cs", file.FileResponse.File.Name);
      }
   }
}