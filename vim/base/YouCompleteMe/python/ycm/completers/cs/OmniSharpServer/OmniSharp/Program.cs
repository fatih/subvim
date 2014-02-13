using System;
using System.IO;
using System.Threading;
using NDesk.Options;
using Nancy.Hosting.Self;
using OmniSharp.Solution;

namespace OmniSharp
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            bool showHelp = false;
            string solutionPath = null;

            var port = 2000;

            var p = new OptionSet
                    {
                        {
                            "s|solution=", "The path to the solution file",
                            v => solutionPath = v
                        },
                        {
                            "p|port=", "Port number to listen on",
                            (int v) => port = v
                        },
                        {
                            "h|help", "show this message and exit",
                            v => showHelp = v != null
                        },
                    };

            try
            {
                p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try 'omnisharp --help' for more information.");
                return;
            }

            showHelp |= solutionPath == null;

            if (showHelp)
            {
                ShowHelp(p);
                return;
            }

            StartServer(solutionPath, port);
            
        }

        private static void StartServer(string solutionPath, int port)
        {
            var lockfile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lockfile-" + port);

            try
            {
                using (new FileStream(lockfile, FileMode.OpenOrCreate, FileAccess.Read, FileShare.None))
                {
                    var solution = new CSharpSolution(solutionPath);
                    Console.CancelKeyPress +=
                        (sender, e) =>
                            {
                                solution.Terminated = true;
                                Console.WriteLine("Ctrl-C pressed");
                                e.Cancel = true;
                            };

                    var nancyHost = new NancyHost(new Bootstrapper(solution), new Uri("http://localhost:" + port));

                    nancyHost.Start();

                    while (!solution.Terminated)
                    {
                        Thread.Sleep(1000);
                    }
                    
                    Console.WriteLine("Quit gracefully");
                    nancyHost.Stop();
                }
                DeleteLockFile(lockfile);
            }
            catch (IOException)
            {
                Console.WriteLine("Detected an OmniSharp instance already running on port " + port + ". Press a key.");
                Console.ReadKey();
            }
        }

        private static void DeleteLockFile(string lockfile)
        {
            File.Delete(lockfile);
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: omnisharp -s /path/to/sln [-p PortNumber]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
        }
    }
}
