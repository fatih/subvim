using Nancy;
using Nancy.ModelBinding;

namespace OmniSharp.ProjectManipulation.RemoveFromProject
{
    public class RemoveFromProjectModule : NancyModule
    {
        public RemoveFromProjectModule(RemoveFromProjectHandler handler)
        {
            Post["/removefromproject"] = x =>
                {
                    var req = this.Bind<RemoveFromProjectRequest>();
                    handler.RemoveFromProject(req);
                    return Response.AsJson(true);
                };
        }
    }
}
