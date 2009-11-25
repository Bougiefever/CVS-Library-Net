using NUnit.Framework;
using PServerClient.Connection;
using PServerClient.Requests;
using PServerClient.Responses;
using Rhino.Mocks;
using Is=Rhino.Mocks.Constraints.Is;

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

         Expect.Call(request.Response).Return(response);
         Expect.Call(request.ResponseExpected).Return(true);
         Expect.Call(response.ResponseString).PropertyBehavior();
         Expect.Call(request.GetRequestString()).Return("abc");
         Expect.Call(() => client.Write(writeBuffer));
         Expect.Call(client.Read()).Return(readBuffer);

         mocks.ReplayAll();
         connection.DoRequest(request);
         string result = request.Response.ResponseString;
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