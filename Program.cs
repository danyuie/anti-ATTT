using Terminal.Gui;
using TerminalSecurityTool.UI;

class Program
{
    static void Main()
    {
        Application.Init();

        Application.Top.ColorScheme = new ColorScheme
        {
            Normal     = Terminal.Gui.Attribute.Make(Color.Black, Color.Gray),
            Focus      = Terminal.Gui.Attribute.Make(Color.Black, Color.Gray),
            HotNormal  = Terminal.Gui.Attribute.Make(Color.Black, Color.Gray),
            HotFocus   = Terminal.Gui.Attribute.Make(Color.Black, Color.Gray)
        };

        var loginView = new LoginView();
        Application.Run(loginView);
    }
}
