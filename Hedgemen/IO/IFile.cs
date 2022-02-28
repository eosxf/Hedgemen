using System;
using System.IO;
using System.Text;

namespace Hgm.IO;

public interface IFile
{
	public FsType Type { get; }
	public FileAccess Access { get; set; }
	public FileShare Share { get; set; }
	public Encoding Encoding { get; set; }

	public bool Exists { get; }
	public IDirectory Directory { get; }
	public long Length { get; }
	public string Name { get; }
	public string DirectoryName { get; }
	public bool IsReadOnly { get; }
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
	public void Delete();
	public Stream Open(FileMode mode = FileMode.Open);
	public void WriteString(string text);
	public void WriteBytes(byte[] buffer);
	public void WriteBytes(byte[] buffer, int index, int count, FileMode fileMode = FileMode.Truncate);
	public string ReadString(FileMode fileMode = FileMode.Open);
	public byte[] ReadBytes(FileMode fileMode = FileMode.Open);
	public void CopyTo(IFile dest, bool overwrite = true);
	public void MoveTo(IFile dest, bool overwrite = true);
	public IFile CopyTo(IDirectory dest, string name, bool overwrite = true);
	public IFile MoveTo(IDirectory dest, string name, bool overwrite = true);
}