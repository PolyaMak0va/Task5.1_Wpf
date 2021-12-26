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
using System.Windows.Ink;
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
        public SolidColorBrush SelectedColor { get; set; } = Brushes.Black;
        public enum SelectedTools { LargeBrush = 0, Brush = 1, Pencil = 2 }
        public int SelectedTool = 2;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                image.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
            }
            //if (openFileDialog.ShowDialog() == true)
            //{
            //    string fileName = openFileDialog.FileName;
            //    BitmapImage bitmap = new BitmapImage();
            //    bitmap.BeginInit();
            //    bitmap.UriSource = new Uri(fileName);
            //    bitmap.EndInit();
            //    image.Source = bitmap;
            //}
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png" };

            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;

                int margin = (int)this.inkCanvas.Margin.Left;
                inkCanvas.Margin = new Thickness(0, 0, 0, 0);
                int width = (int)this.inkCanvas.ActualWidth - margin;
                int height = (int)inkCanvas.ActualHeight - margin;

                RenderTargetBitmap bitmap =
                    new RenderTargetBitmap(width, height, 0d, 0d, PixelFormats.Default);
                inkCanvas.Measure(new Size((int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight));
                inkCanvas.Arrange(new Rect(new Size((int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight)));
                bitmap.Render(inkCanvas);

                using (FileStream fs = new FileStream(fileName, FileMode.Create))
                {
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmap));
                    encoder.Save(fs);
                }
            }
        }
        private void Сlose_Click(object sender, RoutedEventArgs e)
        {
            string messageBoxText = "Вы хотите сохранить изменения?";
            string caption = "Графический редактор";

            MessageBoxButton button = MessageBoxButton.YesNoCancel;
            MessageBoxImage icon = MessageBoxImage.Warning;
            MessageBoxResult result = MessageBox.Show(messageBoxText, caption, button, icon);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    {
                        SaveFileDialog saveFileDialog = new SaveFileDialog() { Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png" };

                        if (saveFileDialog.ShowDialog() == true)
                        {
                            string fileName = saveFileDialog.FileName;

                            int margin = (int)this.inkCanvas.Margin.Left;
                            inkCanvas.Margin = new Thickness(0, 0, 0, 0);
                            int width = (int)this.inkCanvas.ActualWidth - margin;
                            int height = (int)inkCanvas.ActualHeight - margin;

                            RenderTargetBitmap bitmap =
                                new RenderTargetBitmap(width, height, 0d, 0d, PixelFormats.Default);
                            inkCanvas.Measure(new Size((int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight));
                            inkCanvas.Arrange(new Rect(new Size((int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight)));
                            bitmap.Render(inkCanvas);

                            using (FileStream fs = new FileStream(fileName, FileMode.Create))
                            {
                                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                                encoder.Frames.Add(BitmapFrame.Create(bitmap));
                                encoder.Save(fs);
                            }
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
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(inkCanvas, "Распечатываем элемент Canvas");
            }
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
        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            StrokeCollection selection = inkCanvas.GetSelectedStrokes();

            if (selection.Count > 0)
            {
                foreach (Stroke stroke in selection)
                {
                    this.inkCanvas.CutSelection();
                }
            }
        }
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            StrokeCollection selection = inkCanvas.GetSelectedStrokes();

            if (selection.Count > 0)
            {
                foreach (Stroke stroke in selection)
                {
                    this.inkCanvas.CopySelection();
                }
            }
        }
        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            if (this.inkCanvas.CanPaste())
            {
                this.inkCanvas.Paste();
            }
        }
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            this.inkCanvas.Strokes.Clear();
        }
        public void BtnColorSelected(object sender, RoutedEventArgs e)
        {
            Button btnSelected = (Button)sender;
            SelectedColor = (SolidColorBrush)btnSelected.Background;
            RctColorSelected.Fill = SelectedColor;
            strokeAttr.Color = SelectedColor.Color;
        }

        private void Pencil_Click(object sender, RoutedEventArgs e)
        {
            Button btnSelected = (Button)sender;
            SelectedTool = (int)SelectedTools.Pencil;
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;

            inkCanvas.DefaultDrawingAttributes.Color = Colors.Black;
            inkCanvas.DefaultDrawingAttributes.IsHighlighter = false;
            strokeAttr.Width = 3;
            strokeAttr.Height = 3;
        }

        private void Brush_Click(object sender, RoutedEventArgs e)
        {
            Button btnSelected = (Button)sender;
            SelectedTool = (int)SelectedTools.Brush;
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;

            inkCanvas.DefaultDrawingAttributes.IsHighlighter = false;
            strokeAttr.Width = 10;
            strokeAttr.Height = 10;
        }

        private void LargeBrush_Click(object sender, RoutedEventArgs e)
        {
            Button btnSelected = (Button)sender;
            SelectedTool = (int)SelectedTools.LargeBrush;
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;

            inkCanvas.DefaultDrawingAttributes.IsHighlighter = false;
            strokeAttr.Width = 20;
            strokeAttr.Height = 20;
        }
    }
}
