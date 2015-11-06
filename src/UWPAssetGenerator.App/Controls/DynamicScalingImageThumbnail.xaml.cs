namespace UWPAssetGenerator.App.Controls
{
    using System.Windows.Controls;

    public partial class DynamicScalingImageThumbnail : UserControl
    {
        public DynamicScalingImageThumbnail()
        {
            InitializeComponent();
        }

        public double ThumbnailWidth
        {
            get
            {
                return Image.Width;
            }

            set
            {
                Image.Width = value;
            }
        }

        public double ThumbnailHeight
        {
            get
            {
                return Image.Height;
            }

            set
            {
                Image.Height = value;
            }
        }
    }
}
