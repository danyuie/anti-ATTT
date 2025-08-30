using Terminal.Gui;
using System.Text;
using TerminalSecurityTool.Helpers;

namespace TerminalSecurityTool.UI
{
    public class WifiTraceView : Window
    {
        public WifiTraceView() : base("Dấu vết kết nối Wi-Fi")
        {
            Width = Dim.Fill();
            Height = Dim.Fill();
            ColorScheme = UIColors.Default;

            var frame = new FrameView("Thông tin chi tiết")
            {
                X = Pos.Center() - 40,
                Y = 2,
                Width = 80,
                Height = Dim.Fill() - 4,
                ColorScheme = UIColors.Default
            };

            var infoText = new TextView
            {
                ReadOnly = true,
                WordWrap = true,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                ColorScheme = UIColors.Default
            };

            infoText.Text = GetWifiTraceText();
            frame.Add(infoText);

            var backButton = new Button("Quay lại")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(frame) + 1
            };
            backButton.Clicked += () => Application.Top.Remove(this);

            Add(frame, backButton);
        }

        private string GetWifiTraceText()
        {
            var sb = new StringBuilder();

            // 1. Netsh profiles
            var profileHelper = new WifiProfileHelper();
            var profiles = profileHelper.GetProfiles();
            sb.AppendLine("📌 [Netsh WLAN Profiles]");
            foreach (var (ssid, pass) in profiles)
            {
                sb.AppendLine($"SSID: {ssid}\n  ↳ Password: {pass}");
            }

            sb.AppendLine();

            // 2. XML cấu hình
            var xmlHelper = new WifiXmlReader();
            var xmls = xmlHelper.GetXmlProfiles();
            sb.AppendLine("📂 [Cấu hình XML trong ProgramData]");
            foreach (var (ssid, auth, enc) in xmls)
            {
                sb.AppendLine($"SSID: {ssid}\n  ↳ Auth: {auth} | Enc: {enc}");
            }

            sb.AppendLine();

            // 3. Registry
            var regHelper = new WifiRegistryReader();
            var regProfiles = regHelper.GetRegistryProfiles();
            sb.AppendLine("🧬 [Registry - NetworkList\\Profiles]");
            foreach (var (name, date) in regProfiles)
            {
                sb.AppendLine($"Tên: {name}\n  ↳ Ngày tạo: {date}");
            }

            sb.AppendLine();

            // 4. Event Log
            var evtHelper = new WifiEventLogReader();
            var events = evtHelper.GetWifiEvents();
            sb.AppendLine("📅 [Event Log - WLAN AutoConfig]");
            foreach (var (time, msg) in events)
            {
                sb.AppendLine($"[{time}] {msg}");
            }

            sb.AppendLine();

            // 5. Cache DNS + ARP
            var cacheHelper = new WifiCacheReader();
            sb.AppendLine("📚 [DNS Cache]");
            foreach (var domain in cacheHelper.GetDnsCache())
            {
                sb.AppendLine($"  ↳ {domain}");
            }

            sb.AppendLine();
            sb.AppendLine("🔍 [ARP Cache]");
            foreach (var entry in cacheHelper.GetArpCache())
            {
                sb.AppendLine($"  ↳ {entry}");
            }

            return sb.ToString();
        }
    }
}
