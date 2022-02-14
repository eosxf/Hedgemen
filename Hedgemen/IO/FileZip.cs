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
            IFile file = new File(zipPath);
            if(!file.Exists)
                ZipFile.CreateFromDirectory(file.Directory.FullName, file.Name);
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

        // todo remove eventually
        public ZipArchiveEntry CreateEntryUnwrapped(string filePath)
        {
            return archive.CreateEntry(filePath);
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