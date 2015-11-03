namespace UWPAssetGenerator.App
{
    using System.Windows;

    public partial class App
    {
        public static string Language { get; private set; }

        private void AppStartup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length == 0)
            {
                return;
            }

            foreach (var argument in e.Args)
            {
                if (argument.Contains("-E") || argument.Contains("-e"))
                {
                    Language = "English";
                }
                else if (argument.Contains("-J") || argument.Contains("-j"))
                {
                    Language = "Japanese";
                }
            }
        }
    }
}