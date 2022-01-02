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
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace QC
{
    /// <summary>
    /// Interaction logic for AddInfo.xaml
    /// </summary>
    public partial class AddInfo : Window
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString();
        public AddInfo()
        {
            InitializeComponent();
            lbAdd.Content ="TÊN " +Username.button;
        }

        private string IDShop(string nameShop)
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
                finally {
                    myconn.Close();
                }
            }
            return result;
        }
        private void btnAddPos_Click(object sender, RoutedEventArgs e)
        {
            if (tbAdd.Text.Trim() != "")
            {
                string ID = DateTime.Now.ToString("yyyyMMddHHmmss") + tbAdd.Text.Substring(0, 2);
                using (SqlConnection myconn = new SqlConnection(conn))
                {
                    try
                    {
                        string s = "";
                        if (Username.button == "XƯỞNG: ")
                        {
                            s = string.Format("insert into [QTSX].[dbo].[QC_INFORMATION_SHOP]([IDShop],[Shop_name],[OK]) values ('{0}','{1}',{2}) ", ID, tbAdd.Text, 1);
                        }
                        else if (Username.button == "TỔ: ")
                        {
                            s = string.Format("insert into [QTSX].[dbo].[QC_INFORMATION_SECTION]([IDSection],[Section_name],[IDShop]) values ('{0}','{1}','{2}') ", Username.currentShop.Substring(0, 2) + DateTime.Now.ToString("yyyMMddHHmmss"), tbAdd.Text, IDShop(Username.currentShop));

                        }
                        else
                            s = string.Format("insert into [QTSX].[dbo].[QC_INFORMATION_STATION]([IDStation],[Station_name],[IDShop]) values ('{0}','{1}','{2}') ", Username.currentShop.Substring(0, 2) + "POSIT" + DateTime.Now.ToString("yyyMMddHHmmss"), tbAdd.Text, IDShop(Username.currentShop));


                        MessageBoxResult result = MessageBox.Show("Bạn có muốn thêm thông tin?", "CẢNH BÁO", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        switch (result)
                        {
                            case MessageBoxResult.OK:
                                myconn.Open();
                                SqlCommand cmd = new SqlCommand(s, myconn);
                                cmd.ExecuteNonQuery();
                                //MessageBox.Show(s);


                                MessageBox.Show("Đã cập nhật thành công", "THÔNG BÁO", MessageBoxButton.OK, MessageBoxImage.Information);
                                Username.finishAdd = true;
                                tbAdd.Text = "";
                                //tbAdd.Text = s;
                                break;
                            case MessageBoxResult.Cancel:
                                break;
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
            
        }
    }
}
