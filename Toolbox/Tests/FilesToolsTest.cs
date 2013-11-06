using Toolbox.Core.Files;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Collections.Generic;

namespace Tests
{
    
    
    /// <summary>
    ///This is a test class for FilesToolsTest and is intended
    ///to contain all FilesToolsTest Unit Tests
    ///</summary>
   [TestClass()]
   public class FilesToolsTest
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
      ///A test for FindFiles
      ///</summary>
      [TestMethod()]
      public void FindFilesFindFilesInInterval()
      {
         string path = @"\\datacenter\Meteo\WRF\MeteoGalicia\HDF"; // TODO: Initialize to an appropriate value
         string pattern ="WRF-MG-d01*.hdf5"; // TODO: Initialize to an appropriate value
         string file_name_pattern = string.Empty; // TODO: Initialize to an appropriate value
         string date_pattern = string.Empty; // TODO: Initialize to an appropriate value
         DateTime start = new DateTime(2013,10,10,2,0,0); // TODO: Initialize to an appropriate value
         DateTime end = new DateTime(2013,10,13,7,0,0); // TODO: Initialize to an appropriate value
         SearchOption so = SearchOption.TopDirectoryOnly;
         
         List<FileInfo> actual;
         actual = FilesTools.FindFiles(path, pattern, file_name_pattern, date_pattern, start, end, so);

         foreach (FileInfo fi in actual)
            Console.WriteLine("{0}", fi.Name);

         Console.WriteLine("\nPress a key...");
         //Console.ReadKey();
      }
   }
}
