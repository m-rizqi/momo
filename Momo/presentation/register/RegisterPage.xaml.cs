using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Momo
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        private void LoginText_Click(object sender, MouseButtonEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Content = new LoginPage();
        }
    }
}
