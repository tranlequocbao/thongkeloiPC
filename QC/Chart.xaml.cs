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
    /// Interaction logic for Chart.xaml
    /// </summary>
    public partial class Chart : UserControl
    {
        string con_server = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString();
        public Chart()
        {
            load_shop_fist();
            InitializeComponent();
            tbSoluong.Text = "BIỂU ĐỒ SỐ LƯỢNG LỖI " + load_shop_fist() + "";
            tbTile.Text = "BIỂU ĐỒ TỈ LỆ LỖI " + load_shop_fist() + "";
            load_Chart_Amount(load_shop_fist());
            load_Chart_Ratio(load_shop_fist());
            
        }

        private void btnBody_Click(object sender, RoutedEventArgs e)
        {

        }
        private string load_shop_fist()
        {
            string resul = "";
            string s = "select TOP 1 *  FROM [QTSX].[dbo].[QC_INFORMATION_SHOP] order by Shop_name asc";
            using (SqlConnection myconn = new SqlConnection(con_server))
            {
                try
                {
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    read.Read();
                    resul = read["Shop_name"].ToString();
                }
                catch
                {

                }
                finally
                {
                    myconn.Close();
                }
                return resul;
            }
        }



        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            title.Children.Clear();
            using (SqlConnection myconn = new SqlConnection(con_server))
            {
                string s = "select *  FROM [QTSX].[dbo].[QC_INFORMATION_SHOP] where OK=1 order by Shop_name asc";
                try
                {
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        int amount = 0;
                        string count = "select Production  FROM [QTSX].[dbo].[QC_INFORMATION_PRODUCTION] where Shop=N'" + read["Shop_name"].ToString() + "' and Year(datetime)='" + DateTime.Now.Year + "' and Month(datetime)='" + DateTime.Now.Month + "' and DAY(datetime)='" + DateTime.Now.Day + "'";
                        SqlCommand cmd_count = new SqlCommand(count, myconn);
                        SqlDataReader read_count = cmd_count.ExecuteReader();

                        if (read_count.Read())
                        {
                            amount = Convert.ToInt32(read_count["Production"].ToString());
                        }


                        Grid grid = new Grid();
                        grid.Background = Brushes.Transparent;
                        grid.Width = 120;
                        grid.Height = 55;
                        Grid grid2 = new Grid();
                        grid2.Background = Brushes.YellowGreen;
                        grid2.Width = 115;
                        grid2.Height = 53;
                        grid.Children.Add(grid2);
                        TextBlock tb = new TextBlock();
                        tb.TextWrapping = TextWrapping.WrapWithOverflow;
                        tb.Text = "" + amount + "- " + read["Shop_name"].ToString();
                        tb.FontSize = 15;
                        tb.Foreground = Brushes.White;
                        tb.TextAlignment = TextAlignment.Center;
                        tb.FontFamily = new FontFamily("Cambria");
                        tb.FontWeight = FontWeights.Bold;
                        tb.HorizontalAlignment = HorizontalAlignment.Center;
                        tb.VerticalAlignment = VerticalAlignment.Center;
                        Button bt = new Button();
                        bt.ToolTip = read["Shop_name"].ToString();
                        bt.Foreground = Brushes.Transparent;
                        bt.Background = Brushes.Transparent;
                        bt.Click += Bt_Click;
                        grid2.Children.Add(tb);
                        grid2.Children.Add(bt);
                        title.Children.Add(grid);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi load shop: " + ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    myconn.Close();
                }
            }
        }

        private void Bt_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            load_Chart_Amount(button.ToolTip.ToString());
            load_Chart_Ratio(button.ToolTip.ToString());

            tbSoluong.Text = "BIỂU ĐỒ SỐ LƯỢNG LỖI " + button.ToolTip.ToString() + "";
            tbTile.Text = "BIỂU ĐỒ TỈ LỆ LỖI " + button.ToolTip.ToString() + "";

        }
        private string get_nameShop(string IDshop)
        {
            string result = "";
            string s = string.Format("select * FROM [QTSX].[dbo].[QC_INFORMATION_SHOP] where Shop_name=N'{0}'", IDshop);
            using (SqlConnection myconn = new SqlConnection(con_server))
            {
                try
                {
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    read.Read();
                    result = read["IDShop"].ToString();
                }
                catch
                {
                    MessageBox.Show("Lỗi thông tin Xưởng ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    myconn.Close();
                }
            }
            return result;
        }
        private int Amount_year(string year, string shop)
        {
            int result = 0;
            using(SqlConnection myconn = new SqlConnection(con_server))
            {
                try
                {
                    myconn.Open();
                    string s = string.Format("select SUM(Production) as count  FROM [QTSX].[dbo].[QC_INFORMATION_PRODUCTION] where YEAR(Datetime)='{0}' and SHOP=N'{1}'", year, shop);
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    read.Read();
                    result = Convert.ToInt32(read["count"].ToString());
                }
                catch(Exception ex)
                {

                }
                finally
                {
                    myconn.Close();
                }
               
            }
            return result;
        }
        private int Amount_month(string year,string month, string shop)
        {
            int result = 0;
            using (SqlConnection myconn = new SqlConnection(con_server))
            {
                try
                {
                    myconn.Open();
                    string s = string.Format("select SUM(Production) as count  FROM [QTSX].[dbo].[QC_INFORMATION_PRODUCTION] where YEAR(DATETIME)='{0}' and MONTH(DATETIME)='{1}' and SHOP=N'{2}'", year,month, shop);
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    read.Read();
                    result = Convert.ToInt32(read["count"].ToString());
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    myconn.Close();
                }

            }
            return result;
        }

        private int Amount_day(string year, string month,string day, string shop)
        {
            int result = 0;
            using (SqlConnection myconn = new SqlConnection(con_server))
            {
                try
                {
                    myconn.Open();
                    string s = string.Format("select Production as count  FROM [QTSX].[dbo].[QC_INFORMATION_PRODUCTION] where YEAR(DATETIME)='{0}' and MONTH(DATETIME)='{1}' and DAY(DATETIME)='{2}' and Shop=N'{3}'", year, month,day, shop);
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    read.Read();
                    result = Convert.ToInt32(read["count"].ToString());
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    myconn.Close();
                }

            }
            return result;
        }
        private void load_Chart_Amount(string shop)
        {
            chQc.Series.Clear();
            chQc.AxisX.Clear();
            chQc.AxisY.Clear();
            List<string> date = new List<string>();


            List<string> label = new List<string>();
            using (SqlConnection myconn = new SqlConnection(con_server))
            {
                try
                {

                    DateTime Lastyear = DateTime.Now.AddYears(-1);
                    DateTime Month = DateTime.Now.AddMonths(0);


                    myconn.Open();

                    ChartValues<int> value = new ChartValues<int>();
                    ChartValues<double> value1 = new ChartValues<double>();
                    
                    int year = Convert.ToInt16(Lastyear.Year.ToString());
                    string sql_count1 = string.Format("Select  COUNT(ID) as count from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where YEAR(DATETIME)='{0}' and [SHOP]=N'{1}' ", year.ToString(), shop);
                    SqlCommand cmd_count1 = new SqlCommand(sql_count1, myconn);
                    SqlDataReader read_count1 = cmd_count1.ExecuteReader();
                    int count1 = 0;
                    read_count1.Read();

                    count1 = Convert.ToInt32(read_count1["count"].ToString());
                    value.Add(count1);
                  

                    int mont = Convert.ToInt16(Month.Month.ToString());
                    int day = Convert.ToInt16(Month.Day.ToString());
                    date.Add("Năm " + Lastyear.Year.ToString());

                    chQc.Series.Add(new LineSeries
                    {
                        Title = "",
                        Values = value,
                        //StackMode = StackMode.Values,
                        FontFamily = new FontFamily("Cambria"),
                        FontSize = 14,
                        DataLabels = true,
                        Fill = Brushes.Transparent
                        //Stroke = Brushes.Black
                    });

                   
                    if (mont != 1)
                    {
                        for (int i = 1; i < mont; i++)
                        {
                            date.Add("Tháng " + i.ToString());
                            string sql_count = string.Format("Select  COUNT(ID) as count from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where YEAR(DATETIME)='{0}' and MONTH(DATETIME)='{1}' and [SHOP]=N'{2}' ",DateTime.Now.Year.ToString(), i.ToString(), shop);
                            SqlCommand cmd_count = new SqlCommand(sql_count, myconn);
                            SqlDataReader read_count = cmd_count.ExecuteReader();
                            int count = 0;
                            while (read_count.Read())
                            {
                                count = Convert.ToInt32(read_count["count"].ToString());
                                value.Add(count);
                            }
                        }

                        chQc.Series.Add(new LineSeries
                        {
                            Title = "",
                            Values = value,
                            //StackMode = StackMode.Values,
                            FontFamily = new FontFamily("Cambria"),
                            FontSize = 14,
                            DataLabels = true,
                            Fill = Brushes.Transparent
                            //Stroke = Brushes.Black
                        });

                        
                        if (day != 1)
                        {
                            for (int i = 1; i <= day; i++)
                            {
                                date.Add("Ngày " + i.ToString());
                                string sql_count = string.Format("Select  COUNT(ID) as count from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where YEAR(DATETIME)='{0}' and MONTH(DATETIME)='{1}' and DAY(DATETIME)='{2}' and [SHOP]=N'{3}'", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), i.ToString(), shop);
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
                            date.Add(day.ToString());
                            string sql_count = string.Format("Select  COUNT(ID) as count from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where YEAR(DATETIME)='{0}' and MONTH(DATETIME)='{1}' and DAY(DATETIME)='{2}' and [SHOP]=N'{3}'", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), day.ToString(), shop);
                            SqlCommand cmd_count = new SqlCommand(sql_count, myconn);
                            SqlDataReader read_count = cmd_count.ExecuteReader();
                            int count = 0;
                            while (read_count.Read())
                            {
                                count = Convert.ToInt32(read_count["count"].ToString());
                                value.Add(count);
                            }
                        }

                        chQc.Series.Add(new LineSeries
                        {
                            Title = "",
                            Values = value,
                            //StackMode = StackMode.Values,
                            FontFamily = new FontFamily("Cambria"),
                            FontSize = 14,
                            DataLabels = true,
                            Fill = Brushes.Transparent
                            //Stroke = Brushes.Black
                        });

                    }


                }
                //catch (Exception ex)
                //{
                //MessageBox.Show(ex.Message, "Error get dataa", MessageBoxButton.OK, MessageBoxImage.Error);
                // }
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
                MinValue=0
            }) ;
            //chQc.LegendLocation = LegendLocation.Bottom;
        }
       
        private void load_Chart_Ratio(string shop)
        {
            chQc1.Series.Clear();
            chQc1.AxisX.Clear();
            chQc1.AxisY.Clear();
            List<string> date = new List<string>();


            List<string> label = new List<string>();
            using (SqlConnection myconn = new SqlConnection(con_server))
            {
                try
                {

                    DateTime Lastyear = DateTime.Now.AddYears(-1);
                    DateTime Month = DateTime.Now.AddMonths(0);




                    myconn.Open();

                    ChartValues<double> value = new ChartValues<double>();
                    ChartValues<double> value1 = new ChartValues<double>();
                    int year = Convert.ToInt16(Lastyear.Year.ToString());
                    string sql_count1 = string.Format("Select  COUNT(ID) as count from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where YEAR(DATETIME)='{0}' and [SHOP]=N'{1}' ", year.ToString(), shop);
                    SqlCommand cmd_count1 = new SqlCommand(sql_count1, myconn);
                    SqlDataReader read_count1 = cmd_count1.ExecuteReader();
                    double count1 = 0;
                    read_count1.Read();

                    if(Amount_year(Lastyear.ToString(), shop)!=0) count1 = Convert.ToDouble(read_count1["count"].ToString()) / Amount_year(Lastyear.ToString(), shop);
                    count1 = Math.Round(count1, 2);
                    value.Add(count1);
                    value1.Add(count1);
                    int mont = Convert.ToInt16(Month.Month.ToString());
                    int day = Convert.ToInt16(Month.Day.ToString());
                    date.Add("Năm " + Lastyear.Year.ToString());

                    //chQc1.Series.Add(new LineSeries
                    //{
                    //    Title = "",
                    //    Values = value,
                    //    //StackMode = StackMode.Values,
                    //    FontFamily = new FontFamily("Cambria"),
                    //    FontSize = 14,
                    //    DataLabels = true,
                    //    Fill = Brushes.Transparent
                    //    //Stroke = Brushes.Black
                    //});

                    //chQc1.Series.Add(new LineSeries
                    //{
                    //    Title = "",
                    //    Values = value1,
                    //    //StackMode = StackMode.Values,
                    //    FontFamily = new FontFamily("Cambria"),
                    //    FontSize = 14,
                    //    //DataLabels = true,
                    //    Stroke = Brushes.Blue,
                    //    Fill = Brushes.Transparent

                    //}) ;
                    if (mont != 1)
                    {
                        for (int i = 1; i < mont; i++)
                        {
                            date.Add("Tháng " + i.ToString());
                            string sql_count = string.Format("Select  COUNT(ID) as count from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where YEAR(DATETIME)='{0}' and MONTH(DATETIME)='{1}' and [SHOP]=N'{2}' ",DateTime.Now.Year.ToString(), i.ToString(), shop);
                            SqlCommand cmd_count = new SqlCommand(sql_count, myconn);
                            SqlDataReader read_count = cmd_count.ExecuteReader();
                            double count = 0;
                            while (read_count.Read())
                            {
                                if (Amount_month(DateTime.Now.Year.ToString(), i.ToString(), shop)!=0) count = Convert.ToDouble(read_count["count"].ToString())/ Amount_month(DateTime.Now.Year.ToString(), i.ToString(), shop);
                                count = Math.Round(count, 2);
                                value.Add(count);
                                value1.Add(0.07);
                            }
                        }

                        //chQc1.Series.Add(new LineSeries
                        //{
                        //    Title = "",
                        //    Values = value,
                        //    //StackMode = StackMode.Values,
                        //    FontFamily = new FontFamily("Cambria"),
                        //    FontSize = 12,
                        //    DataLabels = true,
                        //    Fill = Brushes.Transparent
                        //    //Stroke = Brushes.Black
                        //});
                        //chQc1.Series.Add(new LineSeries
                        //{
                        //    Title = "",
                        //    Values = value1,
                        //    //StackMode = StackMode.Values,
                        //    FontFamily = new FontFamily("Cambria"),
                        //    FontSize = 14,
                        //    //DataLabels = true,
                        //    Stroke = Brushes.Blue,
                        //    Fill = Brushes.Transparent
                        //});

                        if (day != 1)
                        {
                            for (int i = 1; i <= day; i++)
                            {
                                date.Add("Ngày " + i.ToString());
                                string sql_count = string.Format("Select  COUNT(ID) as count from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where YEAR(DATETIME)='{0}' and MONTH(DATETIME)='{1}' and DAY(DATETIME)='{2}' and  [SHOP]=N'{3}'", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), i.ToString(), shop);
                                SqlCommand cmd_count = new SqlCommand(sql_count, myconn);
                                SqlDataReader read_count = cmd_count.ExecuteReader();
                                double count = 0;
                                while (read_count.Read())
                                {
                                    if(Amount_day(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), i.ToString(), shop)!=0) count = Convert.ToDouble(read_count["count"].ToString())/ Amount_day(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), i.ToString(), shop);
                                    count = Math.Round(count, 2);
                                    value.Add(count);
                                    value1.Add(0.07);
                                }
                            }
                        }
                        else
                        {
                            date.Add(day.ToString());
                            string sql_count = string.Format("Select  COUNT(ID) as count from [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where YEAR(DATETIME)='{0}' and MONTH(DATETIME)='{1}' and DAY(DATETIME)='{2}' and  [SHOP]=N'{3}'", DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), day.ToString(), shop);
                            SqlCommand cmd_count = new SqlCommand(sql_count, myconn);
                            SqlDataReader read_count = cmd_count.ExecuteReader();
                            double count = 0;
                            while (read_count.Read())
                            {
                                if (Amount_day(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), day.ToString(), shop) != 0) count = Convert.ToDouble(read_count["count"].ToString()) / Amount_day(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString(), day.ToString(), shop);
                                //count = Convert.ToInt32(read_count["count"].ToString());
                                count = Math.Round(count, 2);
                                value.Add(count);
                                value1.Add(0.07);
                            }
                        }

                        

                    }
                    chQc1.Series.Add(new LineSeries
                    {
                        Title = "",
                        Values = value,
                        //StackMode = StackMode.Values,
                        FontFamily = new FontFamily("Cambria"),
                        FontSize = 12,
                        DataLabels = true,
                        Fill = Brushes.Transparent
                        //Stroke = Brushes.Black
                    });
                    chQc1.Series.Add(new LineSeries
                    {
                        Title = "",
                        Values = value1,
                        //StackMode = StackMode.Values,
                        FontFamily = new FontFamily("Cambria"),
                        FontSize = 12,
                        // DataLabels = true,
                        Stroke = Brushes.Blue,
                        Fill = Brushes.Transparent
                    });


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
            sep1.Step = 0.01;
            sep1.IsEnabled = true;
            chQc1.AxisX.Add(new Axis
            {
                Title = "THỜI GIAN",
                Labels = label,
                FontFamily = new FontFamily("Cambria"),
                FontSize = 14,
                LabelsRotation = 45,
                Foreground = Brushes.Black,
                Separator = sep,



            });
            chQc1.AxisY.Add(new Axis
            {
                Title = "SỐ LƯỢNG",
                LabelFormatter = value => value + "",
                FontFamily = new FontFamily("Cambria"),
                FontSize = 14,
                Separator = sep1,
                Foreground = Brushes.Black,
                MinValue = 0.0
            });
            //chQc1.LegendLocation = LegendLocation.Bottom;
        }
    }
}
