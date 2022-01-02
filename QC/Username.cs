using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
namespace QC
{
    
    class Username
    {
        public static string con = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ToString();
        public static string username = "user";
        public static string url_image = "";
        public static string ID = "";
        public static string button = "";
        public static string currentShop = "";
        public static bool img2 = false;
        public static bool finishAdd = false;

        public static string get4M(string id)
        {
            string result = "";
            string s = string.Format("select NAME from [QTSX].[dbo].[QC_INFORMATION_4M] where ID='{0}'", id);
            using(SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn); 
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["NAME"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        public static string gettypeError(string id)
        {
            string result = "";
            string s = string.Format("select [Error_name] from [QTSX].[dbo].[QC_INFORMATION_ERROR] where [IDError]='{0}'", id);
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["Error_name"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        public static string getShopError(string id)
        {
            string result = "";
            string s = string.Format("select Shop_name from [QTSX].[dbo].[QC_INFORMATION_SHOP] where IDShop='{0}'", id);
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["Shop_name"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        public static string getLevelError(string id)
        {
            string result = "";
            string s = string.Format("select LEVEL from [QTSX].[dbo].[QC_INFORMATION_LEVEL] where ID='{0}'", id);
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["LEVEL"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        public static string getPositionError(string id)
        {
            string result = "";
            string s = string.Format("select [Position_name] from [QTSX].[dbo].[QC_INFORMATION_POSITION] where [IDPosition]='{0}'", id);
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["Position_name"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        public static string getMandfError(string id)
        {
            string result = "";
            string s = string.Format("select NAME from [QTSX].[dbo].[QC_INFORMATION_MANDF] where ID='{0}'", id);
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["NAME"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        public static string getStationError(string id)
        {
            string result = "";
            string s = string.Format("select [Station_name] from [QTSX].[dbo].[QC_INFORMATION_STATION] where  [IDStation]='{0}'", id);
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["Station_name"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        public static string getSectionError(string id)
        {
            string result = "";
            string s = string.Format("select [Section_name] from [QTSX].[dbo].[QC_INFORMATION_SECTION] where  [IDSection]='{0}'", id);
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["Section_name"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public static string getId4M(string name)
        {
            string result = "";
            string s = string.Format("select ID from [QTSX].[dbo].[QC_INFORMATION_4M] where NAME='{0}'", name);
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["ID"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        public static string gettIdTypeError(string name)
        {
            string result = "";
            string s = string.Format("select [IDError] from [QTSX].[dbo].[QC_INFORMATION_ERROR] where [Error_name]='{0}'", name);
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["IDError"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        public static string getIdShopError(string name)
        {
            string result = "";
            string s = string.Format("select [IDShop] from [QTSX].[dbo].[QC_INFORMATION_SHOP] where [Shop_name]='{0}'", name);
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["IDShop"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        public static string getIdLevelError(string name)
        {
            string result = "";
            string s = string.Format("select ID from [QTSX].[dbo].[QC_INFORMATION_LEVEL] where LEVEL='{0}'", name);
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["ID"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        public static string getIdPositionError(string name)
        {
            string result = "";
            string s = string.Format("select [IDPosition] from [QTSX].[dbo].[QC_INFORMATION_POSITION] where [Position_name]='{0}'", name);
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["IDPosition"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        public static string getIdMandfError(string name)
        {
            string result = "";
            string s = string.Format("select ID  from [QTSX].[dbo].[QC_INFORMATION_MANDF] where NAME='{0}'", name);
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["ID"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        public static string getIdStationError(string name)
        {
            string result = "";
            string s = string.Format("select [IDStation] from [QTSX].[dbo].[QC_INFORMATION_STATION] where  [Station_name]='{0}'", name);
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["IDStation"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
        public static string getIdSectionError(string name)
        {
            string result = "";
            string s = string.Format("select [IDSection] from [QTSX].[dbo].[QC_INFORMATION_SECTION] where  [Section_name]='{0}'", name);
            using (SqlConnection conn = new SqlConnection(con))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(s, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        result = reader["IDSection"].ToString().Trim();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }
    }

}
