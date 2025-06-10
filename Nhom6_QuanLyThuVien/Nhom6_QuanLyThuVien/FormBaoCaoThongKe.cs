using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;



namespace Nhom6_QuanLyThuVien
{
    public partial class FormBaoCaoThongKe : Form
    {
        Docgia docgia = new Docgia();
        Ketnoi ketnoi = new Ketnoi();
        ThongKe thongke = new ThongKe();
        public FormBaoCaoThongKe()
        {
            InitializeComponent();
            LoadThongKeOptions();
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        }
        private void LoadThongKeOptions()
        {
            cbThongKe.Items.Add("Tình trạng sách");
            cbThongKe.Items.Add("Báo Cáo Quá hạn và Mất sách");
            cbThongKe.SelectedIndex = -1;
        }
        private void btnbaocao_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedOption = cbThongKe.SelectedItem.ToString();
                DataTable result = new DataTable();

                switch (selectedOption)
                {
                    
                    case "Tình trạng sách":
                        result = thongke.GetAllSach();
                        int soNhap = thongke.sosachNhap();
                        label2.Text = $"Tổng số sách nhập: {soNhap}";
                        int soCon = thongke.sachControngKho();
                        label5.Text = $"Số sách còn: {soCon}";
                        label6.Text = null;
                        break;

                    case "Báo Cáo Quá hạn và Mất sách":
                        result = thongke.timkiemsachdatra();
                        decimal tongTien = ketnoi.TongTien();
                        label2.Text = $"Tổng phí thu: {tongTien}";
                        int tqh = thongke.demTraQuaHan();
                        label5.Text = $"Số lượng trả quá hạn: {tqh}";
                        int lms = thongke.demLamMatSach();
                        label6.Text = $"Số lượng làm mất sách: {lms}";
                        break;

                }

                if (result != null && result.Rows.Count > 0)
                {
                    DataTable docGiaDaTra = thongke.GetDocGiaDaTra();
                    dgv_baocao.DataSource = result;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy dữ liệu.");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
            
        }

        private void FormBaoCaoThongKe_Load(object sender, EventArgs e)
        {

        }
        private void ExportToExcel(DataGridView dataGridView)
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                var workSheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                // Lấy tiêu đề
                for (int i = 1; i <= dataGridView.Columns.Count; i++)
                {
                    workSheet.Cells[1, i].Value = dataGridView.Columns[i - 1].HeaderText;
                }

                // Xuất dữ liệu từ DataGridView sang Excel
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        workSheet.Cells[i + 2, j + 1].Value = dataGridView.Rows[i].Cells[j].Value?.ToString();
                    }
                }

                // Lưu file Excel
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Excel Files|*.xlsx";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        FileInfo fi = new FileInfo(saveFileDialog.FileName);
                        excelPackage.SaveAs(fi);
                        // mở sau khi lưu
                        Process.Start(new ProcessStartInfo(saveFileDialog.FileName) { UseShellExecute = true });
                    }
                }
            }
        }


        private void btnexcel_Click(object sender, EventArgs e)
        {
            ExportToExcel(dgv_baocao);
        }
    }
}
    

