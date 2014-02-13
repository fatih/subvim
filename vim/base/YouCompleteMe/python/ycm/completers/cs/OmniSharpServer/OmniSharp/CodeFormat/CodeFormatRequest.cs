using OmniSharp.Common;

namespace OmniSharp.CodeFormat
{
    public class CodeFormatRequest : Request
    {
        private bool _expandTab = false;

        public bool ExpandTab
        {
            get { return _expandTab; }
            set { _expandTab = value; }
        }
    }
}
