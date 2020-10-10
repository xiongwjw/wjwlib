using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using wjw.helper.Conversions;


namespace wjw.helper.Cmd
{
    public class ProcessHelper
    {
        public static void KillProcessByName(string processName)
        {
            Process[] ps = Process.GetProcessesByName(processName);
            foreach (Process p in ps)
                p.Kill();

        }

        public static bool CheckProcessByName(string processName)
        {
            Process[] ps = Process.GetProcessesByName(processName);
            if (ps.Length > 0)
                return true;
            else
                return false;
        }

        public static Process ExecuteProcess(string processPath, string arguments = "")
        {
            if (!File.Exists(processPath))
                return null;
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = processPath;
            if (arguments != string.Empty)
                startInfo.Arguments = arguments;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            return Process.Start(startInfo);
        }

        public static bool CheckProcessByPort(int port)
        {
            if (port < 1024 && port > 49151)
                return false;
            string keyWord = "LISTENING";
            string command = "netstat";
            string argument = $" -nao";
            List<string> result = ExecuteCommandAndGetOutput(command, argument);
            if (result.Count == 0)
                return false;
            var line = result.FirstOrDefault(q => q.Contains(keyWord) && q.Contains(port.ToString()));
            if (line == null)
                return false;
            int pid = TryConvert.String2Int(line.Substring(line.IndexOf(keyWord) + keyWord.Length).Trim());
            if (pid == 0)
                return false;
            return true;
        }

        public static Process GetProcessByPort(int port)
        {
            if (port < 1024 && port > 49151)
                return null;
            //if (Configuration.IsWindows)
            //{
                string keyWord = "LISTENING";
                string command = "netstat";
                string argument = $" -nao";
                List<string> result = ExecuteCommandAndGetOutput(command, argument);
                if (result.Count == 0)
                    return null;
                var line = result.FirstOrDefault(q => q.Contains(keyWord) && q.Contains(port.ToString()));
                if (line == null)
                    return null;
                int pid = TryConvert.String2Int(line.Substring(line.IndexOf(keyWord) + keyWord.Length).Trim());
                if (pid == 0)
                    return null;
                return Process.GetProcessById(pid);
            //}
            //else
            //{
            //    string keyWord = "LISTEN";
            //    string command = "netstat";
            //    string argument = "-tnlp";
            //    List<string> result = ExecuteCommandAndGetOutput(command, argument);
            //    if (result.Count == 0)
            //        return null;
            //    var line = result.FirstOrDefault(q => q.Contains(keyWord) && q.Contains(port.ToString()));
            //    if (line == null)
            //        return null;
            //    string pidName = Utility.GetStringArrayValue(line, 6, " ");
            //    if (string.IsNullOrEmpty(pidName))
            //        return null;
            //    int pid = Utility.TryParseInt(pidName.Split("/").FirstOrDefault());
            //    if (pid == 0)
            //        return null;
            //    return Process.GetProcessById(pid);
            //}
        }




        public static List<string> ExecuteCommandAndGetOutput(string command, string arguments = "")
        {
            List<string> content = new List<string>();
            var psi = new ProcessStartInfo(command, arguments)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };
            var proc = Process.Start(psi);
            if (proc == null)
            {
             //   Log.Debug("can not start top process");
            }
            else
            {
                using (var sr = proc.StandardOutput)
                {
                    while (!sr.EndOfStream)
                    {
                        content.Add(sr.ReadLine());
                    }

                    if (!proc.HasExited)
                    {
                        proc.Kill();
                    }
                }
            }
            return content;
        }



        public static Process GetProcessByName(string processName)
        {
            Process[] arrProc = Process.GetProcessesByName(processName);
            if (arrProc != null && arrProc.Length > 0)
                return arrProc[0];
            else
                return null;
        }

        public static Process GetProcess(string processName)
        {
            var process = Process.GetProcesses().FirstOrDefault(q => q.ProcessName.Contains(processName));
            return process;
        }

        public static void KillProcess(string processName)
        {
            var process = Process.GetProcesses().Where(q => q.ProcessName.Contains(processName));
            foreach (Process p in process)
            {
                p.Kill();
            }
        }


        //public static void KillProcessByName(string processName)
        //{
        //    Process[] arrProc = Process.GetProcessesByName(processName);
        //    foreach (Process item in arrProc)
        //        item.Kill();
        //}
        
        public static ExecuteResult AddExecuteAttr(string filePath)
        {
            //if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            //{
            //    return ExecuteCommand("chmod", $"u+x {filePath}");
            //}
            //else
            //{
                return ExecuteResult.Failed;
            //}

        }

        public static ExecuteResult ExecuteCommand(string command, string arguments = "")
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = command;
            if (arguments != string.Empty)
                startInfo.Arguments = arguments;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;

            Process p = Process.Start(startInfo);
            if (p == null)
            {
               // Log.Info($"start process {command} failed");
                return ExecuteResult.Failed;
            }
            if (p.HasExited)
            {
                if (p.ExitCode == 0)
                {
                    return ExecuteResult.Success;
                }
                else
                {
                   // Log.Info($"Execute process {command} failed");
                    return ExecuteResult.Failed;
                }
            }
            else
            {
                if (p.WaitForExit(30000))
                {
                    if (p.ExitCode == 0)
                    {
                        return ExecuteResult.Success;
                    }
                    else
                    {
                       // Log.Info($"Execute process {command} failed");
                        return ExecuteResult.Failed;
                    }
                }
                else
                {
                    //Log.Info($"wait for process {command} timeout");
                    return ExecuteResult.Failed;
                }
            }
        }


    }

    public enum ExecuteResult
    {
        Success,
        Failed
    }
}
