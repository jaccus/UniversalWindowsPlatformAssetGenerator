﻿namespace UWPAssetGenerator.App
{
    using System;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    public partial class Window1
    {
        private const string Notice1 = "Trim area to be Icon by Mouse";
        private const string Notice2 = "Click [Save Icons] button if satisfied";
        private const string Notice3 = "Created folder and Save Completed";
        private const string OpenFilter = "Image Files (*.png, *.jpg)|*.png;*.jpg|All files (*.*)|*.*";
        private const string OpenFolder = "Icons with same project name are already exist, please change your project name";
        private const string Notice4 = "Please load .PNG or .JPG";
        private const string Notice5 = "Fail to make icons, Trim area to be Icon by Mouse";

        private readonly Rectangle rectFrame = new Rectangle();

        private bool isPasted;
        private string fileName;
        private BitmapImage imageSource;
        private double scale = 1.0;
        private Point p1;
        private Point p2;

        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog { Filter = OpenFilter };

            bool? result = dlg.ShowDialog();
            if (result.Value)
            {
                ShowImage(dlg.FileName);
            }

            isPasted = false;
        }

        /// <summary>
        /// Handle Copy & Past (ctrl+V)
        /// </summary>
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyboardDevice.IsKeyDown(Key.LeftCtrl) || e.KeyboardDevice.IsKeyDown(Key.RightCtrl)) && (e.Key == Key.V))
            {
                if (Clipboard.ContainsImage())
                {
                    ShowImage(Clipboard.GetImage());
                }
            }
        }

        /// <summary>
        /// Show Image for Copy & Paste
        /// </summary>
        private void ShowImage(BitmapSource source)
        {
            projectName.Text = "MyProject";
            fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\MyProject";
            var encoder = new BmpBitmapEncoder();
            var memoryStream = new MemoryStream();
            imageSource = new BitmapImage();
            encoder.Frames.Add(BitmapFrame.Create(source));
            encoder.Save(memoryStream);
            imageSource.BeginInit();
            imageSource.StreamSource = new MemoryStream(memoryStream.ToArray());
            imageSource.EndInit();
            memoryStream.Close();

            grayImage.Source = imageSource;
            if (imageSource.PixelHeight < 600 && imageSource.PixelWidth < 800)
            {
                grayImage.Width = imageSource.PixelWidth;
                grayImage.Height = imageSource.PixelHeight;
            }
            else
            {
                grayImage.Width = 800;
                grayImage.Height = 600;
            }

            NewImageDisplayed();
        }

        /// <summary>
        /// Show Image for file load and drag
        /// </summary>
        private void ShowImage(string filename)
        {
            fileName = filename;
            imageSource = new BitmapImage();
            imageSource.BeginInit();
            imageSource.UriSource = new Uri(fileName);
            imageSource.EndInit();

            grayImage.Source = imageSource;
            if (imageSource.PixelHeight < 600 && imageSource.PixelWidth < 800)
            {
                grayImage.Width = imageSource.PixelWidth;
                grayImage.Height = imageSource.PixelHeight;
            }
            else
            {
                grayImage.Width = 800;
                grayImage.Height = 600;
            }

            NewImageDisplayed();
        }

        private void DisplayIconNames()
        {
            var name = projectName.Text;
            name200.Text = name + "_200.png";
            name200.Visibility = Visibility.Visible;
            name173.Text = name + "_173.png";
            name173.Visibility = Visibility.Visible;
            name99.Text = name + "_99.png";
            name99.Visibility = Visibility.Visible;
            name62.Text = name + "_62.png";
            name62.Visibility = Visibility.Visible;
        }

        private void ProjectNameTextChanged(object sender, TextChangedEventArgs e)
        {
            DisplayIconNames();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Icon200.Fill != null)
            {
                var path = System.IO.Path.GetDirectoryName(fileName);
                path = path + "\\" + projectName.Text + " Icons";
                if (Directory.Exists(path))
                {
                    MessageBox.Show(OpenFolder);
                    return;
                }

                Directory.CreateDirectory(path);
                EncodeAndSave(Icon200, name200.Text, path);
                EncodeAndSave(Icon173, name173.Text, path);
                EncodeAndSave(Icon173, "Background.png", path);
                EncodeAndSave(Icon99, name99.Text, path);
                EncodeAndSave(Icon62, name62.Text, path);
                EncodeAndSave(Icon62, "ApplicationIcon.png", path);
                var folder = "On same folder with your image";
                if (isPasted)
                {
                    folder = "On your desktop";
                }

                CompleteNotice.Content = folder + projectName.Text + " Icons" + Notice3;
                CompleteNotice.Visibility = Visibility.Visible;

                var effect = (Storyboard)FindResource("Storyboard1");
                BeginStoryboard(effect);
            }
            else
            {
                CompleteNotice.Content = Notice5;
            }
        }

        private static void EncodeAndSave(FrameworkElement icon, string name, string filePath)
        {
            var rtb = new RenderTargetBitmap((int)icon.Width, (int)icon.Height, 96.0, 96.0, PixelFormats.Pbgra32);
            var dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                var vb = new VisualBrush(icon);
                dc.DrawRectangle(vb, null, new Rect(new Point(), new Size((int)icon.Width, (int)icon.Height)));
            }

            rtb.Render(dv);
            var bmf = BitmapFrame.Create(rtb);
            bmf.Freeze();
            var fileOut = filePath + "\\" + name;
            using (var stream = new FileStream(fileOut, FileMode.Create))
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(bmf);
                encoder.Save(stream);
            }
        }

        private void MyImageOnDrop(object sender, DragEventArgs e)
        {
            e.Handled = true;
            var draggedFileName = IsSingleFile(e);

            if (draggedFileName.Contains(".png") || draggedFileName.Contains(".PNG") || draggedFileName.Contains(".jpg") || draggedFileName.Contains(".JPG"))
            {
                ShowImage(draggedFileName);
                isPasted = false;
            }
            else
            {
                MessageBox.Show(Notice4, "Please drag png/jpg file", MessageBoxButton.OK);
            }
        }

        private void BackgroundSizeChanged(object sender, SizeChangedEventArgs e)
        {
            NewImageDisplayed();
        }

        private void NewImageDisplayed()
        {
            scale = imageSource.Width / grayImage.ActualWidth;
            projectName.Text = System.IO.Path.GetFileNameWithoutExtension(fileName);
            name200.Visibility = Visibility.Hidden;
            name173.Visibility = Visibility.Hidden;
            name62.Visibility = Visibility.Hidden;
            name99.Visibility = Visibility.Hidden;
            CompleteNotice.Content = Notice1;
            Icon200.Fill = null;
            Icon173.Fill = null;
            Icon99.Fill = null;
            Icon62.Fill = null;
            myCanvas.Children.Remove(rectFrame);
        }

        private static string IsSingleFile(DragEventArgs args)
        {
            if (!args.Data.GetDataPresent(DataFormats.FileDrop, true))
            {
                return null;
            }

            var fileNames = args.Data.GetData(DataFormats.FileDrop, true) as string[];
            if (fileNames.Length == 1)
            {
                if (File.Exists(fileNames[0]))
                {
                    return fileNames[0];
                }
            }

            return null;
        }

        private void MyImageOnPreviewDragOver(object sender, DragEventArgs args)
        {
            args.Effects = IsSingleFile(args) != null ? DragDropEffects.Copy : DragDropEffects.None;
            args.Handled = true;
        }

        private void MyCanvasOnPreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CompleteNotice.Content = Notice2;
        }

        private void BackgroundMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            p1 = e.GetPosition(grayImage);
            CompleteNotice.Content = Notice1;
        }

        private void BackgroundMouseMove(object sender, MouseEventArgs e)
        {
            p2 = e.GetPosition(grayImage);

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            double w = Math.Abs(p1.X - p2.X);
            double h = Math.Abs(p1.Y - p2.Y);
            if (w > h)
            {
                p2.Y = (p2.Y > p1.Y) ? p1.Y + w : p1.Y - w;
            }
            else
            {
                p2.X = (p2.X > p1.X) ? p1.X + h : p1.X - w;
            }

            var lt = new Point((p1.X > p2.X) ? p2.X : p1.X, (p1.Y > p2.Y) ? p2.Y : p1.Y);
            var rb = new Point((p1.X > p2.X) ? p1.X : p2.X, (p1.Y > p2.Y) ? p1.Y : p2.Y);

            // rabber band
            myCanvas.Children.Remove(rectFrame);
            rectFrame.Stroke = Brushes.Gray;
            rectFrame.StrokeThickness = 1.0;
            rectFrame.Width = rb.X - lt.X;
            rectFrame.Height = rb.Y - lt.Y;
            rectFrame.RenderTransform = new TranslateTransform(lt.X, lt.Y);
            myCanvas.Children.Add(rectFrame);

            // Icon Images
            var sourceLt = new Point(lt.X * scale, lt.Y * scale);
            var sourceRb = new Point(rb.X * scale, rb.Y * scale);

            var brush = new ImageBrush { ImageSource = imageSource, Viewbox = new Rect(sourceLt, sourceRb), ViewboxUnits = BrushMappingMode.Absolute, Stretch = Stretch.Fill };

            Icon200.Fill = brush;
            Icon173.Fill = brush;
            Icon99.Fill = brush;
            Icon62.Fill = brush;
            DisplayIconNames();
        }
    }
}