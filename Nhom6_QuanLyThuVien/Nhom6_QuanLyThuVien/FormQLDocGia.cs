using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nhom6_QuanLyThuVien
{
    public partial class FormQLDocGia : Form
    {
        Docgia docgia = new Docgia();
        public FormQLDocGia()
        {
            InitializeComponent();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            string madocgia = txt_madocgia.Text.Trim();
            string hoten = txt_hoten.Text.Trim();
            string gioitinh = txt_gioitinh.Text.Trim();
            DateTime ngaysinh = dtp_ngaysinh.Value;
            DateTime ngaydangky = dtp_ngaydangky.Value;
            string diachi = txt_diachi.Text.Trim();
            string sodienthoai = txt_dienthoai.Text.Trim();
            string email = txt_email.Text.Trim();

            if (!string.IsNullOrEmpty(madocgia) && !string.IsNullOrEmpty(hoten) && !string.IsNullOrEmpty(gioitinh)
                && !string.IsNullOrEmpty(diachi) && !string.IsNullOrEmpty(sodienthoai) && !string.IsNullOrEmpty(email))
            {
                docgia.Createdocgia(madocgia, hoten, gioitinh, ngaysinh, ngaydangky, diachi, sodienthoai, email);
                FormQLDocGia_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin độc giả.");
            }
        }

        private void FormQLDocGia_Load(object sender, EventArgs e)
        {
            dgv_docgia.DataSource = docgia.Getalldocgia();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            string madocgia = txt_madocgia.Text.Trim();
            string hoten = txt_hoten.Text.Trim();
            string gioitinh = txt_gioitinh.Text.Trim();
            DateTime ngaysinh = dtp_ngaysinh.Value;
            DateTime ngaydangky = dtp_ngaydangky.Value;
            string diachi = txt_diachi.Text.Trim();
            string sodienthoai = txt_dienthoai.Text.Trim();
            string email = txt_email.Text.Trim();

            if (!string.IsNullOrEmpty(madocgia) && !string.IsNullOrEmpty(hoten) && !string.IsNullOrEmpty(gioitinh)
                && !string.IsNullOrEmpty(diachi) && !string.IsNullOrEmpty(sodienthoai) && !string.IsNullOrEmpty(email))
            {
                docgia.Updatedocgia(madocgia, hoten, gioitinh, ngaysinh, ngaydangky, diachi, sodienthoai, email);
                FormQLDocGia_Load(sender, e);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn độc giả cần sửa và điền đầy đủ thông tin.");
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            string madocgia = txt_madocgia.Text.Trim();

            if (!string.IsNullOrEmpty(madocgia))
            {
                DialogResult confirmDelete = MessageBox.Show("Bạn có chắc chắn muốn xóa độc giả này?", "Xác nhận xóa", MessageBoxButtons.YesNo);
                if (confirmDelete == DialogResult.Yes)
                {
                    docgia.Deletedocgia(madocgia);
                    FormQLDocGia_Load(sender, e);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mã độc giả cần xóa.");
            }
        }

        private void dgv_docgia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgv_docgia.Rows[e.RowIndex];
                txt_madocgia.Text = row.Cells["madocgia"].Value.ToString();
                txt_hoten.Text = row.Cells["hoten"].Value?.ToString();
                txt_gioitinh.Text = row.Cells["gioitinh"].Value.ToString();
                dtp_ngaysinh.Value = row.Cells["ngaysinh"].Value == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row.Cells["ngaysinh"].Value);
                dtp_ngaydangky.Value = row.Cells["ngaydangky"].Value == DBNull.Value ? DateTime.Now : Convert.ToDateTime(row.Cells["ngaydangky"].Value);
                txt_diachi.Text = row.Cells["diachi"].Value.ToString();
                txt_dienthoai.Text = row.Cells["sodienthoai"].Value?.ToString();
                txt_email.Text = row.Cells["email"].Value.ToString();
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            try
            {
                string giaTriTimKiem = txt_timkiem.Text.Trim();

                if (string.IsNullOrEmpty(giaTriTimKiem))
                {
                    MessageBox.Show("Vui lòng nhập giá trị tìm kiếm.");
                    return;
                }

                DataTable ketQuaTimKiem;

                if (cb_Timkiem.SelectedIndex == 0) 
                {
                    ketQuaTimKiem = docgia.Searchmadocgia(giaTriTimKiem);
                }
                else if (cb_Timkiem.SelectedIndex == 1) 
                {
                    ketQuaTimKiem = docgia.Searchtendocgia(giaTriTimKiem);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn kiểu tìm kiếm.");
                    return;
                }

                dgv_docgia.DataSource = ketQuaTimKiem;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
