using Momo.presentation.login;
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
            this.Content = new LoginPage();
        }
    }
}
