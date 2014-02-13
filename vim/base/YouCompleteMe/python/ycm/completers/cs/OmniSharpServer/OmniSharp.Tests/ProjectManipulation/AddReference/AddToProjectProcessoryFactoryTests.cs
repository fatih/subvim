using System.Collections.Generic;
using NUnit.Framework;
using OmniSharp.Common;
using OmniSharp.ProjectManipulation.AddReference;
using OmniSharp.Solution;
using Should;

namespace OmniSharp.Tests.ProjectManipulation.AddReference
{
    [TestFixture]
    public class AddToProjectProcessoryFactoryTests
    {
        IEnumerable<IReferenceProcessor> _processors;
        ISolution _solution;
        AddReferenceProcessorFactory _factory;

        [SetUp]
        public void SetUp()
        {
            _solution = new FakeSolution();
            var project = new FakeProject();
            _solution.Projects.Add(project);
            _processors = new List<IReferenceProcessor>
                              {
                                  new AddProjectReferenceProcessor(_solution),
                                  new AddFileReferenceProcessor(),
                                  new AddGacReferenceProcessor()
                              };

            _factory = new AddReferenceProcessorFactory(_solution, _processors, new FileSystem());
        }

        [Test]
        public void ShouldReturnAddProjectReferenceProcessorWhenReferencingProject()
        {
            var request = new AddReferenceRequest
                              {
                                  Reference = "fake"
                              };

            var processor = _factory.CreateProcessorFor(request);

            processor.ShouldBeType<AddProjectReferenceProcessor>();
        }

        [Test]
        public void ShouldReturnAddFileReferenceProcessorWhenReferencingFile()
        {
            var request = new AddReferenceRequest
                              {
                                  Reference = "Nancy.dll"
                              };

            var processor = _factory.CreateProcessorFor(request);

            processor.ShouldBeType<AddFileReferenceProcessor>();
        }

        [Test]
        public void ShouldReturnAddGacReferenceProcessorWhenReferencingGac()
        {
            var request = new AddReferenceRequest
            {
                Reference = "System.Web.Mvc"
            };

            var processor = _factory.CreateProcessorFor(request);

            processor.ShouldBeType<AddGacReferenceProcessor>();
        }

        [Test]
        public void ShouldReturnAddGacReferenceProcessorWhenFileReferenceNotFound()
        {
            var fs = new FakeFileSystem();
            fs.FileExists(false);
            _factory = new AddReferenceProcessorFactory(_solution, _processors, fs);
            var request = new AddReferenceRequest
            {
                Reference = "Nancy.dll"
            };

            var processor = _factory.CreateProcessorFor(request);
            processor.ShouldBeType<AddGacReferenceProcessor>();
        }
    }
}
