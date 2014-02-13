using System.Collections.Generic;
using System.Xml.Linq;

namespace OmniSharp.ProjectManipulation.RemoveFromProject
{
    public static class XDocumentExtensions
    {
        private static readonly XNamespace _msBuildNameSpace = "http://schemas.microsoft.com/developer/msbuild/2003";

        public static IEnumerable<XElement> ItemGroupNodes(this XDocument document)
        {
            return document.Element(_msBuildNameSpace + "Project")
                           .Elements(_msBuildNameSpace + "ItemGroup");
        }

        public static IEnumerable<XElement> CompilationNodes(this XDocument document)
        {
            return document.Element(_msBuildNameSpace + "Project")
                           .Elements(_msBuildNameSpace + "ItemGroup")
                           .Elements(_msBuildNameSpace + "Compile");
        }
    }
}