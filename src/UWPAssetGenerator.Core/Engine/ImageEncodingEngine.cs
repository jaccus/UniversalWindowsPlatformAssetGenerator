namespace UWPAssetGenerator.Core.Engine
{
    using System.IO;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class ImageEncodingEngine
    {
        public void EncodeAndSave(FrameworkElement icon, string name, string filePath)
        {
            var rtb = new RenderTargetBitmap((int)icon.Width, (int)icon.Height, 96.0, 96.0, PixelFormats.Pbgra32);
            var dv = new DrawingVisual();
            using (var dc = dv.RenderOpen())
            {
                var vb = new VisualBrush(icon);
                dc.DrawRectangle(vb, null, new Rect(new Point(), new Size((int)icon.Width, (int)icon.Height)));
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
