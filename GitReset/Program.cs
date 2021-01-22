using System;
using System.Diagnostics;
using System.IO;

namespace GitReset
{
    class Program
    {
        static string MainBranch = "master";

        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();

            foreach (string argument in args)
            {
                if (argument.StartsWith("-s="))
                {
                    path = argument.Substring(3);
                    continue;
                }

                if (argument.StartsWith("-b="))
                {
                    MainBranch = argument.Substring(3);
                    continue;
                }
            }

            Console.WriteLine($"GitReset being performed on: {path}");
            string[] directories = Directory.GetDirectories(path);
            
            foreach (string directory in directories)
            {
                Console.WriteLine($"Updating: {directory}");
                Directory.SetCurrentDirectory(directory);
                GitCheckout();
                GitPull();
            }
        }

        static string GetGitExecutablePath()
        {
            return "C:\\Program Files\\Git\\bin\\git.exe";
        }

        static void GitCheckout()
        {
            Process p = new Process();
            p.StartInfo.FileName = GetGitExecutablePath();
            p.StartInfo.Arguments = $"checkout {MainBranch}";
            p.StartInfo.UseShellExecute = false;
            p.OutputDataReceived += delegate (object sender, DataReceivedEventArgs eventArgs) { Console.WriteLine(eventArgs.Data); };
            p.Start();
            p.WaitForExit();
        }

        static void GitPull()
        {
            Process p = new Process();
            p.StartInfo.FileName = GetGitExecutablePath();
            p.StartInfo.Arguments = "pull --progress -v --no-rebase --tags --prune \"origin\"";
            p.StartInfo.UseShellExecute = false;
            p.OutputDataReceived += delegate (object sender, DataReceivedEventArgs eventArgs) { Console.WriteLine(eventArgs.Data); };
            p.Start();
            p.WaitForExit();
        }
    }
}
