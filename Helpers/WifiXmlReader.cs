using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace TerminalSecurityTool.Helpers
{
    public class WifiXmlReader
    {
        public List<(string SSID, string Authentication, string Encryption)> GetXmlProfiles()
        {
            var results = new List<(string, string, string)>();
            var basePath = @"C:\ProgramData\Microsoft\Wlansvc\Profiles\Interfaces";

            if (!Directory.Exists(basePath)) return results;

            foreach (var dir in Directory.GetDirectories(basePath))
            {
                foreach (var file in Directory.GetFiles(dir, "*.xml"))
                {
                    try
                    {
                        var doc = XDocument.Load(file);
                        var ssid = doc.Root?.Element("SSIDConfig")?.Element("SSID")?.Element("name")?.Value ?? "(Không tìm thấy)";
                        var auth = doc.Root?.Element("MSM")?.Element("security")?.Element("authEncryption")?.Element("authentication")?.Value ?? "";
                        var enc = doc.Root?.Element("MSM")?.Element("security")?.Element("authEncryption")?.Element("encryption")?.Value ?? "";
                        results.Add((ssid, auth, enc));
                    }
                    catch { }
                }
            }

            return results;
        }
    }
}
