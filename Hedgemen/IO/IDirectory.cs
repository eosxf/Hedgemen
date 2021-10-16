using System;
using System.Collections.Generic;
using System.IO;

namespace Hgm.Engine.IO
{
    public delegate bool FileListFilter(IFile fileHandle);

	public delegate bool DirectoryListFilter(IDirectory directory);

    public interface IDirectory
    {
        public FsType Type { get; }

        public bool Exists { get; }
		public string Name { get; }
		public IDirectory Parent { get; }
		public IDirectory Root { get; }
		public FileAttributes Attributes { get; }
		public string Extension { get; }
		public DateTime CreationTime { get; }
		public string FullName { get; }
		public DateTime CreationTimeUtc { get; }
		public DateTime LastAccessTime { get; }
		public DateTime LastWriteTime { get; }
		public DateTime LastAccessTimeUtc { get; }
		public DateTime LastWriteTimeUtc { get; }

        public void Create();
		public IDirectory CreateSubDirectory(string name, bool createIt = true);
		public void CreateSubDirectories(params string[] names);
		public IFile CreateFile(string name, bool createIt = true);
		public void Delete(bool recursive = true);
		public void DeleteContents();
		public void Refresh();
		public IFile FindFile(string fileName);
		public IList<IFile> FindFiles(params string[] fileNames);
		public IDirectory FindDirectory(string directoryName);
		public IList<IDirectory> FindDirectories(params string[] directoryNames);
		public IList<IDirectory> ListDirectories(DirectoryListFilter filter = null);
		public IList<IFile> ListFiles();
		public IList<IFile> ListFilesRecursively(FileListFilter filter = null);
		public IList<FileSystemInfo> ListFileSystems();
		public void MoveTo(IDirectory dest);
		public void CopyTo(IDirectory dest);
    }
}