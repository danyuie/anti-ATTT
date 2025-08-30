using Terminal.Gui;
namespace TerminalSecurityTool.UI
{
    public class LoginView : Window
    {
        private TextField usernameField;
        private TextField passwordField;
        private Label messageLabel;
        
        public LoginView() : base("Đăng nhập")
{
            this.ColorScheme = UIColors.Login;
    Width = Dim.Fill();
    Height = Dim.Fill();

    int fieldWidth = 30;
    int verticalSpacing = 2;

    // Container view ở giữa màn hình
    var container = new View()
    {
        Width = Dim.Fill(),
        Height = Dim.Fill()
    };

    int centerY = 5;

    var usernameLabel = new Label("Username:")
    {
        X = Pos.Center() - fieldWidth / 5 - 10,
        Y = centerY
    };
    usernameField = new TextField("")
    {
        X = Pos.Center() - fieldWidth / 5,
        Y = centerY,
        Width = fieldWidth
    };

    var passwordLabel = new Label("Password:")
    {
        X = usernameLabel.X,
        Y = centerY + verticalSpacing
    };
    passwordField = new TextField("")
    {
        X = usernameField.X,
        Y = centerY + verticalSpacing,
        Width = fieldWidth,
        Secret = true
    };

    var loginButton = new Button("Login")
    {
        X = Pos.Center(),
        Y = passwordField.Y + verticalSpacing
    };
    loginButton.Clicked += OnLoginClicked;

    messageLabel = new Label("")
    {
        X = Pos.Center(),
        Y = loginButton.Y + verticalSpacing,
        ColorScheme = UIColors.Error
    };

    container.Add(usernameLabel, usernameField,
                  passwordLabel, passwordField,
                  loginButton, messageLabel);

    Add(container);
}


        private void OnLoginClicked()
        {
            string username = usernameField.Text.ToString();
            string password = passwordField.Text.ToString();

            if (username == "admin" && password == "1234")
            {
                messageLabel.Text = "Đăng nhập thành công!";
                messageLabel.ColorScheme = UIColors.Success;
                Application.Top.RemoveAll();
                Application.Top.Add(new MainMenuView());
            }
            else
            {
                messageLabel.Text = "Sai tên đăng nhập hoặc mật khẩu.";
                messageLabel.ColorScheme = UIColors.Error;
            }
        }
    }
}
