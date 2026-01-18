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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Desktop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private async Task FadeOutWindowAsync(Window window, int durationMs = 300)
        {
            var fadeOutAnimation = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(durationMs));
            window.BeginAnimation(Window.OpacityProperty, fadeOutAnimation);
            await Task.Delay(durationMs);
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            await FadeOutWindowAsync(this);
            Window2 f2 = new Window2();
            f2.Opacity = 0;
            f2.Show();

            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));
            f2.BeginAnimation(Window.OpacityProperty, fadeInAnimation);

            this.Hide();
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            await FadeOutWindowAsync(this);
            Window1 f1 = new Window1();
            f1.Opacity = 0;
            f1.Show();

            var fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(300));
            f1.BeginAnimation(Window.OpacityProperty, fadeInAnimation);

            this.Hide();
        }
    }
}
