using System;
using System.Collections.Generic;

namespace Toolbox.Core.Arguments
{
   /// <summary>
   /// Class OptionsParser.
   /// Use Tag to define the option tag (default is '-', like -option1).
   /// Use ParameterSeparator to define the parameter separator (default is ':', like -option1: parameter1).
   /// </summary>
   public class OptionsParser
   {
      /// <summary>
      /// List of options (INTERNAL).
      /// </summary>
      protected Dictionary<string, string> f_options;

      /// <summary>
      /// List of arguments (INTERNAL).
      /// </summary>
      protected List<string> f_arguments;

      /// <summary>
      /// Gets or sets the TAG used to identify an option. The '-' is the default value.
      /// </summary>
      /// <value>A string representing the options tag.</value>
      public char Tag { get; set; }

      /// <summary>
      /// Gets or sets the parameter separator. The ':' is the default value.
      /// </summary>
      /// <value>A char representing the parameter separator.</value>
      public char ParameterSeparator { get; set; }

      /// <summary>
      /// List of arguments. 
      /// Arguments are single strings.
      /// </summary>
      public List<string> Arguments { get { return f_arguments; } }

      /// <summary>
      /// List of options.
      /// Options are strings following the option TAG. 
      /// An option can have a single parameter or multiple parameters if enclosed in "".
      /// </summary>
      public Dictionary<string, string> Options { get { return f_options; } }

      /// <summary>
      /// Determines whether the specified option exists.
      /// </summary>
      /// <returns><c>true</c> if the options exists; otherwise, <c>false</c>.</returns>
      /// <param name="option">Option.</param>
      public bool HasOption(string option) { return f_options.ContainsKey(option); }

      /// <summary>
      /// Returns the argument of the specified option.
      /// </summary>
      /// <returns>The argument for the specified option or null if the option do not exist.</returns>
      /// <param name="option">A string representing a stored option.</param>
      public string OptionArgument(string option)
      {
         if (f_options.ContainsKey(option))
            return f_options[option];
         return null;
      }

      /// <summary>
      /// Common initialization for all the constructors (INTERNAL). 
      /// </summary>
      protected void Init()
      {
         Tag = '-';
         f_options = new Dictionary<string, string>();
         f_arguments = new List<string>();
         ParameterSeparator = ':';
      }

      /// <summary>
      /// Default constructor.
      /// </summary>
      public OptionsParser()
      {
         Init();
      }

      /// <summary>
      /// Alternative constructor.
      /// </summary>
      /// <param name="strings_to_parse">A string to parse for options.</param>
      public OptionsParser(string[] strings_to_parse)
      {
         Init();
         Parse(strings_to_parse);
      }

      /// <summary>
      /// Parses a string looking for options.
      /// The results are stored internally and can be accessed by 
      /// </summary>
      /// <param name="strings_to_parse">A string to parse for options and arguments.</param>
      public void Parse(string[] strings_to_parse)
      {
         string t;
         bool next_is_optarg = false;
         string param = "";

         foreach (string item in strings_to_parse)
         {
            if (string.IsNullOrWhiteSpace(item)) continue;
            t = item.Trim();

            if (!next_is_optarg)
            {
               if (t[0] == Tag)
               {
                  if (t[t.Length - 1] == ParameterSeparator)
                  {
                     next_is_optarg = true;
                     param = t.Substring(1, t.Length - 2);
                  }
                  else
                  {
                     f_options[t.Substring(1)] = "";
                  }
               }
               else
               {
                  f_arguments.Add(t);
               }
            }
            else
            {
               f_options[param] = t;
               next_is_optarg = false;
            }
         }
      }
   }
}



