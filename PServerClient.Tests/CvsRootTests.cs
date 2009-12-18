using System.IO;
using NUnit.Framework;
using PServerClient.CVS;

namespace PServerClient.Tests
{
   [TestFixture]
   public class CvsRootTests
   {

      [Test]
      public void ConstructorTest()
      {
         string hostName = "host-name";
         int port = 1;
         string user = "username";
         string pwd = "password";
         string repoPath = "/f1/f2/f3";

         Root root = new Root(hostName, port, user, pwd, repoPath);
         string expected = ":pserver:username@host-name:/f1/f2/f3";
         Assert.AreEqual(expected, root.CvsConnectionString);
         Assert.AreEqual(hostName, root.Host);
         Assert.AreEqual(user, root.Username);
         Assert.AreNotEqual(pwd, root.Password);
         Assert.AreEqual(pwd.ScramblePassword(), root.Password);
         Assert.AreEqual(repoPath, root.RepositoryPath);
         Assert.AreEqual(1, root.Port);
      }

      [Test]
      public void GetModuleFolderTest()
      {
         string hostName = "host-name";
         int port = 1;
         string user = "username";
         string pwd = "password";
         string repoPath = "/f1/f2/f3";

         Root root = new Root(hostName, port, user, pwd, repoPath);
         DirectoryInfo di = new DirectoryInfo(@"c:\_temp");
         root.WorkingDirectory = di;
         root.Module = "mymod";
         Assert.AreEqual(root.ModuleFolder.Info.FullName, @"c:\_temp\mymod");


      }
   }
}
