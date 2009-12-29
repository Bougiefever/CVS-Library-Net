using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;

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
         IResponse response = factory.ResponseXElementToIResponse(responseElement);  //TestHelper.ResponseXElementToIResponse(responseElement);
         Assert.IsNotNull(response);
         Assert.IsInstanceOf<UpdatedResponse>(response);
         IFileResponse fileResponse = (IFileResponse) response;
         ReceiveFile file = fileResponse.File;
         Assert.AreEqual(74, file.Length);
         string expected = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w\n";
         string fileContents = file.Contents.Decode();
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
      public void GetInfoFromUpdatedTest()
      {
         string test = "/.cvspass/1.1.1.1///";
         string name = ResponseHelper.GetFileNameFromUpdatedLine(test);
         string revision = ResponseHelper.GetRevisionFromUpdatedLine(test);
         Assert.AreEqual(".cvspass", name);
         Assert.AreEqual("1.1.1.1", revision);
      }

      [Test]
      public void GetResponseFromXMLTest()
      {
         string xml = TestStrings.AuthResponseXML;
         XElement responseElement = XElement.Parse(xml);
         bool result = TestHelper.ValidateResponseXML(responseElement);
         Assert.IsTrue(result);
         PServerFactory factory = new PServerFactory();
         IResponse response = factory.ResponseXElementToIResponse(responseElement);  //TestHelper.ResponseXElementToIResponse(responseElement);
         Assert.IsNotNull(response);
         Assert.IsInstanceOf<AuthResponse>(response);
         Assert.AreEqual("I LOVE YOU", response.Display());
      }

      [Test]
      public void GetValidResponsesStringTest()
      {
         ResponseType[] types = new[] {ResponseType.Ok, ResponseType.MessageTag, ResponseType.EMessage};
         string rtypes = ResponseHelper.GetValidResponsesString(types);
         Assert.AreEqual("ok MT E", rtypes);
      }

      [Test]
      public void CommandToXMLTest()
      {
         Root root = new Root(TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password, TestConfig.RepositoryPath);
         CheckOutCommand cmd = new CheckOutCommand(root);

         // add responses to requests
         IResponse response = new AuthResponse();
         IList<string> process = new List<string> { "I LOVE YOU" };
         response.Process(process);
         IRequest request = cmd.RequiredRequests.Where(r => r.Type == RequestType.Auth).First();
         request.Responses.Add(response);

         response = new ValidRequestsResponse();
         IList<string> lines = new List<string> { "Root Valid-responses valid-requests Global_option" };
         response.Process(lines);
         request = cmd.RequiredRequests.Where(r => r.Type == RequestType.ValidRequests).First();
         request.Responses.Add(response);

         IList<IResponse> coresponses = TestHelper.GetMockCheckoutResponses("8 Dec 2009 15:26:27 -0000", "mymod/", "file1.cs");
         request = cmd.Requests.Where(r => r.Type == RequestType.CheckOut).First();
         foreach (IResponse cor in coresponses)
         {
            request.Responses.Add(cor);
         }

         XDocument xdoc = cmd.GetXDocument();
         Console.WriteLine(xdoc.ToString());
         bool result = TestHelper.ValidateCommandXML(xdoc);
         Assert.IsTrue(result);
      }

      [Test]
      public void ValidateResponseXMLTest()
      {
         string xml = TestStrings.AuthResponseXML;
         XElement response = XElement.Parse(xml);
         bool result = TestHelper.ValidateResponseXML(response);
         Assert.IsTrue(result);
      }

      [Test]
      public void XMLSchemaWithTargetNamespsceTest()
      {
         FileInfo fi = new FileInfo(@"..\..\SharedLib\Schemas\XMLSchemaTest.xsd");
         XmlReader reader = XmlReader.Create(fi.OpenRead());
         //var validateSchema = XmlSchema.Read(reader, (o, e) => Assert.Fail(e.Message));
         XmlSchemaSet schemas = new XmlSchemaSet();
         //schemas.Add(validateSchema);
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
         xdoc.Validate(schemas, (o, e) => Assert.Fail(e.Message) );

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
         Root root = new Root(TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password, TestConfig.RepositoryPath);
         PServerFactory factory =new PServerFactory();
         ICommand cmd = factory.CreateCommand(xdoc, new object[] { root }); //TestHelper.CommandXElementToICommand(xdoc, root);
         Assert.IsInstanceOf<CheckOutCommand>(cmd);
         
         Assert.AreEqual(2, cmd.RequiredRequests.Count);
         Assert.AreEqual(3, cmd.Requests.Count);

         AuthRequest authRequest = cmd.RequiredRequests.OfType<AuthRequest>().First();
         Assert.AreEqual(1, authRequest.Responses.Count);
         Assert.AreEqual(AuthStatus.Authenticated, authRequest.Status);

      }
   }
}