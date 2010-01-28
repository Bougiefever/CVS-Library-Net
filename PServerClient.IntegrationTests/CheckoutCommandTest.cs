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
   /// Tests the CheckoutCommand class
   /// </summary>
   [TestFixture]
   public class CheckoutCommandTest
   {
      private IRoot _root;
      private IConnection _connection;
      private string _lineend = "\n";

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
      /// Tests the checkout command execute.
      /// </summary>
      [Test]
      public void TestCheckoutCommandExecute()
      {
         CheckOutCommand command = new CheckOutCommand(_root, _connection);
         command.Execute();
         TestHelper.SaveCommandConversation(command, @"c:\_junk\checkout.xml");
      }

      /// <summary>
      /// Tests the test CVS TCP client checkout commands.
      /// </summary>
      [Test]
      public void TestTestCvsTcpClientCheckoutCommands()
      {
         AuthRequest auth = new AuthRequest(_root);
         string s = auth.GetRequestString();
         Console.WriteLine(s);
         TcpClient client = new TcpClient();
         client.Connect(_root.Host, _root.Port);

         // write auth string
         byte[] bbb = s.Encode();
         NetworkStream stream = client.GetStream();
         stream.Write(bbb, 0, bbb.Length);

         // read auth response
         byte[] rrr = new byte[1024];
         stream.Read(rrr, 0, 1024);
         string s2 = rrr.Decode();
         Console.Write(s2);
         Console.WriteLine();

         // write valid responses string
         s = "Valid-responses ok error Valid-requests Checked-in New-entry Updated Created Merged Mod-time Removed Set-static-directory Clear-static-directory Set-sticky Clear-sticky Module-expansion M E MT" + _lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // write valid requests 
         s = "valid-requests" + _lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // read valid requests
         rrr = new byte[1024];
         stream.Read(rrr, 0, 1024);
         s = rrr.Decode();
         Console.Write(s);
         Console.WriteLine();

         // write unchanged
         s = "UseUnchanged" + _lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // write root
         s = "Root /usr/local/cvsroot/sandbox" + _lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // write global option
         s = "Global_option -q" + _lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // write argument
         s = "Argument abougie" + _lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // write directory
         s = "Directory ." + _lineend + "/usr/local/cvsroot/sandbox" + _lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // expand mods 
         s = "expand-modules" + _lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // get expand mods response
         rrr = new byte[1024];
         stream.Read(rrr, 0, 1024);
         s = rrr.Decode();
         Console.Write(s);
         Console.WriteLine();

         // arg command
         s = "Argument -N" + _lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         s = "Argument abougie" + _lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         s = "Directory ." + _lineend + "/usr/local/cvsroot/sandbox" + _lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         s = "co" + _lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // read checkout response
         rrr = new byte[1024];
         stream.Read(rrr, 0, 1024);
         s = rrr.Decode();
         Console.Write(s);
         Console.WriteLine();
         client.Close();
      }
   }
}
