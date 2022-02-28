using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace Hgm.IO;

public class FileZipEntry : IFile
{
	private readonly ZipArchiveEntry entry;

	private readonly FileZip zip;

	public FileZipEntry(string internalFilePath, FileZip zip)
	{
		this.zip = zip;
		entry = zip.GetEntryUnwrapped(internalFilePath);
	}

	public FsType Type => FsType.Internal;

	public FileAccess Access { get; set; }
	public FileShare Share { get; set; }
	public Encoding Encoding { get; set; }

	public bool Exists => entry != null;

	public IDirectory Directory => zip.Directory; // todo

	public long Length => Exists ? entry.Length : 0;

	public string Name => Exists ? entry.FullName : string.Empty;

	public string DirectoryName => zip.DirectoryName; // todo

	public bool IsReadOnly => zip.IsReadOnly;

	public FileAttributes Attributes => zip.Attributes;

	public string Extension => GetExtension();

	public DateTime CreationTime => zip.CreationTime;

	public string FullName => Exists ? entry.FullName : string.Empty;

	public DateTime CreationTimeUtc => zip.CreationTimeUtc;

	public DateTime LastAccessTime => zip.LastAccessTime;

	public DateTime LastWriteTime => zip.LastAccessTime;

	public DateTime LastAccessTimeUtc => zip.LastAccessTimeUtc;

	public DateTime LastWriteTimeUtc => zip.LastWriteTimeUtc;

	public void CopyTo(IFile dest, bool overwrite = true)
	{
		throw new NotImplementedException();
	}

	public IFile CopyTo(IDirectory dest, string name, bool overwrite = true)
	{
		throw new NotImplementedException();
	}

	public void Create()
	{
		throw new NotImplementedException();
	}

	public void Delete()
	{
		throw new NotImplementedException();
	}

	public void MoveTo(IFile dest, bool overwrite = true)
	{
		throw new NotImplementedException();
	}

	public IFile MoveTo(IDirectory dest, string name, bool overwrite = true)
	{
		throw new NotImplementedException();
	}

	public Stream Open(FileMode mode = FileMode.Open)
	{
		return entry.Open();
	}

	public byte[] ReadBytes(FileMode fileMode = FileMode.Open)
	{
		throw new NotImplementedException();
	}

	public string ReadString(FileMode fileMode = FileMode.Open)
	{
		if (!Exists) return string.Empty;
		using var reader = new StreamReader(Open(fileMode), Encoding);
		return reader.ReadToEnd();
	}

	public void WriteBytes(byte[] buffer)
	{
		throw new NotImplementedException();
	}

	public void WriteBytes(byte[] buffer, int index, int count, FileMode fileMode = FileMode.Truncate)
	{
		throw new NotImplementedException();
	}

	public void WriteString(string text)
	{
		throw new NotImplementedException();
	}

	private string GetExtension()
	{
		if (!Exists) return string.Empty;
		var entryName = entry.FullName;
		return entryName.Substring(entryName.LastIndexOf('.'));
	}
}