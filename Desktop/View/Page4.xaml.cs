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
using static System.Net.Mime.MediaTypeNames;

namespace Desktop
{
    public partial class Window4 : Window
    {
        private List<CheckBox> activeTasks = new List<CheckBox>();
        private List<CheckBox> historyTasks = new List<CheckBox>();
        private bool isHistoryMode = false;
        private string currentFilterCategory = null;

        public Window4()
        {
            InitializeComponent();
            RefreshTaskList();
        }

        public void AddCheckBox(string name, string comment, string category, DateTime date)
        {
            CheckBox cb = new CheckBox();
            // Сохраняем имя отдельно в Tag, чтобы не терять его при перечеркивании
            cb.Tag = new { Name = name, Comment = comment, Category = category, Date = date };
            cb.Content = name;

            cb.Checked += (s, e) => ShowInfo(cb);
            cb.Unchecked += (s, e) => ShowInfo(cb);

            activeTasks.Add(cb);
            RefreshTaskList();
        }

        private void RefreshTaskList()
        {
            list.Items.Clear();
            List<CheckBox> sourceList = isHistoryMode ? historyTasks : activeTasks;

            foreach (var cb in sourceList)
            {
                var tagData = cb.Tag as dynamic;
                if (currentFilterCategory == null || tagData.Category == currentFilterCategory)
                {
                    // Настройка внешнего вида CheckBox в списке
                    if (isHistoryMode)
                    {
                        cb.Foreground = Brushes.Gray;
                        // Создаем текстовый блок с перечеркиванием для содержимого CheckBox
                        cb.Content = new TextBlock
                        {
                            Text = tagData.Name,
                            TextDecorations = TextDecorations.Strikethrough
                        };
                    }
                    else
                    {
                        cb.Foreground = Brushes.Black;
                        cb.Content = tagData.Name;
                    }

                    list.Items.Add(cb);
                }
            }
            UpdateDisplayDefaults();
        }

        private void UpdateDisplayDefaults()
        {
            labZag.Text = isHistoryMode ? "История" : "Задачи"; // labZag теперь TextBlock
            if (currentFilterCategory != null) labZag.Text = currentFilterCategory;

            // Сброс стилей заголовка по умолчанию
            labZag.Foreground = Brushes.Black;
            labZag.TextDecorations = null;

            bloc1.Text = "";
            bloc1.Foreground = Brushes.Black;
            bloc1.TextDecorations = null;

            bloc2.Text = "";
            bloc2.Foreground = Brushes.Black;
            bloc2.TextDecorations = null;

            btnGotovo.IsEnabled = false;
            btnUdlTask.IsEnabled = false;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list.SelectedItem is CheckBox cb)
            {
                ShowInfo(cb);
                btnUdlTask.IsEnabled = true;
                btnGotovo.IsEnabled = true;
            }
            else
            {
                UpdateDisplayDefaults();
            }
        }

        private void ShowInfo(CheckBox cb)
        {
            var tagData = cb.Tag as dynamic;
            if (tagData != null)
            {
                labZag.Text = tagData.Name;
                bloc1.Text = tagData.Date.ToShortDateString();
                bloc2.Text = tagData.Comment;

                // Если мы в истории — делаем текст в правом окне серым и перечеркнутым
                if (isHistoryMode)
                {
                    labZag.Foreground = Brushes.Gray;
                    labZag.TextDecorations = TextDecorations.Strikethrough;

                    bloc1.Foreground = Brushes.Gray;
                    bloc1.TextDecorations = TextDecorations.Strikethrough;

                    bloc2.Foreground = Brushes.Gray;
                    bloc2.TextDecorations = TextDecorations.Strikethrough;
                }
                else
                {
                    labZag.Foreground = Brushes.Black;
                    labZag.TextDecorations = null;

                    bloc1.Foreground = Brushes.Black;
                    bloc1.TextDecorations = null;

                    bloc2.Foreground = Brushes.Black;
                    bloc2.TextDecorations = null;
                }
            }
        }

        private void Button_Click_Gotovo(object sender, RoutedEventArgs e)
        {
            if (list.SelectedItem is CheckBox cb)
            {
                if (!isHistoryMode)
                {
                    cb.IsChecked = true;
                    activeTasks.Remove(cb);
                    historyTasks.Add(cb);
                }
                RefreshTaskList();
            }
        }

        private void Button_Click_DeleteSelectedTask(object sender, RoutedEventArgs e)
        {
            if (list.SelectedItem is CheckBox cb)
            {
                if (isHistoryMode) historyTasks.Remove(cb);
                else activeTasks.Remove(cb);
                RefreshTaskList();
            }
        }

        // Переключение режимов с скрытием/показом кнопок
        private void Button_Click_Tasks(object sender, RoutedEventArgs e)
        {
            isHistoryMode = false;
            currentFilterCategory = null;

            // Показываем кнопки управления и создания
            btnGotovo.Visibility = Visibility.Visible;
            btnUdlTask.Visibility = Visibility.Visible;

            RefreshTaskList();
        }

        private void Button_Click_History(object sender, RoutedEventArgs e)
        {
            isHistoryMode = true;
            currentFilterCategory = null;

            // Скрываем кнопки управления (они не нужны в истории по вашему запросу)
            btnGotovo.Visibility = Visibility.Collapsed;
            btnUdlTask.Visibility = Visibility.Collapsed;
            // btnAdd.Visibility = Visibility.Collapsed;

            RefreshTaskList();
        }

        private void Button_Click_Category(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn)
            {
                currentFilterCategory = (currentFilterCategory == btn.Content.ToString()) ? null : btn.Content.ToString();
                RefreshTaskList();
            }
        }

        private void Button_Click_CreateTask(object sender, RoutedEventArgs e)
        {
            Window3 f3 = new Window3(this);
            f3.Show();
        }
    }
}
