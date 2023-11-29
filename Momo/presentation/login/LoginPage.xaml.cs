
using Momo.data.datasource.database;
using Momo.presentation.home;
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
        public static string userId;

        public LoginPage()
        {
            InitializeComponent();
        }

        public static bool isLogin;
        private void SignUpText_Click(object sender, MouseButtonEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Content = new RegisterPage();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email = txtEmail.Text;
            string password = txtPassword.Password;
  
            DatabaseService dbServices = new();
            userId = dbServices.LoginUser(email, password);
            isLogin = userId != ""; 

            if(isLogin)
            {
                HomePage homePage = new();
                
                Window currentWindow = Window.GetWindow(this);
                currentWindow.Close();

                homePage.Show();
            }
        }
    }
}