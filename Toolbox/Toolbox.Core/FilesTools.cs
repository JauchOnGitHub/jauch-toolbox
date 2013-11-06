using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Toolbox.Core;

namespace Toolbox.Core.Files
{
   /// <summary>
   /// Class FilesTools.
   /// </summary>
   public class FilesTools
   {
      /// <summary>
      /// Static method to find files in a specified path.
      /// </summary>
      /// <param name="path">Path to the folder where to look for files.</param>
      /// <param name="pattern">
      /// The pattern for the search.
      /// Ex.: "*.hdf5"
      /// </param>
      /// <param name="so">
      /// Search option. 
      /// Use TopDirectoryOnly to search only in the top folder or AllDirectories to search also in the sub-directories.
      /// The default is TopDirectoryOnly.
      /// </param>
      /// <returns></returns>
      public static List<FileInfo> FindFiles(string path, string pattern, SearchOption so = SearchOption.TopDirectoryOnly)
      {
         DirectoryInfo di = new System.IO.DirectoryInfo(path);
         FileInfo[] fl = di.GetFiles(pattern, so);

         List<FileInfo> files = new List<FileInfo>();
         foreach (FileInfo file in fl)
            files.Add(file);

         return files;
      }

      /// <summary>
      /// Static method to find files in a specified path.
      /// </summary>
      /// <param name="path">Path to the folder where to look for files.</param>
      /// <param name="pattern">
      /// The pattern for the search.
      /// Ex.: "*.hdf5"
      /// </param>
      /// <param name="regex_pattern">
      /// The regex pattern used to locate the start and end date in the file name.
      /// </param>
      /// <param name="date_pattern">
      /// yy or yyyy for year
      /// dd for day
      /// MM for month
      /// HH for hour
      /// mm for minutes
      /// ss for seconds
      /// ex: yyyy-MM-dd
      /// </param>
      /// <param name="start">Start date.</param>
      /// <param name="end">End date.</param>
      /// <param name="so">
      /// Search option. 
      /// Use TopDirectoryOnly to search only in the top folder or AllDirectories to search also in the sub-directories.
      /// The default is TopDirectoryOnly.
      /// </param>
      /// <returns></returns>
      public static List<FileInfo> FindFiles(string path, string pattern, string regex_pattern, string date_pattern, DateTime start, DateTime end, SearchOption so = SearchOption.TopDirectoryOnly)
      {
         DirectoryInfo di = new System.IO.DirectoryInfo(path);
         FileInfo[] fl = di.GetFiles(pattern, so);
         DatesInterval interval;
         List<FileInfo> files = new List<FileInfo>();
         foreach (FileInfo file in fl)
         {
            interval = StringExtractor.ExtractTimeIntervalFromString(file.Name, regex_pattern, date_pattern);

            if (interval != null)
            {
               if (interval.Start >= start && interval.End <= end)
                     files.Add(file);
            }
         }

         return files;
      }

      /// <summary>
      /// Static method to find files in a specified path.
      /// </summary>
      /// <param name="path">Path to the folder where to look for files.</param>
      /// <param name="pattern">
      /// The pattern for the search.
      /// Ex.: "*.hdf5"
      /// </param>
      /// <param name="start">Start date.</param>
      /// <param name="end">End date.</param>
      /// <param name="so">
      /// Search option. 
      /// Use TopDirectoryOnly to search only in the top folder or AllDirectories to search also in the sub-directories.
      /// The default is TopDirectoryOnly.
      /// </param>
      /// <returns></returns>
      public static List<FileInfo> FindFiles(string path, string pattern, DateTime start, DateTime end, SearchOption so = SearchOption.TopDirectoryOnly)
      {
         return FindFiles(path, pattern, null, null, start, end, so);
      }
   }
}
