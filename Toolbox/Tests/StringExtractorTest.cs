using Toolbox.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    
    
    /// <summary>
    ///This is a test class for StringExtractorTest and is intended
    ///to contain all StringExtractorTest Unit Tests
    ///</summary>
   [TestClass()]
   public class StringExtractorTest
   {


      private TestContext testContextInstance;

      /// <summary>
      ///Gets or sets the test context which provides
      ///information about and functionality for the current test run.
      ///</summary>
      public TestContext TestContext
      {
         get
         {
            return testContextInstance;
         }
         set
         {
            testContextInstance = value;
         }
      }

      #region Additional test attributes
      // 
      //You can use the following additional attributes as you write your tests:
      //
      //Use ClassInitialize to run code before running the first test in the class
      //[ClassInitialize()]
      //public static void MyClassInitialize(TestContext testContext)
      //{
      //}
      //
      //Use ClassCleanup to run code after all tests in a class have run
      //[ClassCleanup()]
      //public static void MyClassCleanup()
      //{
      //}
      //
      //Use TestInitialize to run code before running each test
      //[TestInitialize()]
      //public void MyTestInitialize()
      //{
      //}
      //
      //Use TestCleanup to run code after each test has run
      //[TestCleanup()]
      //public void MyTestCleanup()
      //{
      //}
      //
      #endregion


      /// <summary>
      ///A test for ExtractTimeIntervalFromString
      ///</summary>
      [TestMethod()]
      public void ExtractTimeIntervalFromStringTest()
      {
         string input = "file_name_234_2012-10.01-2012x11-01.hdf5"; // TODO: Initialize to an appropriate value
         string regex_pattern = null; // TODO: Initialize to an appropriate value
         string date_pattern = null; // TODO: Initialize to an appropriate value
         DatesInterval expected = new DatesInterval(new DateTime(2012, 10, 1, 0, 0, 0), new DateTime(2012, 11, 1, 0, 0, 0)); // TODO: Initialize to an appropriate value
         DatesInterval actual;
         actual = StringExtractor.ExtractTimeIntervalFromString(input, regex_pattern, date_pattern);
         Assert.AreEqual(expected.Start, actual.Start);
         Assert.AreEqual(expected.End, actual.End);
         Assert.Inconclusive("Verify the correctness of this test method.");
      }
   }
}
