using System;
using System.Net.Sockets;
using System.Text;
using NUnit.Framework;
using PServerClient.Commands;
using PServerClient.Requests;
using PServerClient.Responses;

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

         _root = new CvsRoot(_host, _port, _username, _password.UnscramblePassword(), _cvsRootPath);
      }

      [Test]
      public void CheckoutCommandExecuteTest()
      {
         _root.Module = "abougie";
         CheckoutCommand command = new CheckoutCommand(_root);
         command.Execute();

         // print out the whole conversation
         foreach (IRequest req in command.Requests)
         {
            Console.Write("C: {0}", req.GetRequestString());
            if (req.ResponseExpected)
               foreach(IResponse res in req.Responses)
               {
                  Console.Write("S: {0}", res.ResponseText);
                  if (res is IFileResponse)
                     Console.WriteLine(Encoding.ASCII.GetString(((IFileResponse)res).CvsEntry.FileContents));
               }
         }
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
         byte[] bbb = s.Encode();
         NetworkStream stream = client.GetStream();
         stream.Write(bbb, 0, bbb.Length);

         // read auth response
         byte[] rrr = new byte[1024];
         stream.Read(rrr, 0, 1024);
         string s2 = rrr.Decode();
         Console.WriteLine(s2);

         // write valid responses string
         s = "Valid-responses ok error Valid-requests Checked-in New-entry Updated Created Merged Mod-time Removed Set-static-directory Clear-static-directory Set-sticky Clear-sticky Module-expansion M E MT" + lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // write valid requests 
         s = "valid-requests" + lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // read valid requests
         rrr = new byte[1024];
         stream.Read(rrr, 0, 1024);
         s = rrr.Decode();
         Console.WriteLine(s);

         // write unchanged
         s = "UseUnchanged" + lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // write root
         s = "Root /usr/local/cvsroot/sandbox" + lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // write global option
         s = "Global_option -q" + lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // write argument
         s = "Argument abougie" + lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // write directory
         s = "Directory ." + lineend + "/usr/local/cvsroot/sandbox" + lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // expand mods 
         s = "expand-modules" + lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // get expand mods response
         rrr = new byte[1024];
         stream.Read(rrr, 0, 1024);
         s = rrr.Decode();
         Console.WriteLine(s);

         // arg command
         s = "Argument -N" + lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         s = "Argument abougie" + lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         s = "Directory ." + lineend + "/usr/local/cvsroot/sandbox" + lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         s = "co" + lineend;
         bbb = s.Encode();
         stream.Write(bbb, 0, bbb.Length);
         stream.Flush();

         // read checkout response
         rrr = new byte[1024];
         stream.Read(rrr, 0, 1024);
         s = rrr.Decode();
         Console.WriteLine(s);
         client.Close();
      }
   }
}
