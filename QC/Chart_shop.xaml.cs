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
    /// Interaction logic for Chart_shop.xaml
    /// </summary>
    public partial class Chart_shop : UserControl
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString();
        string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["conn1"].ToString();
        public Chart_shop()
        {
            InitializeComponent();

            load_Chart();
        }


        private void load_Chart()
        {
            chQc.Series.Clear();
            chQc.AxisX.Clear();
            chQc.AxisY.Clear();
            List<string> date = new List<string>();


            List<string> label = new List<string>();
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                try
                {

                    DateTime Lastyear = DateTime.Now.AddYears(-1);
                    DateTime Month = DateTime.Now.AddMonths(0);






                    string sql_shop = string.Format("Select  * FROM [QTSX].[dbo].[QC_INFORMATION_SHOP]");
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(sql_shop, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        ChartValues<int> value = new ChartValues<int>();
                        ChartValues<int> value1 = new ChartValues<int>();
                        int year = Convert.ToInt16(Lastyear.Year.ToString());
                        string sql_count1 = string.Format("Select  COUNT(ID) as count from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where YEAR(DATETIME)='{0}' and [SHOP]=N'{1}'", year.ToString(), read["Shop_name"].ToString());
                        SqlCommand cmd_count1 = new SqlCommand(sql_count1, myconn);
                        SqlDataReader read_count1 = cmd_count1.ExecuteReader();
                        int count1 = 0;
                        read_count1.Read();

                        count1 = Convert.ToInt32(read_count1["count"].ToString());
                        value.Add(count1);

                        int mont = Convert.ToInt16(Month.Month.ToString());
                        int day = Convert.ToInt16(Month.Day.ToString());
                        date.Add("Năm " + Lastyear.Year.ToString());
                        if (mont != 1)
                        {
                            for (int i = 1; i < mont; i++)
                            {
                                date.Add("Tháng " + i.ToString());
                                string sql_count = string.Format("Select  COUNT(ID) as count from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where MONTH(DATETIME)='{0}' and YEAR(DATETIME)='{2}' and [SHOP]=N'{1}'", i.ToString(), read["Shop_name"].ToString(),DateTime.Now.Year.ToString());
                                SqlCommand cmd_count = new SqlCommand(sql_count, myconn);
                                SqlDataReader read_count = cmd_count.ExecuteReader();
                                int count = 0;
                                while (read_count.Read())
                                {
                                    count = Convert.ToInt32(read_count["count"].ToString());
                                    value.Add(count);
                                }
                            }
                        }

                        if (day != 1)
                        {
                            for (int i = 1; i <= day; i++)
                            {
                                date.Add("Ngày " + i.ToString());
                                string sql_count = string.Format("Select  COUNT(ID) as count from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where DAY(DATETIME)='{0}' and MONTH(DATETIME)='{2}' and YEAR(DATETIME)='{3}' and [SHOP]=N'{1}'", i.ToString(), read["Shop_name"].ToString(), DateTime.Now.Month.ToString(),DateTime.Now.Year.ToString());
                                SqlCommand cmd_count = new SqlCommand(sql_count, myconn);
                                SqlDataReader read_count = cmd_count.ExecuteReader();
                                int count = 0;
                                while (read_count.Read())
                                {
                                    count = Convert.ToInt32(read_count["count"].ToString());
                                    value.Add(count);
                                }
                            }
                        }
                        else
                        {
                            date.Add("Ngày "+day.ToString());
                            string sql_count = string.Format("Select  COUNT(ID) as count from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where DAY(DATETIME)='{0}'  and [SHOP]=N'{1}'", day.ToString(), read["Shop_name"].ToString());
                            SqlCommand cmd_count = new SqlCommand(sql_count, myconn);
                            SqlDataReader read_count = cmd_count.ExecuteReader();
                            int count = 0;
                            while (read_count.Read())
                            {
                                count = Convert.ToInt32(read_count["count"].ToString());
                                value.Add(count);
                            }
                        }


                        //foreach (string items in date)
                        //{
                        //    string sql_count = string.Format("Select  COUNT(ID) as count from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where YEAR(DATETIME)='{0}'", Lastyear.Year.ToString());
                        //    SqlCommand cmd_count = new SqlCommand(sql_count, myconn);
                        //    SqlDataReader read_count = cmd_count.ExecuteReader();
                        //    int count = 0;
                        //    while (read_count.Read())
                        //    {
                        //        count = Convert.ToInt32(read_count["count"].ToString());
                        //        value.Add(count);
                        //    }
                        //}



                        chQc.Series.Add(new StackedColumnSeries
                        {
                            Title = read["Shop_name"].ToString(),
                            Values = value,
                            StackMode = StackMode.Values,
                            FontFamily = new FontFamily("Cambria"),
                            FontSize = 14,
                            DataLabels = true,
                            Stroke=Brushes.Black
                        });

                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error get dataa", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    myconn.Close();
                }

            }

           



            foreach (string items in date)
            {
                label.Add(items);
            }


            LiveCharts.Wpf.Separator sep = new LiveCharts.Wpf.Separator();
            sep.Step = 1;
            sep.IsEnabled = true;
            LiveCharts.Wpf.Separator sep1 = new LiveCharts.Wpf.Separator();
            sep1.Step = 1;
            sep1.IsEnabled = true;
            chQc.AxisX.Add(new Axis
            {
                Title = "THỜI GIAN",
                Labels = label,
                FontFamily = new FontFamily("Cambria"),
                FontSize = 14,
                LabelsRotation = 45,
                Foreground = Brushes.Black,
                Separator = sep,



            });
            chQc.AxisY.Add(new Axis
            {
                Title = "SỐ LƯỢNG",
                LabelFormatter = value => value + "",
                FontFamily = new FontFamily("Cambria"),
                FontSize = 14,
                Separator = sep1,
                Foreground = Brushes.Black,
            }); ;
            chQc.LegendLocation = LegendLocation.Bottom;
        }


    }
}
