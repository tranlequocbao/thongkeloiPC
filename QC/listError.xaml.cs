using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.IO;
using System.Drawing;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ClosedXML.Excel;

namespace QC
{
    /// <summary>
    /// Interaction logic for listError.xaml
    /// </summary>
    public partial class listError : UserControl
    {
        public listError()
        {
            InitializeComponent();
            Loaded += ListError_Loaded;
            cbALL.IsChecked = true;

        }
        string conn = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString();
        string conn1 = System.Configuration.ConfigurationManager.ConnectionStrings["conn1"].ToString();
        private void ListError_Loaded(object sender, RoutedEventArgs e)
        {
            //loadimg();


            load_shop();
            load_TypeError();
            load_Model();
            cbbDateStart.SelectedDate = DateTime.Now;
            cbbDateEnd.SelectedDate = DateTime.Now;
            cbbShop.SelectedIndex = 0;
            cbbType.SelectedIndex = 0;
            cbbModel.SelectedIndex = 0;
            load_listview();

        }
        public class _string
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
        private void load_shop()
        {
            cbbShop.Items.Clear();
            cbbShop.Items.Add("ALL");
            string s = string.Format(" select * from [QTSX].[dbo].[QC_INFORMATION_SHOP]");
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
                        cbbShop.Items.Add(read["Shop_name"].ToString());
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
        }
        private void load_TypeError()
        {
            cbbType.Items.Clear();
            cbbType.Items.Add("ALL");
            string s = string.Format(" select * from [QTSX].[dbo].[QC_INFORMATION_ERROR]");
            using (SqlConnection mycon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString()))
            {
                try
                {
                    mycon.Open();
                    SqlCommand cmd = new SqlCommand(s, mycon);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {

                        cbbType.Items.Add(read["Error_name"].ToString());


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi load dữ liệu combobox loại lỗi " + ex.Message);
                }
                finally
                {
                    mycon.Close();
                }
            }
        }
        private void load_Model()
        {
            cbbModel.Items.Clear();
            cbbModel.Items.Add("ALL");

            string s = "select DISTINCT NameMode from QTSX.dbo.BodyData where NameMode is not null order by NameMode";
            using (SqlConnection myconn = new SqlConnection(conn1))
            {
                try
                {
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(s, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        cbbModel.Items.Add(read["NameMode"].ToString());
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
        }
        private void load_listview()
        {
            string cbb1_shop = "", type_error = "", Model = "", Report = "";
            string s = "select [ID],[VIN_CODE],[MODEL],[DATETIME],[SHOP],[SECTION],[STATION],[POSITION],[AMOUNT_ERROR],[TYPE_ERROR],[DESC_ERROR],[RESPON],[DETECT_TIME],[PRODUCT_TIME],[LOT],[M4M],[CAUSE],[SOLUTED],[NOTE] ,[SEQ],[KINDMAN],[Report],CONTRACT_NO,LEVEL " +
                "from QTSX.dbo.QC_INFOMATION_PROBLEMS where DATETIME  BETWEEN '" + Convert.ToDateTime(cbbDateStart.SelectedDate.Value.ToString()).ToString("MM-dd-yyyy 00:00:00") + "' AND '" + Convert.ToDateTime(cbbDateEnd.SelectedDate.Value.ToString()).ToString("MM-dd-yyyy 23:59:59") + "'";
            if (cbbShop.SelectedValue.ToString() != "ALL")
            {
                cbb1_shop = string.Format(" AND SHOP=N'" + cbbShop.SelectedValue.ToString() + "' ");
            }
            if (cbbType.SelectedValue.ToString() != "ALL")
            {
                type_error = "AND TYPE_ERROR=N'" + cbbType.SelectedValue.ToString() + "' ";
            }
            if (cbbModel.SelectedValue.ToString() != "ALL")
            {
                Model = "AND MODEL=N'" + cbbModel.SelectedValue.ToString() + "' ";
            }
            if (cbALL.IsChecked == false)
            {
                if (cbReport1.IsChecked == false)
                {
                    Report = "AND Report= 'true' ";
                }
                else
                    Report = "AND Report= 'false' ";

            }


            string order_by = " order by DATETIME desc";

            string sql = s + cbb1_shop + type_error + Model + Report + order_by;
            if (tbSearchVIN.Text != "")
            {
                sql = "select [ID],[VIN_CODE],[MODEL],[DATETIME],[SHOP],[SECTION],[STATION],[POSITION],[AMOUNT_ERROR],[TYPE_ERROR],[DESC_ERROR],[RESPON],[DETECT_TIME],[PRODUCT_TIME],[LOT],[M4M],[CAUSE],[SOLUTED],[NOTE] ,[SEQ],[KINDMAN],[Report],CONTRACT_NO, LEVEL " +
               "from QTSX.dbo.QC_INFOMATION_PROBLEMS where  VIN_CODE like '%" + tbSearchVIN.Text + "%'";
            }

            List<_string> data = new List<_string>();
            using (SqlConnection myconn = new SqlConnection(conn))
            {
                try
                {
                    myconn.Open();
                    SqlCommand cmd = new SqlCommand(sql, myconn);
                    SqlDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {

                        data.Add(new _string()
                        {
                            IDError = read["ID"].ToString(),
                            Vincode = read["VIN_CODE"].ToString(),
                            Model = read["MODEL"].ToString(),
                            Date = read["DATETIME"].ToString(),
                            Shop = read["SHOP"].ToString(),
                            Section = read["SECTION"].ToString(),
                            Station = read["STATION"].ToString(),
                            Position = read["POSITION"].ToString(),
                            Amount = Convert.ToInt32(read["AMOUNT_ERROR"].ToString()),
                            Type = read["TYPE_ERROR"].ToString(),
                            Descript = read["DESC_ERROR"].ToString(),
                            Human = read["RESPON"].ToString(),
                            Timedetect = read["DETECT_TIME"].ToString(),
                            TimeProduct = read["PRODUCT_TIME"].ToString(),
                            LOT = read["LOT"].ToString(),
                            T4M = read["M4M"].ToString(),
                            Cause = read["CAUSE"].ToString(),
                            Solution = read["SOLUTED"].ToString(),
                            Note = read["NOTE"].ToString(),
                            Kindman = read["KINDMAN"].ToString(),
                            Report = read["Report"].ToString(),
                            Contract_no = read["CONTRACT_NO"].ToString(),
                            level = read["LEVEL"].ToString()

                        });



                    }
                    lstData.ItemsSource = null;
                    lstData.ItemsSource = data;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi load list view" + ex.Message);
                }
                finally
                {
                    myconn.Close();
                }
            }
        }
        Detail._ItemInfo item;

        private void show_Click(object sender, RoutedEventArgs e)
        {

            item = new Detail._ItemInfo()
            {
                IDError = ((_string)lstData.SelectedItem).IDError.ToString(),
                Vincode = ((_string)lstData.SelectedItem).Vincode.ToString(),
                Model = ((_string)lstData.SelectedItem).Model.ToString(),
                Date = ((_string)lstData.SelectedItem).Date.ToString(),
                Shop = ((_string)lstData.SelectedItem).Shop.ToString(),
                Section = ((_string)lstData.SelectedItem).Section.ToString(),
                Station = ((_string)lstData.SelectedItem).Station.ToString(),
                Position = ((_string)lstData.SelectedItem).Position.ToString(),
                Amount = ((_string)lstData.SelectedItem).Amount,
                Type = ((_string)lstData.SelectedItem).Type.ToString(),
                Descript = ((_string)lstData.SelectedItem).Descript.ToString(),
                Human = ((_string)lstData.SelectedItem).Human.ToString(),
                Timedetect = ((_string)lstData.SelectedItem).Timedetect.ToString(),
                TimeProduct = ((_string)lstData.SelectedItem).TimeProduct.ToString(),
                LOT = ((_string)lstData.SelectedItem).LOT.ToString(),
                T4M = ((_string)lstData.SelectedItem).T4M.ToString(),
                Cause = ((_string)lstData.SelectedItem).Cause.ToString(),
                Solution = ((_string)lstData.SelectedItem).Solution.ToString(),
                Note = ((_string)lstData.SelectedItem).Note.ToString(),
                Kindman = ((_string)lstData.SelectedItem).Kindman.ToString(),
                Report = ((_string)lstData.SelectedItem).Report.ToString(),
                Contract_no = ((_string)lstData.SelectedItem).Contract_no.ToString(),
                level = ((_string)lstData.SelectedItem).level.ToString(),

            };
            Detail show = new Detail(item);
            show.Show();
        }

        private void cbbShop_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (cbbShop.SelectedValue == "ALL")
            //{

            //}
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

            load_listview();
        }

        private void cbALL_Checked(object sender, RoutedEventArgs e)
        {
            cbReport1.IsEnabled = false;
        }

        private void cbALL_Unchecked(object sender, RoutedEventArgs e)
        {
            cbReport1.IsEnabled = true;
        }

        void OnTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {

            if (tbSearchVIN.Text == "")
            {
                // Create an ImageBrush.
                ImageBrush textImageBrush = new ImageBrush();
                textImageBrush.ImageSource =
                    new BitmapImage(
                        new Uri(@"timkiem.PNG", UriKind.Relative)
                    );
                textImageBrush.AlignmentX = AlignmentX.Left;
                textImageBrush.Stretch = Stretch.None;
                // Use the brush to paint the button's background.
                tbSearchVIN.Background = textImageBrush;
            }
            else
            {

                tbSearchVIN.Background = null;
            }
        }

        private void tbSearchVIN_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                load_listview();
            }

        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();
                saveFile.Title = "Browse Text Files";
                saveFile.DefaultExt = "xlsx";
                saveFile.Filter = "Excel files (.xlsx)|*.xlsx|All files (.*)|*.*";
                saveFile.ShowDialog();
                if (saveFile.FileName != "")
                {
                    var wb = new XLWorkbook();
                    var sheet = wb.Worksheets.Add(DateTime.Now.ToString("ddMMyy"));
                    int i = 1, j = 1;
                    sheet.Cell(1, 1).Value = "TT";
                    sheet.Cell(1, 1).Style.Font.Bold = true;
                    sheet.Cell(1, 1).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 1).Style.Font.FontSize = 15;
                    sheet.Cell(1, 1).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 1).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(1).Width = 7;

                    sheet.Cell(1, 2).Value = "THỜI GIAN";
                    sheet.Cell(1, 2).Style.Font.Bold = true;
                    sheet.Cell(1, 2).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 2).Style.Font.FontSize = 15;
                    sheet.Cell(1, 2).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(2).Width = 20;

                    sheet.Cell(1, 3).Value = "LOẠI XE";
                    sheet.Cell(1, 3).Style.Font.Bold = true;
                    sheet.Cell(1, 3).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 3).Style.Font.FontSize = 15;
                    sheet.Cell(1, 3).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(3).Width = 35;

                    sheet.Cell(1, 4).Value = "SỐ VIN";
                    sheet.Cell(1, 4).Style.Font.Bold = true;
                    sheet.Cell(1, 4).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 4).Style.Font.FontSize = 15;
                    sheet.Cell(1, 4).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(4).Width = 15;

                    sheet.Cell(1, 5).Value = "LOT";
                    sheet.Cell(1, 5).Style.Font.Bold = true;
                    sheet.Cell(1, 5).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 5).Style.Font.FontSize = 15;
                    sheet.Cell(1, 5).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(5).Width = 7;

                    sheet.Cell(1, 6).Value = "NƠI PHÁT HIỆN";
                    sheet.Cell(1, 6).Style.Font.Bold = true;
                    sheet.Cell(1, 6).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 6).Style.Font.FontSize = 15;
                    sheet.Cell(1, 6).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(6).Width = 30;




                    sheet.Cell(1, 7).Value = "SỐ LƯỢNG LỖI";
                    sheet.Cell(1, 7).Style.Font.Bold = true;
                    sheet.Cell(1, 7).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 7).Style.Font.FontSize = 15;
                    sheet.Cell(1, 7).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(7).Width = 10;


                    sheet.Cell(1, 8).Value = "ĐIỂM";
                    sheet.Cell(1, 8).Style.Font.Bold = true;
                    sheet.Cell(1, 8).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 8).Style.Font.FontSize = 15;
                    sheet.Cell(1, 8).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(8).Width = 10;

                    sheet.Cell(1, 9).Value = "TỔNG";
                    sheet.Cell(1, 9).Style.Font.Bold = true;
                    sheet.Cell(1, 9).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 9).Style.Font.FontSize = 15;
                    sheet.Cell(1, 9).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(9).Width = 10;

                    sheet.Cell(1, 10).Value = "LOẠI LỖI";
                    sheet.Cell(1, 10).Style.Font.Bold = true;
                    sheet.Cell(1, 10).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 10).Style.Font.FontSize = 15;
                    sheet.Cell(1, 10).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(10).Width = 40;

                    sheet.Cell(1, 11).Value = "MÔ TẢ LỖI";
                    sheet.Cell(1, 11).Style.Font.Bold = true;
                    sheet.Cell(1, 11).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 11).Style.Font.FontSize = 15;
                    sheet.Cell(1, 11).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(11).Width = 50;





                    sheet.Cell(1, 12).Value = "HÌNH ẢNH 1";
                    sheet.Cell(1, 12).Style.Font.Bold = true;
                    sheet.Cell(1, 12).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 12).Style.Font.FontSize = 15;
                    sheet.Cell(1, 12).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(12).Width = 30;


                    sheet.Cell(1, 13).Value = "HÌNH ẢNH 2";
                    sheet.Cell(1, 13).Style.Font.Bold = true;
                    sheet.Cell(1, 13).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 13).Style.Font.FontSize = 15;
                    sheet.Cell(1, 13).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(13).Width = 25;

                    sheet.Cell(1, 14).Value = "TỔ CHỊU TRÁCH NHIỆM";
                    sheet.Cell(1, 14).Style.Font.Bold = true;
                    sheet.Cell(1, 14).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 14).Style.Font.FontSize = 15;
                    sheet.Cell(1, 14).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 14).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(14).Width = 25;

                    sheet.Cell(1, 15).Value = "NGƯỜI TRÁCH NHIỆM";
                    sheet.Cell(1, 15).Style.Font.Bold = true;
                    sheet.Cell(1, 15).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 15).Style.Font.FontSize = 15;
                    sheet.Cell(1, 15).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 15).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(15).Width = 20;


                    //sheet.Cell(1, 13).Value = "TG PHÁT HIỆN";
                    //sheet.Cell(1, 13).Style.Font.Bold = true;
                    //sheet.Cell(1, 13).Style.Font.FontName = "Times New Roman";
                    //sheet.Cell(1, 13).Style.Font.FontSize = 15;
                    //sheet.Cell(1, 13).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    //sheet.Cell(1, 13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //sheet.Cell(1, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    //sheet.Column(13).Width = 20;

                    //sheet.Cell(1, 14).Value = "TG SẢN XUẤT";
                    //sheet.Cell(1, 14).Style.Font.Bold = true;
                    //sheet.Cell(1, 14).Style.Font.FontName = "Times New Roman";
                    //sheet.Cell(1, 14).Style.Font.FontSize = 15;
                    //sheet.Cell(1, 14).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    //sheet.Cell(1, 14).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //sheet.Cell(1, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    //sheet.Column(14).Width = 20;



                    sheet.Cell(1, 16).Value = "4M";
                    sheet.Cell(1, 16).Style.Font.Bold = true;
                    sheet.Cell(1, 16).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 16).Style.Font.FontSize = 15;
                    sheet.Cell(1, 16).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 16).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 16).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(16).Width = 30;

                    sheet.Cell(1, 17).Value = "NGUYÊN NHÂN";
                    sheet.Cell(1, 17).Style.Font.Bold = true;
                    sheet.Cell(1, 17).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 17).Style.Font.FontSize = 15;
                    sheet.Cell(1, 17).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 17).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(17).Width = 30;


                    sheet.Cell(1, 18).Value = "GIẢI PHÁP";
                    sheet.Cell(1, 18).Style.Font.Bold = true;
                    sheet.Cell(1, 18).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 18).Style.Font.FontSize = 15;
                    sheet.Cell(1, 18).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 18).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 18).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(18).Width = 30;


                    sheet.Cell(1, 19).Value = "GIẢI PHÁP LÂU DÀI";
                    sheet.Cell(1, 19).Style.Font.Bold = true;
                    sheet.Cell(1, 19).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 19).Style.Font.FontSize = 15;
                    sheet.Cell(1, 19).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 19).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 19).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(19).Width = 30;

                    sheet.Cell(1, 20).Value = "NGÀY THỰC HIỆN";
                    sheet.Cell(1, 20).Style.Font.Bold = true;
                    sheet.Cell(1, 20).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 20).Style.Font.FontSize = 15;
                    sheet.Cell(1, 20).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 20).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 20).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(20).Width = 30;

                    sheet.Cell(1, 21).Value = "TÌNH TRANG";
                    sheet.Cell(1, 21).Style.Font.Bold = true;
                    sheet.Cell(1, 21).Style.Font.FontName = "Times New Roman";
                    sheet.Cell(1, 21).Style.Font.FontSize = 15;
                    sheet.Cell(1, 21).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    sheet.Cell(1, 21).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    sheet.Cell(1, 21).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    sheet.Column(21).Width = 30;


                    //sheet.Cell(1, 20).Value = "KIND MAN";
                    //sheet.Cell(1, 20).Style.Font.Bold = true;
                    //sheet.Cell(1, 20).Style.Font.FontName = "Times New Roman";
                    //sheet.Cell(1, 20).Style.Font.FontSize = 15;
                    //sheet.Cell(1, 20).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    //sheet.Cell(1, 20).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //sheet.Cell(1, 20).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    //sheet.Column(20).Width = 20;


                    //sheet.Cell(1, 21).Value = "BÁO CÁO";
                    //sheet.Cell(1, 21).Style.Font.Bold = true;
                    //sheet.Cell(1, 21).Style.Font.FontName = "Times New Roman";
                    //sheet.Cell(1, 21).Style.Font.FontSize = 15;
                    //sheet.Cell(1, 21).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    //sheet.Cell(1, 21).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //sheet.Cell(1, 21).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    //sheet.Column(21).Width = 5;
                    //sheet.Cell(1, 22).Value = "SỐ HĐ";
                    //sheet.Cell(1, 22).Style.Font.Bold = true;
                    //sheet.Cell(1, 22).Style.Font.FontName = "Times New Roman";
                    //sheet.Cell(1, 22).Style.Font.FontSize = 15;
                    //sheet.Cell(1, 22).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                    //sheet.Cell(1, 22).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    //sheet.Cell(1, 22).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    //sheet.Column(22).Width = 10;
                    foreach (_string lvi in lstData.Items)
                    {

                        i++;

                        sheet.Cell(i, 1).Value = j.ToString();
                        sheet.Cell(i, 2).Value = lvi.Date;
                        sheet.Cell(i, 3).Value = lvi.Model;
                        sheet.Cell(i, 4).Value = lvi.Vincode;
                        sheet.Cell(i, 5).Value = lvi.LOT;
                        sheet.Cell(i, 6).Value = lvi.Position;
                        sheet.Cell(i, 7).Value = lvi.Amount;
                        sheet.Cell(i, 8).Value = lvi.level;
                        sheet.Cell(i, 9).Value = (Convert.ToInt32(lvi.level) * Convert.ToInt32(lvi.Amount)).ToString();
                        sheet.Cell(i, 10).Value = lvi.Type;
                        sheet.Cell(i, 11).Value = lvi.Descript;

                        var path = "\\\\10.40.12.6\\qc\\QCerror\\thongkeloi\\assets\\img\\QC\\" + lvi.IDError + "-1.jpg";
                        if (File.Exists(@"\\10.40.12.6\qc\QCerror\thongkeloi\assets\img\QC\" + lvi.IDError + "-1.jpg"))
                        {
                            sheet.Row(i).Height = 200;
                            sheet.AddPicture("\\\\10.40.12.6\\qc\\QCerror\\thongkeloi\\assets\\img\\QC\\" + lvi.IDError + "-1.jpg").MoveTo(sheet.Cell(i, 12)).WithSize(200, 200);
                        }

                        if (File.Exists("\\\\10.40.12.6\\qc\\QCerror\\thongkeloi\\assets\\img\\QC\\" + lvi.IDError + "-2.jpg"))
                            sheet.AddPicture("\\\\10.40.12.6\\qc\\QCerror\\thongkeloi\\assets\\img\\QC\\" + lvi.IDError + "-2.jpg").MoveTo(sheet.Cell(i, 13)).WithSize(200, 200);
                        sheet.Cell(i, 14).Value = lvi.Section;
                        sheet.Cell(i, 15).Value = lvi.Human;
                        sheet.Cell(i, 16).Value = lvi.T4M;
                        sheet.Cell(i, 17).Value = lvi.Cause;
                        sheet.Cell(i, 18).Value = lvi.Solution;
                        sheet.Cell(i, 19).Value = "";
                        sheet.Cell(i, 20).Value = "";
                        sheet.Cell(i, 21).Value = lvi.Note;
                        //sheet.Cell(i, 18).Value = lvi.;
                        //sheet.Cell(i, 19).Value = lvi.;
                        //sheet.Cell(i, 20).Value = lvi.Kindman;
                        //sheet.Cell(i, 21).Value = lvi.Report;
                        //sheet.Cell(i, 22).Value = lvi.Contract_no;
                        j++;

                        for (int n = 1; n <= 20; n++)
                        {
                            sheet.Cell(i, n).Style.Font.FontName = "Times New Roman";
                            sheet.Cell(i, n).Style.Font.FontSize = 15;
                            sheet.Cell(i, n).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                            sheet.Cell(i, n).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            sheet.Cell(i, n).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                        }
                    }

                    wb.SaveAs(saveFile.FileName);
                    wb.Dispose();
                    MessageBox.Show("Xuất báo cáo thành công!");
                }
            }
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message, "", MessageBoxButton.OK,MessageBoxImage.Error);
            //}
            finally
            {

            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string id = ((_string)lstData.SelectedItem).IDError.ToString();

            MessageBoxResult result = MessageBox.Show("Bạn có muốn xoá thông tin này?\n Việc xoá không thể hoàn tác. Thông tin mất vĩnh viễn!", "CẢNH BÁO", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.OK:
                    using (SqlConnection myconn = new SqlConnection(conn))
                    {
                        string s = "delete  FROM [QTSX].[dbo].[QC_INFOMATION_PROBLEMS] where ID='" + id + "'";
                        try
                        {
                            myconn.Open();
                            SqlCommand cmd = new SqlCommand(s, myconn);
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi xoá thông tin: " + ex.Message);

                        }
                        finally
                        {
                            myconn.Close();
                        }
                    }
                    if (File.Exists("\\\\10.40.12.6\\qc\\QCerror\\thongkeloi\\assets\\img\\QC\\" + id + "-1.jpg"))
                        File.Delete("\\\\10.40.12.6\\qc\\QCerror\\thongkeloi\\assets\\img\\QC\\" + id + "-1.jpg");
                    if (File.Exists("\\\\10.40.12.6\\qc\\QCerror\\thongkeloi\\assets\\img\\QC\\" + id + "-2.jpg"))
                        File.Delete("\\\\10.40.12.6\\qc\\QCerror\\thongkeloi\\assets\\img\\QC\\" + id + "-2.jpg");
                    MessageBox.Show("Đã xoá thông tin thành công", "THÔNG TIN", MessageBoxButton.OK, MessageBoxImage.Information);
                    load_listview();
                    break;
                case MessageBoxResult.Cancel:

                    break;
            }


        }
    }
    public class OrdinalConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            ListViewItem lvi = value as ListViewItem;
            int ordinal = 0;

            if (lvi != null)
            {
                ListView lv = ItemsControl.ItemsControlFromItemContainer(lvi) as ListView;
                ordinal = lv.ItemContainerGenerator.IndexFromContainer(lvi) + 1;
            }

            return ordinal;

        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // This converter does not provide conversion back from ordinal position to list view item
            throw new System.InvalidOperationException();
        }
    }
    public class TextInputToVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // Always test MultiValueConverter inputs for non-null
            // (to avoid crash bugs for views in the designer)
            if (values[0] is bool && values[1] is bool)
            {
                bool hasText = !(bool)values[0];
                bool hasFocus = (bool)values[1];

                if (hasFocus || hasText)
                    return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }


        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
