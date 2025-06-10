using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom6_QuanLyThuVien
{
    internal class ThongKe
    {
        Ketnoi kn;
        public ThongKe()
        {
            kn = new Ketnoi();
        }

        public DataTable GetAllSach()
        {
            string sql = "SELECT * FROM Sach";
            return kn.Readdata(sql);
        }

        public DataTable GetDocGiaDaTra()
        {
            string sql = @"
            SELECT 
                DG.MaDocGia, 
                DG.HoTen, 
                SUM(CASE 
                        WHEN MT.TinhTrang = 'Làm mất sách' THEN MT.ThanhTien
                        WHEN MT.TinhTrang = 'Trả quá hạn' THEN MT.SoLuongMuon * S.GiaMuon
                        ELSE 0 
                    END) AS TongTien
            FROM 
                MuonTra MT
            INNER JOIN 
                DocGia DG ON MT.MaDocGia = DG.MaDocGia
            INNER JOIN 
                Sach S ON MT.MaSach = S.MaSach
            WHERE 
                MT.TinhTrang = 'Làm mất sách' OR MT.TinhTrang = 'Trả quá hạn'
            GROUP BY 
                DG.MaDocGia, DG.HoTen";

            DataTable dt = kn.Readdata(sql);
            return dt;
        }

        public DataTable timkiemsachdatra()
        {
            string sql = "SELECT mt.MaGiaoDich, mt.MaDocGia, mt.HoTen, mt.MaSach, s.TenSach, mt.SoLuongMuon," +
                " CASE WHEN mt.TinhTrang = N'Làm mất sách' THEN mt.ThanhTien" +
                " WHEN mt.TinhTrang = N'Trả quá hạn' THEN mt.SoLuongMuon*s.GiaMuon" +
                " END AS ThanhTien, mt.NgayMuon, mt.NgayPhaiTra, mt.NgayTra, mt.TinhTrang" +
                " FROM MuonTra mt JOIN Sach s ON mt.MaSach = s.MaSach" +
                " WHERE mt.TinhTrang = N'Làm mất sách' OR mt.TinhTrang = N'Trả quá hạn';";
            return kn.Readdata(sql);
        }

        public int demLamMatSach()
        {
            string sql = "SELECT COUNT(*) FROM MuonTra WHERE TinhTrang = @tinhTrang";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@tinhTrang", "Làm mất sách")
            };
            DataTable dt = kn.Readdata(sql, sp);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }

        public int demTraQuaHan()
        {
            string sql = "SELECT COUNT(*) FROM MuonTra WHERE TinhTrang = @tinhTrang";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@tinhTrang", "Trả quá hạn")
            };
            DataTable dt = kn.Readdata(sql, sp);

            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }

        public int sosachNhap()
        {
            string sql = "SELECT SUM(SoLuong) FROM Sach";
            DataTable dt = kn.Readdata(sql, null);

            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }

        public int sachControngKho()
        {
            string sql = "SELECT SUM(SoLuongConLai) FROM Sach";
            DataTable dt = kn.Readdata(sql, null);

            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }

    }
}
