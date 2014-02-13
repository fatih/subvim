using System;
using System.Collections.Generic;
using System.Diagnostics;
using OmniSharp.Common;

namespace OmniSharp.Build
{
    public class BuildHandler
    {
        private readonly BuildCommandBuilder _commandBuilder;
        private readonly BuildResponse _response;
        private readonly List<QuickFix> _quickFixes;
        private readonly BuildLogParser _logParser;

        public BuildHandler(BuildCommandBuilder commandBuilder)
        {
            _commandBuilder = commandBuilder;
            _response = new BuildResponse();
            _quickFixes = new List<QuickFix>();
            _logParser = new BuildLogParser();
        }

        public BuildResponse Build()
        {
            var build = _commandBuilder.Executable;

            var startInfo = new ProcessStartInfo
                {
                    FileName = build,
                    Arguments = _commandBuilder.Arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    RedirectStandardInput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

            var process = new Process
                {
                    StartInfo = startInfo,
                    EnableRaisingEvents = true
                };

            process.ErrorDataReceived += ErrorDataReceived;
            process.OutputDataReceived += OutputDataReceived;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            process.WaitForExit();
            _response.QuickFixes = _quickFixes;

            return _response;
        }

        void OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
            if (e.Data == null)
                return;

            if (e.Data == "Build succeeded.")
                _response.Success = true;
            var quickfix = _logParser.Parse(e.Data);
            if(quickfix != null)
                _quickFixes.Add(quickfix);
        }

        void ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine(e.Data);
        }
    }
}
