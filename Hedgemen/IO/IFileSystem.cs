namespace Hgm.Engine.IO
{
	public interface IFileSystem
	{
		bool IsLocalStorageAvailable();
		bool IsExternalStorageAvailable();
		string GetLocalPath();
		string GetExternalPath();
		string GetAbsolutePath(string path);
		string ConcatPath(params string[] paths);
	}
}