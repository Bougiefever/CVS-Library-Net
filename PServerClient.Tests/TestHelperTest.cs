using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;
using Rhino.Mocks;

namespace PServerClient.Tests
{
   [TestFixture]
   public class TestHelperTest
   {
      [Test]
      public void FileResponseFromXMLTest()
      {
         string xml = TestStrings.UpdatedResponseXML;
         XElement responseElement = XElement.Parse(xml);
         bool result = TestHelper.ValidateResponseXML(responseElement);
         Assert.IsTrue(result);
         PServerFactory factory = new PServerFactory();
         IResponse response = factory.ResponseXElementToIResponse(responseElement);  
         Assert.IsNotNull(response);
         Assert.IsInstanceOf<UpdatedResponse>(response);
         IFileResponse fileResponse = (IFileResponse) response;
         Assert.AreEqual(74, fileResponse.Length);
         string expected = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         string fileContents = fileResponse.Contents.Decode();
         Assert.AreEqual(expected, fileContents);
      }

      [Test]
      public void GetByteStringTest()
      {
         byte[] buff = new byte[3];
         buff[0] = 97;
         buff[1] = 98;
         buff[2] = 99;

         string result = ResponseHelper.FileContentsToByteArrayString(buff);
         Assert.AreEqual("97,98,99", result);
      }


      [Test]
      public void GetResponseFromXMLTest()
      {
         string xml = TestStrings.AuthResponseXML;
         XElement responseElement = XElement.Parse(xml);
         bool result = TestHelper.ValidateResponseXML(responseElement);
         Assert.IsTrue(result);
         PServerFactory factory = new PServerFactory();
         IResponse response = factory.ResponseXElementToIResponse(responseElement); 
         Assert.IsNotNull(response);
         Assert.IsInstanceOf<AuthResponse>(response);
         Assert.AreEqual("I LOVE YOU", response.Display());
      }

      [Test]
      public void GetValidResponsesStringTest()
      {
         ResponseType[] types = new[] { ResponseType.Ok, ResponseType.MTMessage, ResponseType.EMessage };
         string rtypes = ResponseHelper.GetValidResponsesString(types);
         Assert.AreEqual("ok MT E", rtypes);
      }

      [Test]
      public void CommandToXMLTest()
      {
         IRoot root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
         MockRepository mocks = new MockRepository();
         IConnection connection = mocks.Stub<IConnection>();
         CheckOutCommand cmd = new CheckOutCommand(root, connection);

         // add requests to items
         foreach (ICommandItem item in cmd.RequiredRequests)
         {
            cmd.Items.Add(item);
         }

         foreach (ICommandItem item in cmd.Requests)
         {
            cmd.Items.Add(item);
         }

         // add responses to command
         IResponse response = new AuthResponse();
         IList<string> process = new List<string> { "I LOVE YOU" };
         response.Initialize(process);
         cmd.Items.Add(response);

         response = new ValidRequestsResponse();
         IList<string> lines = new List<string> { "Root Valid-responses valid-requests Global_option" };
         response.Initialize(lines);
         cmd.Items.Add(response);

         IList<IResponse> coresponses = TestHelper.GetMockCheckoutResponses("8 Dec 2009 15:26:27 -0000", "mymod/", "file1.cs");
         foreach (IResponse cor in coresponses)
         {
            cmd.Responses.Add(cor);
         }

         XDocument xdoc = cmd.GetXDocument();
         Console.WriteLine(xdoc.ToString());
         bool result = TestHelper.ValidateCommandXML(xdoc);
         Assert.IsTrue(result);
      }

      [Test]
      public void ValidatorResponseXMLTest()
      {
         string xml = TestStrings.AuthResponseXML;
         XElement response = XElement.Parse(xml);
         bool result = TestHelper.ValidateResponseXML(response);
         Assert.IsTrue(result);
      }

      [Test]
      public void ValidatorRequestXMLTest()
      {
         string xml = TestStrings.AuthRequestXML;
         XElement request = XElement.Parse(xml);
         bool result = TestHelper.ValidateRequestXML(request);
         Assert.IsTrue(result);
      }

      [Test]
      public void ValidatorCommandXMLTest()
      {
         string xml = TestStrings.CommandWithCommandItemsXML1;
         XDocument xdoc = XDocument.Parse(xml);
         bool result = TestHelper.ValidateCommandXML(xdoc);
         Assert.IsTrue(result);
      }

      [Test]
      public void Validator2CommandXMLTest()
      {
         string xml = TestStrings.CommandWithCommandItemsXML2;
         XDocument xdoc = XDocument.Parse(xml);
         bool result = TestHelper.ValidateCommandXML(xdoc);
         Assert.IsTrue(result);
      }

      [Test]
      public void CommandXMLFileSchemaTest()
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\Schemas\Command.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());
         var validateSchema = XmlSchema.Read(reader, (o, e) => Assert.Fail(e.Message));
         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add(validateSchema);

         FileInfo fi2 = new FileInfo(@"c:\_junk\XMLFile2.xml");
         XmlReader reader2 = XmlReader.Create(fi2.OpenRead());
         XDocument xdoc = XDocument.Load(reader2);
         xdoc.Validate(schemas, (o, e) => Assert.Fail(e.Message));
         Console.WriteLine(xdoc.ToString());
      }

      [Test]
      public void XMLSchemaWithTargetNamespsceTest()
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\Schemas\XMLSchemaTest.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());
         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add("http://www.pserverclient.org", reader);
         bool isValid = true;
         string xml = TestStrings.XMLWithTargetNamespace;
         XDocument xdoc = XDocument.Parse(xml);
         xdoc.Validate(schemas, (o, e) => { isValid = false; Assert.Fail(e.Message); });
         Assert.IsTrue(isValid);
      }

      [Test]
      public void ResponseSchemaTest()
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\Schemas\Response.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());
         var validateSchema = XmlSchema.Read(reader, (o, e) => Assert.Fail(e.Message));
         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add(validateSchema);
         string xml = TestStrings.CheckedInWithFileContentsResponse;
         XDocument xdoc = XDocument.Parse(xml);
         xdoc.Validate(schemas, (o, e) => Assert.Fail(e.Message));

         xml = TestStrings.CheckedInResponse;
         xdoc = XDocument.Parse(xml);
         xdoc.Validate(schemas, (o, e) => Assert.Fail(e.Message));
      }

      [Test]
      public void RequestSchemaTest()
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\Schemas\Request.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());
         var validateSchema = XmlSchema.Read(reader, (o, e) => Assert.Fail(e.Message));
         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add(validateSchema);
         string xml = TestStrings.CheckOutRequest;
         XDocument xdoc = XDocument.Parse(xml);
         xdoc.Validate(schemas, (o, e) => Assert.Fail(e.Message));
      }

      [Test]
      public void ValidateCommandXMLTest()
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\Schemas\Command.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());
         var validateSchema = XmlSchema.Read(reader, (o, e) => Assert.Fail(e.Message));
         XmlSchemaSet schemas = new XmlSchemaSet();
         schemas.Add(validateSchema);
         string xml = TestStrings.CommandXMLFile;
         XDocument xdoc = XDocument.Parse(xml);
         xdoc.Validate(schemas, (o, e) => Assert.Fail(e.Message));
         bool result = TestHelper.ValidateCommandXML(xdoc);
         Assert.IsTrue(result);
      }

      [Test]
      public void CommandXMLToCommandObjectTest()
      {
         string xml = TestStrings.CommandXMLFileWithManyItems;
         XDocument xdoc = XDocument.Parse(xml);
         bool result = TestHelper.ValidateCommandXML(xdoc);
         Assert.IsTrue(result);
         IRoot root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
         PServerFactory factory = new PServerFactory();
         IConnection connection = new PServerConnection();
         ICommand cmd = factory.CreateCommand(xdoc, new object[] { root, connection }); 
         Assert.IsInstanceOf<CheckOutCommand>(cmd);
         
         Assert.AreEqual(2, cmd.RequiredRequests.Count);
         Assert.AreEqual(3, cmd.Requests.Count);
         Assert.AreEqual(3, cmd.Items.OfType<IResponse>().Count());
      }

      [Test]
      public void RegexTest()
      {
         string test = "Valid-requests Root Valid-responses";
         string pattern = @"^Valid-requests\s(?<data>.*)";
         Match m = Regex.Match(test, pattern);
         string data = m.Groups["data"].Value;
         Assert.AreEqual("Root Valid-responses", data);
         pattern = @"(?<data>.*)";
         m = Regex.Match(test, pattern);
         data = m.Groups["data"].Value;
         Assert.AreEqual("Valid-requests Root Valid-responses", data);
      }

      [Test]
      public void ImportXMLResponseWithMultipleLinesTest()
      {
         string xml = TestStrings.MTResponse;
         XElement responseElement = XElement.Parse(xml);
         PServerFactory factory = new PServerFactory();
         IResponse response = factory.ResponseXElementToIResponse(responseElement);
         Assert.AreEqual(5, response.Lines.Count);
      }
   }
}