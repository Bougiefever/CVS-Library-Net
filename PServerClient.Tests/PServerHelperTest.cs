using System;
using System.IO;
using NUnit.Framework;
using PServerClient.CVS;


namespace PServerClient.Tests
{
   [TestFixture]
   public class PServerHelperTest
   {
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
      public void Scramble()
      {
         string password = "password";
         Console.WriteLine(password.ScramblePassword());
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
      public void ConvertDateTimeToRfc822StringTest()
      {
         DateTime dt = DateTime.Parse("1/1/2011 1:52:22 PM");
         string result = dt.ToRfc822();
         Assert.AreEqual("01 Jan 2011 13:52:22 -0000", result);
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
      public void ConvertEntryFileDateStringToDateTimeTest()
      {
         string test = "Mon Dec  7 23:15:36 2009";
         DateTime dt = test.EntryToDateTime();
         Assert.AreEqual(DateTime.Parse("12/7/2009 11:15:36 PM"), dt);
         
      }

      [Test]
      public void StringToEnumTest()
      {
         string test = "Dec";
         int result = PServerHelper.StringToEnum(typeof (MonthName), test);
         MonthName mon = MonthName.Dec;
         Assert.AreEqual(mon, (MonthName)result);
      }

      [Test]
      public void CreateModuleFolderStructureTest()
      {
         string module = "mymod/";
         
         DirectoryInfo di = new DirectoryInfo(@"c:\_test");
         Folder mod = PServerHelper.CreateModuleFolderStructure(di, "cvs connection string", module);
         Assert.AreEqual("mymod", mod.Name);
         Assert.AreEqual(0, mod.Count);

         module = "rootmod/mymod";
         mod = PServerHelper.CreateModuleFolderStructure(di, "cvs connection string", module);
         Assert.AreEqual("rootmod", mod.Name);
         Assert.AreEqual(1, mod.Count);
         Assert.AreEqual("mymod", mod[0].Name);

         module = "rootfolder/teamfolder/myproject";
         mod = PServerHelper.CreateModuleFolderStructure(di, "cvs connection string", module);
         Assert.AreEqual("rootfolder", mod.Name);
         Assert.AreEqual(1, mod.Count);
         Assert.AreEqual("teamfolder", mod[0].Name);
         Assert.AreEqual(1, mod[0].Count);
         Assert.AreEqual("myproject", mod[0][0].Name);
      }
   }
}
