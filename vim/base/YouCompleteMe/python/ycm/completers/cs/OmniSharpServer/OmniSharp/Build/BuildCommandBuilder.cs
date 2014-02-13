using System.IO;
using OmniSharp.Solution;

namespace OmniSharp.Build
{
    public class BuildCommandBuilder
    {
        private readonly ISolution _solution;
        
        public BuildCommandBuilder(ISolution solution)
        {
            _solution = solution;
        }

        public string Executable
        {
            get
            {
                return PlatformService.IsUnix
                           ? "xbuild"
                           : Path.Combine(System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory(), "Msbuild.exe");
            }
        }

        public string Arguments
        {
            get { return (PlatformService.IsUnix ? "" : "/m ") + "/nologo /v:q /property:GenerateFullPaths=true \"" + _solution.FileName + "\""; }
        }
    }
}
