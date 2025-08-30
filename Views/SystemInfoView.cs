using Terminal.Gui;
using System.Text;
using TerminalSecurityTool.Helpers;

namespace TerminalSecurityTool.UI
{
    public class SystemInfoView : Window
    {
        public SystemInfoView() : base("Thông tin hệ thống")
        {
            Width = Dim.Fill();
            Height = Dim.Fill();
            this.ColorScheme = UIColors.Default;

            var frame = new FrameView("Thông tin máy tính")
            {
                X = Pos.Center() - 40,
                Y = 2,
                Width = 80,
                Height = Dim.Fill() - 4,
                ColorScheme = UIColors.Default,
            };

            var infoText = new TextView
            {
                ReadOnly = true,
                WordWrap = true,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
                ColorScheme = UIColors.Default,

            };

            infoText.Text = new SystemInfoCollector().GetFormattedInfo();
            frame.Add(infoText);

            var backButton = new Button("Quay lại")
            {
                X = Pos.Center(),
                Y = Pos.Bottom(frame) + 1
            };
            backButton.Clicked += () => Application.Top.Remove(this);

            Add(frame, backButton);
        }
    }
}
