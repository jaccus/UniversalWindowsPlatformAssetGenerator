namespace UWPAssetGenerator.App
{
    using System.Collections.Generic;

    public class Bootstrap
    {
        public static List<ThumbnailSize> LoadThumbnailSizes()
        {
            return new List<ThumbnailSize> { new ThumbnailSize(200, 200), new ThumbnailSize(173, 173), new ThumbnailSize(99, 99), new ThumbnailSize(62, 62) };
        }
    }
}