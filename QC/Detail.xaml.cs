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
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Threading;

namespace QC
{
    /// <summary>
    /// Interaction logic for Detail.xaml
    /// </summary>
    public partial class Detail : Window
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString();
        string pathConfig = System.Configuration.ConfigurationManager.AppSettings["path"].ToString();
        //string id = "", vincode = "", model = "", amount = "",type="",descript="",rebon="",shop="",section="",station="",cause="",solution="";
        _ItemInfo itemlocal = new _ItemInfo();
        public Detail(_ItemInfo item)
        {
            InitializeComponent();
            itemlocal = item;
            Loaded += Detail_Loaded;

            this.Title = System.Configuration.ConfigurationManager.AppSettings["building"].ToString();
            loadimg();
            loadimg1();
            load_item();
            load_timeDetect();
            load_timeProduct();
        }
        public class _ItemInfo
        {
            public string IDError { set; get; }
            public string Vincode { set; get; }
            public string Model { set; get; }
            public string Date { set; get; }
            public string Shop { set; get; }
            public string Section { get; set; }
            public string Station { set; get; }
            public string Position { set; get; }
            public int Amount { set; get; }
            public string Type { set; get; }
            public string Descript { set; get; }

            public string Human { get; set; }
            public string Timedetect { get; set; }
            public string TimeProduct { get; set; }
            public string LOT { get; set; }
            public string T4M { get; set; }
            public string Cause { get; set; }
            public string Solution { get; set; }
            public string Note { get; set; }

            public string Kindman { get; set; }
            public string Report { get; set; }
            public string Contract_no { get; set; }
            public string level { get; set; }


        }

        private void Detail_Loaded(object sender, RoutedEventArgs e)
        {
            //throw new NotImplementedException();
            //loadimg();
            //load_item();
            //load_timeDetect();
            //load_timeProduct();

        }

        private void loadimg()
        {
            try
            {
                path1Source = get_base();
                if (path1Source == "")
                {
                    img.Source = null;
                }
                else if (path1Source.Length < 200)
                {
                    string currentAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string currentAssemblyParentPath = Path.GetDirectoryName(currentAssemblyPath);

                    //img.Source = new BitmapImage(new Uri(String.Format("file:\\\\\\{0}\\"+ pathConfig + "\\" + path1Source, currentAssemblyParentPath)));
                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    logo.CacheOption = BitmapCacheOption.OnLoad;
                    logo.UriSource = new Uri(String.Format("file:\\\\\\{0}\\" + pathConfig + "\\" + path1Source, currentAssemblyParentPath));
                    logo.EndInit();
                    logo.Freeze();
                    img.Source = logo;



                }
                else
                {


                    byte[] binaryData = Convert.FromBase64String(get_base());
                    if (binaryData != null)
                    {
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = new MemoryStream(binaryData);
                        bi.EndInit();


                        img.Source = bi;
                    }


                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Lỗi không load được hình", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void loadimg1()
        {
            try
            {

                path2Source = get_base2();
                if (path2Source == "")
                {
                    img1.Source = null;
                }
                else if (path2Source.Length < 200)
                {
                    string currentAssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    string currentAssemblyParentPath = Path.GetDirectoryName(currentAssemblyPath);

                    //img1.Source = new BitmapImage(new Uri(String.Format("file:\\\\\\{0}\\"+ pathConfig + "\\" + path2Source, currentAssemblyParentPath)));
                    BitmapImage logo = new BitmapImage();
                    logo.BeginInit();
                    logo.CacheOption = BitmapCacheOption.OnLoad;
                    logo.UriSource = new Uri(String.Format("file:\\\\\\{0}\\" + pathConfig + "\\" + path2Source, currentAssemblyParentPath));
                    logo.EndInit();
                    logo.Freeze();
                    img1.Source = logo;

                }
                else
                {

                    byte[] binaryData1 = Convert.FromBase64String(get_base2());
                    if (binaryData1 != null)
                    {
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.StreamSource = new MemoryStream(binaryData1);
                        bi.EndInit();


                        img1.Source = bi;
                    }


                }


            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Lỗi không load được hình", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void load_item()
        {
            
            load_error(itemlocal.Shop);
            load_4M();
            load_shop();
            load_Mandf();
            get_string(null);
            load_position(null);
            load_level();

            tbID.Text = itemlocal.IDError;
            tbVin.Text = itemlocal.Vincode;
            tbModel.Text = itemlocal.Model;
            tbAmount.Text = itemlocal.Amount.ToString();
            cbType.SelectedItem =Username.gettypeError(itemlocal.Type);
            tbDesc.Text = itemlocal.Descript;
            cb4M.SelectedItem = Username.get4M(itemlocal.T4M);
            cbShop.SelectedItem = Username.getShopError(itemlocal.Shop);
            cbLevel.SelectedItem = Username.getLevelError(itemlocal.level);
            cbDefect.SelectedItem = itemlocal.Timedetect;
            cbProduct.SelectedItem = itemlocal.TimeProduct;
            cbPosition.SelectedItem = Username.getPositionError(itemlocal.Position);
            string posit = itemlocal.Position;
            tb4m.Text = itemlocal.Human;
            tbNote.Text = itemlocal.Note;
            tbCause.Text = itemlocal.Cause;
            tbSolution.Text = get_solution(itemlocal.IDError);
            lbContract.Text = itemlocal.Contract_no;
            tbLot.Content = itemlocal.LOT;
            cbMandf.SelectedItem = Username.getMandfError(itemlocal.Kindman);
            cbReport.IsChecked = load_checkReport(itemlocal.IDError);
            cbStation.SelectedItem = Username.getStationError(itemlocal.Station);
            cbSection.SelectedItem = Username.getSectionError(itemlocal.Section);

        }
        private void load_level()
        {
            string s1 = string.Format("select * from [QTSX].[dbo].[QC_INFORMATION_LEVEL] order by ID desc");
            using (SqlConnection myconn1 = new SqlConnection(conn))
            {
                try
                {
                    myconn1.Open();
                    SqlCommand cmd1 = new SqlCommand(s1, myconn1);
                    SqlDataReader read1 = cmd1.ExecuteReader();

                    while (read1.Read())
                    {
                        cbLevel.Items.Add(read1["LEVEL"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Cảnh báo lỗi load LEVEL: " + ex.Message);
                }
                finally
                {
                    myconn1.Close();
                }
            }
        }
        private bool load_checkReport(string ID)
        {
            bool result = false;
            string s = string.Format("select Report from QTSX.dbo.QC_INFOMATION_PROBLEMS where ID='{0}'", ID);
            string check = "";
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                try
                {
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    read.Read();
                    check = read["Report"].ToString();
                    if (check != "")
                    {
                        result = Convert.ToBoolean(check);
                    }

                }
                catch (Exception exx)
                {
                    System.Windows.MessageBox.Show("load check report " + exx.Message);
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
                    System.Windows.MessageBox.Show("RESULT: " + ex.Message);
                }
                finally
                {
                    myconn.Close();
                }
            }
            return result;
        }
       
       
        private string get_string(string s)
        {
            string result = "";
            string query = "SELECT * FROM QTSX.dbo.QC_INFOMATION_PROBLEMS where ID='" + itemlocal.IDError + "' ";
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
                            //result = read[s].ToString();
                            itemlocal.Vincode = read["VIN_CODE"].ToString();
                            itemlocal.Model = read["MODEL"].ToString();
                            itemlocal.Amount = Convert.ToInt32(read["AMOUNT_ERROR"].ToString());
                            itemlocal.Type = read["TYPE_ERROR"].ToString();
                            itemlocal.Descript = read["DESC_ERROR"].ToString();
                            itemlocal.T4M = read["M4M"].ToString();

                            itemlocal.Shop = read["SHOP"].ToString();
                            itemlocal.Section = read["SECTION"].ToString();
                            itemlocal.Station = read["STATION"].ToString();
                            itemlocal.Timedetect = read["DETECT_TIME"].ToString();
                            itemlocal.TimeProduct = read["PRODUCT_TIME"].ToString();
                            itemlocal.Position = read["POSITION"].ToString();
                            itemlocal.Human = read["RESPON"].ToString();
                            itemlocal.Note = read["NOTE"].ToString();
                            itemlocal.Cause = read["CAUSE"].ToString();
                            itemlocal.Solution = read["SOLUTED"].ToString();
                            itemlocal.Contract_no = read["CONTRACT_NO"].ToString();
                            itemlocal.LOT = read["LOT"].ToString();
                            itemlocal.Kindman = read["KINDMAN"].ToString();


                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("RESULT: " + ex.Message);
                }
                finally
                {
                    myconn.Close();
                }
            }
            return result;
        }
        public byte[] Base64ToImage(string base64String)
        {
            byte[] bytes = Convert.FromBase64String(base64String);
            return bytes;
        }
        private string get_base()
        {
            string base1 = "";
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                try
                {
                    string s = "SELECT  IMG FROM QTSX.dbo.QC_INFOMATION_PROBLEMS where ID='" + itemlocal.IDError + "'";
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        base1 = read["IMG"].ToString();

                    }
                    if (base1.Length < 200)
                    {
                        string[] split = base1.Split('/');

                        File.Copy("\\\\10.40.12.6\\qc\\QCerror\\thongkeloi\\assets\\img\\QC\\" + split[split.Length - 1], "Image\\" + split[split.Length - 1], true);
                        base1 = "Image\\" + split[split.Length - 1];
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("loi get tring base" + ex.Message);
                }
                finally
                {
                    myconn.Close();
                }
            }
            return base1;
        }
        private string get_base2()
        {
            string base1 = "";
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                try
                {
                    string s = "SELECT  IMG2 FROM QTSX.dbo.QC_INFOMATION_PROBLEMS where ID='" + itemlocal.IDError + "'";
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        base1 = read["IMG2"].ToString();
                    }
                    if (base1.Length < 200)
                    {
                        string[] split = base1.Split('/');
                        File.Copy("\\\\10.40.12.6\\qc\\QCerror\\thongkeloi\\assets\\img\\QC\\" + split[split.Length - 1], "Image\\" + split[split.Length - 1], true);
                        base1 = "Image\\" + split[split.Length - 1];
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("loi get tring base" + ex.Message);

                }
                finally
                {
                    myconn.Close();
                }
            }
            return base1;
        }
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
                    System.Windows.MessageBox.Show("Lỗi get ID lỗi ", ex.Message);
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
                    System.Windows.MessageBox.Show("Lỗi get ID lỗi ", ex.Message);
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
                        s = string.Format("select * from [QTSX].[dbo].[QC_INFORMATION_POSITION] ");
                        {
                            SqlCommand cmd_pos = new SqlCommand(s, mycon);
                            SqlDataReader read_pos = cmd_pos.ExecuteReader();
                            while (read_pos.Read())
                            {
                                cbPosition.Items.Add(read_pos["Position_name"].ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Cảnh báo: ", ex.Message);
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
                            s = string.Format("select * from [QTSX].[dbo].[QC_INFORMATION_POSITION] where IDShop=N'" + read["IDShop"].ToString() + "'");
                            {
                                SqlCommand cmd_pos = new SqlCommand(s, mycon);
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
                        System.Windows.MessageBox.Show("Cảnh báo: ", ex.Message);
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
            string s = string.Format("Select IDShop FROM [QTSX].[dbo].[QC_INFORMATION_SHOP] where [Shop_name]=N'{0}'", shop);


            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString()))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(s, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        s = string.Format("select * from [QTSX].[dbo].[QC_INFORMATION_ERROR] where IDShop='" + read["IDShop"].ToString() + "'");
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
                    System.Windows.MessageBox.Show("Cảnh báo: ", ex.Message);
                }
                finally
                {
                    mycon.Close();
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
                    System.Windows.MessageBox.Show("Lỗi get ID lỗi ", ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
        }

        private void load_shop()
        {
            string s = string.Format("Select * from [QTSX].[dbo].[QC_INFORMATION_SHOP] where OK=1");
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
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Cảnh báo: ", ex.Message);
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
                            cbSection.Items.Add("Khác/ Other");
                            cbStation.Items.Add("Khác/ Other");

                            load_section(read["IDShop"].ToString());
                            load_station(read["IDShop"].ToString());

                            load_error(cbShop.SelectedValue.ToString().Trim());
                            cbSection.SelectedItem = itemlocal.Section;
                            cbStation.SelectedItem = itemlocal.Station;

                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Lỗi get name cho các station và section", ex.Message);
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
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString()))
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
                    System.Windows.MessageBox.Show("Cảnh báo: lỗi load section ", ex.Message);
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
                    System.Windows.MessageBox.Show("Cảnh báo: lỗi load station ", ex.Message);
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
                    System.Windows.MessageBox.Show("Cảnh báo lỗi load_4m_mandf: " + ex.Message);
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

        private void btn_img_Click(object sender, RoutedEventArgs e)
        {
            Username.img2 = false;
            Username.url_image = "";
            if (img.Source != null)
            {
                Username.ID = itemlocal.IDError;
                View_Image img2 = new View_Image();
                img2.Show();
            }

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
                    System.Windows.MessageBox.Show("Lỗi hình ảnh ", "Cảnh báo lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
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

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Loi upload anh" + ex.Message);
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

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Loi upload anh" + ex.Message);
            }
        }
        private string rename(string path, string ID)
        {
            string result = "";

            if (path != "")
            {
                File.Copy(path, "\\\\10.40.12.6\\qc\\QCerror\\thongkeloi\\assets\\img\\QC\\" + ID + "-1.jpg", true);
                result = ID + "-1.jpg";
            }



            return result;
        }
        private string rename2(string path, string ID)
        {
            string result = "";


            if (path != "")
            {
                File.Copy(path, "\\\\10.40.12.6\\qc\\QCerror\\thongkeloi\\assets\\img\\QC\\" + ID + "-2.jpg", true);
                result = ID + "-2.jpg";
            }


            return result;
        }
        string path1Source = "", path2Source = "";
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            using (SqlConnection myconn = new SqlConnection(conn))
            {

                string stringimg1 = "", pathPic1 = "", pathPic2 = "", stringimg2 = "", sql = "", id1 = "", id2 = "", updateIMG = "", shop = "", section = "", station = "", position = "", type = "", defect = "", product = "", cb4Mf = "", mandf = "";
                stringimg1 = img.Source.ToString(); stringimg2 = img1.Source.ToString();



                //System.Windows.MessageBox.Show(stringimg1); System.Windows.MessageBox.Show(stringimg2);
                string[] path1 = stringimg1.Split('/'); string[] sourcPic1 = path1Source.Split('\\');
                string[] path2 = stringimg2.Split('/'); string[] sourcPic2 = path2Source.Split('\\');

                string comparelocal = path1[path1.Length - 1]; string compareSource = sourcPic1[sourcPic1.Length - 1];
                string comparelocal1 = path2[path2.Length - 1]; string compareSource1 = sourcPic2[sourcPic1.Length - 1];

                if (comparelocal != compareSource)
                {
                    for (int i = 3; i < path1.Length; i++)
                    {
                        if (i == 3) pathPic1 = path1[i];
                        else pathPic1 = pathPic1 + "\\" + path1[i];
                    }
                    updateIMG = ",IMG='../assets/img/QC/" + rename(pathPic1, tbID.Text.Trim()) + "'";
                }
                if (comparelocal1 != compareSource1)
                {
                    for (int i = 3; i < path2.Length; i++)
                    {
                        if (i == 3) pathPic2 = path2[i];
                        else pathPic2 = pathPic2 + "\\" + path2[i];
                    }
                    string s = rename2(pathPic2, tbID.Text.Trim());
                    updateIMG = updateIMG + ",IMG2='../assets/img/QC/" + s + "'";
                }
                if (cbShop.SelectedItem != null) shop = cbShop.SelectedValue.ToString();
                if (cbSection.SelectedItem != null) section = cbSection.SelectedValue.ToString();
                if (cbStation.SelectedItem != null) station = cbStation.SelectedValue.ToString();
                if (cbPosition.SelectedItem != null) position = cbPosition.SelectedValue.ToString();
                if (cbType.SelectedItem != null) type = cbType.SelectedValue.ToString();
                if (cbDefect.SelectedItem != null) defect = cbDefect.SelectedValue.ToString();
                if (cbProduct.SelectedItem != null) product = cbProduct.SelectedValue.ToString();
                if (cb4M.SelectedItem != null) cb4Mf = cb4M.SelectedValue.ToString();
                if (cbMandf.SelectedItem != null) mandf = cbMandf.SelectedValue.ToString();

                //if (stringimg1 != ""){
                //  id1= "../assets/img/QC/"+ rename(stringimg1, false, tbID.Text.Trim());
                //}
                //if (stringimg2 != "")
                //{
                //    id2= "../assets/img/QC/" + rename(stringimg2, true, tbID.Text.Trim());

                //}



                //if (img.Source != null && (img.Source as BitmapImage).UriSource != null)
                //{

                //        c = "IMG='" + convertbase64((img.Source as BitmapImage).UriSource.LocalPath.ToString()) + "',";  
                //}

                //if (img1.Source != null && (img1.Source as BitmapImage).UriSource != null) {

                //    image2 = "IMG2='" + convertbase64((img1.Source as BitmapImage).UriSource.LocalPath.ToString()) + "',";
                //} 
                try
                {
                    sql = "update QTSX.dbo.QC_INFOMATION_PROBLEMS set VIN_CODE='" + tbVin.Text + "',MODEL='" + tbModel.Text + "', " +
                               "SHOP=N'" + shop + "',SECTION=N'" + section + "',STATION=N'" + station + "',POSITION=N'" + position + "'" +
                               ",AMOUNT_ERROR='" + tbAmount.Text + "',TYPE_ERROR=N'" + type + "',DESC_ERROR=N'" + tbDesc.Text + "' " + updateIMG + ", " +
                               "RESPON=N'" + tb4m.Text + "',DETECT_TIME='" + defect + "',PRODUCT_TIME='" + product + "',LOT='" + tbLot.Content + "'," +
                               "M4M=N'" + cb4Mf + "'" +
                               ",CAUSE=N'" + tbCause.Text + "',NOTE=N'" + tbNote.Text + "',KINDMAN=N'" + mandf +
                               "',SOLUTED=N'" + tbSolution.Text + "',Report='" + Report() + "',CONTRACT_NO='" + lbContract.Text + "' where ID='" + tbID.Text + "'";


                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(sql, myconn);
                    cmd.ExecuteNonQuery();
                    System.Windows.MessageBox.Show("Đã lưu thông tin thành công", "Thông báo", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Lỗi upload ảnh" + ex.Message);
                }
                finally
                {
                    myconn.Close();
                    
                }

            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

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


                                dlg.FileName = filePath;
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




            //string setdata = "da thay doi clipboard";
            //System.Windows.Clipboard.SetData(System.Windows.DataFormats.Text, (Object)setdata);

        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);



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


                                dlg.FileName = filePath;
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            img.Source = null; img1.Source = null;
            Thread.Sleep(1000);
        }

        private void btn_img1_Click(object sender, RoutedEventArgs e)
        {
            Username.img2 = true;
            Username.url_image = "";
            if (img1.Source != null)
            {
                Username.ID = itemlocal.IDError;
                View_Image img2 = new View_Image();
                img2.Show();
            }
        }
    }


}

