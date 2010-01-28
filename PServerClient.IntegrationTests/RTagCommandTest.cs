using System;
using System.Net.Sockets;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.CVS;
using PServerClient.Requests;
using PServerClient.Tests.TestSetup;

namespace PServerClient.IntegrationTests
{
   /// <summary>
   /// Test of the RTagCommand class
   /// </summary>
   [TestFixture]
   public class RTagCommandTest
   {
      private IRoot _root;
      private IConnection _connection;

      /// <summary>
      /// Sets up test data.
      /// </summary>
      [SetUp]
      public void SetUpTestData()
      {
         _root = new Root(TestConfig.RepositoryPath, TestConfig.ModuleName, TestConfig.CVSHost, TestConfig.CVSPort, TestConfig.Username, TestConfig.PasswordScrambled.UnscramblePassword());
         _root.WorkingDirectory = TestConfig.WorkingDirectory;
         _connection = new PServerConnection();
      }

      /// <summary>
      /// Tests the R tag command create tag.
      /// </summary>
      [Test]
      public void TestRTagCommandCreateTag()
      {
         RTagCommand cmd = new RTagCommand(_root, _connection);
         cmd.Tag = "mytesttag";
         _root.Module = "abougie/cvstest";
         cmd.Execute();
         TestHelper.SaveCommandConversation(cmd, @"c:\_junk\RTagCommand.xml");
      }

      /// <summary>
      /// Tests the R tag command delete tag.
      /// </summary>
      [Test]
      public void TestRTagCommandDeleteTag()
      {
         RTagCommand cmd = new RTagCommand(_root, _connection);
         cmd.Tag = "mytesttag";
         _root.Module = "abougie/cvstest";
         cmd.TagAction = TagAction.Delete;
         cmd.Execute();
         TestHelper.SaveCommandConversation(cmd, @"c:\_junk\RTagCommand.xml");
      }

      /// <summary>
      /// Tests the R tag CVS TCP client command.
      /// </summary>
      [Test]
      public void TestRTagCvsTcpClientCommand()
      {
         AuthRequest auth = new AuthRequest(_root);
         string send = auth.GetRequestString();
         Console.WriteLine(send);
         TcpClient client = new TcpClient();
         client.Connect(_root.Host, _root.Port);

         // write auth string
         byte[] sendBytes = send.Encode();
         NetworkStream stream = client.GetStream();
         stream.Write(sendBytes, 0, sendBytes.Length);

         // read auth response
         byte[] receiveBytes = new byte[1024];
         stream.Read(receiveBytes, 0, 1024);
         string receive = receiveBytes.Decode();
         Console.Write(receive);
         Console.WriteLine();

         // write valid responses string
         send = "Valid-responses ok error Valid-requests Checked-in New-entry Updated Created Merged Mod-time Removed Set-static-directory Clear-static-directory Set-sticky Clear-sticky Module-expansion M E MT\n";
         sendBytes = send.Encode();
         stream.Write(sendBytes, 0, sendBytes.Length);
         stream.Flush();

         // write valid requests 
         send = "valid-requests\n";
         sendBytes = send.Encode();
         stream.Write(sendBytes, 0, sendBytes.Length);
         stream.Flush();

         // read valid requests
         receiveBytes = new byte[1024];
         stream.Read(receiveBytes, 0, 1024);
         receive = receiveBytes.Decode();
         Console.Write(receive);
         Console.WriteLine();

         // write unchanged
         send = "UseUnchanged\n";
         Console.WriteLine(send);
         sendBytes = send.Encode();
         stream.Write(sendBytes, 0, sendBytes.Length);
         stream.Flush();

         // write root
         send = "Root /usr/local/cvsroot/sandbox\n";
         Console.WriteLine(send);

         sendBytes = send.Encode();
         stream.Write(sendBytes, 0, sendBytes.Length);
         stream.Flush();

         // write global option
         send = "Global_option -q\n";
         Console.WriteLine(send);
         sendBytes = send.Encode();
         stream.Write(sendBytes, 0, sendBytes.Length);
         stream.Flush();

         ////// write directory
         ////send = "Directory abougie/cvstest\n/usr/local/cvsroot/sandbox\n";
         ////Console.WriteLine(send);
         ////sendBytes = send.Encode();
         ////stream.Write(sendBytes, 0, sendBytes.Length);
         ////stream.Flush();

         // tag
         send = "Argument v1_3\n";
         Console.WriteLine(send);
         sendBytes = send.Encode();
         stream.Write(sendBytes, 0, sendBytes.Length);
         stream.Flush();
         
         // write argument
         send = "Argument abougie/cvstest\n";
         Console.WriteLine(send);
         sendBytes = send.Encode();
         stream.Write(sendBytes, 0, sendBytes.Length);
         stream.Flush();
         
         send = "rtag\n";
         Console.WriteLine(send);
         sendBytes = send.Encode();
         stream.Write(sendBytes, 0, sendBytes.Length);
         stream.Flush();

         // read checkout response
         receiveBytes = new byte[1024];
         stream.Read(receiveBytes, 0, 1024);
         receive = receiveBytes.Decode();
         Console.Write(receive);
         Console.WriteLine();
         client.Close();
      }
   }
}