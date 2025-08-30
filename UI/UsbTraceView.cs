using Terminal.Gui;
using TerminalSecurityTool.Helpers;
using System.Text;

namespace TerminalSecurityTool.UI
{
    public class UsbTraceView : Window
    {
        public UsbTraceView() : base("Dấu vết thiết bị USB / điện thoại")
        {
            Width = Dim.Fill();
            Height = Dim.Fill();
            ColorScheme = UIColors.Default;

            var frame = new FrameView("Dấu vết thiết bị")
            {
                X = Pos.Center() - 40,
                Y = 2,
                Width = 80,
                Height = Dim.Fill() - 4,
                ColorScheme = UIColors.Default
            };

            var textView = new TextView
            {
                ReadOnly = true,
                WordWrap = true,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                ColorScheme = UIColors.Default
            };

            var content = new StringBuilder();

            // 1. USBSTOR Registry
            content.AppendLine("🗂 Registry - USBSTOR:");
            var regList = new UsbRegistryReader().GetUsbStors();
            if (regList.Count == 0) content.AppendLine("  - Không tìm thấy thiết bị nào.");
            foreach (var (name, serial, vidpid) in regList)
                content.AppendLine($"  - {name} | Serial: {serial} | {vidpid}");

            content.AppendLine();

            // 2. SetupAPI Logs
            content.AppendLine("📜 setupapi.dev.log:");
            var setupLogs = new UsbSetupApiLogReader().GetDeviceInstallLogs();
            if (setupLogs.Count == 0) content.AppendLine("  - Không có log.");
            foreach (var (dt, info) in setupLogs)
                content.AppendLine($"  - {dt:G} | {info}");

            content.AppendLine();

            // 3. Event Logs
            content.AppendLine("🧾 Event Log (DriverFrameworks):");
            var events = new UsbEventLogReader().GetUsbEvents();
            if (events.Count == 0) content.AppendLine("  - Không có sự kiện.");
            foreach (var (dt, msg) in events)
                content.AppendLine($"  - {dt:G} | {msg}");

            content.AppendLine();

            // 4. Thiết bị đang kết nối
            content.AppendLine("🟢 USB hiện đang kết nối:");
            var devices = new UsbDeviceScanner().GetConnectedUsbDevices();
            if (devices.Count == 0) content.AppendLine("  - Không có thiết bị kết nối.");
            foreach (var (name, id) in devices)
                content.AppendLine($"  - {name} | {id}");

            textView.Text = content.ToString();
            frame.Add(textView);

            var backBtn = new Button("Quay lại")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(frame) + 1
            };
            backBtn.Clicked += () => Application.Top.Remove(this);

            Add(frame, backBtn);
        }
    }
}
