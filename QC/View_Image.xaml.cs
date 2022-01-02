using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;

namespace QC
{
    /// <summary>
    /// Interaction logic for View_Image.xaml
    /// </summary>
    public partial class View_Image : Window
    {
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString();
        public View_Image()
        {
            InitializeComponent();
            if (Username.img2 == true)
                load_image2();
            else load_image();
        }
        public class ID
        {
            public string getID { get; set; }
        }
        private void load_image2()
        {
            if (Username.url_image != "")
            {
                var uri = new Uri(Username.url_image);
                img.Source = new BitmapImage(uri);
            }
            else if (get_base2(Username.ID).Length < 200)
            {
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(get_base2(Username.ID));
                logo.EndInit();
                img.Source = logo;
            }

            else
            {
                if (get_base2(Username.ID) == "")
                {
                    img.Source = null;
                }
                else
                {
                    try
                    {

                        byte[] binaryData = Convert.FromBase64String(get_base2(Username.ID));
                        if (binaryData != null)
                        {
                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = new MemoryStream(binaryData);
                            bi.EndInit();


                            img.Source = bi;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Lỗi không load được hình", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }


        }
        private void load_image()
        {
            if (Username.url_image != "")
            {
                var uri = new Uri(Username.url_image);
                img.Source = new BitmapImage(uri);
            }
            else if (get_base(Username.ID).Length < 200)
            {
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri(get_base(Username.ID));
                logo.EndInit();
                img.Source = logo;
            }

            else
            {
                if (get_base(Username.ID) == "")
                {
                    img.Source = null;
                }
                else
                {
                    try
                    {

                        byte[] binaryData = Convert.FromBase64String(get_base(Username.ID));
                        if (binaryData != null)
                        {
                            BitmapImage bi = new BitmapImage();
                            bi.BeginInit();
                            bi.StreamSource = new MemoryStream(binaryData);
                            bi.EndInit();


                            img.Source = bi;
                        }
                    }
                    catch (Exception ex)
                    {
                        System.Windows.MessageBox.Show("Lỗi không load được hình", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
         

        }
        private string get_base(string ID)
        {
            string base1 = "";
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                try
                {
                    string s = "SELECT  IMG FROM QTSX.dbo.QC_INFOMATION_PROBLEMS where ID='" + ID + "'";
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
                        base1 = "\\\\10.40.12.6\\qc\\QCerror\\thongkeloi\\assets\\img\\QC\\" + split[split.Length - 1];
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
        private string get_base2(string ID)
        {
            string base1 = "";
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                try
                {
                    string s = "SELECT  IMG2 FROM QTSX.dbo.QC_INFOMATION_PROBLEMS where ID='" + ID + "'";
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
                        base1 = "\\\\10.40.12.6\\qc\\QCerror\\thongkeloi\\assets\\img\\QC\\" + split[split.Length - 1];
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
        private void Save_img_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            //    //dlg.FileName = "" + itemlocal.IDError + ""; // Default file name
            //    dlg.DefaultExt = ".jpg"; // Default file extension
            //    dlg.Filter = "Image cua mi (.jpg)|*.jpg"; // Filter files by extension
            //    dlg.ShowDialog();
            //    string filePath = dlg.FileName;


            //    var encoder = new PngBitmapEncoder();
            //    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)img.Source));
            //    using (FileStream stream = new FileStream(filePath, FileMode.Create))
            //        encoder.Save(stream);
            //    System.Windows.MessageBox.Show("Đã lưu hình ảnh", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.MessageBox.Show("Loi luu hinh anh" + ex.Message);
            //}

        }
    }
}
