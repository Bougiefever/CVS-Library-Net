using NUnit.Framework;
using PServerClient.Connection;
using PServerClient.Requests;
using PServerClient.Responses;
using Rhino.Mocks;
using Is = Rhino.Mocks.Constraints.Is;
using System.Collections.Generic;
using System;
using System.Linq;

namespace PServerClient.Tests
{
   [TestFixture]
   public class PServerConnectionTests
   {
      private MockRepository _mocks;
      private ICvsTcpClient _client;
      private PServerConnection _connection;
      //private int[] _mockReadBytes;

      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
         _client = _mocks.StrictMock<ICvsTcpClient>();
         _connection = new PServerConnection();
         _connection.TcpClient = _client;
      }

      [Test]
      public void ConnectTest()
      {
         Expect.Call(() => _client.Connect(null, 0))
            .IgnoreArguments()
            .Constraints(Is.Equal("host"), Is.Equal(1));
         _mocks.ReplayAll();
         _connection.Connect("host", 1);
         _mocks.VerifyAll();
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
      public void ReadLineTest()
      {
         int[] readBytes = GetMockReadBytes(new List<string>() { "abc" }, -1);
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
      public void GetResponseLinesOneLineTest()
      {
         IList<string> lines = new List<string>() 
         {
            "Valid-requests Root Valid-responses valid-requests Repository Directory"
         };

         var result = _connection.GetResponseLines(lines[0], ResponseType.ValidRequests, 1);

         Assert.AreEqual(1, result.Count);
         Assert.AreEqual("Root Valid-responses valid-requests Repository Directory", result[0]);

      }

      [Test]
      public void GetResponseLinesMultipleLinesTest()
      {
         IList<string> lines = new List<string>() 
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
      public void GetResponsesOneResponseTest()
      {
         IList<string> lines = new List<string>() { "ok " };
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
      public void GetResponsesOneLineResponsesTest()
      {
         IList<string> lines = new List<string>() 
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
      public void GetResponsesFileResponseTest()
      {
         IList<string> lines = new List<string>() 
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
         string testFile = response.CvsEntry.FileContents.Decode();
         Assert.AreEqual(fileContents, testFile);
      }

      [Test]
      public void GetResponsesTest()
      {
         IList<string> lines = new List<string>() 
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

      //[Test]
      //public void ReadLinesReadBufferOnceTest()
      //{
      //   MockRepository mocks = new MockRepository();
      //   ICvsTcpClient client = mocks.DynamicMock<ICvsTcpClient>();
      //   //PServerConnection connection = new PServerConnection();
      //   //connection.TcpClient = client;

      //   byte[] readResult = new byte[] { 97, 98, 99, 10, 100, 101, 102, 10, 0, 0, 0 };
      //   Expect.Call(client.Read()).Return(readResult);
      //   //Expect.Call(client.DataAvailable).Return(false);

      //   mocks.ReplayAll();
      //   IList<string> results = PServerHelper.ReadLines(client);
      //   mocks.VerifyAll();
      //   Assert.AreEqual(2, results.Count);
      //   Assert.AreEqual("abc", results[0]);
      //   Assert.AreEqual("def", results[1]);
      //}

      //[Test]
      //public void ReadLinesReadBufferTwiceTest()
      //{
      //   MockRepository mocks = new MockRepository();
      //   ICvsTcpClient client = mocks.DynamicMock<ICvsTcpClient>();
      //   //PServerConnection connection = new PServerConnection();
      //   //connection.TcpClient = client;

      //   byte[] readResult1 = new byte[] { 97, 98, 99, 10, 100, 101, 102, 10 };
      //   byte[] readResult2 = new byte[] { 103, 104, 105, 10, 106, 107, 108, 10, 0 };
      //   using (mocks.Ordered())
      //   {
      //      Expect.Call(client.Read()).Return(readResult1);
      //      //Expect.Call(client.DataAvailable).Return(true);
      //      Expect.Call(client.Read()).Return(readResult2);
      //      //Expect.Call(client.DataAvailable).Return(false);
      //   }

      //   mocks.ReplayAll();
      //   IList<string> results = PServerHelper.ReadLines(client);
      //   mocks.VerifyAll();
      //   Assert.AreEqual(4, results.Count);
      //   Assert.AreEqual("abc", results[0]);
      //   Assert.AreEqual("def", results[1]);
      //}

      //[Test]
      //public void VerifyAuthTest()
      //{
      //   //string password = "AB4%o=wSobI4w";
      //   //PServer connection = new PServer("gb-aix-q", "2401", "abougie", password.UnscramblePassword(), "/usr/local/cvsroot/sandbox");
      //   //Console.WriteLine(connection.CvsRoot.Root);
      //   //string response = connection.VerifyAuthentication();
      //   //Assert.AreNotEqual(string.Empty, response);
      //   //Assert.IsTrue(response.Contains("I LOVE YOU"));
      //   //Console.WriteLine(response);
      //}

      //[Test]
      //public void VerifyAuthBadTest()
      //{
      //   //string password = "A:yZZ30 e";
      //   //PServer connection = new PServer("gb-aix-q", "2401", "abougie", password.UnscramblePassword(), "/usr/local/cvsroot/sandbox");
      //   //Console.WriteLine(connection.CvsRoot.Root);
      //   //string response = connection.VerifyAuthentication();
      //   //Assert.AreNotEqual(string.Empty, response);
      //   //Assert.IsTrue(response.Contains("I HATE YOU"));
      //   //Console.WriteLine(response);
      //}

      //[Test]
      //public void ValidRequestsTest()
      //{
      //   //string password = "AB4%o=wSobI4w";
      //   //PServer connection = new PServer("gb-aix-q", "2401", "abougie", password.UnscramblePassword(), "/usr/local/cvsroot/sandbox");
      //   //Console.WriteLine(connection.CvsRoot.Root);
      //   //string response = connection.ValidRequestsRequest();
      //   //Assert.AreNotEqual(string.Empty, response);
      //   //Console.WriteLine(response);
      //}
   }
}