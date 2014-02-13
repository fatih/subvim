using System.IO;

namespace OmniSharp.Common
{
    public class FileSystem : IFileSystem
    {
        public FileInfo GetFileInfo(string filename)
        {
            return new FileInfo(filename);
        }
    }
}