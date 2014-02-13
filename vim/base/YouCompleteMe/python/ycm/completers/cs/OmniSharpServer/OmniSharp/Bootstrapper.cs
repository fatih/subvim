using System;
using System.Diagnostics;
using MonoDevelop.Projects;
using Nancy.Json;
using Nancy.TinyIoc;
using Nancy.Bootstrapper;
using OmniSharp.ProjectManipulation.AddReference;
using OmniSharp.Solution;

namespace OmniSharp
{
    public class Bootstrapper : Nancy.DefaultNancyBootstrapper
    {
        private readonly ISolution _solution;

        public Bootstrapper(ISolution solution)
        {
            _solution = solution;
            JsonSettings.MaxJsonLength = int.MaxValue;
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            if (PlatformService.IsUnix)
            {
                HelpService.AsyncInitialize();
            }

            pipelines.BeforeRequest.AddItemToStartOfPipeline(ctx =>
                {
                    var stopwatch = new Stopwatch();
                    ctx.Items["stopwatch"] = stopwatch;
                    stopwatch.Start();
                    return null;
                });

            pipelines.AfterRequest.AddItemToEndOfPipeline(ctx =>
                {
                    var stopwatch = (Stopwatch) ctx.Items["stopwatch"];
                    stopwatch.Stop();
                    Console.WriteLine(ctx.Request.Path + " " + stopwatch.ElapsedMilliseconds + "ms");
                });

            pipelines.OnError.AddItemToEndOfPipeline((ctx, ex) =>
                {
                    Console.WriteLine(ex);
                    return null;
                });
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register(_solution);
			container.RegisterMultiple<IReferenceProcessor>(new []{typeof(AddProjectReferenceProcessor), typeof(AddFileReferenceProcessor), typeof(AddGacReferenceProcessor)});			
        }
    }
}
