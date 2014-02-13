using System.IO;

namespace OmniSharp.Common
{
    public interface IFileSystem
    {
        FileInfo GetFileInfo(string filename);
    }
}