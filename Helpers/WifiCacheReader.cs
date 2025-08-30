using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TerminalSecurityTool.Helpers
{
    public class WifiCacheReader
    {
        public List<string> GetDnsCache()
        {
            var result = new List<string>();
            var output = RunCommand("ipconfig /displaydns");

            foreach (Match match in Regex.Matches(output, "Record Name\\s+\\.+:\\s+(.*?)\\n"))
            {
                result.Add(match.Groups[1].Value);
            }

            return result;
        }

        public List<string> GetArpCache()
        {
            var result = new List<string>();
            var output = RunCommand("arp -a");

            foreach (var line in output.Split('\n'))
            {
                if (line.Contains("dynamic"))
                    result.Add(line.Trim());
            }

            return result;
        }

        private string RunCommand(string cmd)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    Arguments = $"/c {cmd}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            process.Start();
            return process.StandardOutput.ReadToEnd();
        }
    }
}
