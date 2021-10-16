using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Hgm.Engine.IO
{
    public class Directory : IDirectory
    {
        private readonly DirectoryInfo info;
		public FsType Type { get; protected set; }

        private string evaluatedPath;

        public Directory(string directoryPath, FsType fsType = FsType.Local)
        {
            this.evaluatedPath = EvaluatePath(directoryPath, fsType);
            this.info = new DirectoryInfo(evaluatedPath);
            this.Type = fsType;
        }

        internal Directory(DirectoryInfo directoryInfo, FsType fsType)
        {
            this.evaluatedPath = EvaluatePath(directoryInfo.Name, fsType);
            this.info = directoryInfo;
            this.Type = fsType;
        }

        public bool Exists => info.Exists;

		public string Name => info.Name;

		public IDirectory Parent => (info.Parent == null) ? null : new Directory(info.Parent, Type);
		
		public IDirectory Root => new Directory(info.Root, FsType.Absolute);

		public FileAttributes Attributes => info.Attributes;

		public string Extension => info.Extension;

		public DateTime CreationTime => info.CreationTime;

		public string FullName => info.FullName;

		public DateTime CreationTimeUtc => info.CreationTimeUtc;

		public DateTime LastAccessTime => info.LastAccessTime;

		public DateTime LastWriteTime => info.LastWriteTime;
		
		public DateTime LastAccessTimeUtc => info.LastAccessTimeUtc;

		public DateTime LastWriteTimeUtc => info.LastWriteTimeUtc;

        public void Create()
		{
			info.Create();
		}

		public IDirectory CreateSubDirectory(string name, bool createIt = true)
		{
			var directory = new Directory(info.CreateSubdirectory(name), Type);
			if(createIt) directory.Create();
			return directory;
		}

		public void CreateSubDirectories(params string[] names)
		{
			foreach (var directoryName in names)
			{
				var directory = CreateSubDirectory(directoryName, true);
			}
		}

		public IFile CreateFile(string name, bool createIt = true)
		{
			var file = new File(name, Type);
			if(createIt) file.Create();
			return file;
		}

		public void Delete(bool recursive = true)
		{
			if(Exists)
				info.Delete(recursive);
		}

		public void DeleteContents()
		{
			if (Exists)
			{
				info.GetFiles().ToList().ForEach(e => e.Delete());
				info.GetDirectories().ToList().ForEach(e => e.Delete(true));
			}
		}

		public void Refresh()
		{
			info.Refresh();
		}

		public IFile FindFile(string fileName)
		{
			return new File(FullName + '/' + fileName, Type);
		}

		public IList<IFile> FindFiles(params string[] fileNames)
		{
			var files = new List<IFile>(fileNames.Length);
			
			foreach (string fileName in fileNames)
			{
				files.Add(FindFile(fileName));
			}

			return files;
		}

		public IDirectory FindDirectory(string directoryName)
		{
			return new Directory(FullName + '/' + directoryName + '/', Type);
		}

		public IList<IDirectory> FindDirectories(params string[] directoryNames)
		{
			var directories = new List<IDirectory>(directoryNames.Length);
			
			foreach (string directoryName in directoryNames)
			{
				directories.Add(FindDirectory(directoryName));
			}

			return directories;
		}

		public IList<IDirectory> ListDirectories(DirectoryListFilter filter = null)
		{
			filter ??= e => true;

			var directoriesArray = info.GetDirectories();
			var directoriesList = new List<IDirectory>();
			
			foreach (var directory in directoriesArray)
			{
				var directoryHandle = new Directory(directory, Type);
				if(filter(directoryHandle)) directoriesList.Add(directoryHandle);
			}

			return directoriesList;
		}

		public IList<IFile> ListFiles()
		{
			var files = info.GetFiles();
			return files.Select(file => new File(file, Type)).ToList<IFile>();
		}
		
		public IList<IFile> ListFilesRecursively(FileListFilter filter = null)
		{
			filter ??= file => true;
			List<IFile> files = new List<IFile>();
			
			InternalListFilesRecursively(filter, this, files);
			
			return files;
		}

		private void InternalListFilesRecursively(FileListFilter filter, IDirectory directory, List<IFile> files)
		{
			foreach(var file in directory.ListFiles())
			{
				if(filter(file)) files.Add(file);
			}

			foreach (var dir in directory.ListDirectories())
			{
				InternalListFilesRecursively(filter, dir, files);
			}
		}

		public IList<FileSystemInfo> ListFileSystems()
		{
			var systems = info.GetFileSystemInfos();
			return systems.ToList();
		}

		public void MoveTo(IDirectory dest)
		{
			info.MoveTo(dest.FullName);
		}

		public void CopyTo(IDirectory dest)
		{
			dest.Delete(true);
			Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(FullName, dest.FullName);
		}

        private string EvaluatePath(string filePath, FsType fsType)
        {
            switch(fsType)
            {
                case FsType.Local:
                    if(Path.IsPathRooted(filePath)) throw new Exception("File path: " + filePath + " is Absolute, but given the FsType of Local!");
                    return filePath;
                case FsType.Absolute:
                    if(Path.IsPathRooted(filePath)) throw new Exception("File path: " + filePath + " is Relative or External, but given the FsType of Absolute!");
                    return filePath;
            }

            return string.Empty;
        }
    }
}