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
    public partial class FormQLMuon : Form
    {
        MuonTraSach muonTra = new MuonTraSach();
        public FormQLMuon()
        {
            InitializeComponent();
            dgvMuonSach.DataSource = muonTra.GetAll();
            cbTenSach.SelectedIndexChanged -= cbTenSach_SelectedIndexChanged;
            LoadSachToComboBox();
            LoadMaDGToComboBox();
            LoadTenDGToComboBox();
            cbTenSach.SelectedIndexChanged += cbTenSach_SelectedIndexChanged;
        }

        private void FormQLMuon_Load(object sender, EventArgs e)
        {
            dgvMuonSach.DataSource = muonTra.GetAll();
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

                // Kiểm tra chỉ số lựa chọn của ComboBox để xác định kiểu tìm kiếm
                if (cbTim.SelectedIndex == 0) // Lựa chọn đầu tiên: Tìm kiếm theo mã độc giả
                {
                    ketQuaTimKiem = muonTra.SearchMA(giaTriTimKiem);
                }
                else if (cbTim.SelectedIndex == 1) // Lựa chọn thứ hai: Tìm kiếm theo tên độc giả
                {
                    ketQuaTimKiem = muonTra.SearchTEN(giaTriTimKiem);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn kiểu tìm kiếm.");
                    return;
                }

                // Hiển thị kết quả tìm kiếm (cập nhật DataGridView hoặc bất kỳ điều khiển nào bạn đang sử dụng để hiển thị dữ liệu)
                dgvMuonSach.DataSource = ketQuaTimKiem;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string maGiaoDich = txtMaGD.Text;
                string maDocGia = cbDocGia.SelectedValue?.ToString();
                string tenDocGia = cbTenDG.SelectedValue?.ToString();
                string tenSach = cbTenSach.SelectedValue?.ToString();

                if (string.IsNullOrEmpty(tenSach))
                {
                    MessageBox.Show("Vui lòng chọn tên sách.");
                    return;
                }

                string maSach = muonTra.getMaSach(tenSach);

                if (string.IsNullOrEmpty(maSach))
                {
                    MessageBox.Show("Không tìm thấy mã sách cho tiêu đề đã chọn.");
                    return;
                }

                string tenDocGiaThucTe = muonTra.GetTenDG(maDocGia);
                if (tenDocGiaThucTe != tenDocGia)
                {
                    MessageBox.Show("Mã độc giả và tên độc giả không khớp. Vui lòng kiểm tra lại.");
                    return;
                }

                string sluongMuon = txtSluong.Text;

                if (!int.TryParse(sluongMuon.Replace(",", "").Trim(), out int soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Số lượng mượn phải là một số nguyên dương.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DateTime ngayMuon = dtpNgayMuon.Value;
                DateTime ngayTra = dtpNgayTra.Value;

                if (!int.TryParse(txtThanhTien.Text.Replace(",", "").Trim(), out int thanhTien))
                {
                    MessageBox.Show("Thành tiền phải là một số hợp lệ.");
                    return;
                }

                muonTra.CreateMuon(maGiaoDich, maDocGia, tenDocGia, maSach, tenSach, soLuong, ngayMuon, ngayTra, thanhTien);
                FormQLMuon_Load(sender, e);
                muonTra.UpdateSoLuongSachCon(maSach, soLuong);

                MessageBox.Show("Thêm mới thành công!");
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
                string maGiaoDich = txtMaGD.Text;
                string maDocGia = cbDocGia.SelectedValue?.ToString();
                string tenDocGia = cbTenDG.SelectedValue?.ToString();
                string tenSach = cbTenSach.SelectedValue?.ToString();

                if (string.IsNullOrEmpty(maGiaoDich) || string.IsNullOrEmpty(maDocGia) || string.IsNullOrEmpty(tenDocGia) || string.IsNullOrEmpty(tenSach))
                {
                    MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                    return;
                }

                string maSach = muonTra.getMaSach(tenSach);

                if (!int.TryParse(txtSluong.Text.Replace(",", "").Trim(), out int soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Số lượng phải là một số nguyên dương.");
                    return;
                }

                DateTime ngayMuon = dtpNgayMuon.Value;
                DateTime ngayTra = dtpNgayTra.Value;

                if (!int.TryParse(txtThanhTien.Text.Replace(",", "").Trim(), out int thanhTien))
                {
                    MessageBox.Show("Thành tiền phải là một số hợp lệ.");
                    return;
                }

                muonTra.UpdateMuon(maGiaoDich, maDocGia, tenDocGia, maSach, tenSach, soLuong, ngayMuon, ngayTra, thanhTien);
                FormQLMuon_Load(sender, e);

                MessageBox.Show("Cập nhật thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maGiaoDich = txtMaGD.Text;

                if (string.IsNullOrEmpty(maGiaoDich))
                {
                    MessageBox.Show("Vui lòng chọn giao dịch cần xóa.");
                    return;
                }

                DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    string maSach = muonTra.GetMaSachByMaGiaoDich(maGiaoDich);
                    int sluongMuon = muonTra.GetSoLuongMuonByMaGiaoDich(maGiaoDich);
                    muonTra.UpdateSoLuongSachConSauTra(maSach, sluongMuon);

                    muonTra.DeleteMuon(maGiaoDich);
                    FormQLMuon_Load(sender, e);
                    clear();
                    MessageBox.Show("Xóa thành công!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btnKienTra_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbTenSach.SelectedItem == null)
                {
                    MessageBox.Show("Vui lòng chọn tên sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtSluong.Text) || !int.TryParse(txtSluong.Text, out int soLuongMuon))
                {
                    MessageBox.Show("Vui lòng nhập số lượng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string tenSach = cbTenSach.SelectedValue?.ToString();

                string maSach = muonTra.getMaSach(tenSach);

                txtMaSach.Text = maSach;

                if (string.IsNullOrEmpty(maSach))
                {
                    MessageBox.Show("Không tìm thấy mã sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DataTable dtTotal = muonTra.getTotal(maSach);
                if (dtTotal.Rows.Count > 0)
                {
                    int soLuongConLai = Convert.ToInt32(dtTotal.Rows[0]["SoLuongConLai"]);

                    if (soLuongConLai >= soLuongMuon)
                    {
                        MessageBox.Show($"Số lượng sách trong kho hiện có: {soLuongConLai}. Bạn có thể mượn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnThem.Enabled = true;
                        btnSua.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Số lượng sách muốn mượn không đủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        btnThem.Enabled = false;
                        btnSua.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy thông tin số lượng sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void LoadSachToComboBox()
        {
            DataTable dtsach = muonTra.GetAllTenSach();

            cbTenSach.DataSource = dtsach;
            cbTenSach.DisplayMember = "TenSach"; 
            cbTenSach.ValueMember = "TenSach";

            cbTenSach.SelectedIndex = -1;
        }

        private void LoadMaDGToComboBox()
        {
            DataTable dtDG = muonTra.GetAllMaDG();

            cbDocGia.DataSource = dtDG;
            cbDocGia.DisplayMember = "MaDocGia"; 
            cbDocGia.ValueMember = "MaDocGia";

            cbDocGia.SelectedIndex = -1;
        }

        private void LoadTenDGToComboBox()
        {
            DataTable dtTenDG = muonTra.GetAllTenDG();

            cbTenDG.DataSource = dtTenDG;
            cbTenDG.DisplayMember = "HoTen";
            cbTenDG.ValueMember = "HoTen";

            cbTenDG.SelectedIndex = -1; 
        }

        private void cbTenSach_Validated(object sender, EventArgs e)
        {

        }

        private void cbTenSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTenSach.SelectedItem != null && cbTenSach.SelectedIndex != -1)
            {
                string tenSach = cbTenSach.SelectedValue?.ToString();

                if (!string.IsNullOrEmpty(tenSach))
                {

                    string maSach = muonTra.getMaSach(tenSach);

                    if (!string.IsNullOrEmpty(maSach)) 
                    {
                        DataTable dtGia = muonTra.getPrice(maSach);
                        DataTable dtGiaMuon = muonTra.getPriceMuon(maSach);
                        if (dtGia != null && dtGia.Rows.Count > 0)
                        {
                            txtGiaSach.Text = dtGia.Rows[0]["Gia"].ToString();
                            txtGiaMuon.Text = dtGiaMuon.Rows[0]["GiaMuon"].ToString();
                            TinhThanhTien(); 
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy giá tiền sách trong cơ sở dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã sách tương ứng với tên sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }
        private void TinhThanhTien()
        {
            if (int.TryParse(txtSluong.Text, out int soLuong) && decimal.TryParse(txtGiaSach.Text, out decimal giaTien)
                && decimal.TryParse(txtGiaMuon.Text, out decimal giaMuon))
            {
                decimal thanhTien = (soLuong * giaMuon) + giaTien;
                txtThanhTien.Text = thanhTien.ToString("N0");
            }
            else
            {
                txtThanhTien.Text = "0";
            }
        }

        private void txtSluong_TextChanged(object sender, EventArgs e)
        {
            TinhThanhTien();
        }

        private void dgvMuonSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMuonSach.Rows[e.RowIndex];

                txtMaGD.Text = row.Cells["MaGiaoDich"].Value.ToString();
                cbDocGia.Text = row.Cells["MaDocGia"].Value.ToString();
                cbTenDG.Text = row.Cells["HoTen"].Value.ToString();
                dtpNgayMuon.Text = row.Cells["NgayMuon"].Value.ToString();
                dtpNgayTra.Text = row.Cells["NgayPhaiTra"].Value.ToString();
                txtMaSach.Text = row.Cells["MaSach"].Value.ToString();
                cbTenSach.Text = row.Cells["TenSach"].Value.ToString();
                txtSluong.Text = row.Cells["SoLuongMuon"].Value.ToString();

                txtThanhTien.Text = row.Cells["ThanhTien"].Value.ToString();
            }
        }

        private void cbTenDG_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tenDocGia = cbTenDG.SelectedValue?.ToString();

            if (!string.IsNullOrEmpty(tenDocGia))
            {
                string maDocGia = muonTra.GetMaDG(tenDocGia);

                for (int i = 0; i < cbDocGia.Items.Count; i++)
                {
                    DataRowView item = (DataRowView)cbDocGia.Items[i];
                    if (item["MaDocGia"].ToString() == maDocGia)
                    {
                        cbDocGia.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        private void cbDocGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maDocGia = cbDocGia.SelectedValue?.ToString();

            if (!string.IsNullOrEmpty(maDocGia))
            {
                string tenDocGia = muonTra.GetTenDG(maDocGia);

                for (int i = 0; i < cbTenDG.Items.Count; i++)
                {
                    DataRowView item = (DataRowView)cbTenDG.Items[i];
                    if (item["HoTen"].ToString() == tenDocGia)
                    {
                        cbTenDG.SelectedIndex = i;
                        break;
                    }

                }
            }

        }
        private void clear()
        {
            txtGiaSach.Clear();
            txtMaGD.Clear();
            txtMaSach.Clear();
            txtSluong.Clear();
            txtThanhTien.Clear();
            cbDocGia.SelectedIndex = -1;
            cbTenDG.SelectedIndex = -1;
            cbTenSach.SelectedIndex = -1;
            dtpNgayMuon.Value = DateTime.Now;
            dtpNgayTra.Value = DateTime.Now;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}


