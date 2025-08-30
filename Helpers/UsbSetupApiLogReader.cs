using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace TerminalSecurityTool.Helpers
{
    public class UsbSetupApiLogReader
    {
        public List<(DateTime Timestamp, string DeviceInfo)> GetDeviceInstallLogs()
        {
            var result = new List<(DateTime, string)>();
            var path = @"C:\Windows\inf\setupapi.dev.log";

            if (!File.Exists(path)) return result;

            var lines = File.ReadAllLines(path);
            DateTime currentDate = DateTime.MinValue;
            string currentDevice = null;

            foreach (var line in lines)
            {
                if (line.StartsWith(">>>  [Device Install"))
                {
                    var match = Regex.Match(line, @">>>  \[Device Install \((.*?)\)\]");
                    if (match.Success && DateTime.TryParse(match.Groups[1].Value, out var dt))
                        currentDate = dt;
                }

                if (line.Contains("USB") && line.Contains("Device Description"))
                {
                    currentDevice = line.Trim();
                    if (currentDevice != null)
                        result.Add((currentDate, currentDevice));
                }
            }

            return result;
        }
    }
}
