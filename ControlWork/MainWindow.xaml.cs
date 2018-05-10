using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using Google.Apis.Drive;

namespace ControlWork
{
    public partial class MainWindow : Window
    {
        private Timer updateIconTimer;

        public MainWindow()
        {
            InitializeComponent();

            updateIconTimer = new Timer(new TimerCallback(UpdateIcon), null, TimerInterval.Interval, TimerInterval.Interval);
        }

        public void UpdateIcon(object paths)
        {
            string[] IcoPaths = Directory.GetFiles(@"C:\Users\МайзельсК\Documents\visual studio 2017\Projects\ControlWork\ICOS");

            DateTime newIconAccessDate = new DateTime(1, 1, 1);
            string iconToLoad = null;

            foreach (string path in IcoPaths)
            {
                FileInfo imageInfo = new FileInfo(path);

                if (imageInfo.LastAccessTime > newIconAccessDate)
                {
                    iconToLoad = path;
                    newIconAccessDate = imageInfo.LastAccessTime;
                }
            }

            Dispatcher.Invoke(() =>
            {
                this.updateIconTimer.Dispose();
                Process.GetCurrentProcess().CloseMainWindow();

                MainWindow main = new MainWindow();
                main.Show();
                main.Icon = BitmapFrame.Create(new Uri(iconToLoad));
            });
        }

        private void UpdateBGClick(object sender, RoutedEventArgs e)
        {
            UpdateBackground();
        }
        public void UpdateBackground()
        {
            string[] BgPaths = Directory.GetFiles(@"C:\Users\МайзельсК\Documents\visual studio 2017\Projects\ControlWork\BG");

            DateTime newBGAccessDate = new DateTime(1, 1, 1);
            string BGToLoad = null;

            foreach (string path in BgPaths)
            {
                FileInfo imageInfo = new FileInfo(path);

                if (imageInfo.LastAccessTime > newBGAccessDate)
                {
                    BGToLoad = path;
                    newBGAccessDate = imageInfo.LastAccessTime;
                }
            }

            ImageBrush myBrush = new ImageBrush()
            {
                ImageSource = new BitmapImage(new Uri(BGToLoad))
            };

            this.Background = myBrush;
        }

        private void UpdateIntervalClick(object sender, RoutedEventArgs e)
        {
            int newInterval;

            if (int.TryParse(IntervalText.Text, out newInterval))
            {
                updateIconTimer.Change(newInterval * 1000, newInterval * 1000);
                TimerInterval.Interval = newInterval * 1000;
            }
            else
            {
                MessageBox.Show("Неккоректный ввод!");
            }
        }
    }
}
