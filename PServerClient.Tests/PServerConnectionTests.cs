using System;
using System.Collections.Generic;
using NUnit.Framework;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Responses;
using PServerClient.Tests.TestSetup;
using Rhino.Mocks;
using Is = Rhino.Mocks.Constraints.Is;

namespace PServerClient.Tests
{
   /// <summary>
   /// Tests for the PServerConnection class
   /// </summary>
   [TestFixture]
   public class PServerConnectionTests
   {
      private MockRepository _mocks;

      private ICVSTcpClient _client;

      private PServerConnection _connection;

      /// <summary>
      /// Sets up the TcpClient mock with the connection instance
      /// </summary>
      [SetUp]
      public void SetUp()
      {
         _mocks = new MockRepository();
         _client = _mocks.StrictMock<ICVSTcpClient>();
         _connection = new PServerConnection();
         _connection.TcpClient = _client;
      }

      /// <summary>
      /// Tests the connection Close
      /// </summary>
      [Test]
      public void CloseTest()
      {
         Expect.Call(_client.Close);
         _mocks.ReplayAll();
         _connection.Close();
         _mocks.VerifyAll();
      }

      /// <summary>
      /// Test for Connect
      /// </summary>
      [Test]
      public void ConnectTest()
      {
         Expect.Call(() => _client.Connect(null, 0))
            .IgnoreArguments()
            .Constraints(Is.Equal("host-name"), Is.Equal(2401));
         _mocks.ReplayAll();

         IRoot root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.Password);
         _connection.Connect(root);
         _mocks.VerifyAll();
      }

      /// <summary>
      /// Tests getting a response with multiple text lines making up the response
      /// </summary>
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
            ////for (int i = 0; i < readBytes.Length - 1; i++)
            ////{
            ////   Expect.Call(_client.ReadByte()).Return(readBytes[i]).Repeat.Once();
            ////}
            Expect.Call(_client.ReadLine()).Return(lines[0]);
            Expect.Call(_client.ReadLine()).Return(lines[1]);
            Expect.Call(_client.ReadLine()).Return(lines[2]);
            Expect.Call(_client.ReadLine()).Return(lines[3]);
         }

         using (_mocks.Playback())
         {
            var result = _connection.GetResponseLines("Updated abougie/", ResponseType.Updated, 5);

            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("abougie/", result[0]);
         }
      }

      /// <summary>
      /// Tests getting the response when it has only one line
      /// </summary>
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

      /////// <summary>
      /////// Tests a response that contains a file transmission
      /////// </summary>
      ////[Test]
      ////public void GetResponsesFileResponseTest()
      ////{
      ////   IList<string> lines = new List<string>
      ////                            {
      ////                               "Updated abougie/",
      ////                               "/usr/local/cvsroot/sandbox/abougie/.cvspass",
      ////                               "/.cvspass/1.1.1.1///",
      ////                               "u=rw,g=rw,o=rw",
      ////                               "74"
      ////                            };
      ////   string fileContents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w";

      ////   Expect.Call(_client.ReadLine()).Return(lines[0]);
      ////   Expect.Call(_client.ReadLine()).Return(lines[1]);
      ////   Expect.Call(_client.ReadLine()).Return(lines[2]);
      ////   Expect.Call(_client.ReadLine()).Return(lines[3]);
      ////   Expect.Call(_client.ReadLine()).Return(lines[4]);

      ////   Expect.Call(_client.ReadBytes(74))
      ////      .Return(fileContents.Encode());

      ////   _mocks.ReplayAll();
      ////   var result = _connection.GetAllResponses();
      ////   _mocks.VerifyAll();

      ////   ////int[] readBytes = GetMockReadBytes(lines, -1);
      ////   ////for (int i = 0; i < readBytes.Length; i++)
      ////   ////   Expect.Call(_client.ReadByte()).Return(readBytes[i]).Repeat.Once();

      ////   Assert.AreEqual(1, result.Count);
      ////   IFileResponse response = (IFileResponse)result[0];
      ////   string testFile = response.Contents.Decode();
      ////   Assert.AreEqual(fileContents, testFile);
      ////}

      /////// <summary>
      /////// Tests GetResponses when there is only one response
      /////// </summary>
      ////[Test]
      ////public void GetResponsesOneResponseTest()
      ////{
      ////   IList<string> lines = new List<string> { "ok " };
      ////   ////int[] readBytes = GetMockReadBytes(lines, -1);
      ////   ////for (int i = 0; i < readBytes.Length; i++)
      ////   ////   Expect.Call(_client.ReadByte()).Return(readBytes[i]).Repeat.Once();
      ////   Expect.Call(_client.ReadLine()).Return(lines[0]);
      ////   _mocks.ReplayAll();

      ////   var result = _connection.GetAllResponses();
      ////   _mocks.VerifyAll();
      ////   Assert.AreEqual(1, result.Count);
      ////   Assert.IsInstanceOf<OkResponse>(result[0]);
      ////}

      /////// <summary>
      /////// Tests getting several responses test
      /////// </summary>
      ////[Test]
      ////public void GetResponsesTest()
      ////{
      ////   IList<string> lines = new List<string>
      ////                            {
      ////                               "I LOVE YOU ",
      ////                               "ok ",
      ////                               "Module-expansion abougie",
      ////                               "Clear-sticky abougie/",
      ////                               "/usr/local/cvsroot/sandbox/abougie/",
      ////                               "Clear-static-directory abougie/",
      ////                               "/usr/local/cvsroot/sandbox/abougie/",
      ////                               "Mod-time 27 Nov 2009 14:21:06 -0000",
      ////                               "MT +updated",
      ////                               "MT text U ",
      ////                               "MT fname abougie/.cvspass",
      ////                               "MT newline",
      ////                               "MT -updated",
      ////                               "Updated abougie/",
      ////                               "/usr/local/cvsroot/sandbox/abougie/.cvspass",
      ////                               "/.cvspass/1.1.1.1///",
      ////                               "u=rw,g=rw,o=rw",
      ////                               "74"
      ////                            };
      ////   string fileContents = "/1 :pserver:abougie@gb-aix-q:2401/usr/local/cvsroot/sandbox AB4%o=wSobI4w";
      ////   ////int[] readBytes = GetMockReadBytes(lines, -1);
      ////   ////for (int i = 0; i < readBytes.Length; i++)
      ////   ////   Expect.Call(_client.ReadByte()).Return(readBytes[i]).Repeat.Once();

      ////   for (int i = 0; i < lines.Count; i++)
      ////   {
      ////      Expect.Call(_client.ReadLine()).Return(lines[i]);
      ////   }

      ////   Expect.Call(_client.ReadBytes(74))
      ////      .Return(fileContents.Encode());
      ////   _mocks.ReplayAll();
      ////   var result = _connection.GetAllResponses();
      ////   _mocks.VerifyAll();
      ////   Assert.AreEqual(12, result.Count);
      ////}

      /// <summary>
      /// A test for testing the connection mockery
      /// </summary>
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

      /// <summary>
      /// Gets the one response test.
      /// </summary>
      [Test]
      public void GetOneResponseTest()
      {
         IList<string> lines = new List<string>
                                  {
                                     "I LOVE YOU ",
                                     "ok "
                                  };
         ////int[] readBytes = GetMockReadBytes(lines, -1);
         ////for (int i = 0; i < readBytes.Length; i++)
         ////   Expect.Call(_client.ReadByte()).Return(readBytes[i]).Repeat.Once();

         Expect.Call(_client.ReadLine()).Return(lines[0]);
         Expect.Call(_client.ReadLine()).Return(lines[1]);
         Expect.Call(_client.ReadLine()).Return(null);

         _mocks.ReplayAll();
         var r1 = _connection.GetResponse();
         var r2 = _connection.GetResponse();
         var r3 = _connection.GetResponse();
         _mocks.VerifyAll();
         Assert.IsNotNull(r1);
         Assert.IsNotNull(r2);
         Assert.IsNull(r3);
      }

      /// <summary>
      /// Gets the mock read bytes.
      /// </summary>
      /// <param name="lines">The lines.</param>
      /// <param name="lastChar">The last char.</param>
      /// <returns>int array of bytes</returns>
      private int[] GetMockReadBytes(IList<string> lines, int lastChar)
      {
         byte[] chars = new byte[0];
         byte[] copy;
         foreach (string line in lines)
         {
            byte[] lineBytes = line.Encode();
            copy = new byte[chars.Length];
            chars.CopyTo(copy, 0);
            chars = new byte[chars.Length + lineBytes.Length + 1];
            copy.CopyTo(chars, 0);
            lineBytes.CopyTo(chars, copy.Length);
            chars[chars.Length - 1] = 10;
         }

         int[] ints = new int[chars.Length + 1];
         chars.CopyTo(ints, 0);
         ints[ints.Length - 1] = lastChar;
         return ints;
      }
   }
}