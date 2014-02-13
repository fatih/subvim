using OmniSharp.Solution;

namespace OmniSharp.ProjectManipulation.AddReference
{
    public class AddReferenceHandler
    {
        private readonly ISolution _solution;
        private readonly IAddReferenceProcessorFactory _addReferenceProcessorFactory;

        public AddReferenceHandler(ISolution solution, IAddReferenceProcessorFactory addReferenceProcessorFactory)
        {
            _solution = solution;
            _addReferenceProcessorFactory = addReferenceProcessorFactory;
        }

        public AddReferenceResponse AddReference(AddReferenceRequest request)
        {
            var project = _solution.ProjectContainingFile(request.FileName);
           
            var processor = _addReferenceProcessorFactory.CreateProcessorFor(request);

            return processor.AddReference(project, request.Reference);
           
        }
    }
}