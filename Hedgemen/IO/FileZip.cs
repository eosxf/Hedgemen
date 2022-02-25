using System;
using System.IO;
using System.IO.Compression;

namespace Hgm.IO
{
    public class FileZip : File, IDisposable
    {
        private ZipArchive _archive;
        private bool _disposed = false;

        public FileZip(string zipPath, FsType fsType = FsType.Local) : base(zipPath, fsType)
        {
            IFile file = new File(zipPath);
            if(!file.Exists)
                ZipFile.CreateFromDirectory(file.Directory.FullName, file.Name);
            _archive = new ZipArchive(Open(), ZipArchiveMode.Update, false);
        }

        ~FileZip()
        {
            Dispose();
        }

        public Stream Open(string filePath, FileMode mode = FileMode.Open)
        {
            if(filePath == string.Empty)
                return base.Open(mode);
            
            var entry = _archive.GetEntry(filePath);
            return entry?.Open();
        }

        internal ZipArchiveEntry GetEntryUnwrapped(string filePath)
        {
            return _archive.GetEntry(filePath);
        }

        // todo remove eventually
        public ZipArchiveEntry CreateEntryUnwrapped(string filePath)
        {
            return _archive.CreateEntry(filePath);
        }

        public FileZipEntry GetEntry(string filePath)
        {
            return new FileZipEntry(filePath, this);
        }

        public bool EntryExists(string filePath)
        {
            return _archive.GetEntry(filePath) != null;
        }

        public void Dispose()
        {
            if(_disposed) return;
            _archive.Dispose();
            _disposed = true;
        }
    }
}