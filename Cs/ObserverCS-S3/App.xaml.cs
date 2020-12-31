using ObserverCS_S3.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ObserverCS_S3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private PointPublisher pointPublisher;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            pointPublisher = new PointPublisher(new Point(1, 1));

            MainWindow mw = new MainWindow(pointPublisher);
            mw.Show();
        }
    }
}
