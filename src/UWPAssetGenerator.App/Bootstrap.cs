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
                           new ThumbnailSpecification(71, 71, "Square71x71Logo") { ScaledImages = DefaultScaledImages() },
                           new ThumbnailSpecification(150, 150, "Square150x150Logo") { ScaledImages = DefaultScaledImages() },
                           new ThumbnailSpecification(310, 150, "Wide310x150Logo") { ScaledImages = DefaultScaledImages() },
                           new ThumbnailSpecification(310, 310, "Square310x310Logo") { ScaledImages = DefaultScaledImages() },
                           new ThumbnailSpecification(44, 44, "Square44x44Logo") { ScaledImages = DefaultScaledImages() },
                           new ThumbnailSpecification(50, 50, "StoreLogo") { ScaledImages = DefaultScaledImages() },
                           new ThumbnailSpecification(24, 24, "BadgeLogo") { ScaledImages = DefaultScaledImages() },
                           new ThumbnailSpecification(620, 300, "SplashScreen") { ScaledImages = DefaultScaledImages() },
                       };
        }

        private static List<ScaledThumbnailDefinition> DefaultScaledImages()
        {
            return new List<ScaledThumbnailDefinition>
                       {
                           new ScaledThumbnailDefinition { ScalePercent = 400, FileNameSuffix = ".scale-400" },
                           new ScaledThumbnailDefinition { ScalePercent = 200, FileNameSuffix = ".scale-200" },
                           new ScaledThumbnailDefinition { ScalePercent = 150, FileNameSuffix = ".scale-150" },
                           new ScaledThumbnailDefinition { ScalePercent = 125, FileNameSuffix = ".scale-125" },
                           new ScaledThumbnailDefinition { ScalePercent = 100, FileNameSuffix = ".scale-100" },
                       };
        }
    }
}