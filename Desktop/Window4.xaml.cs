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
    /// <summary>
    /// Логика взаимодействия для Window4.xaml
    /// </summary>
    public partial class Window4 : Window
    {

        private List<CheckBox> activeTasks = new List<CheckBox>();
        private List<CheckBox> historyTasks = new List<CheckBox>();
        private bool isHistoryMode = false;
        public Window4()
        {
            InitializeComponent();
        }


        public void AddCheckBox(string name, string comment)
        {
            var cb = new CheckBox { Content = name, Tag = comment };

            cb.IsThreeState = false;

            cb.Checked += (s, e) => ShowInfo(cb);
            cb.Unchecked += (s, e) => ShowInfo(cb);

            activeTasks.Add(cb);

            RefreshTaskList();
        }

        private void RefreshTaskList()
        {
            list.Items.Clear();
            if (isHistoryMode)
            {
                foreach (var cb in historyTasks)
                {
                    list.Items.Add(cb);
                }
            }
            else
            {
                foreach (var cb in activeTasks)
                {
                    list.Items.Add(cb);
                }
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
  
        }
        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (list.SelectedItem is CheckBox cb) ShowInfo(cb);
        }

        private void ShowInfo(CheckBox cb)
        {
            bloc1.Text = cb.Content.ToString();
            
            if (cb.Content is TextBlock textBlock)
            {

                var run = textBlock.Inlines.FirstOrDefault() as Run;
                if (run != null)
                    bloc2.Text = cb.Tag?.ToString() ?? "";
            }
            else
            {
                bloc2.Text = cb.Tag?.ToString() ?? "";
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window3 f3 = new Window3();
            f3.Show();

            Hide();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var selectedCheckBoxes = activeTasks
                .Where(cb => cb.IsChecked == true)
                .ToList();

            if (selectedCheckBoxes.Count == 0)
            {
                MessageBox.Show("Выберите хотя бы одну задачу для завершения",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var cb in selectedCheckBoxes)
            {
                var textBlock = new TextBlock();
                textBlock.Inlines.Add(new Run(cb.Content.ToString())
                {
                    TextDecorations = TextDecorations.Strikethrough,
                    Foreground = Brushes.Gray
                });

                cb.Tag = cb.Tag ?? "";
                cb.Content = textBlock;
                cb.IsEnabled = false; 

                historyTasks.Add(cb);
                activeTasks.Remove(cb);
            }

            if (!isHistoryMode)
            {
                RefreshTaskList();
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var sourceCollection = isHistoryMode ? historyTasks : activeTasks;
            var checkboxesToDelete = sourceCollection
                .Where(cb => cb.IsChecked == true)
                .ToList();

            if (checkboxesToDelete.Count == 0)
            {
                MessageBox.Show("Поставьте галочки на чекбоксах, которые хотите удалить",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            foreach (var cb in checkboxesToDelete)
            {
                sourceCollection.Remove(cb);
            }
            RefreshTaskList();

            bloc1.Text = "";
            bloc2.Text = "";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            isHistoryMode = true;
            RefreshTaskList();

            if (sender is Button button)
            {
                button.Content = "Задачи";
                button.Click -= Button_Click_3;
                button.Click += Button_Click_4; 
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

            isHistoryMode = false;
            RefreshTaskList();

            if (sender is Button button)
            {
                button.Content = "История";
                button.Click -= Button_Click_4;
                button.Click += Button_Click_3; 
            }
        }
        private void ShowTasks_Click(object sender, RoutedEventArgs e)
        {
            isHistoryMode = false;
            RefreshTaskList();
        }

        private void ShowHistory_Click(object sender, RoutedEventArgs e)
        {
            isHistoryMode = true;
            RefreshTaskList();
        }
    }
}
