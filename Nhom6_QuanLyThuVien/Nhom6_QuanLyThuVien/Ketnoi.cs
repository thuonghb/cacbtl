using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom6_QuanLyThuVien
{
    internal class Ketnoi
    {
        SqlConnection conn;
        string kn = @"Data Source=LAPTOP-TKDRUJMP;Initial Catalog=QLThuVien;Integrated Security=True;";
        public void ketnoi()
        {
            conn = new SqlConnection(kn);
            conn.Open();
        }
        public void dongkn()
        {
            conn.Close();
        }

        //CRUD
        public DataTable Readdata(string sql, SqlParameter[] sqlParameters = null)
        {
            DataTable dt = new DataTable();
            try
            {
                ketnoi();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (sqlParameters != null)
                    {
                        cmd.Parameters.AddRange(sqlParameters);
                    }
                    using (SqlDataReader sr = cmd.ExecuteReader())
                    {
                        dt.Load(sr);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dongkn();
            }
            return dt;
        }

        public void CUD(string table, SqlParameter[] sqlParameters = null)
        {
            try
            {
                ketnoi();
                using (SqlCommand cmd = new SqlCommand(table, conn))
                {
                    if (sqlParameters != null)
                    {
                        cmd.Parameters.AddRange(sqlParameters);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                dongkn();
            }
        }

        //tính tổng tiền
        public decimal TongTien()
        {
            decimal tongTien = 0;
            string sql = @"
                SELECT 
                    COALESCE(SUM(CASE 
                        WHEN MT.TinhTrang = N'Làm mất sách' THEN MT.ThanhTien 
                        WHEN MT.TinhTrang = N'Trả quá hạn' THEN MT.SoLuongMuon * S.GiaMuon
                        ELSE 0 
                    END), 0) AS TongTien
                FROM 
                    MuonTra MT
                JOIN 
                    Sach S ON MT.MaSach = S.MaSach
                WHERE 
                    MT.TinhTrang = N'Làm mất sách' OR MT.TinhTrang = N'Trả quá hạn';
            ";

            try
            {
                ketnoi();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        tongTien = Convert.ToDecimal(result);
                    }
                    else
                    {
                        tongTien = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tính tổng tiền: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dongkn();
            }

            return tongTien;
        }
    }
}

