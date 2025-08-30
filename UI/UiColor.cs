using Terminal.Gui;

namespace TerminalSecurityTool.UI
{
    public static class UIColors
    {
        public static readonly ColorScheme Default = new ColorScheme
        {
            Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.Gray),
            Focus = Terminal.Gui.Attribute.Make(Color.White, Color.DarkGray),
            HotNormal = Terminal.Gui.Attribute.Make(Color.Black, Color.Gray),
            HotFocus = Terminal.Gui.Attribute.Make(Color.Black, Color.Gray)
        };
        public static readonly ColorScheme Login = new ColorScheme
        {
            Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.Gray),
            Focus = Terminal.Gui.Attribute.Make(Color.Black, Color.Gray),
            HotNormal = Terminal.Gui.Attribute.Make(Color.Black, Color.Gray),
            HotFocus = Terminal.Gui.Attribute.Make(Color.Black, Color.DarkGray)
        };
        public static readonly ColorScheme Info = new ColorScheme
        {
            Normal = Terminal.Gui.Attribute.Make(Color.Black, Color.DarkGray),
            Focus = Terminal.Gui.Attribute.Make(Color.Black, Color.Gray),
            HotNormal = Terminal.Gui.Attribute.Make(Color.Black, Color.Gray),
            HotFocus = Terminal.Gui.Attribute.Make(Color.Black, Color.DarkGray)
        };


        public static readonly ColorScheme Error = new ColorScheme
        {
            Normal = Terminal.Gui.Attribute.Make(Color.BrightRed, Color.Black)
        };

        public static readonly ColorScheme Success = new ColorScheme
        {
            Normal = Terminal.Gui.Attribute.Make(Color.BrightGreen, Color.Black)
        };

        public static readonly ColorScheme Highlight = new ColorScheme
        {
            Normal = Terminal.Gui.Attribute.Make(Color.BrightBlue, Color.Black)
        };
    }
}
