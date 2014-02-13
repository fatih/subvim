using System.IO;
using OmniSharp.Common;

namespace OmniSharp.Tests
{
    public class FakeFileSystem : IFileSystem
    {
        private bool _exists = true;

        public void FileExists(bool exists)
        {
            _exists = exists;
        }

        public FileInfo GetFileInfo(string filename)
        {
            if(_exists)
                return new FileInfo("Nancy.dll");

            return new FileInfo("IDontExist.dll");
        }
    }
}