namespace UWPAssetGenerator.Core.Engine
{
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    using UWPAssetGenerator.Core.Types;

    public class ImageEncodingEngine
    {
        public void EncodeAndSave(ThumbnailSaveSpecification thumbnail, string destFolderName)
        {
            var rtb = new RenderTargetBitmap(thumbnail.Width, thumbnail.Height, 96.0, 96.0, PixelFormats.Pbgra32);
            var dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                dc.DrawRectangle(thumbnail.Brush, null, new Rect(new Point(), new Size(thumbnail.Width, thumbnail.Height)));
            }

            rtb.Render(dv);
            var bmf = BitmapFrame.Create(rtb);
            bmf.Freeze();
            var fileOut = $"{destFolderName}\\{thumbnail.FileName}.png";
            using (var stream = new FileStream(fileOut, FileMode.Create))
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(bmf);
                encoder.Save(stream);
            }
        }
    }
}
