
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ImageSlicer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<BitmapSource> Result;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Png file *.png|*.png";
            if (fileDialog.ShowDialog().Value)
            {
                Slicer slicer = new Slicer();
                slicer.OnException += ShowExceptionMessage;
                var image = new BitmapImage(new Uri(fileDialog.FileName));
                Result = slicer.Slice(image, int.Parse(ResultImagesScaleText.Text),int.Parse(MarginXTextBox.Text),int.Parse(MarginYTextBox.Text));
                MessageTextBlock.Text = $"Opened file:{fileDialog.FileName}";
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFolderDialog folderDialog = new OpenFolderDialog();
                folderDialog.DefaultDirectory = @"C:\Documents";
                folderDialog.ShowDialog();
                string pathToSave = folderDialog.FolderName;

                BitmapEncoder encoder = new PngBitmapEncoder();
                int i = 0;
                foreach (BitmapSource item in Result)
                {
                    encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(item));
                    using (FileStream stream = new FileStream($"{pathToSave}/{i}.png", FileMode.Create))
                    {
                        encoder.Save(stream);
                    }
                    i++;
                }
                MessageTextBlock.Text = $"Files saved to:{folderDialog.FolderName}";
            }
            catch (Exception ex)
            {

                ShowExceptionMessage(ex);
            }

        }
        private void ShowExceptionMessage(Exception ex)
        {
            MessageBox.Show($"{ex.Message}\n{ex.StackTrace}", "Oh, there is an error!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}