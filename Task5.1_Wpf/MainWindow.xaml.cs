using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task5._1_Wpf;

namespace Example1_WpfApp1
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string fontName = ((sender as ComboBox).SelectedItem as TextBlock).Text;

            if (textBox != null)
            {
                textBox.FontFamily = new FontFamily(fontName);
            }
        }

        private void ComboBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            string fontSize = ((sender as ComboBox).SelectedItem as TextBlock).Text;

            if (int.TryParse(fontSize, out int newValue))
            {
                if (textBox != null)
                {
                    textBox.FontSize = newValue;
                }
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog() { Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*" };
            //openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                textBox.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*" };
            //saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, textBox.Text);
            }
        }

        private void Save_as_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "PDF file (*.pdf)|*.pdf|Все файлы (*.*)|*.*" };
            //saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf|Все файлы (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, textBox.Text);
            }
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            Print print = new Print() { WindowStyle = WindowStyle.ToolWindow };
            print.WindowStyle = WindowStyle.ToolWindow;
            print.Show();
            //PrintDialog printDialog = new PrintDialog();
            //if (printDialog.ShowDialog() == true)
            //{
            //    printDialog.PrintVisual(textBox, "Распечатываем элемент Canvas");
            //}
        }

        private void CloseIt_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Вы хотите сохранить изменения?";
            string caption = "Текстовый редактор";

            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*" };

                        if (saveFileDialog.ShowDialog() == true)
                        {
                            File.WriteAllText(saveFileDialog.FileName, textBox.Text);
                        }
                        break;
                    }
                case MessageBoxResult.No:
                    {
                        Application.Current.Shutdown();
                        break;
                    }
                case MessageBoxResult.Cancel:
                    {
                        break;
                    }
            }
        }

        private void BtnBold_Click(object sender, RoutedEventArgs e)
        {
            //int weightValue = 999;
            //textBox.FontWeight = FontWeight.FromOpenTypeWeight(weightValue);
            if (textBox.FontWeight == FontWeights.Normal)
            {
                textBox.FontWeight = FontWeights.Bold;
            }
            else
            {
                textBox.FontWeight = FontWeights.Normal;
            }
        }

        private void BtnItalic_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.FontStyle == FontStyles.Normal)
            {
                textBox.FontStyle = FontStyles.Italic;
            }
            else
            {
                textBox.FontStyle = FontStyles.Normal;
            }
        }

        private void BtnUnderline_Click(object sender, RoutedEventArgs e)
        {
            if (textBox.TextDecorations == TextDecorations.Underline)
            {
                textBox.TextDecorations = null;
            }
            else
            {
                textBox.TextDecorations = TextDecorations.Underline;
            }
        }

        private void Rb1_Checked(object sender, RoutedEventArgs e)
        {
            if (Rb1.IsChecked == true)
            {
                if (textBox != null)
                {
                    textBox.Foreground = Brushes.Black;
                }
            }
            else if (Rb2.IsChecked == true)
            {
                if (textBox != null)
                {
                    textBox.Foreground = Brushes.Red;
                }
            }
        }
    }
}
