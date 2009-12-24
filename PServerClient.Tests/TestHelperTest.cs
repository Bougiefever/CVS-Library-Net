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
         string xml = @"
  <Response>
    <Name>Updated</Name>
    <ResponseType>7</ResponseType>
    <ProcessLines>
      <Line>Updated mod1/</Line>
      <Line>/usr/local/cvsroot/sandbox/mod1/file1.cs</Line>
      <Line>/file1.cs/1.2.3.4///</Line>
      <Line>u=rw,g=rw,o=rw</Line>
      <Line>74</Line>
    </ProcessLines>
    <ResponseFile>
      <Length>74</Length>
      <Contents>47,49,32,58,112,115,101,114,118,101,114,58,97,98,111,117,103,105,101,64,103,98,45,97,105,120,45,113,58,50,52,48,49,47,117,115,114,47,108,111,99,97,108,47,99,118,115,114,111,111,116,47,115,97,110,100,98,111,120,32,65,66,52,37,111,61,119,83,111,98,73,52,119,10</Contents>
    </ResponseFile>
  </Response>";
         XElement responseElement = XElement.Parse(xml);
         IResponse response = TestHelper.XMLToResponse(responseElement);
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
         string xml = @"
  <Response>
    <ResponseName>Auth</ResponseName>
    <ResponseType>0</ResponseType>
    <ProcessLines>
      <Line>I LOVE YOU</Line>
    </ProcessLines>
  </Response>";
         XElement responseElement = XElement.Parse(xml);
         IResponse response = TestHelper.XMLToResponse(responseElement);
         Assert.IsNotNull(response);
         Assert.IsInstanceOf<AuthResponse>(response);
         Assert.AreEqual("I LOVE YOU", response.DisplayResponse());
      }

      [Test]
      public void GetResponsesFromXMLTest()
      {
         string xml = @"
<Responses>
  <Response>
    <Name>Auth</Name>
    <ResponseType>0</ResponseType>
    <ProcessLines>
      <Line>I LOVE YOU</Line>
    </ProcessLines>
  </Response>
  <Response>
    <Name>CheckedIn</Name>
    <ResponseType>5</ResponseType>
    <ProcessLines>
      <Line>Checked-in mod1/</Line>
      <Line>/usr/local/cvsroot/sandbox/mod1/file1.cs</Line>
      <Line>/file1.cs/1.2.3.4///</Line>
      <Line>u=rw,g=rw,o=rw</Line>
      <Line>74</Line>
    </ProcessLines>
    <ResponseFile>
      <Length>74</Length>
      <Contents>47,49,32,58,112,115,101,114,118,101,114,58,97,98,111,117,103,105,101,64,103,98,45,97,105,120,45,113,58,50,52,48,49,47,117,115,114,47,108,111,99,97,108,47,99,118,115,114,111,111,116,47,115,97,110,100,98,111,120,32,65,66,52,37,111,61,119,83,111,98,73,52,119,10</Contents>
    </ResponseFile>
  </Response>
</Responses>";
         XDocument xdoc = XDocument.Parse(xml);
         IList<IResponse> responses = TestHelper.XMLToResponseList(xdoc);
         Assert.AreEqual(2, responses.Count);
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
         response.ProcessResponse(process);
         IRequest request = cmd.RequiredRequests.Where(r => r.RequestType == RequestType.Auth).First();
         request.Responses.Add(response);

         response = new ValidRequestsResponse();
         IList<string> lines = new List<string> { "Root Valid-responses valid-requests Global_option" };
         response.ProcessResponse(lines);
         request = cmd.RequiredRequests.Where(r => r.RequestType == RequestType.ValidRequests).First();
         request.Responses.Add(response);

         IList<IResponse> coresponses = TestHelper.GetMockCheckoutResponses("8 Dec 2009 15:26:27 -0000", "mymod/", "file1.cs");
         request = cmd.Requests.Where(r => r.RequestType == RequestType.CheckOut).First();
         foreach (IResponse cor in coresponses)
         {
            request.Responses.Add(cor);
         }

         XDocument xdoc = TestHelper.CommandToXML(cmd);
         Console.WriteLine(xdoc.ToString());
         bool result = TestHelper.ValidateCommandXML(xdoc);
         Assert.IsTrue(result);
      }

      [Test]
      public void XMLToCommandTest()
      {
         string xml = @"<Command>
  <Name>CheckOut</Name>
  <Type>0</Type>
  <RequiredRequests>
    <Request>
      <Name>Auth</Name>
      <Type>5</Type>
      <Lines>
        <Line>BEGIN AUTH REQUEST</Line>
        <Line>/f1/f2/f3</Line>
        <Line>username</Line>
        <Line>A:yZZ30 e</Line>
        <Line>END AUTH REQUEST</Line>
      </Lines>
      <Responses>
        <Response>
          <Name>Auth</Name>
          <Type>0</Type>
          <Lines>
            <Line>I LOVE YOU</Line>
          </Lines>
        </Response>
      </Responses>
    </Request>
    <Request>
      <Name>UseUnchanged</Name>
      <Type>51</Type>
      <Lines>
        <Line>UseUnchanged</Line>
      </Lines>
      <Responses />
    </Request>
    <Request>
      <Name>ValidResponses</Name>
      <Type>53</Type>
      <Lines>
        <Line>Valid-responses ok error Valid-requests Checked-in New-entry Updated Created Merged Mod-time Removed Set-static-directory Clear-static-directory Set-sticky Clear-sticky Module-expansion M E MT</Line>
      </Lines>
      <Responses />
    </Request>
    <Request>
      <Name>ValidRequests</Name>
      <Type>52</Type>
      <Lines>
        <Line>valid-requests</Line>
      </Lines>
      <Responses>
        <Response>
          <Name>ValidRequests</Name>
          <Type>4</Type>
          <Lines>
            <Line>Valid-requests Root Valid-responses valid-requests Global_option</Line>
          </Lines>
        </Response>
      </Responses>
    </Request>
  </RequiredRequests>
  <Requests>
    <Request>
      <Name>Root</Name>
      <Type>41</Type>
      <Lines>
        <Line>Root /f1/f2/f3</Line>
      </Lines>
      <Responses />
    </Request>
    <Request>
      <Name>GlobalOption</Name>
      <Type>17</Type>
      <Lines>
        <Line>Global_option -q</Line>
      </Lines>
      <Responses />
    </Request>
    <Request>
      <Name>Argument</Name>
      <Type>3</Type>
      <Lines>
        <Line>Argument </Line>
      </Lines>
      <Responses />
    </Request>
    <Request>
      <Name>Directory</Name>
      <Type>11</Type>
      <Lines>
        <Line>Directory .</Line>
        <Line>/f1/f2/f3/</Line>
      </Lines>
      <Responses />
    </Request>
    <Request>
      <Name>CheckOut</Name>
      <Type>9</Type>
      <Lines>
        <Line>co</Line>
      </Lines>
      <Responses>
        <Response>
          <Name>ModTime</Name>
          <Type>23</Type>
          <Lines>
            <Line>Mod-time 8 Dec 2009 15:26:27 -0000</Line>
          </Lines>
        </Response>
        <Response>
          <Name>MessageTag</Name>
          <Type>19</Type>
          <Lines>
            <Line>MT +updated</Line>
          </Lines>
        </Response>
        <Response>
          <Name>MessageTag</Name>
          <Type>19</Type>
          <Lines>
            <Line>MT text U</Line>
          </Lines>
        </Response>
        <Response>
          <Name>MessageTag</Name>
          <Type>19</Type>
          <Lines>
            <Line>MT fname mymod/file1.cs</Line>
          </Lines>
        </Response>
        <Response>
          <Name>MessageTag</Name>
          <Type>19</Type>
          <Lines>
            <Line>MT newline</Line>
          </Lines>
        </Response>
        <Response>
          <Name>MessageTag</Name>
          <Type>19</Type>
          <Lines>
            <Line>MT -updated</Line>
          </Lines>
        </Response>
        <Response>
          <Name>Updated</Name>
          <Type>7</Type>
          <Lines>
            <Line>Updated Updated mymod/</Line>
            <Line>/usr/local/cvsroot/sandbox/mymod/file1.cs</Line>
            <Line>/file1.cs/1.1.1.1///</Line>
            <Line>u=rw,g=rw,o=rw</Line>
            <Line>5</Line>
          </Lines>
          <File>
            <Length>5</Length>
            <Contents>97,98,99,100,101</Contents>
          </File>
        </Response>
      </Responses>
    </Request>
  </Requests>
</Command>
";
         XDocument xdoc = XDocument.Parse(xml);

      }

      [Test]
      public void RequestXMLTest()
      {
         IRequest request = new CheckOutRequest();
         XElement requestElement = TestHelper.RequestToXML(request);
         bool result = TestHelper.ValidateRequestXML(requestElement);
         Assert.IsTrue(result);
      }

      [Test]
      public void ValidateResponseXMLTest()
      {
         string xml = @"
         <Response>
          <Name>Auth</Name>
          <Type>0</Type>
          <Lines>
            <Line>I LOVE YOU</Line>
          </Lines>
        </Response>";
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
         string xml = @"<?xml version='1.0' encoding='utf-8'?>
            <psvr:Lines xmlns:psvr='http://www.pserverclient.org' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance'
               xsi:schemaLocation='http://www.pserverclient.org XMLSchemaTest.xsd'>
                  <psvr:Line>my line 1</psvr:Line>
                  <psvr:Line>line 2</psvr:Line>
            </psvr:Lines>";
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
         string xml = @"<Response>
          <Name>CheckedIn</Name>
          <Type>5</Type>
          <Lines>
            <Line>Checked-in mod1/</Line>
            <Line>/usr/local/cvsroot/sandbox/mod1/file1.cs</Line>
            <Line>/file1.cs/1.2.3.4///</Line>
            <Line>u=rw,g=rw,o=rw</Line>
            <Line>74</Line>
          </Lines>
          <File>
            <Length>74</Length>
            <Contents>47,49,32,58,112,115,101,114,118,101,114,58,97,98,111,117,103,105,101,64,103,98,45,97,105,120,45,113,58,50,52,48,49,47,117,115,114,47,108,111,99,97,108,47,99,118,115,114,111,111,116,47,115,97,110,100,98,111,120,32,65,66,52,37,111,61,119,83,111,98,73,52,119,10</Contents>
          </File>
        </Response>";
         XDocument xdoc = XDocument.Parse(xml);
         xdoc.Validate(schemas, (o, e) => Assert.Fail(e.Message) );

         xml = @"<Response>
          <Name>CheckedIn</Name>
          <Type>5</Type>
          <Lines>
            <Line>Checked-in mod1/</Line>
            <Line>/usr/local/cvsroot/sandbox/mod1/file1.cs</Line>
            <Line>/file1.cs/1.2.3.4///</Line>
            <Line>u=rw,g=rw,o=rw</Line>
            <Line>74</Line>
          </Lines>
        </Response>";
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
         string xml = @"<Request>
      <Name>CheckOut</Name>
      <Type>9</Type>
      <Lines>
         <Line>co</Line>
      </Lines>
      <Responses>
        <Response>
          <Name>Auth</Name>
          <Type>0</Type>
          <Lines />
        </Response>
      </Responses>
      </Request>";
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
         string xml = @"<?xml version='1.0' encoding='utf-8'?>
<Command>
   <Name>CheckOut</Name>
   <Type>0</Type>
   <RequiredRequests>
      <Request>
         <Name>Auth</Name>
         <Type>5</Type>
           <Lines>
            <Line>BEGIN AUTH REQUEST</Line>
            <Line>/usr/local/cvsroot/sandbox</Line>
            <Line>abougie</Line>
            <Line>AB4%o=wSobI4w</Line>
            <Line>END AUTH REQUEST</Line>
         </Lines>
           <Responses>
              <Response>
               <Name>Auth</Name>
               <Type>0</Type>
                 <Lines>
                  <Line>I LOVE YOU</Line>
               </Lines>
            </Response>
         </Responses>
      </Request>
      <Request>
         <Name>UseUnchanged</Name>
         <Type>51</Type>
          <Lines>
            <Line>UseUnchanged</Line>
         </Lines>
         <Responses />
      </Request>
   </RequiredRequests>
   <Requests>
      <Request>
         <Name>Root</Name>
         <Type>41</Type>
         <Lines>
            <Line>Root /usr/local/cvsroot/sandbox</Line>
         </Lines>
         <Responses />
      </Request>
      <Request>
         <Name>GlobalOption</Name>
         <Type>17</Type>
         <Lines>
            <Line>Global_option -q</Line>
         </Lines>
         <Responses />
      </Request>
   </Requests>
</Command>";
         XDocument xdoc = XDocument.Parse(xml);
         xdoc.Validate(schemas, (o, e) => Assert.Fail(e.Message));
         bool result = TestHelper.ValidateCommandXML(xdoc);
         Assert.IsTrue(result);
      }

      [Test]
      public void GetCommandByTypeTest()
      {
         Root root = new Root(TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password, TestConfig.RepositoryPath);
         CommandType t = CommandType.CheckOut;
         ICommand cmd = TestHelper.GetCommandByType(t, root);
         Assert.IsInstanceOf<CheckOutCommand>(cmd);

         t = CommandType.Import;
         cmd = TestHelper.GetCommandByType(t, root);
         Assert.IsInstanceOf<ImportCommand>(cmd);

         t = CommandType.Log;
         cmd = TestHelper.GetCommandByType(t, root);
         Assert.IsInstanceOf<LogCommand>(cmd);

         t = CommandType.ValidRequestsList;
         cmd = TestHelper.GetCommandByType(t, root);
         Assert.IsInstanceOf<ValidRequestsListCommand>(cmd);

         t = CommandType.VerifyAuth;
         cmd = TestHelper.GetCommandByType(t, root);
         Assert.IsInstanceOf<VerifyAuthCommand>(cmd);

         t = CommandType.Version;
         cmd = TestHelper.GetCommandByType(t, root);
         Assert.IsInstanceOf<VersionCommand>(cmd);
      }

      [Test]
      public void CommandXMLToCommandObjectTest()
      {
         string xml = @"<?xml version='1.0' encoding='utf-8'?>
<Command>
   <Name>CheckOut</Name>
   <Type>0</Type>
   <RequiredRequests>
      <Request>
         <Name>Auth</Name>
         <Type>5</Type>
           <Lines>
            <Line>BEGIN AUTH REQUEST</Line>
            <Line>/usr/local/cvsroot/sandbox</Line>
            <Line>abougie</Line>
            <Line>AB4%o=wSobI4w</Line>
            <Line>END AUTH REQUEST</Line>
         </Lines>
           <Responses>
              <Response>
               <Name>Auth</Name>
               <Type>0</Type>
                 <Lines>
                  <Line>I LOVE YOU</Line>
               </Lines>
            </Response>
         </Responses>
      </Request>
      <Request>
         <Name>UseUnchanged</Name>
         <Type>51</Type>
          <Lines>
            <Line>UseUnchanged</Line>
         </Lines>
         <Responses />
      </Request>
   </RequiredRequests>
   <Requests>
      <Request>
         <Name>Root</Name>
         <Type>41</Type>
         <Lines>
            <Line>Root /usr/local/cvsroot/sandbox</Line>
         </Lines>
         <Responses />
      </Request>
      <Request>
         <Name>GlobalOption</Name>
         <Type>17</Type>
         <Lines>
            <Line>Global_option -q</Line>
         </Lines>
         <Responses />
      </Request>
     <Request>
       <Name>CheckOut</Name>
       <Type>9</Type>
       <Lines>
         <Line>co</Line>
       </Lines>
       <Responses>
         <Response>
           <Name>ClearSticky</Name>
           <Type>17</Type>
           <Lines>
             <Line>Clear-sticky abougie/</Line>
             <Line>/usr/local/cvsroot/sandbox/abougie/</Line>
           </Lines>
         </Response>
         <Response>
           <Name>SetStaticDirectory</Name>
           <Type>14</Type>
           <Lines>
             <Line>Set-static-directory abougie/</Line>
             <Line>/usr/local/cvsroot/sandbox/abougie/</Line>
           </Lines>
         </Response>
       </Responses>
     </Request>
   </Requests>
</Command>";
         XDocument xdoc = XDocument.Parse(xml);
         TestHelper.ValidateCommandXML(xdoc);
         Root root = new Root(TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password, TestConfig.RepositoryPath);

         ICommand cmd = TestHelper.CommandXMLToCommandObject(xdoc, root);
         Assert.IsInstanceOf<CheckOutCommand>(cmd);
         
         Assert.AreEqual(2, cmd.RequiredRequests.Count);
         Assert.AreEqual(3, cmd.Requests.Count);

      }
   }
}