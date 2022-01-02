using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
using Path = System.IO.Path;

namespace QC
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString();
        string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["conn1"].ToString();
        //_ItemInfo itemlocal = new _ItemInfo();
        
        public UserControl1()
        {
            InitializeComponent();
            load_timeDetect();
            load_timeProduct();
            load_4M();
            load_shop();
            load_Mandf();
            load_position("");
            load_error("");
            dpDate.SelectedDate = DateTime.Now;

            btn_img.Click += Btn_img_Click;
            btn_img1.Click += Btn_img1_Click;
        }

        private void Btn_img1_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();

            if (img1.Source != null)
            {
                Username.url_image = (img1.Source as BitmapImage).UriSource.LocalPath.ToString();
                View_Image img2 = new View_Image();
                img2.Show();

            }
           
        }

        private void Btn_img_Click(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            if (img.Source != null)
            {
                Username.url_image = (img.Source as BitmapImage).UriSource.LocalPath.ToString();
                View_Image img2 = new View_Image();
                img2.Show();
            }
            

        }

        //public class _ItemInfo
        //{
        //    public string IDError { set; get; }
        //    public string Vincode { set; get; }
        //    public string Model { set; get; }
        //    public string Date { set; get; }
        //    public string Shop { set; get; }
        //    public string Section { get; set; }
        //    public string Station { set; get; }
        //    public string Position { set; get; }
        //    public int Amount { set; get; }
        //    public string Type { set; get; }
        //    public string Descript { set; get; }

        //    public string Human { get; set; }
        //    public string Timedetect { get; set; }
        //    public string TimeProduct { get; set; }
        //    public string LOT { get; set; }
        //    public string T4M { get; set; }
        //    public string Cause { get; set; }
        //    public string Solution { get; set; }
        //    public string Note { get; set; }

        //    public string Kindman { get; set; }
        //    public string Report { get; set; }
        //    public string Contract_no { get; set; }


        //}

        private void Detail_Loaded(object sender, RoutedEventArgs e)
        {

        }

        //private void loadimg()
        //{
        //    byte[] binaryData = Convert.FromBase64String(get_base());

        //    BitmapImage bi = new BitmapImage();
        //    bi.BeginInit();
        //    bi.StreamSource = new MemoryStream(binaryData);
        //    bi.EndInit();
        //    img.Source = bi;
        //}
        //private void load_item()
        //{
        //    load_position(itemlocal.Shop);
        //    load_error(itemlocal.Shop);
        //    load_4M();
        //    load_shop();
        //    load_Mandf();
        //    get_string(null);
        //    tbID.Text = itemlocal.IDError;
        //    tbVin.Text = itemlocal.Vincode;
        //    tbModel.Text = itemlocal.Model;
        //    tbAmount.Text = itemlocal.Amount.ToString();
        //    cbType.SelectedItem = itemlocal.Type;
        //    tbDesc.Text = itemlocal.Descript;
        //    cb4M.SelectedItem = itemlocal.T4M;
        //    cbShop.SelectedItem = itemlocal.Shop;

        //    cbDefect.SelectedItem = itemlocal.Timedetect;
        //    cbProduct.SelectedItem = itemlocal.TimeProduct;
        //    cbPosition.SelectedItem = itemlocal.Position;
        //    tb4m.Text = itemlocal.Human;
        //    tbNote.Text = itemlocal.Note;
        //    tbCause.Text = itemlocal.Cause;
        //    tbSolution.Text = get_solution(itemlocal.IDError);
        //    lbContract.Text = itemlocal.Contract_no;
        //    tbLot.Content = itemlocal.LOT;
        //    cbMandf.SelectedItem = itemlocal.Kindman;
        //    cbReport.IsChecked = load_checkReport(itemlocal.IDError);


        //}
        private bool load_checkReport(string ID)
        {
            bool result = false;
            string s = string.Format("select Report from QTSX.dbo.QC_INFOMATION_PROBLEMS where ID='{0}'", ID);
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                try
                {
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        result = Convert.ToBoolean(read["Report"].ToString());
                    }

                }
                catch (Exception exx)
                {
                    MessageBox.Show("load check report " + exx.Message);
                }
                finally
                {
                    myconn.Close();
                }
            }

            return result;
        }

        private string get_solution(string ID)
        {
            string result = "";
            string query = "SELECT * FROM QTSX.dbo.QC_INFOMATION_PROBLEMS where ID='" + ID + "' ";
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                try
                {
                    myconn.Open();
                    if (myconn.State == System.Data.ConnectionState.Open)
                    {
                        SqlCommand cmd = new SqlCommand(query, myconn);
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            result = read["SOLUTED"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("RESULT: " + ex.Message);
                }
                finally
                {
                    myconn.Close();
                }
            }
            return result;
        }
        //private string get_string(string s)
        //{
        //    string result = "";
        //    string query = "SELECT * FROM QTSX.dbo.QC_INFOMATION_PROBLEMS where ID='" + itemlocal.IDError + "' ";
        //    using (SqlConnection myconn = new SqlConnection(conn))
        //    {
        //        try
        //        {
        //            myconn.Open();
        //            if (myconn.State == System.Data.ConnectionState.Open)
        //            {
        //                SqlCommand cmd = new SqlCommand(query, myconn);
        //                SqlDataReader read = cmd.ExecuteReader();
        //                while (read.Read())
        //                {
        //                    //result = read[s].ToString();
        //                    itemlocal.Vincode = read["VIN_CODE"].ToString();
        //                    itemlocal.Model = read["MODEL"].ToString();
        //                    itemlocal.Amount = Convert.ToInt32(read["AMOUNT_ERROR"].ToString());
        //                    itemlocal.Type = read["TYPE_ERROR"].ToString();
        //                    itemlocal.Descript = read["DESC_ERROR"].ToString();
        //                    itemlocal.T4M = read["M4M"].ToString();
        //                    itemlocal.Shop = read["SHOP"].ToString();
        //                    itemlocal.Section = read["SECTION"].ToString();
        //                    itemlocal.Station = read["STATION"].ToString();
        //                    itemlocal.Timedetect = read["DETECT_TIME"].ToString();
        //                    itemlocal.TimeProduct = read["PRODUCT_TIME"].ToString();
        //                    itemlocal.Position = read["POSITION"].ToString();
        //                    itemlocal.Human = read["RESPON"].ToString();
        //                    itemlocal.Note = read["NOTE"].ToString();
        //                    itemlocal.Cause = read["CAUSE"].ToString();
        //                    itemlocal.Solution = read["SOLUTED"].ToString();
        //                    itemlocal.Contract_no = read["CONTRACT_NO"].ToString();
        //                    itemlocal.LOT = read["LOT"].ToString();
        //                    itemlocal.Kindman = read["KINDMAN"].ToString();


        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("RESULT: " + ex.Message);
        //        }
        //        finally
        //        {
        //            myconn.Close();
        //        }
        //    }
        //    return result;
        //}
        public byte[] Base64ToImage(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            return bytes;
        }
        //private string get_base()
        //{
        //    string base1 = "";
        //    using (SqlConnection myconn = new SqlConnection(conn))
        //    {
        //        try
        //        {
        //            string s = "SELECT  * FROM QTSX.dbo.QC_INFOMATION_PROBLEMS where ID='" + itemlocal.IDError + "'";
        //            myconn.Open();
        //            SqlCommand cmd = new SqlCommand(s, myconn);
        //            SqlDataReader read = cmd.ExecuteReader();
        //            while (read.Read())
        //            {
        //                base1 = read["IMG"].ToString();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show("loi get tring base" + ex.Message);
        //        }
        //        finally
        //        {
        //            myconn.Close();
        //        }
        //    }
        //    return base1;
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// load cho các combobox
        /// </summary>
        private void load_timeDetect()
        {
            string s = string.Format("select * from [QTSX].[dbo].[QC_INFORMATION_TIME]");
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString()))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(s, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        cbDefect.Items.Add(read["Detect_Time"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi get ID lỗi ", ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
        }
        private void load_timeProduct()
        {
            string s = string.Format("select * from [QTSX].[dbo].[QC_INFORMATION_TIME]");
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString()))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(s, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        cbProduct.Items.Add(read["Product_Time"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi get ID lỗi ", ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
        }

        private void load_position(string IDshop)
        {
            cbPosition.Items.Clear();

            string s = string.Format("Select IDShop FROM [QTSX].[dbo].[QC_INFORMATION_SHOP] where [Shop_name]=N'{0}'", IDshop);
            if (IDshop == null || IDshop == "")
            {
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        string sql = string.Format("select DISTINCT Position_name from [QTSX].[dbo].[QC_INFORMATION_POSITION]");
                        {
                            SqlCommand cmd_pos = new SqlCommand(sql, mycon);
                            SqlDataReader read_pos = cmd_pos.ExecuteReader();
                            while (read_pos.Read())
                            {
                                cbPosition.Items.Add(read_pos["Position_name"].ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Cảnh báo: ", ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            }
            else
            {
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        SqlCommand cmd = new SqlCommand(s, mycon);
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            string sql = string.Format("select DISTINCT Position_name from [QTSX].[dbo].[QC_INFORMATION_POSITION] where IDShop=N'" + read["IDShop"].ToString() + "'");

                            {
                                SqlCommand cmd_pos = new SqlCommand(sql, mycon);
                                SqlDataReader read_pos = cmd_pos.ExecuteReader();
                                while (read_pos.Read())
                                {
                                    cbPosition.Items.Add(read_pos["Position_name"].ToString());
                                }
                            }


                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Cảnh báo: ", ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            }

        }

        private void load_error(string shop)
        {
            cbType.Items.Clear();
            string s = string.Format("Select IDShop FROM [QTSX].[dbo].[QC_INFORMATION_SHOP] where [Shop_name]=N'{0}'", shop);
            if (shop == null || shop == "")
            {
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        s = string.Format("select DISTINCT Error_name from [QTSX].[dbo].[QC_INFORMATION_ERROR] ");
                        {
                            SqlCommand cmd_pos = new SqlCommand(s, mycon);
                            SqlDataReader read_pos = cmd_pos.ExecuteReader();
                            while (read_pos.Read())
                            {
                                cbType.Items.Add(read_pos["Error_name"].ToString());
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Cảnh báo: ", ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            }
            else
            {
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString()))
                {
                    try
                    {
                        mycon.Open();
                        SqlCommand cmd = new SqlCommand(s, mycon);
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {

                            s = string.Format("select DISTINCT Error_name from [QTSX].[dbo].[QC_INFORMATION_ERROR] where IDShop='" + read["IDShop"].ToString() + "'");

                            {
                                SqlCommand cmd_pos = new SqlCommand(s, mycon);
                                SqlDataReader read_pos = cmd_pos.ExecuteReader();
                                while (read_pos.Read())
                                {
                                    cbType.Items.Add(read_pos["Error_name"].ToString());
                                }
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Cảnh báo: ", ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }
            }

        }

        private void load_4M()
        {
            cb4M.Items.Clear();
            string s = string.Format("select * from [QTSX].[dbo].[QC_INFORMATION_4M]");
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString()))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(s, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        cb4M.Items.Add(read["NAME"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi get ID lỗi ", ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
        }

        private void load_shop()
        {
            string s = string.Format("Select * from [QTSX].[dbo].[QC_INFORMATION_SHOP] where OK='1'");
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString()))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(s, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        cbShop.Items.Add(read["Shop_name"].ToString());
                        cbShopDetect.Items.Add(read["Shop_name"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cảnh báo: ", ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
        }
        private void cbShop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbType.SelectedItem != null)
            {
                string error = cbType.SelectedValue.ToString().Trim();
                cbType.Items.Clear();
                cbType.Items.Add(error);
                cbType.SelectedIndex = 1;
            }



            if (cbShop.SelectedItem != null)
            {
                using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString()))
                {
                    string s = string.Format("select IDShop from [QTSX].[dbo].[QC_INFORMATION_SHOP] where Shop_name=N'{0}'", cbShop.SelectedItem.ToString().Trim());
                    try
                    {
                        mycon.Open();
                        SqlCommand cmd = new SqlCommand(s, mycon);
                        SqlDataReader read = cmd.ExecuteReader();
                        while (read.Read())
                        {
                            cbSection.Items.Clear();
                            cbStation.Items.Clear();
                            //cbSection.Items.Add("Khác/ Other");
                            cbStation.Items.Add("Khác/ Other");

                            load_section(read["IDShop"].ToString());
                            load_station(read["IDShop"].ToString());

                            load_error(cbShop.SelectedValue.ToString().Trim());
                            //cbSection.SelectedItem = itemlocal.Section;
                            //cbStation.SelectedItem = itemlocal.Station;

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi get name cho các station và section", ex.Message);
                    }
                    finally
                    {
                        mycon.Close();
                    }
                }

            }
        }
        private void cbSection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void load_section(string IDshop)
        {

            string s = string.Format("Select * from [QTSX].[dbo].[QC_INFORMATION_SECTION] where IDShop='{0}'", IDshop);
            using (SqlConnection mycon = new SqlConnection(conn))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(s, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {


                        cbSection.Items.Add(read["Section_name"].ToString());


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cảnh báo: lỗi load section ", ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }

        }
        private void load_station(string IDshop)
        {

            string s = string.Format("Select * from [QTSX].[dbo].[QC_INFORMATION_STATION] where IDShop='{0}'", IDshop);
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString()))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(s, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {


                        cbStation.Items.Add(read["Station_name"].ToString());


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cảnh báo: lỗi load station ", ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }

        }

        private bool Report()
        {
            bool result = false;
            if (cbReport.IsChecked == true)
            {
                result = true;
            }

            return result;
        }

        private void load_Mandf()
        {
            string s1 = string.Format("select * from [QTSX].[dbo].[QC_INFORMATION_MANDF] order by ID desc");
            using (SqlConnection myconn1 = new SqlConnection(conn))
            {
                try
                {
                    myconn1.Open();
                    SqlCommand cmd1 = new SqlCommand(s1, myconn1);
                    SqlDataReader read1 = cmd1.ExecuteReader();

                    while (read1.Read())
                    {
                        cbMandf.Items.Add(read1["NAME"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Cảnh báo lỗi load_4m_mandf: " + ex.Message);
                }
                finally
                {
                    myconn1.Close();
                }
            }
        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        ///
        public string ID;
       

        private void btn_img_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            //    dlg.FileName = "" + itemlocal.IDError + ""; // Default file name
            //    dlg.DefaultExt = ".jpg"; // Default file extension
            //    dlg.Filter = "Image cua mi (.jpg)|*.jpg"; // Filter files by extension
            //    dlg.ShowDialog();
            //    string filePath = dlg.FileName;
            //    MessageBox.Show(filePath);

            //    var encoder = new PngBitmapEncoder();
            //    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)img.Source));
            //    using (FileStream stream = new FileStream(filePath, FileMode.Create))
            //        encoder.Save(stream);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Loi luu hinh anh" + ex.Message);
            //}
            
           

        }

        private string convertbase64(string path)
        {
            string result = "";
            if (path != "")
            {
                try
                {

                    byte[] imageArray = System.IO.File.ReadAllBytes(path);
                    result = Convert.ToBase64String(imageArray);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hình ảnh ", "Cảnh báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }


            return result;

        }
        Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                dlg.ShowDialog();
                string path = dlg.FileName;
                var uri = new Uri(path);
                img.Source = new BitmapImage(uri);
                //using (SqlConnection myconn = new SqlConnection(conn))
                //{

                //    try
                //    {
                //        string path1 = ((BitmapImage)img.Source).UriSource.AbsolutePath;
                //        string s = " [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] set IMG='" + convertbase64(path) + "' where ID='" + itemlocal.IDError + "'";
                //        myconn.Open();
                //        SqlCommand cmd = new SqlCommand(s, myconn);
                //        cmd.ExecuteNonQuery();
                //        MessageBox.Show("Đã lưu thông tin thành công");
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show("Lỗi upload ảnh" + ex.Message);
                //    }
                //    finally
                //    {
                //        myconn.Close();
                //    }

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi upload anh" + ex.Message);
            }

        }
        private string getID_error(string error)
        {
            string result = "";
            string s = string.Format("select * from [QTSX].[dbo].[QC_INFORMATION_ERROR] where [Error_name]=N'{0}'", error);
            using (SqlConnection mycon = new SqlConnection(conn))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(s, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        result = read["IDError"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi get ID " + ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
            return result;
        }
        private string check_id()
        {
            string result = "";
            int seq = 0;
            string s = string.Format("select TOP 1 SEQ  FROM [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] order by SEQ desc");
            using (SqlConnection mycon = new SqlConnection(conn))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(s, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        seq = Convert.ToInt32(read["SEQ"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi get SEQ " + ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
            if (seq == 0)
            {
                seq = seq + 1;
            }
            else
            {
                seq = seq + 1;
                result = seq.ToString();
            }

            return result;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            using (SqlConnection myconn = new SqlConnection(conn))
            {
                //string img2 = convertbase64(dlg.FileName.ToString());
                //string pic = ((BitmapImage)img.Source).UriSource.ToString();
                string c = "",image2="";
                if (img.Source != null)
                {
                    c = convertbase64((img.Source as BitmapImage).UriSource.LocalPath.ToString());
                }
                if (img1.Source != null)
                {
                    image2 = convertbase64((img1.Source as BitmapImage).UriSource.LocalPath.ToString());
                }

                tbID.Text = "";
                string report = "false";
                if (cbReport.IsChecked == true)
                    report = "true";
                try
                {
                    tbID.Text = DateTime.Now.ToString("yyMMdd") + getID_error(cbType.SelectedValue.ToString()) + check_id();
                    string sql = "insert into QTSX.dbo.QC_INFOMATION_PROBLEMS(ID,VIN_CODE,MODEL,DATETIME,SHOP,SECTION,STATION,POSITION,AMOUNT_ERROR,TYPE_ERROR,DESC_ERROR,IMG,IMG2,RESPON,DETECT_TIME,PRODUCT_TIME,LOT,M4M,CAUSE,SOLUTED,NOTE,SEQ, KINDMAN,Report,CONTRACT_NO) " +
                            "values('" + tbID.Text + "',N'" + tbVin.Text + "',N'" + tbModel.Text + "'," +
                            "'" + dpDate.SelectedDate.Value.ToString("yyyy-MM-dd") + " "+DateTime.Now.ToString("HH:mm:ss")+ "',N'" + cbShop.SelectedValue.ToString() + "'," +
                            "N'" + cbSection.SelectedValue.ToString() + "',N'" + cbStation.SelectedValue.ToString() + "',N'" + cbPosition.SelectedValue.ToString() + "'," +
                            "'" + tbAmount.Text + "',N'" + cbType.SelectedValue.ToString() + "',N'" + tbDesc.Text + "','" + c + "','"+image2+"',N'" + tb4m.Text + "'," +
                            "'" + cbDefect.SelectedItem.ToString() + "','" + cbProduct.SelectedValue.ToString() + "','" + tbLot.Content + "',N'" + cb4M.SelectedValue.ToString() + "'," +
                            "N'" + tbCause.Text + "',N'" + tbSolution.Text + "',N'" + tbNote.Text + "',N'" + check_id() + "',N'" + cbMandf.SelectedValue.ToString() + "','" + report + "','" + lbContract.Text + "')";

                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(sql, myconn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã lưu thông tin thành công", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Xin vui lòng nhập đủ thông tin các trường \n" + ex.Message);
                }
                finally
                {
                    myconn.Close();
                }



            }
        }

        private void cbReport_Checked(object sender, RoutedEventArgs e)
        {

        }

        private string fullVIN(string vin)
        {
            string result = "";
            string s = "select * from QTSX.dbo.BodyData where BarCode like '%" + vin + "%'";
            using (SqlConnection mycon = new SqlConnection(conn1))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(s, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        result = read["BarCode"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi get full vin" + ex.Message);
                }
                finally
                {
                    mycon.Close();
                }

            }
            return result;
        }
        private string Model(string VIN)
        {
            string result = "";
            string s = "select NameMode from QTSX.dbo.BodyData where BarCode='" + VIN + "'";
            using (SqlConnection myconn = new SqlConnection(conn1))
            {
                try
                {
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        result = read["NameMode"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi get Model" + ex.Message);

                }
                finally
                {
                    myconn.Close();
                }
            }
            return result;
        }
        private string get_LOT(string vincode)
        {
            string result = "";
            string s = string.Format(" select top 1 * from [QTSX].[dbo].[BodyData] where BarCode='{0}'", vincode);
            using (SqlConnection mycon = new SqlConnection(conn1))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(s, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        result = read["LOT"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi get Lot" + ex.Message);

                }
                finally
                {
                    mycon.Close();
                }
            }

            return result;
        }
        private string contract_No(string vin)
        {
            string result = "";
            string s = string.Format("select [Contract]  FROM [QTSX].[dbo].[BodyData] where BarCode='{0}'", vin);
            using (SqlConnection mycon = new SqlConnection(conn1))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(s, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        result = read["Contract"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi get Contract" + ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }

            return result;
        }
        private void tbVin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                string vin = fullVIN(tbVin.Text);
                tbVin.Text = vin;
                string model = Model(vin);
                tbLot.Content = get_LOT(vin);
                tbModel.Text = model;
                lbContract.Text = contract_No(vin);
            }
        }

        private void btnCoppy_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.Clipboard.ContainsImage())
            {
                System.Windows.Forms.IDataObject clipboardData = System.Windows.Forms.Clipboard.GetDataObject();
                if (clipboardData != null)
                {
                    if (clipboardData.GetDataPresent(System.Windows.Forms.DataFormats.Bitmap))
                    {
                        System.Drawing.Bitmap bitmap = (System.Drawing.Bitmap)clipboardData.GetData(System.Windows.Forms.DataFormats.Bitmap);
                        IntPtr hBitmap = bitmap.GetHbitmap();
                        try
                        {
                            //img.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                            try
                            {
                                string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString();
                                string filePath = path + "\\Image\\" + DateTime.Now.ToString("yyyMMddHHmmss") + ".jpg";
                                string folderName = @".\Image";
                                if (!Directory.Exists(folderName))
                                {

                                    Directory.CreateDirectory(folderName);

                                }


                                var encoder = new PngBitmapEncoder();
                                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())));
                                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                                    encoder.Save(stream);


                                //dlg.FileName = filePath;
                                var uri = new Uri(filePath);
                                img.Source = new BitmapImage(uri);


                            }
                            catch (Exception ex)
                            {
                                System.Windows.MessageBox.Show("Loi luu hinh anh" + ex.Message);
                            }


                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            DeleteObject(hBitmap);
                        }
                    }
                }
            }

        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private void cbShopDetect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbPosition.Items.Clear();
            using(SqlConnection myconn = new SqlConnection(conn))
            {
                
                try
                {
                    myconn.Open();
                    string sql = string.Format("select *  FROM [QTSX].[dbo].[QC_INFORMATION_SHOP] where Shop_name='{0}'", cbShopDetect.SelectedValue.ToString());
                    SqlCommand cmd1 = new SqlCommand(sql, myconn);
                    SqlDataReader read1 = cmd1.ExecuteReader();
                    read1.Read();

                    string s = string.Format("Select *  FROM [QTSX].[dbo].[QC_INFORMATION_POSITION] where IDShop='{0}'",read1["IDShop"].ToString());
                
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        cbPosition.Items.Add(read["Position_name"].ToString());
                    }
                }
                catch
                {
                    MessageBox.Show("Sai thông tin Xưởng phát hiện","ERRROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    myconn.Close();
                }
            }
        }

        private void btn_img1_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCoppy1_Click(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.Clipboard.ContainsImage())
            {
                System.Windows.Forms.IDataObject clipboardData = System.Windows.Forms.Clipboard.GetDataObject();
                if (clipboardData != null)
                {
                    if (clipboardData.GetDataPresent(System.Windows.Forms.DataFormats.Bitmap))
                    {
                        System.Drawing.Bitmap bitmap = (System.Drawing.Bitmap)clipboardData.GetData(System.Windows.Forms.DataFormats.Bitmap);
                        IntPtr hBitmap = bitmap.GetHbitmap();
                        try
                        {
                            //img.Source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                            try
                            {
                                string path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString();
                                string filePath = path + "\\Image\\" + DateTime.Now.ToString("yyyMMddHHmmss") + ".jpg";
                                string folderName = @".\Image";
                                if (!Directory.Exists(folderName))
                                {

                                    Directory.CreateDirectory(folderName);

                                }


                                var encoder = new PngBitmapEncoder();
                                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions())));
                                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                                    encoder.Save(stream);


                                //dlg.FileName = filePath;
                                var uri = new Uri(filePath);
                                img1.Source = new BitmapImage(uri);


                            }
                            catch (Exception ex)
                            {
                                System.Windows.MessageBox.Show("Loi luu hinh anh" + ex.Message);
                            }


                        }
                        catch (Exception ex)
                        {
                            System.Windows.MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            DeleteObject(hBitmap);
                        }
                    }
                }
            }
        }

        private void btnUpload1_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                dlg.ShowDialog();
                string path = dlg.FileName;
                var uri = new Uri(path);
                img1.Source = new BitmapImage(uri);
                //using (SqlConnection myconn = new SqlConnection(conn))
                //{

                //    try
                //    {
                //        string path1 = ((BitmapImage)img.Source).UriSource.AbsolutePath;
                //        string s = " [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] set IMG='" + convertbase64(path) + "' where ID='" + itemlocal.IDError + "'";
                //        myconn.Open();
                //        SqlCommand cmd = new SqlCommand(s, myconn);
                //        cmd.ExecuteNonQuery();
                //        MessageBox.Show("Đã lưu thông tin thành công");
                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show("Lỗi upload ảnh" + ex.Message);
                //    }
                //    finally
                //    {
                //        myconn.Close();
                //    }

                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi upload anh" + ex.Message);
            }
        }
    }
}
