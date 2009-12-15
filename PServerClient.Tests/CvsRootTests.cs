using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace PServerClient.Tests
{
   [TestFixture]
   public class CvsRootTests
   {

      [Test]
      public void ConstructorTest()
      {
         CvsRoot root = new CvsRoot("host-name", 1, "username", "password", "/f1/f2/f3");
         string expected = ":pserver:username@host-name:/f1/f2/f3";
         Assert.AreEqual(expected, root.CvsConnectionString);
         Assert.AreEqual("host-name", root.Host);
         Assert.AreEqual("username", root.Username);
         Assert.AreNotEqual("password", root.Password);
         Assert.AreEqual("A:yZZ30 e", root.Password);
         Assert.AreEqual("/f1/f2/f3", root.Root);
         Assert.AreEqual(1, root.Port);
      }
   }
}
