using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace BodyBuilding2011
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("ru-RU");

        }
    }
}