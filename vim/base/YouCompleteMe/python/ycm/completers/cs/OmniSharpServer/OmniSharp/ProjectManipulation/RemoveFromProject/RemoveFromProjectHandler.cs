using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using OmniSharp.Solution;

namespace OmniSharp.ProjectManipulation.RemoveFromProject
{
    public class RemoveFromProjectHandler
    {
        readonly ISolution _solution;

        public RemoveFromProjectHandler(ISolution solution)
        {
            _solution = solution;
        }

        public void RemoveFromProject(RemoveFromProjectRequest request)
        {
            var relativeProject = _solution.ProjectContainingFile(request.FileName);

            if (relativeProject == null || relativeProject is OrphanProject)
            {
                throw new ProjectNotFoundException(string.Format("Unable to find project relative to file {0}", request.FileName));
            }

            var project = relativeProject.AsXml();

            var relativeFileName = request.FileName.Replace(relativeProject.FileName.Substring(0, relativeProject.FileName.LastIndexOf(Path.DirectorySeparatorChar) + 1), "")
                .Replace("/", @"\");

            var fileNode = project.CompilationNodes().FirstOrDefault(n => n.Attribute("Include").Value.Equals(relativeFileName, StringComparison.InvariantCultureIgnoreCase));

            if (fileNode != null)
            {
                RemoveFileFromProject(relativeProject, request.FileName);

                project.CompilationNodes().Where(n => n.Attribute("Include").Value.Equals(relativeFileName, StringComparison.InvariantCultureIgnoreCase)).Remove();

                var nodes = project.CompilationNodes();

                if (!nodes.Any())
                {
                    project.ItemGroupNodes().Where(n => !n.Nodes().Any()).Remove();
                }
                
                relativeProject.Save(project);
            }
        }

        private void RemoveFileFromProject(IProject project, string filename)
        {
            project.Files.Remove(project.Files.First(f => f.FileName == filename));
        }
    }
}
