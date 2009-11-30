using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Connection;
using PServerClient.Requests;
using System.Net.Sockets;

namespace PServerClient.IntegrationTests
{
   [TestFixture]
   public class CheckoutCommandTest
   {
      private CvsRoot _root;
      private string _username;
      private string _password;
      private string _cvsRootPath;
      private string _workingDirectory;
      private string _host;
      private int _port;
      private const string lineend = "\n";

      [SetUp]
      public void SetUp()
      {
         _host = "gb-aix-q";
         _port = 2401;
         _username = "abougie";
         _password = "AB4%o=wSobI4w";
         _cvsRootPath = "/usr/local/cvsroot/sandbox";
         _workingDirectory = "";

         _root = new CvsRoot(_host, _port, _username, _password.UnscramblePassword(), _cvsRootPath, _workingDirectory);
      }

      [Test]
      public void CheckoutCommandExecuteTest()
      {
         _root.Module = "abougie";
         _root.LocalDirectory = "mydir";
         CheckoutCommand command = new CheckoutCommand(_root);
         command.Execute();
      }

      [Test]
      public void TestRawCvsCheckoutCommandsTest()
      {
         AuthRequest auth = new AuthRequest(_root);
         string s = auth.GetRequestString();
         Console.WriteLine(s);
         TcpClient client = new TcpClient();
         client.Connect(_root.Host, _root.Port);

         // write auth string
         byte[] bbb = PServerHelper.EncodeString(s);
         NetworkStream stream = client.GetStream();
         stream.Write(bbb, 0, bbb.Length);

         // read auth response
         byte[] rrr = new byte[1024];
         stream.Read(rrr, 0, 1024);
         string s2 = PServerHelper.DecodeString(rrr);
         Console.WriteLine(s2);

         // write valid responses string
         s = "Valid-responses ok error Valid-requests Checked-in New-entry Updated Created Merged Mod-time Removed Set-static-directory Clear-static-directory Set-sticky Clear-sticky Module-expansion M E MT" + lineend;
         bbb = PServerHelper.EncodeString(s);
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // write valid requests 
         s = "valid-requests" + lineend;
         bbb = PServerHelper.EncodeString(s);
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // read valid requests
         rrr = new byte[1024];
         stream.Read(rrr, 0, 1024);
         s = PServerHelper.DecodeString(rrr);
         Console.WriteLine(s);

         // write unchanged
         s = "UseUnchanged" + lineend;
         bbb = PServerHelper.EncodeString(s);
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // write root
         s = "Root /usr/local/cvsroot/sandbox" + lineend;
         bbb = PServerHelper.EncodeString(s);
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // write global option
         s = "Global_option -q" + lineend;
         bbb = PServerHelper.EncodeString(s);
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // write argument
         s = "Argument abougie" + lineend;
         bbb = PServerHelper.EncodeString(s);
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // write directory
         s = "Directory ." + lineend + "/usr/local/cvsroot/sandbox" + lineend;
         bbb = PServerHelper.EncodeString(s);
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // expand mods 
         s = "expand-modules" + lineend;
         bbb = PServerHelper.EncodeString(s);
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // get expand mods response
         rrr = new byte[1024];
         stream.Read(rrr, 0, 1024);
         s = PServerHelper.DecodeString(rrr);
         Console.WriteLine(s);

         // arg command
         s = "Argument -N" + lineend;
         bbb = PServerHelper.EncodeString(s);
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         s = "Argument abougie" + lineend;
         bbb = PServerHelper.EncodeString(s);
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         s = "Directory ." + lineend + "/usr/local/cvsroot/sandbox" + lineend;
         bbb = PServerHelper.EncodeString(s);
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         s = "co" + lineend;
         bbb = PServerHelper.EncodeString(s);
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // read checkout response
         rrr = new byte[1024];
         stream.Read(rrr, 0, 1024);
         s = PServerHelper.DecodeString(rrr);
         Console.WriteLine(s);
         client.Close();
      }
   }
}
