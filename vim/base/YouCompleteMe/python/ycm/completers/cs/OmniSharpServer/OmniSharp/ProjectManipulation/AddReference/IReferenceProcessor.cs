using OmniSharp.Solution;

namespace OmniSharp.ProjectManipulation.AddReference
{
    public interface IReferenceProcessor
    {
        AddReferenceResponse AddReference(IProject project, string reference);
    }
}