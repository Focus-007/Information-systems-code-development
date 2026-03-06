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
        // Ссылка на Window4, чтобы мы могли добавлять задачи
        private Window4 _mainWindow;

        public Window3(Window4 mainWindow) // Передаем ссылку на Window4
        {
            InitializeComponent();
            _mainWindow = mainWindow; // Сохраняем ссылку
            InitializeCategories();
        }

        private void InitializeCategories()
        {
            // Заполняем ComboBox comboKat
            comboKat.ItemsSource = new List<string> { "Дом", "Работа", "Учеба", "Отдых" };
            comboKat.SelectedIndex = 0; // Выбираем первый элемент по умолчанию
            datePicker.SelectedDate = DateTime.Today; // Выбираем сегодняшнюю дату по умолчанию
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Кнопка "Создать"
        {
            string taskName = text.Text;
            string taskDescription = text2.Text;
            string selectedCategory = comboKat.SelectedItem?.ToString(); // Получаем выбранную категорию
            DateTime? selectedDate = datePicker.SelectedDate; // Получаем выбранную дату

            if (string.IsNullOrWhiteSpace(taskName))
            {
                MessageBox.Show("Введите название задачи!", "Ошибка");
                return;
            }
            if (string.IsNullOrWhiteSpace(taskDescription))
            {
                MessageBox.Show("Введите описание задачи!", "Ошибка");
                return;
            }
            if (selectedCategory == null)
            {
                MessageBox.Show("Выберите категорию!", "Ошибка");
                return;
            }
            if (selectedDate == null)
            {
                MessageBox.Show("Выберите дату!", "Ошибка");
                return;
            }

            // Передаем все данные в метод AddCheckBox Window4
            _mainWindow.AddCheckBox(taskName, taskDescription, selectedCategory, selectedDate.Value);

            text.Text = "";
            text2.Text = "";
            comboKat.SelectedIndex = 0;
            datePicker.SelectedDate = DateTime.Today; // Сбрасываем на сегодня или null

            // Если Window3 должно закрываться после создания
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) // Кнопка "Отмена"
        {
            this.Close();
        }
    }
}