using System.IO;

namespace Hgm.Engine.IO
{
	public static class DirectoryInfoExtensions
	{
		public static FileInfo GetFile(this DirectoryInfo info, string name)
		{
			foreach (var file in info.EnumerateFiles())
			{
				if (file.Name.Equals(name))
					return file;
			}

			return null;
		}
	}
}