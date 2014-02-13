using ICSharpCode.NRefactory.TypeSystem;
using OmniSharp.Solution;

namespace OmniSharp.Common
{
    public class QuickFix
    {
        public string FileName { get; set; }
        public int Line { get; set; }
        public int Column { get; set; }
        public string Text { get; set; }

        /// <summary>
        ///   Initialize a QuickFix pointing to the first line of the
        ///   given region in the given file.
        /// </summary>
        public static QuickFix ForFirstLineInRegion
            (DomRegion region, CSharpFile file) {

            return new QuickFix
                { FileName = region.FileName
                , Line     = region.BeginLine
                , Column   = region.BeginColumn

                // Note that we could display an arbitrary amount of
                // context to the user: ranging from one line to tens,
                // hundreds..
                , Text = file.Document.GetText
                    ( offset: file.Document.GetOffset(region.Begin)
                    , length: file.Document.GetLineByNumber
                                (region.BeginLine).Length)};
        }

    }
}
