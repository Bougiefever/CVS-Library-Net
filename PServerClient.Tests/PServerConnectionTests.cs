using System;
using System.Collections.Generic;
using NUnit.Framework;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;
using Rhino.Mocks;
using Is=Rhino.Mocks.Constraints.Is;

namespace PServerClient.Tests
{
   [TestFixture]
   public class PServerConnectionTests
   {
      #region Setup/Teardown

      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
         _client = _mocks.StrictMock<ICvsTcpClient>();
         _connection = new PServerConnection();
         _connection.TcpClient = _client;
      }

      #endregion

      private MockRepository _mocks;
      private ICvsTcpClient _client;
      private PServerConnection _connection;

      private int[] GetMockReadBytes(IList<string> lines, int lastChar)
      {
         byte[] cBytes = new byte[0];
         byte[] copy;
         foreach (string line in lines)
         {
            byte[] lineBytes = line.Encode();
            copy = new byte[cBytes.Length];
            cBytes.CopyTo(copy, 0);
            cBytes = new byte[cBytes.Length + lineBytes.Length + 1];
            copy.CopyTo(cBytes, 0);
            lineBytes.CopyTo(cBytes, copy.Length);
            cBytes[cBytes.Length - 1] = 10;
         }
         int[] iBytes = new int[cBytes.Length + 1];
         cBytes.CopyTo(iBytes, 0);
         iBytes[iBytes.Length - 1] = lastChar;
         return iBytes;
      }

      [Test]
      public void CloseTest()
      {
         Expect.Call(_client.Close);
         _mocks.ReplayAll();
         _connection.Close();
         _mocks.VerifyAll();
      }

      [Test]
      public void ConnectTest()
      {
         Expect.Call(() => _client.Connect(null, 0))
            .IgnoreArguments()
            .Constraints(Is.Equal("host-name"), Is.Equal(1));
         _mocks.ReplayAll();
         string host = TestConfig.CVSHost;
         int port = TestConfig.CVSPort;
         string user = TestConfig.Username;
         string pwd = TestConfig.Password;
         string path = TestConfig.RepositoryPath;
         Root root = new Root(host, port, user, pwd, path);
         _connection.Connect(root);
         _mocks.VerifyAll();
      }

      [Test]
      public void GetResponseLinesMultipleLinesTest()
      {
         IList<string> lines = new List<string>
                                  {
                                     "/usr/local/cvsroot/sandbox/abougie/.cvspass",
                                     "/.cvspass/1.1.1.1///",
                                     "u=rw,g=rw,o=rw",
                                     "74"
                                  };
         int[] readBytes = GetMockReadBytes(lines, -1);
         using (_mocks.Record())
         {
            for (int i = 0; i < readBytes.Length - 1; i++)
            {
               Expect.Call(_client.ReadByte()).Return(readBytes[i]).Repeat.Once();
            }
         }
         using (_mocks.Playback())
         {
            var result = _connection.GetResponseLines("Updated abougie/", ResponseType.Updated, 5);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("abougie/", result[0]);
         }
      }

      [Test]
      public void GetResponseLinesOneLineTest()
      {
         IList<string> lines = new List<string>
                                  {
                                     "Valid-requests Root Valid-responses valid-requests Repository Directory"
                                  };

         var result = _connection.GetResponseLines(lines[0], ResponseType.ValidRequests, 1);

         Assert.AreEqual(1, result.Count);
         Assert.AreEqual("Root Valid-responses valid-requests Repository Directory", result[0]);
      }

      [Test]
      public void GetResponsesFileResponseTest()
      {
         IList<string> lines = new List<string>
                                  {
                                     "Updated abougie/",
                                     "/usr/local/cvsroot/sandbox/abougie/.cvspass",
                                     "/.cvspass/1.1.1.1///",
                                     "u=rw,g=rw,o=rw",
                                     "74"
                                  };
         string fileContents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w";
         int[] readBytes = GetMockReadBytes(lines, -1);
         for (int i = 0; i < readBytes.Length; i++)
            Expect.Call(_client.ReadByte()).Return(readBytes[i]).Repeat.Once();
         Expect.Call(_client.ReadBytes(74))
            .Return(fileContents.Encode());
         _mocks.ReplayAll();
         var result = _connection.GetResponses();
         _mocks.VerifyAll();
         Assert.AreEqual(1, result.Count);
         IFileResponse response = (IFileResponse) result[0];
         string testFile = response.File.Contents.Decode();
         Assert.AreEqual(fileContents, testFile);
      }

      [Test]
      public void GetResponsesOneLineResponsesTest()
      {
         IList<string> lines = new List<string>
                                  {
                                     "MT +updated",
                                     "MT text U ",
                                     "MT fname abougie/.cvspass",
                                     "MT newline",
                                     "MT -updated"
                                  };
         int[] readBytes = GetMockReadBytes(lines, -1);
         for (int i = 0; i < readBytes.Length; i++)
            Expect.Call(_client.ReadByte()).Return(readBytes[i]).Repeat.Once();
         _mocks.ReplayAll();
         var result = _connection.GetResponses();
         _mocks.VerifyAll();
         Assert.AreEqual(5, result.Count);
         Assert.IsInstanceOf<MessageTagResponse>(result[0]);
      }

      [Test]
      public void GetResponsesOneResponseTest()
      {
         IList<string> lines = new List<string> {"ok "};
         int[] readBytes = GetMockReadBytes(lines, -1);
         for (int i = 0; i < readBytes.Length; i++)
            Expect.Call(_client.ReadByte()).Return(readBytes[i]).Repeat.Once();
         _mocks.ReplayAll();

         var result = _connection.GetResponses();
         _mocks.VerifyAll();
         Assert.AreEqual(1, result.Count);
         Assert.IsInstanceOf<OkResponse>(result[0]);
      }

      [Test]
      public void GetResponsesTest()
      {
         IList<string> lines = new List<string>
                                  {
                                     "I LOVE YOU ",
                                     "ok ",
                                     "Module-expansion abougie",
                                     "Clear-sticky abougie/",
                                     "/usr/local/cvsroot/sandbox/abougie/",
                                     "Clear-static-directory abougie/",
                                     "/usr/local/cvsroot/sandbox/abougie/",
                                     "Mod-time 27 Nov 2009 14:21:06 -0000",
                                     "MT +updated",
                                     "MT text U ",
                                     "MT fname abougie/.cvspass",
                                     "MT newline",
                                     "MT -updated",
                                     "Updated abougie/",
                                     "/usr/local/cvsroot/sandbox/abougie/.cvspass",
                                     "/.cvspass/1.1.1.1///",
                                     "u=rw,g=rw,o=rw",
                                     "74"
                                  };
         string fileContents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w";
         int[] readBytes = GetMockReadBytes(lines, -1);
         for (int i = 0; i < readBytes.Length; i++)
            Expect.Call(_client.ReadByte()).Return(readBytes[i]).Repeat.Once();
         Expect.Call(_client.ReadBytes(74))
            .Return(fileContents.Encode());
         _mocks.ReplayAll();
         var result = _connection.GetResponses();
         _mocks.VerifyAll();
         Assert.AreEqual(12, result.Count);
      }

      [Test]
      public void ReadLineTest()
      {
         int[] readBytes = GetMockReadBytes(new List<string> {"abc"}, -1);
         using (_mocks.Record())
         {
            for (int i = 0; i < readBytes.Length - 1; i++)
            {
               Expect.Call(_client.ReadByte()).Return(readBytes[i]).Repeat.Once();
            }
         }
         using (_mocks.Playback())
         {
            string result = _connection.ReadLine();
            Assert.AreEqual("abc", result);
         }
      }

      [Test]
      public void TestReadBytes()
      {
         IList<string> lines = new List<string>
                                  {
                                     "I LOVE YOU ",
                                     "ok "
                                  };
         int[] result = GetMockReadBytes(lines, 0);
         for (int i = 0; i < result.Length; i++)
         {
            Console.Write("{0} {1}", result[i], " ");
         }
      }
   }
}