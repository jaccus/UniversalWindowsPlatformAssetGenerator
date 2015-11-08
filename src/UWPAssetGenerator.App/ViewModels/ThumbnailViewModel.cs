namespace UWPAssetGenerator.App.ViewModels
{
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

        public int Key { get; set; }

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

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}