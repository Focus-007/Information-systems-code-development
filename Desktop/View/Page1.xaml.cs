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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void registr()
        {
            string name = TextBox.Text;
            string email = TextBox1.Text;
            var password = TextBox2.Text;
            var password2 = TextBox3.Text;

            // Валидация полей ввода
            // Имя
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Поле <<Имя>> должно быть заполненно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (name.Length < 3)
            {
                MessageBox.Show("Имя должно содержать не менее 3 символов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //Почта
            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Неверный формат почты! Почта должна быть в формате *@*.*", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            //Пароль
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните поля <<Пароль>>", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (password != password2)
            {
                MessageBox.Show("Пароли не совпадают", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Вы успешно авторизовались", "Успех", MessageBoxButton.OK);


            peres(name, email, password);

            Window2 f2 = new Window2();
            f2.Show();
            Hide();
        }
        public void peres(string nam, string ema, string pas)
        {
            string ime = nam;
            string poc = ema;
            string pss = pas;

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            registr();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
