using Momo.data.datasource.database;
using Momo.data.datasource.database.entity;
using Momo.domain;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;

namespace Momo.presentation.home
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        private string activeMode = "Pomodoro";
        public HomePage()
        {
            InitializeComponent();
            InitializeTimer();
            btnPomodoroMode.Background = new SolidColorBrush(Colors.LightPink);
            Display_Activities();
        }

        private DispatcherTimer pomodoroTimer;
        private TimeSpan remainingTime;
        private bool isRunning;
        private bool isPaused;

        private void InitializeTimer()
        {
            pomodoroTimer = new DispatcherTimer();
            pomodoroTimer.Interval = TimeSpan.FromSeconds(1);
            pomodoroTimer.Tick += PomodoroTimer_Tick;

            remainingTime = TimeSpan.FromMinutes(25);

            UpdateTimerDisplay();
        }

        private void ChangeMode(string mode)
        {
            activeMode = mode;
            btnPomodoroMode.Background = new SolidColorBrush(Colors.LightGray);
            btnShortBreakMode.Background = new SolidColorBrush(Colors.LightGray);
            btnLongBreakMode.Background = new SolidColorBrush(Colors.LightGray);

            pomodoroTimer.Stop();
            isRunning = false;

            switch (mode)
            {
                case "Pomodoro":
                    btnPomodoroMode.Background = new SolidColorBrush(Colors.LightPink);
                    remainingTime = TimeSpan.FromMinutes(25);
                    break;
                case "Short Break":
                    btnShortBreakMode.Background = new SolidColorBrush(Colors.LightPink);
                    remainingTime = TimeSpan.FromMinutes(5);
                    break;
                case "Long Break":
                    btnLongBreakMode.Background = new SolidColorBrush(Colors.LightPink);
                    remainingTime = TimeSpan.FromMinutes(15);
                    break;
                default:
                    break;
            }

            UpdateTimerDisplay();
        }


        private void PomodoroTimer_Tick(object sender, EventArgs e)
        {
            if (remainingTime > TimeSpan.Zero)
            {
                remainingTime = remainingTime.Subtract(TimeSpan.FromSeconds(1));
                UpdateTimerDisplay();
            }
            else
            {
                MessageBox.Show($"{activeMode} completed!");
                pomodoroTimer.Stop();
                remainingTime = TimeSpan.FromMinutes(25);
                isRunning = false;
                isPaused = false;
            }
        }

        private void UpdateTimerDisplay()
        {
            string displayRemainingTime = $"{remainingTime:mm\\:ss}";
            lblPomodoroTimer.Text = displayRemainingTime;
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!isRunning)
            {
                pomodoroTimer.Start();
                isRunning = true;
                isPaused = false;
            }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            if (isRunning)
            {
                if (isPaused)
                {
                    pomodoroTimer.Start();
                    isPaused = false;
                }
                else
                {
                    pomodoroTimer.Stop();
                    isPaused = true;
                }
            }
        }   

        private void btnPomodoroMode_Click(object sender, RoutedEventArgs e)
        {
            ChangeMode("Pomodoro");
        }

        private void btnShortBreakMode_Click(object sender, RoutedEventArgs e)
        {
            ChangeMode("Short Break");
        }

        private void btnLongBreakMode_Click(object sender, RoutedEventArgs e)
        {
            ChangeMode("Long Break");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DatabaseService dbServices = new();

            string activityName = txtActivity.Text;
            TaskEntity taskEntity = new TaskEntity(activityName, "-");

            dbServices.CreateTask(taskEntity);
            Display_Activities();
        }

        private void Display_Activities()
        {
            DatabaseService dbService = new();

            List<TaskEntity> tasks = dbService.GetAllTasks();

            List<String> taskNames = tasks.Select(task => task.Name).ToList();

            historyActivities.ItemsSource = taskNames;
            listActivities.ItemsSource = taskNames;
        }

    }
}
