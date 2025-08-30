using System;
using System.Management;
using System.Text;

namespace TerminalSecurityTool.Helpers
{
    public class SystemInfoCollector
    {
        public string GetFormattedInfo()
        {
            
            var sb = new StringBuilder();

            sb.AppendLine($"🖥️ Tên máy: {Environment.MachineName}");
            sb.AppendLine($"👤 Người dùng: {Environment.UserName}");
            sb.AppendLine($"💻 Hệ điều hành: {GetOSInfo()}");
            sb.AppendLine($"📆 Ngày cài đặt: {GetInstallDate()}");
            sb.AppendLine($"🔑 Windows đã active: {IsWindowsActivated()}");

            sb.AppendLine();
            sb.AppendLine("🧬 BIOS:");
            sb.AppendLine($"  - {GetBIOSInfo()}");

            sb.AppendLine();
            sb.AppendLine("💾 RAM:");
            sb.AppendLine(GetRAMInfo());

            sb.AppendLine();
            sb.AppendLine("🧠 CPU:");
            sb.AppendLine(GetCPUInfo());

            sb.AppendLine();
            sb.AppendLine("🖼️ GPU:");
            sb.AppendLine(GetGPUInfo());

            sb.AppendLine();
            sb.AppendLine("🗃️ Ổ cứng:");
            sb.AppendLine(GetDriveInfo());

            sb.AppendLine();
            sb.AppendLine("🌐 Mạng:");
            sb.AppendLine(GetNetworkInfo());

            return sb.ToString();
        }

        private string GetOSInfo()
        {
            return $"{Environment.OSVersion}";
        }

        private string GetInstallDate()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT InstallDate FROM Win32_OperatingSystem");
                foreach (var obj in searcher.Get())
                {
                    var date = ManagementDateTimeConverter.ToDateTime(obj["InstallDate"].ToString());
                    return date.ToString("dd/MM/yyyy HH:mm");
                }
            }
            catch { }
            return "Không xác định";
        }

        private string IsWindowsActivated()
        {
            try
            {
                var result = Microsoft.Win32.Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion", "DigitalProductId", null);
                return result != null ? "✔ Đã kích hoạt" : "✘ Chưa kích hoạt";
            }
            catch { return "Không rõ"; }
        }

        private string GetBIOSInfo()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT Manufacturer, SMBIOSBIOSVersion FROM Win32_BIOS");
                foreach (var obj in searcher.Get())
                {
                    return $"{obj["Manufacturer"]} - Version: {obj["SMBIOSBIOSVersion"]}";
                }
            }
            catch { }
            return "Không rõ";
        }

        private string GetRAMInfo()
        {
            var sb = new StringBuilder();
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT Capacity, Speed FROM Win32_PhysicalMemory");
                int index = 1;
                foreach (var obj in searcher.Get())
                {
                    var cap = Convert.ToInt64(obj["Capacity"]) / (1024 * 1024);
                    var speed = obj["Speed"];
                    sb.AppendLine($"  - Thanh {index++}: {cap} MB @ {speed} MHz");
                }
            }
            catch { sb.AppendLine("  Không rõ"); }
            return sb.ToString();
        }

        private string GetCPUInfo()
        {
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_Processor");
                foreach (var obj in searcher.Get())
                {
                    return $"  - {obj["Name"]}";
                }
            }
            catch { }
            return "  Không rõ";
        }

        private string GetGPUInfo()
        {
            var sb = new StringBuilder();
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT Name FROM Win32_VideoController");
                foreach (var obj in searcher.Get())
                {
                    sb.AppendLine($"  - {obj["Name"]}");
                }
            }
            catch { sb.AppendLine("  Không rõ"); }
            return sb.ToString();
        }

        private string GetDriveInfo()
        {
            var sb = new StringBuilder();
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT Model, Size FROM Win32_DiskDrive");
                foreach (var obj in searcher.Get())
                {
                    var size = Convert.ToInt64(obj["Size"]) / (1024 * 1024 * 1024);
                    sb.AppendLine($"  - {obj["Model"]}: {size} GB");
                }
            }
            catch { sb.AppendLine("  Không rõ"); }
            return sb.ToString();
        }

        private string GetNetworkInfo()
        {
            var sb = new StringBuilder();
            try
            {
                using var searcher = new ManagementObjectSearcher("SELECT Description, MACAddress, IPAddress FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = TRUE");
                foreach (ManagementObject obj in searcher.Get())
                {
                    sb.AppendLine($"  - {obj["Description"]}");
                    sb.AppendLine($"    MAC: {obj["MACAddress"]}");

                    if (obj["IPAddress"] is string[] ipList)
                    {
                        foreach (var ip in ipList)
                            sb.AppendLine($"    IP: {ip}");
                    }
                }
            }
            catch { sb.AppendLine("  Không rõ"); }

            return sb.ToString();
        }
    }
}
