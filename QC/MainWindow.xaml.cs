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

namespace QC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            //this.Title = System.Configuration.ConfigurationManager.AppSettings["building"].ToString();
            title.Text = System.Configuration.ConfigurationManager.AppSettings["building"].ToString();

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnShutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnList_Click(object sender, RoutedEventArgs e)
        {
            dpShow.Children.Clear();
            dpShow.Children.Add(new listError());
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //dpShow.Children.Clear();
            //dpShow.Children.Add(new UserControl1());
        }

        private void btnDiagram_Click(object sender, RoutedEventArgs e)
        {
            dpShow.Children.Clear();
            dpShow.Children.Add(new Chart_Detail());
        }

        private void btnDiagram_Shop_Click(object sender, RoutedEventArgs e)
        {
            dpShow.Children.Clear();
            dpShow.Children.Add(new Chart());
        }

        private void btnDiagram_Section_Click(object sender, RoutedEventArgs e)
        {
            dpShow.Children.Clear();
            dpShow.Children.Add(new ChartSection());
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            if(Username.username == "user")
            {
                dpShow.Children.Clear();
                dpShow.Children.Add(new Login());
                dispatcherTimer.Tick += DispatcherTimer_Tick; ;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Start();
            }
            else
            {
                dpShow.Children.Clear();
                dpShow.Children.Add(new Setting());
            }
            
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {

            if (Username.username != "user")
            {
                dpShow.Children.Clear();
                dpShow.Children.Add(new Setting());
                dispatcherTimer.Stop();
            }
        }
    }


}
