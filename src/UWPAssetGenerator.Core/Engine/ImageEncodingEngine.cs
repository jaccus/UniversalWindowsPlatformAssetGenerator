namespace UWPAssetGenerator.Core.Engine
{
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class ImageEncodingEngine
    {
        public void EncodeAndSave(ImageBrush icon, string name, string filePath, int iconWidth, int iconHeight)
        {
            var rtb = new RenderTargetBitmap(iconWidth, iconHeight, 96.0, 96.0, PixelFormats.Pbgra32);
            var dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                dc.DrawRectangle(icon, null, new Rect(new Point(), new Size(iconWidth, iconHeight)));
            }

            rtb.Render(dv);
            var bmf = BitmapFrame.Create(rtb);
            bmf.Freeze();
            var fileOut = filePath + "\\" + name;
            using (var stream = new FileStream(fileOut, FileMode.Create))
            {
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(bmf);
                encoder.Save(stream);
            }
        }
    }
}
