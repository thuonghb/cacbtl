using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom6_QuanLyThuVien
{
    internal class Docgia
    {
        Ketnoi kn;
        public Docgia()
        {
            kn = new Ketnoi();
        }
        // lay du lieu bang docgia
        public DataTable Getalldocgia()
        {
            string sql = "SELECT * FROM DocGia";
            return kn.Readdata(sql);
        }
        // them doc gia
        public void Createdocgia(string Madocgia, string Hoten, string Gioitinh, DateTime ngaysinh, DateTime ngaydangky, string diachi, string sodienthoai, string email)
        {
            string sql = "INSERT INTO DocGia (Madocgia, Hoten, Gioitinh, Ngaysinh, Ngaydangky, DiaChi, SoDienThoai, Email) VALUES (@MaDocGia, @HoTen, @GioiTinh, @NgaySinh, @NgayDangKy, @DiaChi, @SoDienThoai, @Email)";

            SqlParameter[] sp = new SqlParameter[]
            {
                  new SqlParameter("@MaDocGia", Madocgia),
                  new SqlParameter("@HoTen", Hoten),
                  new SqlParameter("@GioiTinh", Gioitinh),
                  new SqlParameter("@NgaySinh", ngaysinh),
                  new SqlParameter("@NgayDangKy", ngaydangky),
                  new SqlParameter("@DiaChi", diachi),
                  new SqlParameter("@SoDienThoai", sodienthoai),
                  new SqlParameter("@Email", email)
            };

            kn.CUD(sql, sp);
        }
        public void Updatedocgia(string Madocgia, string Hoten, string Gioitinh, DateTime ngaysinh, DateTime ngaydangky, string diachi, string sodienthoai, string email)
        {
            string sql = "UPDATE DocGia SET Hoten = @HoTen, Gioitinh = @GioiTinh, Ngaysinh = @NgaySinh, Ngaydangky = @NgayDangKy, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai, Email = @Email WHERE Madocgia = @MaDocGia";

            SqlParameter[] sp = new SqlParameter[]
            {
                  new SqlParameter("@MaDocGia", Madocgia),
                  new SqlParameter("@HoTen", Hoten),
                  new SqlParameter("@GioiTinh", Gioitinh),
                  new SqlParameter("@NgaySinh", ngaysinh),
                  new SqlParameter("@NgayDangKy", ngaydangky),
                  new SqlParameter("@DiaChi", diachi),
                  new SqlParameter("@SoDienThoai", sodienthoai),
                  new SqlParameter("@Email", email)
            };

            kn.CUD(sql, sp);
        }

        public void Deletedocgia(string Madocgia)
        {
            string sql = "DELETE FROM DocGia WHERE Madocgia = @MaDocGia";

            SqlParameter[] sp = new SqlParameter[]
            {
        new SqlParameter("@MaDocGia", Madocgia)
            };

            kn.CUD(sql, sp);
        }
        public DataTable Searchmadocgia(string Madocgia)
        {
            string sql = "SELECT * FROM DocGia WHERE Madocgia = @MaDocGia";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@MaDocGia", Madocgia)
            };
            return kn.Readdata(sql, sp);
        }

        public DataTable Searchtendocgia(string tendocgia)
        {
            string sql = "SELECT * FROM DocGia WHERE HoTen LIKE @tenDG";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@tenDG", "%" + tendocgia + "%")
            };
            return kn.Readdata(sql, sp);
        }

        public int CountDocGia()
        {
            string sql = "SELECT COUNT(*) FROM DocGia";
            DataTable dt = kn.Readdata(sql);
            return dt.Rows.Count > 0 ? Convert.ToInt32(dt.Rows[0][0]) : 0;
        }
    }
}

