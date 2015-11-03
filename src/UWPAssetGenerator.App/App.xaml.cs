namespace UWPAssetGenerator.App
{
    using System.Windows;

    public partial class App : Application
    {
        public static string Language;

        /// <summary>
        /// コマンドラインを処理するスタートアップ
        /// </summary>
        private void app_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length == 0) return;
            foreach (string argument in e.Args)
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