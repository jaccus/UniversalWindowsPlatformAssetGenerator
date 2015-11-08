namespace UWPAssetGenerator.App
{
    using System.Collections.Generic;

    public class Bootstrap
    {
        public static List<ThumbnailSpecification> LoadThumbnailsSpecification()
        {
            return new List<ThumbnailSpecification>
                       {
                           new ThumbnailSpecification(200, 200, "200x200.png"),
                           new ThumbnailSpecification(173, 173, "173x173.png"),
                           new ThumbnailSpecification(99, 99, "99x99.png"),
                           new ThumbnailSpecification(62, 62, "62x62.png")
                       };
        }
    }
}