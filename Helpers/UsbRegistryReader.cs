using Microsoft.Win32;
using System.Collections.Generic;

namespace TerminalSecurityTool.Helpers
{
    public class UsbRegistryReader
    {
        public List<(string DeviceName, string Serial, string VidPid)> GetUsbStors()
        {
            var list = new List<(string, string, string)>();
            var keyPath = @"SYSTEM\CurrentControlSet\Enum\USBSTOR";

            using var baseKey = Registry.LocalMachine.OpenSubKey(keyPath);
            if (baseKey == null) return list;

            foreach (var subKeyName in baseKey.GetSubKeyNames())
            {
                var deviceKey = baseKey.OpenSubKey(subKeyName);
                if (deviceKey == null) continue;

                foreach (var instance in deviceKey.GetSubKeyNames())
                {
                    var instanceKey = deviceKey.OpenSubKey(instance);
                    if (instanceKey == null) continue;

                    string name = instanceKey.GetValue("FriendlyName")?.ToString() ?? subKeyName;
                    string hardwareId = (instanceKey.GetValue("HardwareID") as string[])?[0] ?? "";
                    string serial = instance;
                    list.Add((name, serial, hardwareId));
                }
            }

            return list;
        }
    }
}
