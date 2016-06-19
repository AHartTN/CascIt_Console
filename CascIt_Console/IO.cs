using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CascIt_Console
{
    public class Log
    {
        public static string FilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["LogFilePath"];
            }
        }

        public static FileInfo FileInfo
        {
            get
            {
                return new FileInfo(FilePath);
            }
        }

        public static FileStream FileWriteStream
        {
            get
            {
                return File.OpenWrite(FilePath);
            }
        }

        public static FileStream FileReadStream
        {
            get
            {
                return File.OpenRead(FilePath);
            }
        }

        /// <summary>
        /// Create the log file
        /// </summary>
        /// <param name="truncate">Whether or not to truncate any contents of the file if it already exists</param>
        /// <returns></returns>
        public static FileStream CreateFile(bool truncate)
        {
            if (FilePath.IsNullOrWhiteSpace())
            {
                return null;
            }

            return File.Open(FilePath, truncate ? FileMode.Create : FileMode.OpenOrCreate);
        }

        public static bool FileExists
        {
            get
            {
                return FileInfo != null && FileInfo.Exists;
            }
        }
    }
}
