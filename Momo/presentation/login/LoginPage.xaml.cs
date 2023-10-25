
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Momo
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        private void SignUpText_Click(object sender, MouseButtonEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Content = new RegisterPage();
        }
    }
}