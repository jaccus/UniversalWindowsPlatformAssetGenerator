namespace UWPAssetGenerator.App.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public class ThumbnailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private ImageBrush brush;
        private string title;

        public ThumbnailViewModel()
        {
            TitleVisibility = Visibility.Visible;
        }

        public string FileName { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public Visibility TitleVisibility { get; set; }

        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                if (value == title)
                {
                    return;
                }
                title = value;
                OnPropertyChanged();
            }
        }

        public ImageBrush Brush
        {
            get
            {
                return brush;
            }
            set
            {
                brush = value;
                OnPropertyChanged(nameof(Brush));
            }
        }

        public List<string> ExtraCopyFileNames { get; set; }

        public List<ScaledThumbnailDefinition> ScaledImages { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}