using System;
using System.IO;
using System.IO.Compression;

namespace Hgm.Engine.IO
{
    public class FileZip : File, IDisposable
    {
        private ZipArchive archive;
        private bool disposed = false;

        public FileZip(string zipPath, FsType fsType = FsType.Local) : base(zipPath, fsType)
        {
            archive = new ZipArchive(Open(), ZipArchiveMode.Update, false);
        }

        ~FileZip()
        {
            Dispose();
        }

        public Stream Open(string filePath, FileMode mode = FileMode.Open)
        {
            if(filePath == string.Empty)
                return base.Open(mode);
            
            var entry = archive.GetEntry(filePath);
            return entry?.Open();
        }

        internal ZipArchiveEntry GetEntryUnwrapped(string filePath)
        {
            return archive.GetEntry(filePath);
        }

        public FileZipEntry GetEntry(string filePath)
        {
            return new FileZipEntry(filePath, this);
        }

        public bool EntryExists(string filePath)
        {
            return archive.GetEntry(filePath) != null;
        }

        public void Dispose()
        {
            if(disposed) return;
            archive.Dispose();
            disposed = true;
        }
    }
}