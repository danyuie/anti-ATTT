using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace TerminalSecurityTool.Helpers
{
    public class WifiRegistryReader
    {
        public List<(string ProfileName, DateTime LastConnected)> GetRegistryProfiles()
        {
            var results = new List<(string, DateTime)>();
            string baseKey = @"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\NetworkList\\Profiles";

            using (var profilesKey = Registry.LocalMachine.OpenSubKey(baseKey))
            {
                if (profilesKey != null)
                {
                    foreach (var subkeyName in profilesKey.GetSubKeyNames())
                    {
                        using (var subkey = profilesKey.OpenSubKey(subkeyName))
                        {
                            var name = subkey?.GetValue("ProfileName")?.ToString() ?? "(Không tìm thấy)";
                            var dateBinary = subkey?.GetValue("DateCreated") as byte[];
                            var date = dateBinary != null ? DateTime.FromFileTime(BitConverter.ToInt64(dateBinary, 0)) : DateTime.MinValue;
                            results.Add((name, date));
                        }
                    }
                }
            }

            return results;
        }
    }
}
