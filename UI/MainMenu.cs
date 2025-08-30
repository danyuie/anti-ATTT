using Terminal.Gui;
using System.Collections.Generic;
using TerminalSecurityTool;

namespace TerminalSecurityTool.UI
{
    public class MainMenuView : Window
    {
        private readonly List<string> menuOptions = new List<string>
        {
            "🔍 Xem thông tin phần cứng",
            "📡 Quét dấu vết kết nối Wi-Fi",
            "🔌 Quét dấu vết kết nối USB / điện thoại",
            "🧹 Xoá tất cả dấu vết",
            "❌ Thoát"
        };

        public MainMenuView() : base("Menu chính")
        {
            this.ColorScheme = UIColors.Default;
            Width = Dim.Fill();
            Height = Dim.Fill();

            var menuLabel = new Label("Chọn chức năng:")
            {
                X = Pos.Center(),
                Y = 4
            };
            Add(menuLabel);

            var listView = new ListView(menuOptions)
            {
                X = Pos.Center(),
                Y = 6,
                Width = 40,
                Height = menuOptions.Count + 2,
                ColorScheme = UIColors.Default
            };

            listView.OpenSelectedItem += args =>
            {
                HandleMenuSelection(args.Item);
            };

            Add(listView);

        }

        private void HandleMenuSelection(int index)
        {
            switch (index)
            {
                case 0:
                    var systemInfoView = new SystemInfoView();
                    Application.Top.Add(systemInfoView);
                    Application.Refresh();
                    break;
                case 1:
                    var wifiTraceView = new WifiTraceView();
                    Application.Top.Add(wifiTraceView);
                    Application.Refresh();
                    break;
                case 2:
                    var usbView = new UsbTraceView();
                    Application.Top.Add(usbView);
                    Application.Refresh();
                    break;
                case 3:
                    MessageBox.Query("Dọn dẹp", "→ Xoá dấu vết (đang phát triển)", "OK");
                    break;
                case 4:
                    int result = MessageBox.Query("Xác nhận", "Bạn có chắc muốn thoát?", "Có", "Không");
                    if (result == 0)
                        Application.RequestStop();
                    break;
            }
        }
    }
}
