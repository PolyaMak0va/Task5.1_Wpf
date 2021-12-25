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

namespace Task5._2_Wpf
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

        //private void BtnInk_Click(object sender, RoutedEventArgs e)
        //{
        //    inkCanvas.EditingMode = InkCanvasEditingMode.Ink;

        //    // Set the DefaultDrawingAttributes for a red pen.
        //    inkCanvas.DefaultDrawingAttributes.Color = Colors.Red;
        //    inkCanvas.DefaultDrawingAttributes.IsHighlighter = false;
        //    inkCanvas.DefaultDrawingAttributes.Height = 2;
        //}
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                image.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            this.inkCanvas.EditingMode = InkCanvasEditingMode.None;
            SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png" };
            saveFileDialog.Title = "Безымянный";
            FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.Create);

            RenderTargetBitmap bitmap = new RenderTargetBitmap((int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight,96,96,PixelFormats.Default);
            bitmap.Render(inkCanvas);
            inkCanvas.Strokes.Save(fs);
        }

        //private void close_Click(object sender, RoutedEventArgs e)
        //{
        //    string messageBoxText = "Вы хотите сохранить изменения?";
        //    string caption = "Графический редактор";

        //    MessageBoxButton button = MessageBoxButton.YesNoCancel;
        //    MessageBoxImage icon = MessageBoxImage.Warning;
        //    MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

        //    switch (result)
        //    {
        //        case MessageBoxResult.Yes:
        //            {
        //                SaveFileDialog saveFileDialog = new SaveFileDialog();
        //                saveFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";

        //                if (saveFileDialog.ShowDialog() == true)
        //                {
        //                    File.WriteAllText(saveFileDialog.FileName, canvas.ima);
        //                }
        //                break;
        //            }
        //        case MessageBoxResult.No:
        //            {
        //                Application.Current.Shutdown();
        //                break;
        //            }
        //        case MessageBoxResult.Cancel:
        //            {
        //                break;
        //            }
        //    }
        //}

        private void BtnEraseStroke_Checked(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
        }


        private void BtnInk_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;

            // Set the DefaultDrawingAttributes for a red pen.
            inkCanvas.DefaultDrawingAttributes.Color = Colors.Black;
            inkCanvas.DefaultDrawingAttributes.IsHighlighter = false;
            inkCanvas.DefaultDrawingAttributes.Height = 2;
        }

        private void Highlight_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;

            inkCanvas.DefaultDrawingAttributes.Color = Colors.Yellow;
            inkCanvas.DefaultDrawingAttributes.IsHighlighter = true;
            inkCanvas.DefaultDrawingAttributes.Height = 25;
        }

        private void BtnEraseByPoint_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void BtnSelect_Click(object sender, RoutedEventArgs e)
        {
            this.inkCanvas.EditingMode = InkCanvasEditingMode.Select;
        }



        //private void save_as_Click(object sender, RoutedEventArgs e)
        //{
        //    SaveFileDialog saveFileDialog = new SaveFileDialog();
        //    saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf|Все файлы (*.*)|*.*";

        //    if (saveFileDialog.ShowDialog() == true)
        //    {
        //        File.WriteAllText(saveFileDialog.FileName, textBox.Text);
        //    }
        //}

        private void print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(inkCanvas, "Распечатываем элемент Canvas");
            }
        }

        private void MainSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            inkCanvas.DefaultDrawingAttributes.Width = ((Slider)sender).Value; // одно из двух или MainInk=null или MainInk.DefaultDrawingAttributes=null
            inkCanvas.DefaultDrawingAttributes.Height = ((Slider)sender).Value;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StackPanel mainStackPanel = new StackPanel();
            TextBox pasteTextBox = new TextBox();
            Menu stackPanelMenu = new Menu();
            MenuItem pasteMenuItem = new MenuItem();

            // Adding objects to the panel and the menu
            stackPanelMenu.Items.Add(pasteMenuItem);
            mainStackPanel.Children.Add(stackPanelMenu);
            mainStackPanel.Children.Add(pasteTextBox);

            // Setting the command to the Paste command
            pasteMenuItem.Command = ApplicationCommands.Paste;

            // Setting the command target to the TextBox
            pasteMenuItem.CommandTarget = pasteTextBox;
        }

        private void _Cut_Click(object sender, RoutedEventArgs e)
        {
            Button cutButton = new Button();
            cutButton.Command = ApplicationCommands.Cut;
        }

    }
}
