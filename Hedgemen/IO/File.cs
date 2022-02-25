using System;
using System.IO;
using System.Text;

namespace Hgm.IO
{
	public class File : IFile
	{
        private readonly FileInfo _info;

        public FsType Type { get; private set; }
        public FileAccess Access { get; set; } = FileAccess.ReadWrite;
        public FileShare Share { get; set; } = FileShare.ReadWrite;
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        private string _evaluatedPath;

        public File(string filePath, FsType fsType = FsType.Local)
        {
            _evaluatedPath = EvaluatePath(filePath, fsType);
            _info = new FileInfo(_evaluatedPath);
            Type = fsType;
        }

        internal File(FileInfo fileInfo, FsType fsType)
        {
            _evaluatedPath = EvaluatePath(fileInfo.FullName, fsType);
            _info = new FileInfo(this._evaluatedPath);
            Type = fsType;
        }

        public bool Exists => _info.Exists;

		public IDirectory Directory => new Directory(_info.Directory, Type);

		public long Length => _info.Length;

		public string Name => _info.Name;

		public string DirectoryName => Directory.Name;

		public bool IsReadOnly => _info.IsReadOnly;

		public FileAttributes Attributes => _info.Attributes;

		public string Extension => _info.Extension;

		public DateTime CreationTime => _info.CreationTime;

		public string FullName => _info.FullName;

		public DateTime CreationTimeUtc => _info.CreationTimeUtc;

		public DateTime LastAccessTime => _info.LastAccessTime;

		public DateTime LastWriteTime => _info.LastWriteTime;
		
		public DateTime LastAccessTimeUtc => _info.LastAccessTimeUtc;

		public DateTime LastWriteTimeUtc => _info.LastWriteTimeUtc;

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
			_info.Create().Close();
		}

		public void Delete()
		{
			_info.Delete();
		}
		
		public virtual Stream Open(FileMode mode = FileMode.Open)
		{
			return _info.Open(mode, Access, Share);
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
			_info.CopyTo(dest.FullName, overwrite);
		}

		public void MoveTo(IFile dest, bool overwrite = true)
		{
			_info.MoveTo(dest.FullName, overwrite);
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
			hash = hash * 37 + _info.GetHashCode();
			hash = hash * 67 + FullName.GetHashCode();
			return hash;
		}

		private void EnsureCreated()
		{
			Create();
		}
    }
}