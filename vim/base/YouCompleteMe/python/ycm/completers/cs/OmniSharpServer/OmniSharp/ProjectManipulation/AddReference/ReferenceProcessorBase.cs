using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace OmniSharp.ProjectManipulation.AddReference
{
    public abstract class ReferenceProcessorBase
    {
        protected readonly XNamespace MsBuildNameSpace = "http://schemas.microsoft.com/developer/msbuild/2003";
        
        protected List<XElement> GetReferenceNodes(XDocument project, string nodeName)
        {
            return project.Element(MsBuildNameSpace + "Project")
                          .Elements(MsBuildNameSpace + "ItemGroup")
                          .Elements(MsBuildNameSpace + nodeName).ToList();
        }
    }
}