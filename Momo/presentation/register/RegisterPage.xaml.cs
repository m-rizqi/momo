using Momo.data.datasource.database;
using Momo.data.datasource.database.entity;
using Momo.presentation.home;
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

        private void btnSignup_Click(object sender, RoutedEventArgs e)
        {
            string password = txtPassword.Password;            
            string email = txtEmail.Text;
            string name = txtName.Text;
           
            UserEntity user = new UserEntity(0, name, email, password);

            DatabaseService dbServices = new DatabaseService();

            dbServices.CreateUser(user);
        }
    }
}
