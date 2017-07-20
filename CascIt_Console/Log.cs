namespace CascIt_Console
{
	#region Imports

	using System.Configuration;
	using System.IO;

	#endregion
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

		public static bool FileExists
		{
			get
			{
				return FileInfo != null && FileInfo.Exists;
			}
		}

		/// <summary>
		///     Create the log file
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
	}
}