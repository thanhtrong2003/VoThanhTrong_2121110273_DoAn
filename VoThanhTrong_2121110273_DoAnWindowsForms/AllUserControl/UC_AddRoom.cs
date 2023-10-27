
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeOpenXml;
using BLL;
using System.Drawing.Printing;
using System.Xml.Linq;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace VoThanhTrong_2121110273_DoAnWindowsForms.AllUserControl
{
    public partial class UC_AddRoom : UserControl
    {

        RoomsBLL bll = new RoomsBLL();
        public UC_AddRoom()
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;  // Thêm dòng này
            InitializeComponent();

        }

        //object sender: Tham chiếu đến đối tượng kích hoạt sự kiện, trong trường hợp này là UC_AddRoom.
        //EventArgs e: Chứa thông tin chi tiết về sự kiện. Trong trường hợp này, không có thông tin chi tiết nào được sử dụng.
        private void UC_AddRoom_Load(object sender, EventArgs e)
        {
          
            DataSet ds = bll.GetAllRooms();
            DataGridView1.DataSource = ds.Tables[0];

        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem tất cả các trường nhập liệu có được điền đầy đủ hay không
            if (txtRoomNo.Text != "" && txtRoomType.Text != "" && txtBed.Text != "" && txtPrice.Text != "")
            {
                // Lấy giá trị từ các trường nhập liệu
                string roomno = txtRoomNo.Text;
                string type = txtRoomType.Text;
                string bed = txtBed.Text;
                Int64 price = Int64.Parse(txtPrice.Text);// Chuyển đổi giá từ chuỗi sang số nguyên lớn


                // Tạo truy vấn SQL để thêm dữ liệu vào cơ sở dữ liệu

                bll.AddRoom(roomno, type, bed, price);
                MessageBox.Show("Đã thêm phòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Tải lại dữ liệu (có lẽ để cập nhật giao diện sau khi thêm phòng)
                UC_AddRoom_Load(this, null);

                clearAll();
            }
            else
            {
                MessageBox.Show("Xin vui lòng nhập đủ thông tin", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void clearAll()
        {
            txtRoomNo.Clear(); // dùng cho TextBox
            txtRoomType.SelectedIndex = -1; //dùng cho Combo Box ( không trảng thái nào được chọn )
            txtBed.SelectedIndex = -1; //dùng cho Combo Box ( không trảng thái nào được chọn )
            txtPrice.Clear();
        }

        private void UC_AddRoom_Leave(object sender, EventArgs e)
        {
            clearAll(); //xóa khi chuyển từ mục này sang muc khác ( mất tiêu điểm )

        }

        private void UC_AddRoom_Enter(object sender, EventArgs e)
        {
            UC_AddRoom_Load(this, null);  // Nhận tiêu điểm khi chuyển từ mục khác sang và Load dữ liệu
        }

        private void btbDeleteRoom_Click(object sender, EventArgs e)
        {
            if (DataGridView1.SelectedRows.Count > 0) // kiểm tra dòng mình chọn phải có
            {
                // Lấy dòng đang được chọn
                DataGridViewRow row = DataGridView1.SelectedRows[0];

                string roomno = row.Cells["roomNo"].Value.ToString();
                string type = row.Cells["roomType"].Value.ToString();
                string bed = row.Cells["bed"].Value.ToString();
                Int64 price = Int64.Parse(row.Cells["price"].Value.ToString());

                bll.DeleteRoom(roomno, type, bed, price);
                MessageBox.Show("Đã xóa phòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Cập nhật DataGridView
                UC_AddRoom_Load(this, null);
            }

        }

        private void txtRoomNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Hủy sự kiện, không cho phép nhập ký tự này
                MessageBox.Show("Vui lòng chỉ nhập số cho số phòng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtRoomNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (DataGridView1.SelectedRows.Count > 0) // Kiểm tra dòng mình chọn phải có
            {
                // Lấy dòng đang được chọn
                DataGridViewRow row = DataGridView1.SelectedRows[0];
                DataGridView1.Columns["roomNo"].ReadOnly = true;
                string roomno = row.Cells["roomNo"].Value.ToString();

                if (txtRoomNo.Text != "" && txtRoomType.Text != "" && txtBed.Text != "" && txtPrice.Text != "")
                {
                    string type = txtRoomType.Text;
                    string bed = txtBed.Text;
                    Int64 price = Int64.Parse(txtPrice.Text);

                    bll.UpdateRoom(roomno, type, bed, price);
                    MessageBox.Show("Đã cập nhật thông tin phòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Tải lại dữ liệu
                    UC_AddRoom_Load(this, null);
                    clearAll();
                }
                else
                {
                    MessageBox.Show("Xin vui lòng nhập đủ thông tin", "Warning !", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Xin vui lòng chọn một phòng để chỉnh sửa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo không phải là header
            {
                DataGridViewRow row = DataGridView1.Rows[e.RowIndex];

                // Đổ dữ liệu từ dòng được chọn vào các trường nhập liệu
                txtRoomNo.Text = row.Cells["roomNo"].Value.ToString();
                txtRoomType.Text = row.Cells["roomType"].Value.ToString();
                txtBed.Text = row.Cells["bed"].Value.ToString();
                txtPrice.Text = row.Cells["price"].Value.ToString();
            }
        }

        private void btnImportFromExcel_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (var package = new ExcelPackage(new FileInfo(ofd.FileName)))
                    {
                        var worksheet = package.Workbook.Worksheets[0];
                        var rowCount = worksheet.Dimension.Rows;

                        DataTable tbl = new DataTable();

                        for (int j = 1; j <= worksheet.Dimension.Columns; j++)
                        {
                            tbl.Columns.Add(worksheet.Cells[1, j].Text);
                        }

                        for (int i = 2; i <= rowCount; i++)
                        {
                            DataRow row = tbl.NewRow();
                            for (int j = 1; j <= worksheet.Dimension.Columns; j++)
                            {
                                row[j - 1] = worksheet.Cells[i, j].Text;
                            }
                            tbl.Rows.Add(row);
                        }

                        foreach (DataRow dr in tbl.Rows)
                        {
                            string roomno = dr["roomNo"].ToString();
                            string type = dr["roomType"].ToString();
                            string bed = dr["bed"].ToString();
                            Int64 price = Int64.Parse(dr["price"].ToString());

                            int count = bll.CheckRoomExistence(roomno);

                            if (count == 0) // Nếu không có dữ liệu trùng lặp
                            {
                                // Thêm dữ liệu vào cơ sở dữ liệu
                                bll.AddRoom(roomno, type, bed, price);
                            }
                        }

                        DataGridView1.DataSource = tbl;

                        // Tải lại dữ liệu sau khi nhập
                        UC_AddRoom_Load(this, null);

                        // Hiển thị thông báo cuối cùng sau khi đã thêm tất cả dữ liệu từ Excel
                        MessageBox.Show("Thêm dữ liệu từ Excel vào cơ sở dữ liệu hoàn tất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnExportFromExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                   
                    using (var package = new ExcelPackage())
                    {
                        var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                        for (int i = 1; i <= DataGridView1.Columns.Count; i++)
                        {
                            worksheet.Cells[1, i].Value = DataGridView1.Columns[i - 1].HeaderText;
                        }

                        for (int i = 1; i <= DataGridView1.Rows.Count; i++)
                        {
                            for (int j = 1; j <= DataGridView1.Columns.Count; j++)
                            {
                                worksheet.Cells[i + 1, j].Value = DataGridView1.Rows[i - 1].Cells[j - 1].Value?.ToString();
                            }
                        }

                        package.SaveAs(new FileInfo(sfd.FileName));
                    }
                }
            }
        }

        private void ExportToPdf(DataGridView dgv, string filename)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdfTable = new PdfPTable(dgv.Columns.Count);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 100;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;

            foreach (DataGridViewColumn column in dgv.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText, new iTextSharp.text.Font(bf, 12)));
                pdfTable.AddCell(cell);
            }

            foreach (DataGridViewRow row in dgv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null)
                        pdfTable.AddCell(new Phrase(cell.Value.ToString(), new iTextSharp.text.Font(bf, 10)));
                }
            }

            using (FileStream stream = new FileStream(filename, FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
            }

            MessageBox.Show("Đã xuất dữ liệu ra file PDF!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExportToPdf_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF files|*.pdf" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ExportToPdf(DataGridView1, sfd.FileName);
                }
            }
        }
    }
}
