using System;
using System.IO;
using NUnit.Framework;
using PServerClient.CVS;
using PServerClient.Tests.TestSetup;

namespace PServerClient.Tests
{
   [TestFixture]
   public class PServerHelperTest
   {
      [Test]
      public void ConvertDateTimeToEntryFormatTest()
      {
         DateTime dt = DateTime.Parse("12/11/2009 19:09:26");
         string date = dt.ToEntryString();
         Assert.AreEqual("Fri Dec 11 19:09:26 2009", date);

         dt = dt.AddDays(-5);
         date = dt.ToEntryString();
         Assert.AreEqual("Sun Dec  6 19:09:26 2009", date);
      }

      [Test]
      public void ConvertDateTimeToRfc822StringTest()
      {
         DateTime dt = DateTime.Parse("1/1/2011 1:52:22 PM");
         string result = dt.ToRfc822();
         Assert.AreEqual("01 Jan 2011 13:52:22 -0000", result);
      }

      [Test]
      public void ConvertEntryFileDateStringToDateTimeTest()
      {
         string test = "Mon Dec  7 23:15:36 2009";
         DateTime dt = test.EntryToDateTime();
         Assert.AreEqual(DateTime.Parse("12/7/2009 11:15:36 PM"), dt);
      }

      [Test]
      public void ConvertRfc822ToDateTimeTest()
      {
         string dt = "01 Jan 2011 13:52:22 -0000";
         DateTime expected = DateTime.Parse("1/1/2011 1:52:22 PM");
         DateTime result = dt.Rfc822ToDateTime();
         Assert.AreEqual(result, expected);
      }

      [Test]
      public void DecodeTest()
      {
         byte[] decode = new byte[] { 97, 98, 99, 10 };
         string expected = "abc\n";
         string result = decode.Decode();
         Assert.AreEqual(expected, result);
      }

      [Test]
      public void DecodeWithTrailingNullsTest()
      {
         byte[] decode = new byte[] { 97, 98, 99, 10, 0, 0, 0 };
         string expected = "abc\n";
         string result = decode.Decode();
         Assert.AreEqual(expected, result);
      }

      [Test]
      public void EncodeTest()
      {
         string encode = "abc";
         byte[] expected = new byte[] { 97, 98, 99 };
         byte[] result = encode.Encode();
         Assert.AreEqual(expected, result);
      }

      [Test]
      public void Scramble()
      {
         string password = "password";
         Console.WriteLine(password.ScramblePassword());
      }

      [Test]
      public void StringToEnumTest()
      {
         string test = "Dec";
         int result = PServerHelper.StringToEnum(typeof(MonthName), test);
         MonthName mon = MonthName.Dec;
         Assert.AreEqual(mon, (MonthName) result);
      }

      [Test]
      public void TestPasswordScramble()
      {
         string password = @"!""%&'()*+z";
         string scrambled = password.ScramblePassword();
         string expected = "Ax5mHlF@LC>";
         Assert.AreEqual(expected, scrambled, "Password was not scrambled correctly");
      }

      [Test]
      public void UnscramblePasswordTest()
      {
         string scrambled = "Ax5mHlF@LC>";
         string expected = @"!""%&'()*+z";
         string unscrambled = scrambled.UnscramblePassword();
         Assert.AreEqual(expected, unscrambled, "Password was not unscrambled correctly");
      }

      [Test]
      public void GetRootModuleFolderPathTest()
      {
         DirectoryInfo working = TestConfig.WorkingDirectory;
         string module = "mymod";
         DirectoryInfo di = PServerHelper.GetRootModuleFolderPath(working, module);
         Assert.AreEqual(@"c:\_temp\mymod", di.FullName);

         module = "mymod/project";
         di = PServerHelper.GetRootModuleFolderPath(working, module);
         Assert.AreEqual(@"c:\_temp\mymod\project", di.FullName);
      }

      [Test]
      public void GetModuleNameTest()
      {
         string mod1 = "abougie/cvstest";
         string mod2 = "abougie/cvstest/CVSROOT";
         string name = mod2.Substring(mod2.IndexOf(mod1) + mod1.Length + 1);
         Assert.AreEqual("CVSROOT", name);
      }
   }
}