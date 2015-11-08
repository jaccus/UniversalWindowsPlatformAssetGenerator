namespace UWPAssetGenerator.Core.Types
{
    using System.Windows.Media;

    public class ThumbnailSaveSpecification
    {
        public int Width { get; set; }

        public int Height { get; set; }

        public string FileName { get; set; }

        public ImageBrush Brush { get; set; }
    }
}