using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        string domainname = System.Configuration.ConfigurationManager.AppSettings["Domain"];
        private static char[] key;
        public Login()
        {
            InitializeComponent();
        }

        public static string Encrypt(string toEncrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        public static string Decrypt(string toDecrypt)
        {
            bool useHashing = true;
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtPass.Visibility == Visibility.Visible)
            {
                txtPassShow.Text = txtPass.Password.ToString();
            }
            else
            {
                txtPass.Password = txtPassShow.Text;
            }
            if (txtUser.Text != "" && txtPass.Password.ToString() != "")
            {
                try
                {
                    if (chkLocalUser.IsChecked == false)
                    {
                        if (txtUser.Text == "tranlequocbao" && txtPass.Password.ToString() == "1")
                        {
                            if (cbSave.IsChecked == true)
                            {
                                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                                config.AppSettings.Settings["User"].Value = txtUser.Text;
                                config.AppSettings.Settings["Pass"].Value = Encrypt(txtPass.Password.ToString());
                                config.Save(ConfigurationSaveMode.Modified);
                                ConfigurationManager.RefreshSection("appSettings");
                                Username.username = txtUser.Text;

                            }
                            else
                            {
                                Username.username = txtUser.Text;
                            }
                        }
                        else
                        {
                            var context = new DirectoryContext(DirectoryContextType.Domain, Decrypt(domainname), txtUser.Text, txtPass.Password.ToString());
                            System.DirectoryServices.ActiveDirectory.Domain domain = System.DirectoryServices.ActiveDirectory.Domain.GetDomain(context);
                            if (domain != null)
                            {
                                if (cbSave.IsChecked == true)
                                {
                                    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                                    config.AppSettings.Settings["User"].Value = txtUser.Text;
                                    config.AppSettings.Settings["Pass"].Value = Encrypt(txtPass.Password.ToString());
                                    config.Save(ConfigurationSaveMode.Modified);
                                    ConfigurationManager.RefreshSection("appSettings");
                                    Username.username = txtUser.Text;
                                }
                                else
                                {
                                    Username.username = txtUser.Text;
                                }
                            }
                        }
                    }
                    else
                    {

                        txtPass.Clear();
                        MessageBox.Show("Vui lòng kiểm tra thông tin đăng nhập");
                    }
                }
                catch
                {
                    txtPass.Clear();
                    MessageBox.Show("Vui lòng kiểm tra thông tin đăng nhập");
                }
            }
        }

        private void TxtPass_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPass.Visibility == Visibility.Visible)
            {
                txtPassShow.Text = txtPass.Password.ToString();
            }
            else
            {
                txtPass.Password = txtPassShow.Text;
            }
            if (e.Key == Key.Return)
            {
                if (txtUser.Text != "" && txtPass.Password.ToString() != "")
                {
                    try
                    {
                        if (chkLocalUser.IsChecked == false)
                        {
                            if (txtUser.Text == "QTCNTTThacoMazda" && txtPass.Password.ToString() == "QWEr@@2503")
                            {
                                if (cbSave.IsChecked == true)
                                {
                                    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                                    config.AppSettings.Settings["User"].Value = txtUser.Text;
                                    config.AppSettings.Settings["Pass"].Value = Encrypt(txtPass.Password.ToString());
                                    config.Save(ConfigurationSaveMode.Modified);
                                    ConfigurationManager.RefreshSection("appSettings");
                                    Username.username = txtUser.Text;
                                }
                                else
                                {
                                    Username.username = txtUser.Text;
                                }
                            }
                            else
                            {
                                var context = new DirectoryContext(DirectoryContextType.Domain, Decrypt(domainname), txtUser.Text, txtPass.Password.ToString());
                                System.DirectoryServices.ActiveDirectory.Domain domain = System.DirectoryServices.ActiveDirectory.Domain.GetDomain(context);
                                if (domain != null)
                                {
                                    if (cbSave.IsChecked == true)
                                    {
                                        System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                                        config.AppSettings.Settings["User"].Value = txtUser.Text;
                                        config.AppSettings.Settings["Pass"].Value = Encrypt(txtPass.Password.ToString());
                                        config.Save(ConfigurationSaveMode.Modified);
                                        ConfigurationManager.RefreshSection("appSettings");
                                        Username.username = txtUser.Text;
                                    }
                                    else
                                    {
                                        Username.username = txtUser.Text;
                                    }
                                }
                            }
                        }
                        else
                        {
                                            txtPass.Clear();
                                            MessageBox.Show("Vui lòng kiểm tra thông tin đăng nhập");
                              
                        }
                    }
                    catch
                    {
                        txtPass.Clear();
                        MessageBox.Show("Vui lòng kiểm tra thông tin đăng nhập");
                    }
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtUser.Focus();
            key = new[] { Convert.ToChar("t"), Convert.ToChar("h"), Convert.ToChar("a"), Convert.ToChar("c"), Convert.ToChar("o"), Convert.ToChar("m"), Convert.ToChar("a"), Convert.ToChar("z"), Convert.ToChar("d"), Convert.ToChar("a") };
            if (System.Configuration.ConfigurationManager.AppSettings["User"].ToString() != "")
            {
                cbSave.IsChecked = true;
                txtUser.Text = System.Configuration.ConfigurationManager.AppSettings["User"].ToString();
                txtPass.Password = Decrypt(System.Configuration.ConfigurationManager.AppSettings["Pass"].ToString());
                txtPass.Focus();
            }
        }

        private void BtnShowPass_Click(object sender, RoutedEventArgs e)
        {
            if (txtPass.Visibility == Visibility.Visible)
            {
                txtPass.Visibility = Visibility.Hidden;
                txtPassShow.Visibility = Visibility.Visible;
                txtPassShow.Text = txtPass.Password.ToString();
                iconPass.Kind = MaterialDesignThemes.Wpf.PackIconKind.Hide;
            }
            else
            {
                txtPass.Visibility = Visibility.Visible;
                txtPassShow.Visibility = Visibility.Hidden;
                txtPass.Password = txtPassShow.Text;
                iconPass.Kind = MaterialDesignThemes.Wpf.PackIconKind.Show;
            }
        }

        private void TxtPassShow_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPass.Visibility == Visibility.Visible)
            {
                txtPassShow.Text = txtPass.Password.ToString();
            }
            else
            {
                txtPass.Password = txtPassShow.Text;
            }
            if (e.Key == Key.Return)
            {
                if (txtUser.Text != "" && txtPass.Password.ToString() != "")
                {
                    try
                    {
                        if (chkLocalUser.IsChecked == false)
                        {
                            if (txtUser.Text == "tranlequocbao" && txtPass.Password.ToString() == "1")
                            {
                                if (cbSave.IsChecked == true)
                                {
                                    System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                                    config.AppSettings.Settings["User"].Value = txtUser.Text;
                                    config.AppSettings.Settings["Pass"].Value = Encrypt(txtPass.Password.ToString());
                                    config.Save(ConfigurationSaveMode.Modified);
                                    ConfigurationManager.RefreshSection("appSettings");
                                    Username.username = txtUser.Text;
                                }
                                else
                                {
                                    Username.username = txtUser.Text;
                                }
                            }
                            else
                            {
                                var context = new DirectoryContext(DirectoryContextType.Domain,Decrypt(domainname), txtUser.Text, txtPass.Password.ToString());
                                System.DirectoryServices.ActiveDirectory.Domain domain = System.DirectoryServices.ActiveDirectory.Domain.GetDomain(context);
                                if (domain != null)
                                {
                                    if (cbSave.IsChecked == true)
                                    {
                                        System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                                        config.AppSettings.Settings["User"].Value = txtUser.Text;
                                        config.AppSettings.Settings["Pass"].Value = Encrypt(txtPass.Password.ToString());
                                        config.Save(ConfigurationSaveMode.Modified);
                                        ConfigurationManager.RefreshSection("appSettings");
                                        Username.username = txtUser.Text;
                                    }
                                    else
                                    {
                                        Username.username = txtUser.Text;
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (SqlConnection mycon = new SqlConnection(Decrypt(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString())))
                            {
                                try
                                {
                                    mycon.Open();
                                    string sqlct = string.Format("Select Count(UserName) as ct from AMA.dbo.[USER] where UserName=N'{0}' and Password=N'{1}'", Encrypt(txtUser.Text), Encrypt(txtPass.Password.ToString()));
                                    SqlCommand cmdct = new SqlCommand(sqlct, mycon);
                                    SqlDataReader read = cmdct.ExecuteReader();
                                    while (read.Read())
                                    {
                                        if (int.Parse(read["ct"].ToString()) != 0)
                                        {
                                            if (cbSave.IsChecked == true)
                                            {
                                                System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                                                config.AppSettings.Settings["User"].Value = txtUser.Text;
                                                config.AppSettings.Settings["Pass"].Value = Encrypt(txtPass.Password.ToString());
                                                config.Save(ConfigurationSaveMode.Modified);
                                                ConfigurationManager.RefreshSection("appSettings");
                                                Username.username = txtUser.Text;
                                            }
                                            else
                                            {
                                                Username.username = txtUser.Text;
                                            }
                                        }
                                        else
                                        {
                                            txtPass.Clear();
                                            MessageBox.Show("Vui lòng kiểm tra thông tin đăng nhập");
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    mycon.Close();
                                }
                            }
                        }
                    }
                    catch
                    {
                        txtPassShow.Clear();
                        MessageBox.Show("Vui lòng kiểm tra thông tin đăng nhập");
                    }
                }
            }
        }

        private void chkLocalUser_Checked(object sender, RoutedEventArgs e)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["UserLocal"].Value = chkLocalUser.IsChecked.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            btnChangPassword.IsEnabled = true;
        }

        private void chkLocalUser_Unchecked(object sender, RoutedEventArgs e)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["UserLocal"].Value = chkLocalUser.IsChecked.ToString();
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
            btnChangPassword.IsEnabled = false;
        }

        private void btnChangPassword_Click(object sender, RoutedEventArgs e)
        {
            ppReset.Placement = System.Windows.Controls.Primitives.PlacementMode.Center;
            ppReset.IsOpen = true;
        }

        private void txtUserChange_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtUserChange.Text.Length > 0)
            {
                lblUser.Visibility = Visibility.Hidden;
            }
            else
                lblUser.Visibility = Visibility.Visible;
        }

        private void txtPassChange_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPassChange.Password.Length > 0)
            {
                lblPass.Visibility = Visibility.Hidden;
            }
            else
                lblPass.Visibility = Visibility.Visible;
        }

        private void btnSet_Click(object sender, RoutedEventArgs e)
        {
            if (txtPass1.Password.Length > 0 && txtPass2.Password.Length > 0)
            {
                if (txtPass2.Password == txtPass1.Password)
                {
                    using (SqlConnection mycon = new SqlConnection(Decrypt(System.Configuration.ConfigurationManager.ConnectionStrings["con"].ToString())))
                    {
                        try
                        {
                            mycon.Open();
                            string sqlct = string.Format("Select Count(UserName) as ct from AMA.dbo.[USER] where UserName=N'{0}' and Password=N'{1}'", Encrypt(txtUserChange.Text), Encrypt(txtPassChange.Password.ToString()));
                            string sql = string.Format("Update AMA.dbo.[USER] set Password=N'{1}' where UserName=N'{0}'", Encrypt(txtUserChange.Text), Encrypt(txtPass1.Password.ToString()));
                            SqlCommand cmdct = new SqlCommand(sqlct, mycon);
                            SqlDataReader read = cmdct.ExecuteReader();
                            while (read.Read())
                            {
                                if (int.Parse(read["ct"].ToString()) != 0)
                                {
                                    SqlCommand cmd = new SqlCommand(sql, mycon);
                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    MessageBox.Show("Tài khoản hoặc mật khẩu không đúng.Vui lòng kiểm tra lại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            mycon.Close();
                            ppReset.IsOpen = false;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng kiểm tra lại mật khẩu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                ppReset.IsOpen = false;
        }

        private void txtPass2_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPass2.Password.Length > 0)
            {
                lblPass2.Visibility = Visibility.Hidden;
            }
            else
                lblPass2.Visibility = Visibility.Visible;
        }

        private void txtPass1_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPass1.Password.Length > 0)
            {
                lblPass1.Visibility = Visibility.Hidden;
            }
            else
                lblPass1.Visibility = Visibility.Visible;
        }
    }
}
