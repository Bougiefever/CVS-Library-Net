using System;
using NUnit.Framework;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Tests.TestSetup;

namespace PServerClient.IntegrationTests
{
   /// <summary>
   /// Test of the CVSTcpClient class
   /// </summary>
   [TestFixture]
   public class CVSTcpClientTest
   {
      private IRoot _root;
      private CVSTcpClient _client;

      /// <summary>
      /// Sets up testing.
      /// </summary>
      [SetUp]
      public void SetUpTesting()
      {
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.PasswordScrambled.UnscramblePassword());
         _root.WorkingDirectory = TestConfig.WorkingDirectory;
         _client = new CVSTcpClient();
      }

      /// <summary>
      /// Tests the read bytes.
      /// </summary>
      [Test]
      public void TestReadBytes()
      {
         AuthRequest auth = new AuthRequest(_root);
         string s = auth.GetRequestString();
         Console.WriteLine(s);
         _client.Connect(_root.Host, _root.Port);
         byte[] send = s.Encode();
         _client.Write(send);
         
         // read auth response
         byte[] receive = _client.ReadBytes(11);

         string r = receive.Decode();
         Console.Write(r);
         Console.WriteLine();

         Assert.AreEqual("I LOVE YOU\n", r);
         ValidRequestsRequest request = new ValidRequestsRequest();
         s = request.GetRequestString();
         send = s.Encode();
         _client.Write(send);

         r = _client.ReadLine();
         Console.WriteLine(r);

         Assert.IsTrue(r.StartsWith("Valid-requests Root"));
         _client.Close();
      }
   }
}