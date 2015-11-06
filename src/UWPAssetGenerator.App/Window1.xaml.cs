﻿namespace UWPAssetGenerator.App
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Media.Imaging;
    using System.Windows.Shapes;

    using UWPAssetGenerator.App.Controls;
    using UWPAssetGenerator.Core.Engine;

    public partial class Window1
    {
        private const string TrimAreaToBeIconByMouseMessage = "Trim area to be Icon by Mouse";

        private readonly Rectangle rectFrame = new Rectangle();
        private readonly ImageEncodingEngine imageEncodingEngine = new ImageEncodingEngine();
        private readonly Dictionary<int, DynamicScalingImageThumbnail> thumbnails = new Dictionary<int, DynamicScalingImageThumbnail>();
        private readonly List<int> thumbnailSizes = new List<int> { 200, 173, 99, 62 };

        private bool isPasted;
        private string fileName;
        private BitmapImage imageSource;
        private double scale = 1.0;
        private Point p1;
        private Point p2;

        public Window1()
        {
            InitializeComponent();

            foreach (var thumbnailSize in thumbnailSizes)
            {
                var thumbnail = new DynamicScalingImageThumbnail { ThumbnailHeight = thumbnailSize, ThumbnailWidth = thumbnailSize, };
                thumbnails.Add(thumbnailSize, thumbnail);
                iconPanel.Children.Add(thumbnail);
            }
        }

        private bool HasUserTrimmedInputImage
        {
            get
            {
                return ((DynamicScalingImageThumbnail)iconPanel.Children[0]).Image.Fill != null;
            }
        }

        private string CompleteNoticeMessage
        {
            get
            {
                var folderMessage = isPasted ? "On your desktop" : "On same folder with your image";
                return string.Format("{0}{1} Icons{2}", folderMessage, projectName.Text, "Created folder and Save Completed");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new Microsoft.Win32.OpenFileDialog { Filter = "Image Files (*.png, *.jpg)|*.png;*.jpg|All files (*.*)|*.*" };

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
            using (var memoryStream = new MemoryStream())
            {
                imageSource = new BitmapImage();
                encoder.Frames.Add(BitmapFrame.Create(source));
                encoder.Save(memoryStream);
                imageSource.BeginInit();
                imageSource.StreamSource = new MemoryStream(memoryStream.ToArray());
                imageSource.EndInit();
            }

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

            foreach (var thumbnail in thumbnails)
            {
                thumbnail.Value.Title.Text = $"{name}_{thumbnail.Key}.png";
                thumbnail.Value.Title.Visibility = Visibility.Visible;
            }
        }

        private void ProjectNameTextChanged(object sender, TextChangedEventArgs e)
        {
            DisplayIconNames();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (!HasUserTrimmedInputImage)
            {
                CompleteNotice.Content = "Fail to make icons, Trim area to be Icon by Mouse";
                return;
            }

            var destFolderPath = BuildDestFolderPath();
            if (Directory.Exists(destFolderPath))
            {
                MessageBox.Show("Icons with same project name are already exist, please change your project name");
                return;
            }

            Directory.CreateDirectory(destFolderPath);

            foreach (var thumbnail in thumbnails)
            {
                imageEncodingEngine.EncodeAndSave(thumbnail.Value.Image, thumbnail.Value.Title.Text, destFolderPath);
            }

            if (thumbnails.ContainsKey(173))
            {
                imageEncodingEngine.EncodeAndSave(thumbnails[173].Image, "Background.png", destFolderPath);
            }

            if (thumbnails.ContainsKey(62))
            {
                imageEncodingEngine.EncodeAndSave(thumbnails[62].Image, "ApplicationIcon.png", destFolderPath);
            }

            CompleteNotice.Content = CompleteNoticeMessage;
            CompleteNotice.Visibility = Visibility.Visible;

            BeginStoryboard((Storyboard)FindResource("Storyboard1"));
        }

        private string BuildDestFolderPath()
        {
            var destFolderPath = System.IO.Path.GetDirectoryName(fileName);
            destFolderPath = $"{destFolderPath}\\{projectName.Text} Icons";
            return destFolderPath;
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
                MessageBox.Show("Please load .PNG or .JPG", "Please drag png/jpg file", MessageBoxButton.OK);
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
            CompleteNotice.Content = TrimAreaToBeIconByMouseMessage;

            foreach (var thumbnail in thumbnails)
            {
                thumbnail.Value.Title.Visibility = Visibility.Hidden;
                thumbnail.Value.Image.Fill = null;
            }

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
            CompleteNotice.Content = "Click [Save Icons] button if satisfied";
        }

        private void BackgroundMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            p1 = e.GetPosition(grayImage);
            CompleteNotice.Content = TrimAreaToBeIconByMouseMessage;
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

            foreach (var thumbnail in thumbnails)
            {
                thumbnail.Value.Image.Fill = brush;
            }

            DisplayIconNames();
        }
    }
}