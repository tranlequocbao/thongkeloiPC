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
using System.Data.SqlClient;
using LiveCharts;
using LiveCharts.Wpf;

namespace QC
{
    /// <summary>
    /// Interaction logic for Chart_Detail.xaml
    /// </summary>
    public partial class Chart_Detail : UserControl
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString();
        string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["conn1"].ToString();
        public Chart_Detail()
        {
            InitializeComponent();
            loadShop();
            cbbDateStart.SelectedDate = DateTime.Now;
            cbbDateEnd.SelectedDate = DateTime.Now;
            cbbShop.SelectedIndex = 0;
            load_Chart(cbbShop.SelectedValue.ToString());

        }
        private void loadShop()
        {
            cbbShop.Items.Clear();
            string s = "select Shop_name from [QTSX].[dbo].[QC_INFORMATION_SHOP]";
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                try
                {
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        cbbShop.Items.Add(read["Shop_name"].ToString());
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message,"Error",MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    myconn.Close();
                }

            }
        }
        private void load_Chart(string shop)
        {

            chQc.Series.Clear();
            chQc.AxisX.Clear();
            chQc.AxisY.Clear();
            List<string> label = new List<string>();
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                try
                {
                    
                    string sql_typeEror = string.Format("Select DISTINCT TYPE_ERROR  from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where SHOP =N'" + shop + "' order by TYPE_ERROR desc");
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(sql_typeEror, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        ChartValues<int> value = new ChartValues<int>();
                        string sql_model = string.Format("Select DISTINCT MODEL from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where SHOP =N'" + shop + "' order by MODEL desc");
                        SqlCommand cmd_model = new SqlCommand(sql_model, myconn);
                        SqlDataReader read_model = cmd_model.ExecuteReader();
                        while (read_model.Read())
                        {
                            
                            string sql_count = string.Format("Select  COUNT(ID) as count from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where MODEL =N'{0}' and TYPE_ERROR =N'{1}' and  SHOP =N'{2}'", read_model["MODEL"].ToString(),read["TYPE_ERROR"].ToString() ,shop);
                            SqlCommand cmd_count = new SqlCommand(sql_count, myconn);
                            SqlDataReader read_count = cmd_count.ExecuteReader();
                            int count = 0;
                            while (read_count.Read())
                            {
                                count = Convert.ToInt32(read_count["count"].ToString());
                                value.Add(count);
                            }

                            
                           
                        }
                        chQc.Series.Add(new StackedColumnSeries
                        {
                            Title = read["TYPE_ERROR"].ToString(),
                            Values = value,
                            StackMode = StackMode.Values,
                            FontFamily = new FontFamily("Cambria"),
                            FontSize = 14,
                            DataLabels = true
                        });
                        
                    }
                    

                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error get dataa",MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    myconn.Close();
                }
               
            }
            
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                try
                {
                    string s = "Select DISTINCT MODEL  from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where SHOP =N'" + shop + "' order by MODEL desc";
              
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        label.Add(read["MODEL"].ToString());
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    myconn.Close();
                }
            }
            LiveCharts.Wpf.Separator sep = new LiveCharts.Wpf.Separator();
            sep.Step = 1;
            sep.IsEnabled = true;
            LiveCharts.Wpf.Separator sep1 = new LiveCharts.Wpf.Separator();
            sep1.Step = 1;
            sep1.IsEnabled = true;
            chQc.AxisX.Add(new Axis
            {
                Title = "LOẠI XE",
                Labels = label,
                FontFamily = new FontFamily("Cambria"),
                FontSize = 14,
                LabelsRotation = 45,
                Foreground = Brushes.Black,
                Separator=sep,

                

        });
            chQc.AxisY.Add(new Axis
            {
                Title = "Số lượng",
                LabelFormatter = value => value+"",
                FontFamily = new FontFamily("Cambria"),
                FontSize = 14,
                Separator = sep1,
                Foreground = Brushes.Black,
            }); ;
            chQc.LegendLocation = LegendLocation.Bottom;
        }

        private void cbbShop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            load_Chart(cbbShop.SelectedValue.ToString());
        }
    }
}
