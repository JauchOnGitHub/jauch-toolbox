using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
      /// <param name="pattern">File name pPattern for the search.</param>
      /// <param name="so">Search option. TopDirectoryOnly to search only in the top folder or AllDirectories to search also in the sub-directories. The default is TopDirectoryOnly.</param>
      /// <returns></returns>
      public static FileInfo[] FindFiles(string path, string pattern, SearchOption so = SearchOption.TopDirectoryOnly)
      {
         DirectoryInfo di = new System.IO.DirectoryInfo(path);
         FileInfo[] fl = di.GetFiles(pattern, so);

         return fl;
      }


   }
}
