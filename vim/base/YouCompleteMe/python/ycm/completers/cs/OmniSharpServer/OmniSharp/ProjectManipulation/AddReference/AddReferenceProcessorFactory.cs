using System;
using System.Collections.Generic;
using System.Linq;
using OmniSharp.Common;
using OmniSharp.Solution;

namespace OmniSharp.ProjectManipulation.AddReference
{
    public interface IAddReferenceProcessorFactory
    {
        IReferenceProcessor CreateProcessorFor(AddReferenceRequest request);
    }

    public class AddReferenceProcessorFactory : IAddReferenceProcessorFactory
    {
        private readonly ISolution _solution;
        private readonly IFileSystem _fileSystem;
        private readonly IDictionary<Type, IReferenceProcessor> _processors; 

        public AddReferenceProcessorFactory(ISolution solution, IEnumerable<IReferenceProcessor> processors, IFileSystem fileSystem)
        {
            _solution = solution;
            _fileSystem = fileSystem;
            _processors = processors.ToDictionary(k => k.GetType(), v => v);
        }

        public IReferenceProcessor CreateProcessorFor(AddReferenceRequest request)
        {
            if (IsProjectReference(request.Reference))
            {
                return _processors[typeof (AddProjectReferenceProcessor)];
            }

            if (IsFileReference(request.Reference))
            {
                return _processors[typeof (AddFileReferenceProcessor)];
            }

            return _processors[typeof (AddGacReferenceProcessor)];
        }

        private bool IsProjectReference(string referenceName)
        {
            return _solution.Projects.Any(p => p.FileName.Contains(referenceName));
        }

        private bool IsFileReference(string reference)
        {
            var fileInfo = _fileSystem.GetFileInfo(reference);
            return fileInfo.Exists && fileInfo.Extension.Equals(".dll", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}