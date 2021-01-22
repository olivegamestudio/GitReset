using System;
using System.Diagnostics;
using System.IO;

namespace GitReset
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] directories = Directory.GetDirectories(args[0]);

            foreach (string directory in directories)
            {
                Console.WriteLine("Updating: " + directory);
                Directory.SetCurrentDirectory(directory);
                GitCheckout();
                GitPull();
            }
        }

        static void GitCheckout()
        {
            Process p = new Process();
            p.StartInfo.FileName = "C:\\Program Files\\Git\\bin\\git.exe";
            p.StartInfo.Arguments = "checkout master";
            p.StartInfo.UseShellExecute = false;
            p.OutputDataReceived += delegate (object sender, DataReceivedEventArgs eventArgs) { Console.WriteLine(eventArgs.Data); };
            p.Start();
            p.WaitForExit();
        }

        static void GitPull()
        {
            Process p = new Process();
            p.StartInfo.FileName = "C:\\Program Files\\Git\\bin\\git.exe";
            p.StartInfo.Arguments = "pull --progress -v --no-rebase --tags --prune \"origin\"";
            p.StartInfo.UseShellExecute = false;
            p.OutputDataReceived += delegate (object sender, DataReceivedEventArgs eventArgs) { Console.WriteLine(eventArgs.Data); };
            p.Start();
            p.WaitForExit();
        }
    }
}
