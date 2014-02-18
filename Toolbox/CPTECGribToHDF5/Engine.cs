using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Toolbox.Core;
using Toolbox.Core.Files;

namespace CPTECGribToHDF5
{
   public class Engine
   {
      public int Run(string tag, string workingPath, string outputPath, string path, string pattern, 
         SearchOption searchOption = SearchOption.TopDirectoryOnly)
      {
         //First, it's necessary to construct the list of files to process.
         //The path where to look for the files is in the "path" argument.
         //The "pattern" can be used to filter the files that must be processed.
         //The "searchOption" is used to define if it will look only in the Top Directory, or
         //if it will look for files also in the sub-directories.
         List<FileInfo> files_to_process = FilesTools.FindFiles(path, pattern, searchOption);

         if (files_to_process == null)
            throw new Exception("Undefined error. The list of files to process returned with null.");

         if (files_to_process.Count <= 0)
            return 0;

         int files_processed = 0;
         FileInfo result = null;

         foreach (FileInfo file in files_to_process)
         {
            result = ConvertGribToGrib2(file, workingPath);
            if (result == null)
               continue;

            result = ConvertGrib2ToNetCDF(result, workingPath);
            if (result == null)
               continue;

            result = ConvertNetCDFToHDF(result, workingPath);
            if (result == null)
               continue;

            result = ProcessHDF(result, workingPath, outputPath, tag);
            if (result == null)
               continue;
         }

         return files_processed;
      }

      protected FileInfo ConvertGribToGrib2(FileInfo file, string workingPath)
      {

      }
   }
}
