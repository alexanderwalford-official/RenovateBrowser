using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace RenovateBrowser
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var win = new Browser();
            win.Show();
            StartCloseTimer();

            SoundPlayer player = new SoundPlayer(Properties.Resources.notif);
            player.Play();
            if (System.Diagnostics.Debugger.IsAttached)
            {
                vertext.Content = "Developer Version";
            }
            else
            {
                vertext.Content = "Version " + ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();
            }       
        }

        private void StartCloseTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1d);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            DispatcherTimer timer = (DispatcherTimer)sender;
            timer.Stop();
            timer.Tick -= TimerTick;
            Close();
        }
    }
}
