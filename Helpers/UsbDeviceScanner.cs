using System.Collections.Generic;
using System.Management;

namespace TerminalSecurityTool.Helpers
{
    public class UsbDeviceScanner
    {
        public List<(string Name, string DeviceID)> GetConnectedUsbDevices()
        {
            var list = new List<(string, string)>();
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PnPEntity WHERE DeviceID LIKE 'USB%'");

            foreach (var device in searcher.Get())
            {
                string name = device["Name"]?.ToString() ?? "Unknown";
                string id = device["DeviceID"]?.ToString() ?? "Unknown";
                list.Add((name, id));
            }

            return list;
        }
    }
}
