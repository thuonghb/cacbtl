using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace Nhom6_QuanLyThuVien
{
    internal class MuonTraSach
    {
        Ketnoi kn;
        public MuonTraSach()
        {
            kn = new Ketnoi();
        }

      
        // lay du lieu bang 
        public DataTable GetAll()
        {
            string sql = "SELECT * FROM MuonTra";
            return kn.Readdata(sql);
        }

        //lấy tên sách cho cccbox
        public DataTable GetAllTenSach()
        {
            string sql = "SELECT TenSach FROM Sach";
            return kn.Readdata(sql);
        }

        //lấy mã đọc gải cho cbbox
        public DataTable GetAllMaDG()
        {
            string sql = "SELECT MaDocGia FROM DocGia";
            return kn.Readdata(sql);
        }

        //lấy tên độc giả cho cbBox
        public DataTable GetAllTenDG()
        {
            string sql = "SELECT HoTen FROM DocGia";
            return kn.Readdata(sql);
        }


        //lấy dữ liệu cho form Trả Sách
        public DataTable GetTrasach()
        {
            string sql = "SELECT MaGiaoDich, MaDocGia, HoTen, TenSach, SoLuongMuon, NgayMuon, NgayPhaiTra, NgayTra, TinhTrang FROM MuonTra";
            return kn.Readdata(sql);
        }


        //lấy giá tiền sách
        public DataTable getPrice(string maS)
        {
            string sql = "SELECT Gia FROM Sach WHERE MaSach = @maS";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@maS", maS)
            };
            return kn.Readdata(sql, sp);
        }

        //lấy giá cho mượn
        public DataTable getPriceMuon(string maS)
        {
            string sql = "SELECT GiaMuon FROM Sach WHERE MaSach = @maS";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@maS", maS)
            };
            return kn.Readdata(sql, sp);
        }


        //lấy số lượng sách còn lại
        public DataTable getTotal(string maS)
        {
            string sql = "SELECT SoLuongConLai FROM Sach WHERE MaSach = @maS";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@maS", maS)
            };
            return kn.Readdata(sql, sp);
        }


        //lấy mã sách
        public string getMaSach(string tenS)
        {
            string sql = "SELECT MaSach FROM Sach WHERE TenSach  = @tenS";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@tenS", tenS)
            };

            DataTable dt = kn.Readdata(sql, sp);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["MaSach"].ToString();
            }
            return null;
        }


        //lấy tên độc giả theo mã
        public string GetTenDG(string maDocGia)
        {
            string sql = "SELECT HoTen FROM DocGia WHERE MaDocGia = @maDocGia";
            SqlParameter[] sp = new SqlParameter[]
            {
                 new SqlParameter("@maDocGia", maDocGia)
            };

            DataTable dt = kn.Readdata(sql, sp);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["HoTen"].ToString();
            }
            return null;
        }


        //lấy mã độc giả theo tên
        public string GetMaDG(string tenDocGia)
        {
            string sql = "SELECT MaDocGia FROM DocGia WHERE HoTen = @tenDocGia";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@tenDocGia", tenDocGia)
            };

            DataTable dt = kn.Readdata(sql, sp);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["MaDocGia"].ToString();
            }
            return null;
        }

        //lấy sluong sách mượn theo mãGD
        public int GetSoLuongMuonByMaGiaoDich(string maGiaoDich)
        {
            int soLuongMuon = 0;
            string sql = "SELECT SoLuongMuon FROM MuonTra WHERE MaGiaoDich = @maGiaoDich";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@maGiaoDich", maGiaoDich)
            };

            DataTable dt = kn.Readdata(sql, sp);
            if (dt.Rows.Count > 0)
            {
                soLuongMuon = Convert.ToInt32(dt.Rows[0]["SoLuongMuon"]);
            }

            return soLuongMuon;
        }

        //lấy mã sách theo mã giao dịch
        public string GetMaSachByMaGiaoDich(string maGiaoDich)
        {
            string maSach = null;
            string sql = "SELECT MaSach FROM MuonTra WHERE MaGiaoDich = @maGiaoDich";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@maGiaoDich", maGiaoDich)
            };

            DataTable dt = kn.Readdata(sql, sp);
            if (dt.Rows.Count > 0)
            {
                maSach = dt.Rows[0]["MaSach"].ToString();
            }

            return maSach;
        }
        
      
        //thêm lượt mượn
        public void CreateMuon(string maGD, string maDG, string tenDG, string maS, string tenS,
            int Soluong, DateTime ngayMuon, DateTime ngayTra, int thanhTien)
        {
            string sql = "INSERT INTO MuonTra (MaGiaoDich, MaDocGia, HoTen, MaSach, TenSach, " +
                "SoLuongMuon, NgayMuon, NgayPhaiTra, ThanhTien) " +
                "VALUES (@a, @b, @c, @d, @e, @g, @h, @i, @k)";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@a", maGD),
                new SqlParameter("@b", maDG),
                new SqlParameter("@c", tenDG),
                new SqlParameter("@d", maS),
                new SqlParameter("@e", tenS),
                new SqlParameter("@g", Soluong),
                new SqlParameter("@h", ngayMuon),
                new SqlParameter("@i", ngayTra),
                new SqlParameter("@k", thanhTien)
            };
            kn.CUD(sql, sp);
        }

        //cập nhật thông tin mượn
        public void UpdateMuon(string maGD, string maDG, string tenDG, string maS, string tenS,
            int Soluong, DateTime ngayMuon, DateTime ngayTra, int thanhTien)
        {
            string sql = "UPDATE MuonTra SET MaDocGia = @b, HoTen = @c, MaSach = @d, TenSach = @e," +
                " SoLuongMuon = @g, NgayMuon = @h, NgayPhaiTra = @i, ThanhTien = @k " +
                " WHERE MaGiaoDich = @a ";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@a", maGD),
                new SqlParameter("@b", maDG),
                new SqlParameter("@c", tenDG),
                new SqlParameter("@d", maS),
                new SqlParameter("@e", tenS),
                new SqlParameter("@g", Soluong),
                new SqlParameter("@h", ngayMuon),
                new SqlParameter("@i", ngayTra),
                new SqlParameter("@k", thanhTien)
            };
            kn.CUD(sql, sp);
        }

        //update số lượng sách sáu khi mượn
        public void UpdateSoLuongSachCon(string maSach, int soLuongMuon)
        {
            string sql = "UPDATE Sach SET SoLuongConLai = SoLuongConLai - @soLuongMuon WHERE MaSach = @maSach";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@maSach", maSach),
                new SqlParameter("@soLuongMuon", soLuongMuon)
            };
            kn.CUD(sql, parameters);
        }

        //xóa sai mượn
        public void DeleteMuon(string maGD)
        {
            string sql = "DELETE FROM MuonTra WHERE MaGiaoDich = @a";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@a", maGD)
            };
            kn.CUD(sql, sp);
        }
     
        //kiểm tr tình trạng trả sách
        public string KiemTraTinhTrang(DateTime? ngayTra, DateTime ngayPhaiTra)
        {
            if (ngayTra == null)
            {
                if (DateTime.Now > ngayPhaiTra)
                {
                    return "Đã quá hạn";
                }
                else
                {
                    return "Chưa trả";
                }
            }
            else
            {
                if (ngayTra > ngayPhaiTra)
                {
                    return "Trả quá hạn";
                }
                else
                {
                    return "Đã trả";
                }
            }
        }

        //cập nhật tình trạng
        public void CapNhatTinhTrang(string maGD, string tinhTrang)
        {
            string sql = "UPDATE MuonTra SET TinhTrang = @tinhTrang WHERE MaGiaoDich = @maGD";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@tinhTrang", tinhTrang),
                new SqlParameter("@maGD", maGD)
            };
            kn.CUD(sql, sp);
        }

        //cập nhật thông tin trả
        public void UpdateTra(string maGD, DateTime? ngayTra, string tinhTrang)
        {
            string sql = "UPDATE MuonTra SET NgayTra = @ngayTra, TinhTrang = @tinhTrang WHERE MaGiaoDich = @maGD ";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@maGD", maGD),
                new SqlParameter("@ngayTra", ngayTra.HasValue ? (object)ngayTra.Value : DBNull.Value),
                new SqlParameter("@tinhTrang", tinhTrang)
            };
            kn.CUD(sql, sp);
        }

        //update sluong sách sau khi trả
        public void UpdateSoLuongSachConSauTra(string maSach, int soLuongTra)
        {
            string sql = "UPDATE Sach SET SoLuongConLai = SoLuongConLai + @soLuongTra WHERE MaSach = @maSach";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@maSach", maSach),
                new SqlParameter("@soLuongTra", soLuongTra)
            };
            kn.CUD(sql, parameters);
        }

        //xóa sai trả
        public void DeleteTra(string maGD, DateTime ngayPhaiTra)
        {
            string tinhTrang = KiemTraTinhTrang(null, ngayPhaiTra);

            string sql = "UPDATE MuonTra SET NgayTra = NULL, TinhTrang = @tinhTrang WHERE MaGiaoDich = @maGD";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@maGD", maGD),
                new SqlParameter("@tinhTrang", tinhTrang)
            };

            try
            {
                kn.CUD(sql, sp);
                MessageBox.Show("Cập nhật trạng thái thành công.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái: " + ex.Message);
            }
        }
     
        //tìm kiếm
        public DataTable SearchMA(string maDG)
        {
            string sql = "SELECT * FROM MuonTra Where MaDocGia = @ma";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@ma", maDG)
            };
            return kn.Readdata(sql, sp);
        }
        public DataTable SearchTEN(string tenDG)
        {
            string sql = "SELECT * FROM MuonTra WHERE HoTen LIKE @tenDocGia";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@tenDocGia", "%" + tenDG + "%")
            };

            return kn.Readdata(sql, sp);
        }
        
      
    }
}

