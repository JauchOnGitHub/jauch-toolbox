using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Toolbox.Core.Files
{
   /// <summary>
   /// Class TextFile.
   /// </summary>
   public class TextFile
   {
      /// <summary>
      /// Indicates if a file is open to read (INTERNAL).
      /// </summary>
      protected bool f_fileIsOpenToRead;

      /// <summary>
      /// Indicates if a file is open to write (INTERNAL).
      /// </summary>
      protected bool f_fileIsOpenToWrite;

      /// <summary>
      /// Stores the file stream reference for the file (INTERNAL).
      /// </summary>
      protected FileStream f_fileStream;

      /// <summary>
      /// Stores the stream reader reference for the file (INTERNAL).
      /// </summary>
      protected StreamReader f_reader;

      /// <summary>
      /// Stores the stream writer reference for the file (INTERNAL).
      /// </summary>
      protected StreamWriter f_writer;

      /// <summary>
      /// Common initialization for all the constructors (INTERNAL). 
      /// </summary>
      /// <param name="file">FileInfo reference with the file information (name, path, etc).</param>
      protected virtual void Init(FileInfo file = null)
      {
         this.File = file;
         this.f_fileIsOpenToRead = false;
         this.f_fileIsOpenToWrite = false;
         this.f_fileStream = null;
         this.f_reader = null;
         this.f_writer = null;
      }

      /// <summary>
      /// Default constructor.
      /// </summary>
      public TextFile()
      {
         Init(null);
      }

      /// <summary>
      /// Alternative constructor.
      /// </summary>
      /// <param name="fullpath">string representing the full path to the file (including the file name).</param>
      public TextFile(string fullpath)
      {
         Init(new FileInfo(fullpath));
      }

      /// <summary>
      /// Alternative constructor.
      /// </summary>
      /// <param name="file">FileInfo reference with the file information (name, path, etc).</param>
      public TextFile(FileInfo file)
      {
         Init(file);
      }

      /// <summary>
      /// Class desructor.
      /// Closes the file if it is open.
      /// </summary>
      ~TextFile()
      {
         Close();
      }

      /// <summary>
      /// Gets or sets the FileInfo reference with the file information (name, path, etc).
      /// </summary>
      public FileInfo File { get; set; }

      /// <summary>
      /// Open a file to read or write.
      /// </summary>
      /// <param name="mode">File mode value (Open, Append, Create, etc).</param>
      /// <param name="access">File access value (read or Write).</param>
      /// <param name="share">File share value (read, write, none, etc).</param>
      public void Open(FileMode mode, FileAccess access, FileShare share)
      {
         f_fileStream = new FileStream(File.FullName, mode, access, share);
         
         switch (access)
         {
            case FileAccess.Read:
               f_reader = new StreamReader(f_fileStream);
               f_fileIsOpenToRead = true;
               f_fileIsOpenToWrite = false;
               break;
            case FileAccess.Write:
               f_writer = new StreamWriter(f_fileStream);
               f_fileIsOpenToRead = false;
               f_fileIsOpenToWrite = true;
               break;
            default:
               throw new Exception("File can be open for reading or writing, but not both at same time");
         }

      }

      /// <summary>
      /// Open a file to read.
      /// </summary>
      /// <param name="share">File share value (default is None)</param>
      public void OpenToRead(FileShare share = FileShare.None)
      {
         Open(FileMode.Open, FileAccess.Read, share);
      }

      /// <summary>
      /// Open a file to write.
      /// </summary>
      /// <param name="share">File share value (default is None)</param>
      public void OpenToWrite(FileShare share = FileShare.None)
      {
         Open(FileMode.Open, FileAccess.Write, share);
      }

      /// <summary>
      /// Open a file to append.
      /// </summary>
      /// <param name="share">File share value (default is None)</param>
      public void OpenToAppend(FileShare share = FileShare.None)
      {
         Open(FileMode.Append, FileAccess.Write, share);
      }

      /// <summary>
      /// Open a file to write.
      /// The file must not exist.
      /// </summary>
      /// <param name="share">File share value (default is None)</param>
      public void OpenNewToWrite(FileShare share = FileShare.None)
      {
         Open(FileMode.Create, FileAccess.Write, share);
      }

      /// <summary>
      /// Close the file.
      /// </summary>
      public void Close()
      {
         if (f_fileStream != null)
         {
            if (f_writer != null && f_fileStream.CanWrite)
            {
               f_writer.Flush();
            }

            f_fileStream.Close();
            f_fileStream = null;
         }

         f_fileIsOpenToRead = false;
         f_fileIsOpenToWrite = false;
      }

      /// <summary>
      /// Write a string to the file.
      /// </summary>
      /// <param name="text">string to write to the file.</param>
      /// <returns>Returns false if the file is not open to write. True otherwise.</returns>
      public bool Write(string text)
      {
         if (!f_fileIsOpenToWrite) return false;
         f_writer.Write(text);
         return true;
      }

      /// <summary>
      /// Write the text in a "StringBuilder" instance to the file.
      /// </summary>
      /// <param name="text">"StringBuilder" with the text to write to the file.</param>
      /// <returns>Returns false if the file is not open to write. True otherwise.</returns>
      public bool Write(StringBuilder text)
      {
         if (!f_fileIsOpenToWrite) return false;
         f_writer.Write(text.ToString());
         return true;
      }

      /// <summary>
      /// Write a string and appends a new line to the file.
      /// </summary>
      /// <param name="text">String to write to the file.</param>
      /// <returns>Returns false if the file is not open to write. True otherwise.</returns>
      public bool WriteLine(string line)
      {
         if (!f_fileIsOpenToWrite) return false;
         f_writer.WriteLine(line);
         return true;
      }

      /// <summary>
      /// Write a list of strings (each on a new line) and appends a new line to the file.
      /// </summary>
      /// <param name="text">List of string to write to the file.</param>
      /// <returns>Returns false if the file is not open to write. True otherwise.</returns>
      public bool WriteLines(List<string> lines)
      {
         if (!f_fileIsOpenToWrite) return false;
         foreach (string line in lines)
            f_writer.WriteLine(line);
         return true;
      }

      /// <summary>
      /// Reads text from the file.
      /// </summary>
      /// <param name="n_chars">Number of characters to read.</param>
      /// <returns>A string with the text readed from the file.</returns>
      public string Read(int n_chars = 1)
      {
         if (!f_fileIsOpenToRead) return null;

         string text = "";
         int c;
         int i;

         for (i = 1; i <= n_chars; i++)
         {
            c = f_reader.Read();
            if (c == -1) break;
            text = text + c.ToString();
         }

         return text;
      }

      /// <summary>
      /// Reads a line from the file.
      /// </summary>
      /// <returns>A string with the line readed from the file.</returns>
      public string ReadLine()
      {
         if (!f_fileIsOpenToRead) return null;
         return f_reader.ReadLine();
      }

      /// <summary>
      /// Reads the entire file.
      /// </summary>
      /// <returns>A list of strings representing each line in the file.</returns>
      public List<string> ReadLines()
      {
         if (!f_fileIsOpenToRead) return null;

         List<string> lines = new List<String>();
         string line;

         for (; ; )
         {
            line = f_reader.ReadLine();
            if (line == null) break;
            lines.Add(line);
         }

         return lines;
      }

      /// <summary>
      /// Reads the entire file.
      /// </summary>
      /// <returns>A string with the text readed from the file.</returns>
      public string ReadLinesToString()
      {
         if (!f_fileIsOpenToRead) return null;

         StringBuilder lines = new StringBuilder();
         string line;

         for (; ; )
         {
            line = f_reader.ReadLine();
            if (line == null) break;
            lines.AppendLine(line);
         }

         return lines.ToString();
      }

      /// <summary>
      /// Creates a new file replacing the defined keys.
      /// The keys are strings that will be searched in the file and will be replaced by its value every time it is found.
      /// </summary>
      /// <param name="old_file">A string representing the name of the "old" file.</param>
      /// <param name="new_file">A string representing the name of the "new" file.</param>
      /// <param name="replace_list">A Dictionary with keys and values to replace in the new file.</param>
      /// <param name="warning_on_exception">A boolean value indicating if in the case of exception a warning will be shown instead.</param>
      /// <returns>The number of replaces made to the file or -1 in case of exception and warning_on_exception is set to true.</returns>
      public static int Replace(string old_file, string new_file, ref Dictionary<string, string> replace_list, bool warning_on_exception = true)
      {
         try
         {
            TextFile input = null;
            TextFile output = null;
            int NumberOfChanges = 0;

            input = new TextFile(old_file);
            output = new TextFile(new_file);

            input.Open(FileMode.Open, FileAccess.Read, FileShare.Read);
            output.Open(FileMode.Create, FileAccess.Write, FileShare.None);

            string line = "";
            string new_line = "";

            line = input.ReadLine();

            while (line != null)
            {
               foreach (KeyValuePair<string, string> keyPair in replace_list)
               {
                  new_line = line.Replace(keyPair.Key, replace_list[keyPair.Key]);
                  if (new_line != line) NumberOfChanges++;
                  line = new_line;
               }

               output.WriteLine(line);
               line = input.ReadLine();
            }

            input.Close();
            output.Close();

            return NumberOfChanges;
         }
         catch (Exception ex)
         {
            if (warning_on_exception)
            {
               Console.WriteLine();
               while (ex != null)
               {
                  Console.WriteLine("=> {0}", ex.Message);
                  ex = ex.InnerException;
               }
               Console.WriteLine();
            }

            return -1;
         }
      }
   }
}
