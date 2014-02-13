using Nancy;
using OmniSharp.Solution;

namespace OmniSharp.CheckAliveStatusModule
{
    public class CheckAliveStatusModule : NancyModule
    {
        public CheckAliveStatusModule(ISolution solution)
        {
            Post["/checkalivestatus"] = x =>
                {
                    return Response.AsJson(true);
                };
        }
    }
}
