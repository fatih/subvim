using Nancy;

namespace OmniSharp.Build
{
    public class BuildCommandModule : NancyModule
    {
        public BuildCommandModule(BuildCommandBuilder commandBuilder)
        {
            Post["/buildcommand"] = x =>
                Response.AsText(commandBuilder.Executable + " " + commandBuilder.Arguments);
        }
    }
}