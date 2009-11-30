using NUnit.Framework;
using PServerClient.Connection;
using PServerClient.Requests;
using PServerClient.Responses;
using Rhino.Mocks;
using Is=Rhino.Mocks.Constraints.Is;
using System.Collections.Generic;

namespace PServerClient.Tests
{
   [TestFixture]
   public class PServerConnectionTests
   {

      [Test]
      public void ConnectTest()
      {
         MockRepository mocks = new MockRepository();
         ICvsTcpClient client = mocks.DynamicMock<ICvsTcpClient>();
         PServerConnection connection = new PServerConnection();
         connection.TcpClient = client;

         Expect.Call(() => client.Connect(null, 0))
            .IgnoreArguments()
            .Constraints(Is.Equal("host"), Is.Equal(1));
         mocks.ReplayAll();
         connection.Connect("host", 1);
         mocks.VerifyAll();
      }

      [Test]
      public void DoRequestWithResponseTest()
      {
         MockRepository mocks = new MockRepository();
         ICvsTcpClient client = mocks.DynamicMock<ICvsTcpClient>();
         PServerConnection connection = new PServerConnection();
         connection.TcpClient = client;
         IRequest request = mocks.DynamicMock<IRequest>();
         IResponse response = mocks.DynamicMock<IResponse>();

         
         byte[] writeBuffer = new byte[3] {97, 98, 99};
         byte[] readBuffer = new byte[3] {100, 101, 102};

         //Expect.Call(request.Response).Return(response);
         Expect.Call(request.ResponseExpected).Return(true);
         //Expect.Call(response.ResponseString).PropertyBehavior();
         Expect.Call(request.GetRequestString()).Return("abc");
         Expect.Call(() => request.SetCvsResponse("def"));
         Expect.Call(() => client.Write(writeBuffer));
         Expect.Call(client.Read()).Return(readBuffer);
         Expect.Call(request.CvsResponse).Return("def");

         mocks.ReplayAll();
         connection.DoRequest(request);
         string result = request.CvsResponse;
         mocks.VerifyAll();
         Assert.AreEqual("def", result);
      }

      [Test]
      public void CloseTest()
      {
         MockRepository mocks = new MockRepository();
         ICvsTcpClient client = mocks.DynamicMock<ICvsTcpClient>();
         PServerConnection connection = new PServerConnection();
         connection.TcpClient = client;

         Expect.Call(client.Close);
         mocks.ReplayAll();
         connection.Close();
         mocks.VerifyAll();
      }

      [Test]
      public void ReadLinesReadBufferOnceTest()
      {
         MockRepository mocks = new MockRepository();
         ICvsTcpClient client = mocks.DynamicMock<ICvsTcpClient>();
         //PServerConnection connection = new PServerConnection();
         //connection.TcpClient = client;

         byte[] readResult = new byte[] { 97, 98, 99, 10, 100, 101, 102, 10, 0, 0, 0 };
         Expect.Call(client.Read()).Return(readResult);
         //Expect.Call(client.DataAvailable).Return(false);

         mocks.ReplayAll();
         IList<string> results = PServerHelper.ReadLines(client);
         mocks.VerifyAll();
         Assert.AreEqual(2, results.Count);
         Assert.AreEqual("abc", results[0]);
         Assert.AreEqual("def", results[1]);
      }

      [Test]
      public void ReadLinesReadBufferTwiceTest()
      {
         MockRepository mocks = new MockRepository();
         ICvsTcpClient client = mocks.DynamicMock<ICvsTcpClient>();
         //PServerConnection connection = new PServerConnection();
         //connection.TcpClient = client;

         byte[] readResult1 = new byte[] { 97, 98, 99, 10, 100, 101, 102, 10 };
         byte[] readResult2 = new byte[] { 103, 104, 105, 10, 106, 107, 108, 10, 0 };
         using (mocks.Ordered())
         {
            Expect.Call(client.Read()).Return(readResult1);
            Expect.Call(client.DataAvailable).Return(true);
            Expect.Call(client.Read()).Return(readResult2);
            Expect.Call(client.DataAvailable).Return(false);
         }

         mocks.ReplayAll();
         IList<string> results = PServerHelper.ReadLines(client);
         mocks.VerifyAll();
         Assert.AreEqual(4, results.Count);
         Assert.AreEqual("abc", results[0]);
         Assert.AreEqual("def", results[1]);
      }

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