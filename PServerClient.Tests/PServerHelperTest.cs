using System;
using NUnit.Framework;


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
   }
}
