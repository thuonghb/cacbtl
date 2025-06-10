using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom6_QuanLyThuVien
{
    public partial class FormQLSach : Form
    {
        Sach sach = new Sach();
        public FormQLSach()
        {
            InitializeComponent();
            LoadMaTacGiaComboBox();
            dgv_sach.DataSource = sach.Getallbook();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            string Masach = tb_masach.Text;
            string Tensach = tb_tensach.Text;
            string Matg = cb_matg.SelectedValue.ToString(); // Lấy mã tác giả đúng
            string Nhaxb = tb_nhaxb.Text;
            int Namxb = int.Parse(tb_namxb.Text);
            string theloai = tb_theloai.Text;
            int Soluong = int.Parse(tb_soluong.Text);
            int soluongconlai = int.Parse(tb_soluongcl.Text);
            string gia = tb_gia.Text;
            int giamuon = int.Parse(tb_giamuon.Text);

            sach.Createbook(Masach, Tensach, Matg, Nhaxb, Namxb, theloai, Soluong, soluongconlai, gia, giamuon);
            dgv_sach.DataSource = null;
            dgv_sach.Rows.Clear();
            FormQLSach_Load(sender, e);
        }

        private void FormQLSach_Load(object sender, EventArgs e)
        {
            dgv_sach.DataSource = sach.Getallbook();
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            string Masach = tb_masach.Text;

            sach.Deletebook(Masach);
            dgv_sach.DataSource = null;
            dgv_sach.Rows.Clear();
            FormQLSach_Load(sender, e);
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            string Masach = tb_masach.Text;
            string Tensach = tb_tensach.Text;
            string Matg = cb_matg.Items.ToString();
            string Nhaxb = tb_nhaxb.Text;
            int Namxb = int.Parse(tb_namxb.Text);
            string theloai = tb_theloai.Text;
            int Soluong = int.Parse(tb_soluong.Text);
            int soluongconlai = int.Parse(tb_soluongcl.Text);
            string gia = tb_gia.Text;
            int giamuon = int.Parse(tb_giamuon.Text);
            sach.Updatebook(Masach, Tensach, Matg, Nhaxb, Namxb, theloai, Soluong, soluongconlai, gia, giamuon);
            FormQLSach_Load(sender, e);
        }

        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            string tim = tb_masach.Text;
            dgv_sach.DataSource = sach.Searchmabook(tim);
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadMaTacGiaComboBox()
        {
            // Lấy danh sách mã nhà cung cấp
            DataTable ma = sach.Getallmatg();

            cb_matg.DataSource = ma;
            cb_matg.DisplayMember = "MaTacGia"; // Cột hiển thị
            cb_matg.ValueMember = "MaTacGia"; // Giá trị mã nhà cung cấp

            cb_matg.SelectedIndex = -1; // Không chọn mục nào ban đầu
        }

        private void btn_hienthi_Click(object sender, EventArgs e)
        {
            FormQLSach_Load(sender, e);
        }

        private void dgv_sach_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_sach.Rows[e.RowIndex];
                // lay du lieu cot va day vao tb
                tb_masach.Text = row.Cells["MaSach"].Value.ToString();
                tb_tensach.Text = row.Cells["TenSach"].Value.ToString();
                cb_matg.Text = row.Cells["MaTacGia"].Value.ToString();
                tb_nhaxb.Text = row.Cells["NhaXuatBan"].Value.ToString();
                tb_namxb.Text = row.Cells["NamXuatBan"].Value.ToString();
                tb_theloai.Text = row.Cells["TheLoai"].Value.ToString();
                tb_soluong.Text = row.Cells["SoLuong"].Value.ToString();
                tb_soluongcl.Text = row.Cells["SoLuongConLai"].Value.ToString();
                tb_gia.Text = row.Cells["Gia"].Value.ToString();
                tb_giamuon.Text = row.Cells["GiaMuon"].Value.ToString();
            }
        }
    }
}

