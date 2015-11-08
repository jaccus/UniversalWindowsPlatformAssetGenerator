namespace UWPAssetGenerator.App
{
    using System.Collections.Generic;

    public class ThumbnailSpecification
    {
        public ThumbnailSpecification(int width, int height, string fileName)
        {
            Width = width;
            Height = height;
            FileName = fileName;
        }

        public int Width { get; private set; }

        public int Height { get; private set; }

        public string FileName { get; private set; }

        public List<string> ExtraCopyFileNames { get; set; }
    }
}