using System;
using System.Windows;
using System.Windows.Input;

namespace Momo.presentation.login
{
    public partial class LoginPage : Window
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void SignUpText_Click(object sender, MouseButtonEventArgs e)
        {
            // Navigate to the RegisterPage.xaml
            this.Content = new RegisterPage();
        }
    }
}