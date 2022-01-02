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

namespace QC
{
    /// <summary>
    /// Interaction logic for Setting.xaml
    /// </summary>
    public partial class Setting : UserControl
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString();
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        
        public Setting()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            load_lstProduct();
            
            load_shop();
            cbShop.SelectedIndex = 1;
            cbShop_.SelectedIndex = 1;
            cbbDate.SelectedDate = DateTime.Now;
            load_lstSection();
            load_lstStation();
        }
        public class items_listProduct
        {
            public string Shop { get; set; }
            public int Product { get; set; }
            public string Date { get; set; }

            
        }
        public class items_listsection
        {
            public string Section { get; set; }
        }
        public class items_liststation
        {
            public string Station { get; set; }
        }
        private string get_IDshop(string nameShop)
        {
            string result = "";
            string s = string.Format("SELECT IDShop from [QTSX].[dbo].[QC_INFORMATION_SHOP] where Shop_name='{0}' ", nameShop);
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                try
                {
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    read.Read();
                    result = read["IDShop"].ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ERROR GET IDSHOP", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    myconn.Close();
                }
            }
            return result;
        }
        private void load_shop()
        {
            cbShop.Items.Clear();
            cbShop_.Items.Clear();

            
            string s = string.Format(" select * from [QTSX].[dbo].[QC_INFORMATION_SHOP] where OK=1");
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString()))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(s, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        //if (cbbShop.SelectedValue.ToString() != null)
                        //{
                        cbShop.Items.Add(read["Shop_name"].ToString());
                        cbShop_.Items.Add(read["Shop_name"].ToString());
                        //}

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi load dữ liệu combobox xưởng " + ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
            cbShop.SelectedIndex = 0;
            cbShop_.SelectedIndex = 0;
        }
        List<items_listsection> _item_lstSection;
        private void load_lstStation()
        {
           
            _item_lstSection = new List<items_listsection>();
            if (cbShop_.SelectedItem != null)
            {
                using (SqlConnection myconn = new SqlConnection(conn))
                {
                    string s = string.Format("select TOP 1000 *  FROM [QTSX].[dbo].[QC_INFORMATION_SECTION] where IDShop='{0}' order by Section_name asc", get_IDshop(cbShop_.SelectedValue.ToString()));
                    try
                    {
                        myconn.Open();
                        SqlCommand cmd = new SqlCommand(s, myconn);
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {

                            _item_lstSection.Add(new items_listsection()
                            {
                                Section = read["Section_name"].ToString()
                            }); ;

                        }
                        lstStation.ItemsSource = null;
                        lstStation.ItemsSource = _item_lstSection;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi loadlistview " + ex.Message, "LỖI", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        myconn.Close();
                    }
                }
            }
              
        }
        List<items_liststation> _item_lstStation;
        private void load_lstSection()
        {

            _item_lstStation = new List<items_liststation>();
            if (cbShop_.SelectedItem != null)
            {
                using (SqlConnection myconn = new SqlConnection(conn))
                {
                    string s = string.Format("select TOP 1000 *  FROM [QTSX].[dbo].[QC_INFORMATION_STATION] where IDShop='{0}' order by Station_name asc", get_IDshop(cbShop_.SelectedValue.ToString()));
                    try
                    {
                        myconn.Open();
                        SqlCommand cmd = new SqlCommand(s, myconn);
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {

                            _item_lstStation.Add(new items_liststation()
                            {
                                Station = read["Station_name"].ToString()
                            }); ;

                        }
                        lstPosition.ItemsSource = null;
                        lstPosition.ItemsSource = _item_lstStation;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi loadlistview " + ex.Message, "LỖI", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    finally
                    {
                        myconn.Close();
                    }
                }
            }
           
        }
        List<items_listProduct> _item_lstProduct;
        private void load_lstProduct()
        {
            DateTime date;
            _item_lstProduct =  new List<items_listProduct>();
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                string s = string.Format("select TOP 1000 *  FROM [QTSX].[dbo].[QC_INFORMATION_PRODUCTION] order by Datetime desc");
                try
                {
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        date = Convert.ToDateTime(read["Datetime"].ToString());
                        _item_lstProduct.Add(new items_listProduct()
                        {
                            Shop = read["Shop"].ToString(),
                            Product = Convert.ToInt32(read["Production"].ToString()),
                            Date = date.ToString("dd-MM-yyyy")
                        }) ;
                        
                    }
                    lstProduct.ItemsSource = null;
                    lstProduct.ItemsSource = _item_lstProduct;
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Lỗi loadlistview " + ex.Message, "LỖI", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    myconn.Close();
                }
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            using(SqlConnection myconn  = new SqlConnection(conn))
            {
                try
                {
                    string s = string.Format("select *  FROM [QTSX].[dbo].[QC_INFORMATION_PRODUCTION] where Shop='{0}' and DAY(Datetime)='{1}' and MONTH(Datetime)='{2}' and YEAR(Datetime)='{3}'"
                        , cbShop.SelectedValue.ToString(), cbbDate.SelectedDate.Value.Day, cbbDate.SelectedDate.Value.Month, cbbDate.SelectedDate.Value.Year);

                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    if (read.Read())
                    {
                        MessageBoxResult result = MessageBox.Show("Bạn có muốn CẬP NHẬT sản lượng?", "CẢNH BÁO", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        switch (result)
                        {
                            case MessageBoxResult.OK:
                                string update = string.Format("update [QTSX].[dbo].[QC_INFORMATION_PRODUCTION] set Production={0} where Shop='{1}' and DAY(Datetime)='{2}' and MONTH(Datetime)='{3}' and YEAR(Datetime)='{4}'",
                          tbProduct.Text, cbShop.SelectedValue.ToString(), cbbDate.SelectedDate.Value.Day, cbbDate.SelectedDate.Value.Month, cbbDate.SelectedDate.Value.Year);
                                SqlCommand cmd_update = new SqlCommand(update, myconn);
                                cmd_update.ExecuteNonQuery();
                                MessageBox.Show("Đã cập nhật thành công","THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                                load_lstProduct();
                                break;
                            case MessageBoxResult.Cancel:
                                break;
                        }
                              
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Bạn có muốn THÊM MỚI sản lượng?", "CẢNH BÁO", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        switch (result)
                        {
                            case MessageBoxResult.OK:
                                string insert = string.Format("insert into [QTSX].[dbo].[QC_INFORMATION_PRODUCTION](Shop,Production,Datetime) values('{0}',{1},'{2}') ", cbShop.SelectedValue.ToString(), Convert.ToInt32(tbProduct.Text), cbbDate.SelectedDate.Value.ToString("MM-dd-yyyy"));
                                SqlCommand cmd_insert = new SqlCommand(insert, myconn);
                                cmd_insert.ExecuteNonQuery();
                                MessageBox.Show("Đã thêm mới thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                                load_lstProduct();
                                break;
                            case MessageBoxResult.Cancel:
                                break;
                        }

                       
                    }
                }
                catch
                {
                   
                }
                finally
                {
                    myconn.Close();
                }
            }
        }

        private void btnAddShop_Click(object sender, RoutedEventArgs e)
        {
            Username.button = "XƯỞNG: ";
            //Username.currentShop = cbShop_.SelectedValue.ToString();
            AddInfo add = new AddInfo();
            add.Show();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (Username.finishAdd == true)
            {
                load_shop();
                load_lstSection();
                load_lstStation();
                Username.finishAdd = false;
                
            }
            if (Application.Current.Windows.OfType<AddInfo>().FirstOrDefault() == null)
            {
                //second window not exist
                timer.Stop();
            }
        }

        private void btnAddStat_Click(object sender, RoutedEventArgs e)
        {
            Username.button = "TỔ: ";
            Username.currentShop = cbShop_.SelectedValue.ToString();
            AddInfo add = new AddInfo();
            add.Show();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void btnAddPos_Click(object sender, RoutedEventArgs e)
        {
            Username.button = "VỊ TRÍ: ";
            Username.currentShop = cbShop_.SelectedValue.ToString();
            AddInfo add = new AddInfo();
            add.Show();
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void cbShop__SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbShop_.SelectedItem != null)
            {
                load_lstSection();
                load_lstStation();
            }
        }
    }
}
