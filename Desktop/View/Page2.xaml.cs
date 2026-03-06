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
using System.Windows.Media.Animation; // Добавлено из вашего скриншота

namespace Desktop
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml (Page2)
    /// </summary>
    public partial class Window2 : Window
    {
        public Window2()
        {
            InitializeComponent();
        }

        // Асинхронный метод для плавного скрытия окна
        private async Task FadeOutAsync(Window window, int durationMs = 300)
        {
            var fadeOutAnimation = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(durationMs));
            window.BeginAnimation(Window.OpacityProperty, fadeOutAnimation);
            await Task.Delay(durationMs);
        }

        // Обработчик кнопки, которая переходит от Window2 к Window4
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            // Плавно скрываем текущее окно (Window2)
            await FadeOutAsync(this);

            // Создаем и показываем основное окно приложения (Window4)
            Window4 mainWindow = new Window4();
            mainWindow.Show(); // Показываем Window4

            // Закрываем Window2, так как оно нам больше не нужно
            this.Close();

            // Если Window4 также имеет анимацию появления, её нужно делать в самом Window4,
            // или передать ссылку на Window4 в метод FadeIn, если он универсален.
            // Например:
            // await FadeInAsync(mainWindow); // Если у вас есть такой метод
        }
    }
}
