using System.IO;
using NUnit.Framework;
using PServerClient.CVS;
using PServerClient.Tests.TestSetup;

namespace PServerClient.Tests
{
   [TestFixture]
   public class CvsRootTests
   {
      //private readonly string _hostName = TestConfig.CVSHost; //ConfigurationManager.AppSettings["CVS Host"];// "host-name";
      //private readonly int _port = TestConfig.CVSPort; //Convert.ToInt32(ConfigurationManager.AppSettings["CVS Port"]); // 1;
      //private readonly string _user = TestConfig.Username; //ConfigurationManager.AppSettings["CVS Username"]; //"username";
      //private readonly string _pwd = TestConfig.Password; //ConfigurationManager.AppSettings["Password scrambled"]; //"password";
      //private readonly string _repoPath = TestConfig.RepositoryPath; //ConfigurationManager.AppSettings["Repository Path"]; //"/f1/f2/f3";

      //[Test]
      //public void ConstructorTest()
      //{
      //   Root root = new Root(_hostName, _port, _user, _pwd, _repoPath);
      //   string expected = ":pserver:username@host-name:/f1/f2/f3";
      //   Assert.AreEqual(expected, root.CVSConnectionString);
      //   Assert.AreEqual(_hostName, root.Host);
      //   Assert.AreEqual(_user, root.Username);
      //   Assert.AreNotEqual(_pwd, root.Password);
      //   Assert.AreEqual(_pwd.ScramblePassword(), root.Password);
      //   Assert.AreEqual(_repoPath, root.Repository);
      //   Assert.AreEqual(1, root.Port);
      //}

      //[Test]
      //public void GetModuleFolderTest()
      //{
      //   Root root = new Root(_hostName, _port, _user, _pwd, _repoPath);
      //   DirectoryInfo di = new DirectoryInfo(@"c:\_temp");
      //   root.WorkingDirectory = di;
      //   root.Module = "mymod";
      //   Assert.AreEqual(root.RootFolder.Info.FullName, @"c:\_temp\mymod");
      //}

      //[Test]
      //public void GetModuleFolderWithMultipleNamesInPathTest()
      //{
      //   Root root = new Root(_hostName, _port, _user, _pwd, _repoPath);
      //   DirectoryInfo di = new DirectoryInfo(@"c:\_temp");
      //   root.WorkingDirectory = di;
      //   root.Module = "rootmod/mymod";
      //   Folder modFolder = root.RootFolder;
      //}
   }
}