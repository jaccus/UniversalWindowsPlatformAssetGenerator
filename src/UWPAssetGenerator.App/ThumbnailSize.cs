namespace UWPAssetGenerator.App
{
    public class ThumbnailSize
    {
        public ThumbnailSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; private set; }

        public int Height { get; private set; }
    }
}