using System;
using System.Diagnostics;
using SQLCC.Core.Interfaces;

namespace SQLCC.Commands
{
    public class ExecuteCommand : ICommand
    {
        private string _target;
        private string _targetArgs;

        public ExecuteCommand(string target, string targetArgs)
        {
            _target = target;
            _targetArgs = targetArgs;
        }

        public void Execute()
        {
            var cmdUtility = new Process
            {
                StartInfo =
                {
                    FileName = _target,
                    Arguments = _targetArgs,
                    UseShellExecute = false
                }
            };
            cmdUtility.Start();
            cmdUtility.WaitForExit();

            if (cmdUtility.ExitCode != 0)
            {
                throw new InvalidOperationException($"Command '{_target}' finished with exit code '{cmdUtility.ExitCode}'");
            }
        }
    }
}
