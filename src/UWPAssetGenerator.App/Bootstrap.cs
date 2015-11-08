namespace UWPAssetGenerator.App
{
    using System.Collections.Generic;

    public class Bootstrap
    {
        public static List<ThumbnailSpecification> LoadThumbnailsSpecification()
        {
            return UwpThumbnailSpecifications();
        }

        private static List<ThumbnailSpecification> UwpThumbnailSpecifications()
        {
            return new List<ThumbnailSpecification>
                       {
                           new ThumbnailSpecification(71, 71),
                           new ThumbnailSpecification(150, 150),
                           new ThumbnailSpecification(310, 150),
                           new ThumbnailSpecification(310, 310),
                           new ThumbnailSpecification(44, 44),
                           new ThumbnailSpecification(50, 50),
                           new ThumbnailSpecification(620, 300),
                       };
        }

        private static List<ThumbnailSpecification> Wp8ThumbnailSpecifications()
        {
            return new List<ThumbnailSpecification>
                       {
                           new ThumbnailSpecification(200, 200, "200x200.png"),
                           new ThumbnailSpecification(173, 173, "173x173.png") { ExtraCopyFileNames = new List<string> { "Background.png" } },
                           new ThumbnailSpecification(99, 99, "99x99.png"),
                           new ThumbnailSpecification(62, 62, "62x62.png") { ExtraCopyFileNames = new List<string> { "ApplicationIcon.png" } },
                       };
        }
    }
}