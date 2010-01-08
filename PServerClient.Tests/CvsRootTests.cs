using System.IO;
using NUnit.Framework;
using PServerClient.CVS;
using PServerClient.Tests.TestSetup;

namespace PServerClient.Tests
{
   [TestFixture]
   public class CvsRootTests
   {
      private readonly string _hostName = TestConfig.CVSHost; 
      private readonly int _port = TestConfig.CVSPort; 
      private readonly string _user = TestConfig.Username; 
      private readonly string _pwd = TestConfig.Password; 
      private readonly string _repoPath = TestConfig.RepositoryPath; 
      private readonly string _module = TestConfig.ModuleName;

      [Test]
      public void ConstructorTest()
      {
         IRoot root = new Root(_repoPath, _module, _hostName, _port, _user, _pwd);
         string expected = ":pserver:username@host-name:2401/f1/f2/f3";
         Assert.AreEqual(expected, root.CVSConnectionString);
         Assert.AreEqual(_hostName, root.Host);
         Assert.AreEqual(_user, root.Username);
         Assert.AreNotEqual(_pwd, root.Password);
         Assert.AreEqual(_pwd.ScramblePassword(), root.Password);
         Assert.AreEqual(_repoPath, root.Repository);
         Assert.AreEqual(2401, root.Port);
      }

      [Test]
      public void GetModuleFolderWithMultipleNamesInPathTest()
      {
         IRoot root = new Root(_repoPath, _module, _hostName, _port, _user, _pwd);
         DirectoryInfo di = new DirectoryInfo(@"c:\_temp");
         root.WorkingDirectory = di;
         root.Module = "rootmod/mymod";
         Folder modFolder = root.RootFolder;
      }
   }
}