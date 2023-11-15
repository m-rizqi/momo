using Momo.data.datasource.database;
using Momo.data.datasource.database.entity;
using Momo.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Momo.presentation.home
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DatabaseService dbServices = new();

            string activityName = txtActivity.Text;
            TaskEntity taskEntity = new TaskEntity(activityName, "-");

            dbServices.CreateTask(taskEntity); 
        }
    }
}
