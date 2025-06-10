using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nhom6_QuanLyThuVien
{
    internal class Tg
    {
        Ketnoi kn;
        public Tg()
        {
            kn = new Ketnoi();
        }
        // lay du lieu bang TacGia
        public DataTable GetallTacGia()
        {
            string sql = "SELECT * FROM TacGia";
            return kn.Readdata(sql);
        }
        // them doc gia
        public void CreateTacGia(string MaTacGia, string TenTacGia, string GioiTinh, string QueQuan)
        {
            string sql = "INSERT INTO TacGia (MaTacGia, TenTacGia,GioiTinh, QueQuan) VALUES (@maTacGia, @tenTacGia,@gioiTinh,@queQuan)"; // truyen vao cac tham so 
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@maTacGia", MaTacGia),
                new SqlParameter("@tenTacGia", TenTacGia),
                new SqlParameter("@gioiTinh", GioiTinh),
                new SqlParameter("@queQuan", QueQuan)
            };
            kn.CUD(sql, sp);
        }
        public void UpdateTacGia(string MaTacGia, string TenTacGia, string GioiTinh, string QueQuan)
        {
            string sql = "UPDATE TacGia SET TenTacGia = @tenTacGia, GioiTinh =@gioiTinh, QueQuan = @queQuan WHERE MaTacGia = @maTacGia"; // truyen vao cac tham so 
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@maTacGia", MaTacGia),
                new SqlParameter("@tenTacGia", TenTacGia),
                new SqlParameter("@gioiTinh", GioiTinh),
                new SqlParameter("@queQuan", QueQuan)
            };
            kn.CUD(sql, sp);
        }
        public void DeleteTacGia(string MaTacGia)
        {
            string sql = "DELETE FROM TacGia WHERE MaTacGia = @maTacGia"; // truyen vao cac tham so 
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@maTacGia", MaTacGia)
            };
            kn.CUD(sql, sp);
        }
        public DataTable SearchmaTacGia(string MaTacGia)
        {
            string sql = "SELECT * FROM TacGia Where MaTacGia = @maTacGia";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@maTacGia", MaTacGia)
            };
            return kn.Readdata(sql, sp);
        }
        public DataTable SearchtenTacGia(string TenTacGia)
        {
            string sql = "SELECT * FROM TacGia Where TenTacGia = @tenTacGia";
            SqlParameter[] sp = new SqlParameter[]
            {
                new SqlParameter("@tenTacGia", TenTacGia)
            };
            return kn.Readdata(sql, sp);
        }
    }
}

