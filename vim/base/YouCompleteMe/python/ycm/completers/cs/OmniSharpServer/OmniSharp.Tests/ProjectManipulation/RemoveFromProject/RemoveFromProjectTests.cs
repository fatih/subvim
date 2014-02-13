using System.Xml.Linq;
using NUnit.Framework;
using OmniSharp.ProjectManipulation;
using OmniSharp.ProjectManipulation.RemoveFromProject;
using Should;

namespace OmniSharp.Tests.ProjectManipulation.RemoveFromProject
{
    [TestFixture]
    public class RemoveFromProjectTests
    {
        [Test, ExpectedException(typeof(ProjectNotFoundException))]
        public void ShouldThrowProjectNotFoundExceptionWhenProjectNotFound()
        {
            var project = new FakeProject(fileName: @"/test/code/fake.csproj");
            var solution = new FakeSolution(@"/test/fake.sln");
            solution.Projects.Add(project);

            var request = new RemoveFromProjectRequest
            {
                FileName = @"/test/folder/Test.cs"
            };

            var handler = new RemoveFromProjectHandler(solution);
            handler.RemoveFromProject(request);
        }

        [Test]
        public void ShouldRemoveFileFromProjectXml()
        {
            var project = new FakeProject(fileName: @"c:\test\code\fake.csproj");
            project.AddFile("some content", @"c:\test\code\test.cs");

            var xml = @"
                <Project xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
                    <ItemGroup>
                        <Compile Include=""Hello.cs""/>
                        <Compile Include=""Test.cs""/>
                    </ItemGroup>
                </Project>";

            project.XmlRepresentation = XDocument.Parse(xml);
            var expectedXml = XDocument.Parse(@"
                <Project xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
                    <ItemGroup>
                        <Compile Include=""Hello.cs""/>
                    </ItemGroup>
                </Project>");

            var solution = new FakeSolution(@"c:\test\fake.sln");
            solution.Projects.Add(project);

            var request = new RemoveFromProjectRequest
            {
                FileName = @"c:\test\code\test.cs"
            };

            var handler = new RemoveFromProjectHandler(solution);
            handler.RemoveFromProject(request);

            project.AsXml().ToString().ShouldEqual(expectedXml.ToString());
        }

        [Test]
        public void ShouldRemoveItemGroupWhenRemovingLastFile()
        {
            var project = new FakeProject(fileName: @"c:\test\code\fake.csproj");
            project.AddFile("some content", @"c:\test\code\test.cs");

            var xml = @"
                <Project xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
                    <ItemGroup>
                        <Compile Include=""Test.cs""/>
                    </ItemGroup>
                </Project>";

            project.XmlRepresentation = XDocument.Parse(xml);
            var expectedXml = XDocument.Parse(@"
                <Project xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"" />");

            var solution = new FakeSolution(@"c:\test\fake.sln");
            solution.Projects.Add(project);

            var request = new RemoveFromProjectRequest
            {
                FileName = @"c:\test\code\test.cs"
            };

            var handler = new RemoveFromProjectHandler(solution);
            handler.RemoveFromProject(request);

            project.AsXml().ToString().ShouldEqual(expectedXml.ToString());
        }

        [Test]
        public void ShouldRemoveFileFromProject()
        {
            var project = new FakeProject(fileName: @"c:\test\code\fake.csproj");
            project.AddFile("some content", @"c:\test\code\test.cs");

            var xml = @"
                <Project xmlns=""http://schemas.microsoft.com/developer/msbuild/2003"">
                    <ItemGroup>
                        <Compile Include=""Test.cs""/>
                    </ItemGroup>
                </Project>";

            project.XmlRepresentation = XDocument.Parse(xml);

            var solution = new FakeSolution(@"c:\test\fake.sln");
            solution.Projects.Add(project);

            var request = new RemoveFromProjectRequest
            {
                FileName = @"c:\test\code\test.cs"
            };

            var handler = new RemoveFromProjectHandler(solution);
            handler.RemoveFromProject(request);

            project.Files.ShouldBeEmpty();
        }
    }
}
