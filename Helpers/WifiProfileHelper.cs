using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace TerminalSecurityTool.Helpers
{
    public class WifiProfileHelper
    {
        public List<(string SSID, string Password)> GetProfiles()
        {
            var profiles = new List<(string, string)>();
            var output = RunCommand("netsh wlan show profiles");
            var ssidMatches = Regex.Matches(output, "All User Profile\\s*:\\s*(.*)");

            foreach (Match match in ssidMatches)
            {
                var ssid = match.Groups[1].Value.Trim();
                var detail = RunCommand($"netsh wlan show profile name=\"{ssid}\" key=clear");
                var passMatch = Regex.Match(detail, "Key Content\\s*:\\s*(.*)");
                var password = passMatch.Success ? passMatch.Groups[1].Value.Trim() : "(Không tìm thấy)";
                profiles.Add((ssid, password));
            }
            return profiles;
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
