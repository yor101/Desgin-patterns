using ObserverCS_S3.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ObserverCS_S3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly PointPublisher pointPublisher;
        private PointDrawerSubscriber pointDrawer;
        private PointWriterSubscriber pointWriter;
        private IDisposable subscriptionDrawer;
        private IDisposable subscriptionWriter;

        public MainWindow(PointPublisher pointPublisher)
        {
            InitializeComponent();

            this.pointPublisher = pointPublisher;
            pointDrawer = new PointDrawerSubscriber();
            pointWriter = new PointWriterSubscriber(pointDrawer.Visual);

            Button subBtn = new Button();
            subBtn.Height = 20;
            subBtn.Width = 180;
            subBtn.Margin = new Thickness(0, 40, 0, 0);
            subBtn.Content = "Toggle subscribe/unsubscribe";

            canvas.Children.Add(pointDrawer);
            canvas.Children.Add(subBtn);

            subBtn.Click += (obj, eventArgs) =>
            {
                if (subscriptionDrawer == null)
                    subscriptionDrawer = pointPublisher.Subscribe(pointDrawer);
                else
                {
                    subscriptionDrawer.Dispose();
                    subscriptionDrawer = null;
                }

                if (subscriptionWriter == null)
                    subscriptionWriter = pointPublisher.Subscribe(pointWriter);
                else
                {
                    subscriptionWriter.Dispose();
                    subscriptionWriter = null;
                }

                this.Title = subscriptionDrawer == null ? "MainWindow" : "MainWindow (Subscribed)";
            };

            this.MouseDown += (obj, eventArgs) =>
            {
                if (eventArgs.LeftButton == MouseButtonState.Pressed && subscriptionDrawer != null)
                    pointPublisher.point = eventArgs.GetPosition(pointDrawer);
                if (eventArgs.RightButton == MouseButtonState.Pressed)
                {
                    // lets create a new clone window
                    MainWindow mainWindow = new MainWindow(pointPublisher);

                    mainWindow.Show();
                }

            };

            this.MouseMove += (obj, eventArgs) =>
            {
                if(eventArgs.LeftButton == MouseButtonState.Pressed && subscriptionDrawer != null)
                    pointPublisher.point = eventArgs.GetPosition(pointDrawer);

            };

        }
    }
}
