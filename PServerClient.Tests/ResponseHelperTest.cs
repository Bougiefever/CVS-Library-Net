using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace PServerClient.Tests
{
   [TestFixture]
   public class ResponseHelperTest
   {
      [Test]
      public void GetModuleNameTest()
      {
         string mod = "mymod/";
         string result = ResponseHelper.FixResponseModuleSlashes(mod);
         Assert.AreEqual("mymod", result);

         mod = "mymod/Properties/";
         result = ResponseHelper.FixResponseModuleSlashes(mod);
         Assert.AreEqual("mymod/Properties", result);

         mod = "mymod/cvstest/CVSROOT";
         result = ResponseHelper.FixResponseModuleSlashes(mod);
         Assert.AreEqual(mod, result);
      }

      [Test]
      public void GetLastModuleTest()
      {
         string mod = "mymod/";
         string result = ResponseHelper.GetLastModuleName(mod);
         Assert.AreEqual("mymod", result);

         mod = "mymod/Properties/";
         result = ResponseHelper.GetLastModuleName(mod);
         Assert.AreEqual("Properties", result);

         mod = "mymod/cvstest/CVSROOT/";
         result = ResponseHelper.GetLastModuleName(mod);
         Assert.AreEqual("CVSROOT", result);
      }
   }
}
