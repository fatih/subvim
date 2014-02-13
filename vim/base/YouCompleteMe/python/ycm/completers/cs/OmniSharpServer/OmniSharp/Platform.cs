using System;

namespace OmniSharp
{
    public static class PlatformService
    {
        public static bool IsUnix
        {
            get
            {
                var p = (int)Environment.OSVersion.Platform;
                return (p == 4) || (p == 6) || (p == 128);
            }
        }
    }
}
