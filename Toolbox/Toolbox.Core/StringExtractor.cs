using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Globalization;

namespace Toolbox.Core
{
   public class DatesInterval
   {
      private DateTime f_start;
      private DateTime f_end;

      public DateTime Start 
      { 
         get 
         {
            return f_start;
         }
         set
         {
            f_start = value;
         }
      }
      public DateTime End
      {
         get
         {
            return f_end;
         }
         set
         {
            f_end = value;
         }
      }

      public DatesInterval(DateTime start, DateTime end)
      {
         f_start = start;
         f_end = end;
      }
   }

   /// <summary>
   /// Extracts info from strings (like dates in file/folder names).
   /// </summary>
   public class StringExtractor
   {
      /// <summary>
      /// Extract interval dates (start and end) from a string.
      /// </summary>
      /// <param name="input">String containing the dates.</param>
      /// <param name="regex_pattern">Optional regex pattern for search the dates.</param>
      /// <param name="date_pattern">Date pattern. Used only if regex_pattern is not null, in wich case it is mandatory.</param>
      /// <returns>DateInterval with the values for the start and end interval dates.</returns>
      public static DatesInterval ExtractTimeIntervalFromString(string input, string regex_pattern = null, string date_pattern = null)
      {
         MatchCollection matches_;

         if (regex_pattern != null)
         {
            if (date_pattern == null)
               throw new Exception("Missing date_pattern argument.");

            matches_ = Regex.Matches(input, regex_pattern);
            if (matches_.Count == 1) //start and end are in the format yyyy-MM-dd-HH
            {
               return new DatesInterval(DateTime.ParseExact(matches_[0].Groups["start"].Value, date_pattern, CultureInfo.InvariantCulture),
                                        DateTime.ParseExact(matches_[0].Groups["end"].Value, date_pattern, CultureInfo.InvariantCulture));
            }
         }

         matches_ = Regex.Matches(input, @"(?<start>\d{4}\D?\d{2}\D?\d{2}\D?\d{2}).(?<end>\d{4}\D?\d{2}\D?\d{2}\D?\d{2})");
         if (matches_.Count == 1) //start and end are in the format yyyy-MM-dd-HH
         {
            return new DatesInterval(DateTime.ParseExact(Regex.Replace(matches_[0].Groups["start"].Value, @"\D", ""), "yyyyMMddHH", CultureInfo.InvariantCulture),
                                     DateTime.ParseExact(Regex.Replace(matches_[0].Groups["end"].Value, @"\D", ""), "yyyyMMddHH", CultureInfo.InvariantCulture));
         }

         matches_ = Regex.Matches(input, @"(?<start>\d{4}\D?\d{2}\D?\d{2}).(?<end>\d{4}\D?\d{2}\D?\d{2})");
         if (matches_.Count == 1) //start and end are in the format yyyy-MM-dd
         {
            return new DatesInterval(DateTime.ParseExact(Regex.Replace(matches_[0].Groups["start"].Value, @"\D", ""), "yyyyMMdd", CultureInfo.InvariantCulture),
                                     DateTime.ParseExact(Regex.Replace(matches_[0].Groups["end"].Value, @"\D", ""), "yyyyMMdd", CultureInfo.InvariantCulture));
         }

         return null;
      }

   }
}
