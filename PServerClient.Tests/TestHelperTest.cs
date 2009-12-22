using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using PServerClient.Responses;

namespace PServerClient.Tests
{
   [TestFixture]
   public class TestHelperTest
   {
      [Test]
      public void ValidateResponseXMLTest()
      {
         string xml = @"
<Requests>
   <Request>
      <Name>Auth</Name>
      <RequestType>5</RequestType>
      <Responses>
        <Response>
          <Name>Auth</Name>
          <ResponseType>0</ResponseType>
          <ProcessLines>
            <Line>I LOVE YOU</Line>
          </ProcessLines>
        </Response>
      </Responses>
   </Request>
</Requests>";
         XDocument xdoc = XDocument.Parse(xml);
         bool result = TestHelper.ValidateResponseXML(xdoc);
         Assert.IsTrue(result);

         xml = @"
<Requests>
   <Request>
      <Name>Auth</Name>
      <RequestType>5</RequestType>
      <Responses>
        <Response>
          <ResponseName>Auth</ResponseName>
          <ResponseType>0</ResponseType>
          <ProcessLines>
            <Line>I LOVE YOU</Line>
          </ProcessLines>
        </Response>
      </Responses>
   </Request>
</Requests>";
         xdoc = XDocument.Parse(xml);
         result = TestHelper.ValidateResponseXML(xdoc);
         Assert.IsFalse(result);

         xml = @"
<Requests>
   <Request>
      <Name>CheckIn</Name>
      <RequestType>7</RequestType>
      <Responses>
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
      </Responses>
   </Request>
</Requests>";
         xdoc = XDocument.Parse(xml);
         result = TestHelper.ValidateResponseXML(xdoc);
         Assert.IsTrue(result);
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
      public void GetValidResponsesStringTest()
      {
         ResponseType[] types = new[] { ResponseType.Ok, ResponseType.MessageTag, ResponseType.EMessage };
         string rtypes = ResponseHelper.GetValidResponsesString(types);
         Assert.AreEqual("ok MT E", rtypes);
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
         IResponse response = TestHelper.ResponseFromXElement(responseElement);
         Assert.IsNotNull(response);
         Assert.IsInstanceOf<AuthResponse>(response);
         Assert.AreEqual("I LOVE YOU", response.DisplayResponse());
      }

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
         IResponse response = TestHelper.ResponseFromXElement(responseElement);
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
         IList<IResponse> responses = TestHelper.ResponsesFromXML(xdoc);
         Assert.AreEqual(2, responses.Count);
      }

      [Test]
      public void RequestXMLTest()
      {
         
      }
   }
}