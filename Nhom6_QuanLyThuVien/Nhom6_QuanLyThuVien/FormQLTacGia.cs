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
    public partial class FormQLTacGia : Form
    {
        Tg tg = new Tg();
        public FormQLTacGia()
        {
            InitializeComponent();
            dataGridView1.DataSource = tg.GetallTacGia();
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            string maTacGia = txt_matacgia.Text;
            string tenTacGia = txt_hoten.Text;
            string gioitinh = cb_gioitinh.SelectedItem.ToString();
            string diachi = txt_quequan.Text;
            tg.CreateTacGia(maTacGia, tenTacGia, gioitinh, diachi);
            FormQLTacGia_Load(sender, e);
        }

        private void FormQLTacGia_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = tg.GetallTacGia();
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            string maTacGia = txt_matacgia.Text;
            string tenTacGia = txt_hoten.Text;
            string gioitinh = cb_gioitinh.SelectedItem.ToString();
            string diachi = txt_quequan.Text;
            tg.UpdateTacGia(maTacGia, tenTacGia, gioitinh, diachi);
            FormQLTacGia_Load(sender, e);
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            string maTacGia = txt_matacgia.Text;
            tg.DeleteTacGia(maTacGia);
            FormQLTacGia_Load(sender, e);
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            string tim = txt_matacgia.Text.Trim();
            dataGridView1.DataSource = tg.SearchmaTacGia(tim);
        }

        private void Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                // lay du lieu cot va day vao tb
                txt_matacgia.Text = row.Cells["maTacGia"].Value.ToString();
                txt_hoten.Text = row.Cells["tenTacGia"].Value.ToString();
                txt_quequan.Text = row.Cells["quequan"].Value.ToString();
                cb_gioitinh.Text = row.Cells["gioitinh"].Value.ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
