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
using System.Windows.Shapes;

namespace Desktop
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {


            if (string.IsNullOrWhiteSpace(text.Text))
            {
                MessageBox.Show("Введите название!", "Ошибка");
                return;
            }

            Window4 window4 = null;
            foreach (Window w in Application.Current.Windows)
                if (w is Window4) { window4 = w as Window4; break; }

            if (window4 == null)
            {
                window4 = new Window4();
            }

            window4.Show();

            window4.AddCheckBox(text.Text, text2.Text);

            text.Text = text2.Text = "";
            text.Focus();

            window4.Show();
            this.Close();
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {

            Close();
        }
    }
    
}
