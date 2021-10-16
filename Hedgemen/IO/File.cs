using System;
using System.IO;
using System.Text;

namespace Hgm.Engine.IO
{
	public class File : IFile
	{
        protected readonly FileInfo info;

        public FsType Type { get; private set; }
        public FileAccess Access { get; set; } = FileAccess.ReadWrite;
        public FileShare Share { get; set; } = FileShare.ReadWrite;
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        private string evaluatedPath;

        public File(string filePath, FsType fsType = FsType.Local)
        {
            this.evaluatedPath = EvaluatePath(filePath, fsType);
            this.info = new FileInfo(this.evaluatedPath);
            this.Type = fsType;
        }

        internal File(FileInfo fileInfo, FsType fsType)
        {
            this.evaluatedPath = EvaluatePath(fileInfo.FullName, fsType);
            this.info = new FileInfo(this.evaluatedPath);
            this.Type = fsType;
        }

        public bool Exists => info.Exists;

		public IDirectory Directory => new Directory(info.Directory, Type);

		public long Length => info.Length;

		public string Name => info.Name;

		public string DirectoryName => Directory.Name;

		public bool IsReadOnly => info.IsReadOnly;

		public FileAttributes Attributes => info.Attributes;

		public string Extension => info.Extension;

		public DateTime CreationTime => info.CreationTime;

		public string FullName => info.FullName;

		public DateTime CreationTimeUtc => info.CreationTimeUtc;

		public DateTime LastAccessTime => info.LastAccessTime;

		public DateTime LastWriteTime => info.LastWriteTime;
		
		public DateTime LastAccessTimeUtc => info.LastAccessTimeUtc;

		public DateTime LastWriteTimeUtc => info.LastWriteTimeUtc;

        protected virtual string EvaluatePath(string filePath, FsType fsType)
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

        public void Create()
		{
			if (Exists) return;
			Directory.Create();
			info.Create().Close();
		}

		public void Delete()
		{
			info.Delete();
		}
		
		public virtual Stream Open(FileMode mode = FileMode.Open)
		{
			return info.Open(mode, Access, Share);
		}

		public void WriteString(string text)
		{
			EnsureCreated();
			using StreamWriter writer = new StreamWriter(Open(FileMode.Truncate), Encoding);
			writer.Write(text);
			writer.Flush();
		}
		
		public void WriteBytes(byte[] buffer)
		{
			WriteBytes(buffer, 0, buffer.Length);
		}
		
		public void WriteBytes(byte[] buffer, int index, int count, FileMode fileMode = FileMode.Truncate)
		{
			EnsureCreated();
			using BinaryWriter writer = new BinaryWriter(Open(fileMode), Encoding);
			writer.Write(buffer, index, count);
			writer.Flush();
		}

		public string ReadString(FileMode fileMode = FileMode.Open)
		{
			if (!Exists) return string.Empty;
			using StreamReader reader = new StreamReader(Open(fileMode), Encoding);
			return reader.ReadToEnd();
		}

		public byte[] ReadBytes(FileMode fileMode = FileMode.Open)
		{
			using var stream = Open(fileMode);
			using var ms = new MemoryStream();
			
			stream.CopyTo(ms);
			return ms.ToArray();
		}

		public void CopyTo(IFile dest, bool overwrite = true)
		{
			dest.Directory.Create();
			info.CopyTo(dest.FullName, overwrite);
		}

		public void MoveTo(IFile dest, bool overwrite = true)
		{
			info.MoveTo(dest.FullName, overwrite);
		}
		
		public IFile CopyTo(IDirectory dest, string name, bool overwrite = true)
		{
			var file = new File(dest.FullName + '/' + name, dest.Type);
			CopyTo(file, overwrite);
			return file;
		}

		public IFile MoveTo(IDirectory dest, string name, bool overwrite = true)
		{
			var file = new File(dest.FullName + '/' + name, dest.Type);
			MoveTo(file, overwrite);
			return file;
		}

		public override string ToString()
		{
			return FullName;
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			var objVal = obj as IFile;
			return Type == objVal.Type && FullName.Equals(objVal.FullName);
		}

		public override int GetHashCode()
		{
			int hash = 1;
			hash = hash * 37 + info.GetHashCode();
			hash = hash * 67 + FullName.GetHashCode();
			return hash;
		}

		private void EnsureCreated()
		{
			Create();
		}
    }
}