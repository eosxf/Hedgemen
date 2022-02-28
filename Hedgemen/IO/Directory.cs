using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace Hgm.IO;

public class Directory : IDirectory
{
	private readonly DirectoryInfo _info;

	private readonly string _evaluatedPath;

	public Directory(string directoryPath, FsType fsType = FsType.Local)
	{
		_evaluatedPath = EvaluatePath(directoryPath, fsType);
		_info = new DirectoryInfo(_evaluatedPath);
		Type = fsType;
	}

	internal Directory(DirectoryInfo directoryInfo, FsType fsType)
	{
		_evaluatedPath = EvaluatePath(directoryInfo.Name, fsType);
		_info = directoryInfo;
		Type = fsType;
	}

	public FsType Type { get; protected set; }

	public bool Exists => _info.Exists;

	public string Name => _info.Name;

	public IDirectory Parent => _info.Parent == null ? null : new Directory(_info.Parent, Type);

	public IDirectory Root => new Directory(_info.Root, FsType.Absolute);

	public FileAttributes Attributes => _info.Attributes;

	public string Extension => _info.Extension;

	public DateTime CreationTime => _info.CreationTime;

	public string FullName => _info.FullName;

	public DateTime CreationTimeUtc => _info.CreationTimeUtc;

	public DateTime LastAccessTime => _info.LastAccessTime;

	public DateTime LastWriteTime => _info.LastWriteTime;

	public DateTime LastAccessTimeUtc => _info.LastAccessTimeUtc;

	public DateTime LastWriteTimeUtc => _info.LastWriteTimeUtc;

	public void Create()
	{
		_info.Create();
	}

	public IDirectory CreateSubDirectory(string name, bool createIt = true)
	{
		var directory = new Directory(_info.CreateSubdirectory(name), Type);
		if (createIt) directory.Create();
		return directory;
	}

	public void CreateSubDirectories(params string[] names)
	{
		foreach (var directoryName in names)
		{
			var directory = CreateSubDirectory(directoryName);
		}
	}

	public IFile CreateFile(string name, bool createIt = true)
	{
		var file = new File(name, Type);
		if (createIt) file.Create();
		return file;
	}

	public void Delete(bool recursive = true)
	{
		if (Exists)
			_info.Delete(recursive);
	}

	public void DeleteContents()
	{
		if (Exists)
		{
			_info.GetFiles().ToList().ForEach(e => e.Delete());
			_info.GetDirectories().ToList().ForEach(e => e.Delete(true));
		}
	}

	public void Refresh()
	{
		_info.Refresh();
	}

	public IFile FindFile(string fileName)
	{
		return new File(FullName + '/' + fileName, Type);
	}

	public IList<IFile> FindFiles(params string[] fileNames)
	{
		var files = new List<IFile>(fileNames.Length);

		foreach (var fileName in fileNames) files.Add(FindFile(fileName));

		return files;
	}

	public IDirectory FindDirectory(string directoryName)
	{
		return new Directory(FullName + '/' + directoryName + '/', Type);
	}

	public IList<IDirectory> FindDirectories(params string[] directoryNames)
	{
		var directories = new List<IDirectory>(directoryNames.Length);

		foreach (var directoryName in directoryNames) directories.Add(FindDirectory(directoryName));

		return directories;
	}

	public IList<IDirectory> ListDirectories(DirectoryListFilter filter = null)
	{
		filter ??= e => true;

		var directoriesArray = _info.GetDirectories();
		var directoriesList = new List<IDirectory>();

		foreach (var directory in directoriesArray)
		{
			var directoryHandle = new Directory(directory, Type);
			if (filter(directoryHandle)) directoriesList.Add(directoryHandle);
		}

		return directoriesList;
	}

	public IList<IFile> ListFiles()
	{
		var files = _info.GetFiles();
		return files.Select(file => new File(file, Type)).ToList<IFile>();
	}

	public IList<IFile> ListFilesRecursively(FileListFilter filter = null)
	{
		filter ??= _ => true;
		var files = new List<IFile>();

		InternalListFilesRecursively(filter, this, files);

		return files;
	}

	public IList<FileSystemInfo> ListFileSystems()
	{
		var systems = _info.GetFileSystemInfos();
		return systems.ToList();
	}

	public void MoveTo(IDirectory dest)
	{
		_info.MoveTo(dest.FullName);
	}

	public void CopyTo(IDirectory dest)
	{
		dest.Delete();
		FileSystem.CopyDirectory(FullName, dest.FullName);
	}

	private void InternalListFilesRecursively(FileListFilter filter, IDirectory directory, List<IFile> files)
	{
		foreach (var file in directory.ListFiles())
			if (filter(file))
				files.Add(file);

		foreach (var dir in directory.ListDirectories()) InternalListFilesRecursively(filter, dir, files);
	}

	private string EvaluatePath(string filePath, FsType fsType)
	{
		switch (fsType)
		{
			case FsType.Local:
				if (Path.IsPathRooted(filePath))
					throw new Exception("File path: " + filePath + " is Absolute, but given the FsType of Local!");
				return filePath;
			case FsType.Absolute:
				if (Path.IsPathRooted(filePath))
					throw new Exception("File path: " + filePath +
					                    " is Relative or External, but given the FsType of Absolute!");
				return filePath;
		}

		return string.Empty;
	}
}