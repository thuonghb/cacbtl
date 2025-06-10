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
    public partial class FormQLTra : Form
    {
        MuonTraSach tra = new MuonTraSach();
        public FormQLTra()
        {
            InitializeComponent();
            dgvTraSach.DataSource = tra.GetTrasach();
        }

        private void FormQLTra_Load(object sender, EventArgs e)
        {
            dgvTraSach.DataSource = tra.GetTrasach();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string giaTriTimKiem = txtTim.Text.Trim();

                if (string.IsNullOrEmpty(giaTriTimKiem))
                {
                    MessageBox.Show("Vui lòng nhập giá trị tìm kiếm.");
                    return;
                }

                DataTable ketQuaTimKiem;

                if (cbTim.SelectedIndex == 0)
                {
                    ketQuaTimKiem = tra.SearchMA(giaTriTimKiem);
                }
                else if (cbTim.SelectedIndex == 1)
                {
                    ketQuaTimKiem = tra.SearchTEN(giaTriTimKiem);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn kiểu tìm kiếm.");
                    return;
                }

                dgvTraSach.DataSource = ketQuaTimKiem;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string maGD = txtMaGD.Text;
                string maSach = tra.GetMaSachByMaGiaoDich(maGD);
                if (string.IsNullOrEmpty(maGD))
                {
                    MessageBox.Show("Vui lòng chọn giao dịch để cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string sluongMuon = txtSluong.Text;
                if (!int.TryParse(sluongMuon, out int soLuongTra) || soLuongTra <= 0)
                {
                    MessageBox.Show("Số lượng mượn phải là số nguyên dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DateTime ngayPhaiTra = dtpNgayPTra.Value;
                DateTime? ngayTra = dtpNgayTra.CustomFormat == " " ? (DateTime?)null : dtpNgayTra.Value;

                string tinhtrang;
                if (ckb_Mat.Checked)
                {
                    tinhtrang = "Làm mất sách";
                    tra.UpdateSoLuongSachCon(maSach, soLuongTra);
                }
                else
                {
                    tinhtrang = tra.KiemTraTinhTrang(ngayTra, ngayPhaiTra);
                }

                if (tinhtrang == "Đã trả" || tinhtrang == "Trả quá hạn")
                {
                    
                    if (!string.IsNullOrEmpty(maSach))
                    {
                        tra.UpdateSoLuongSachConSauTra(maSach, soLuongTra);
                    }
                }

                tra.UpdateTra(maGD, ngayTra, tinhtrang);
                txtTinhTrang.Text = tinhtrang;

                MessageBox.Show("Cập nhật thông tin trả sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FormQLTra_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maGD = txtMaGD.Text.Trim();
            DateTime ngayPhaiTra = dtpNgayPTra.Value;

            if (!string.IsNullOrEmpty(maGD))
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa trạng thái trả của mã giao dịch này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    tra.DeleteTra(maGD, ngayPhaiTra);
                    FormQLTra_Load(sender, e);
                }
                else
                {
                    MessageBox.Show("Đã hủy thao tác xóa.");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn mã giao dịch để xóa.");
            }
        }

        private void dgvTraSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTraSach.Rows[e.RowIndex];

                txtMaGD.Text = row.Cells["MaGiaoDich"].Value.ToString();
                txtMaDG.Text = row.Cells["MaDocGia"].Value.ToString();
                txtTenDG.Text = row.Cells["HoTen"].Value.ToString();
                txtTenS.Text = row.Cells["TenSach"].Value.ToString();
                txtSluong.Text = row.Cells["SoLuongMuon"].Value.ToString();
                dtpNgayMuon.Text = row.Cells["NgayMuon"].Value.ToString();
                dtpNgayPTra.Text = row.Cells["NgayPhaiTra"].Value.ToString();
                dtpNgayTra.Text = row.Cells["NgayTra"].Value.ToString();
                txtTinhTrang.Text = row.Cells["TinhTrang"].Value.ToString();

            }
        }

        private void dgvTraSach_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvTraSach.SelectedRows.Count > 0)
            {
                DataRow record = ((DataRowView)dgvTraSach.SelectedRows[0].DataBoundItem).Row;

                DateTime? ngayTra = record["NgayTra"] as DateTime?;
                DateTime ngayPhaiTra = (DateTime)record["NgayPhaiTra"];
                string maGD = record["MaGiaoDich"].ToString();

                string tinhTrang = tra.KiemTraTinhTrang(ngayTra, ngayPhaiTra);

                txtTinhTrang.Text = tinhTrang;

                tra.CapNhatTinhTrang(maGD, tinhTrang);
                FormQLTra_Load(sender, e);
            }
        }

    }
}
    

