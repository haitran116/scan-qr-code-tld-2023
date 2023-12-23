using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;

namespace Qr_Code_TLD_1213T10
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            //this.TopMost = true;
            //this.ShowInTaskbar = true;
        }

        List<nguoi> danh_sach_tat_ca_nguoi = new List<nguoi>();

        List<nguoi> danh_sach_nguoi_check_in = new List<nguoi>();

        string name_file_check_in = "";

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(@"image_sys/background_chot_13_10.png");

            Cursor.Hide();

            DocFileExcel();

            // tạo trường cho dữ liệu check in
            nguoi nguoi1 = new nguoi("Mã QR", "Đơn vị", "Họ và Tên", "Giới tính", "Chức vụ", "Time check");
            danh_sach_nguoi_check_in.Add(nguoi1);

            // tạo tên file lưu data check in 
            name_file_check_in = string.Format("data_check_in_{0}", DateTime.Now.ToString().Replace("/", "_").Replace(" ", "_").Replace(":", "_"));

            lable_hovaten.Hide();

            label_chucvu.Text = "";
            label_chucvu.Hide();

            label_result_qrcode.Text = "";

            timer_scanning_qrcode.Start();
        }

        private void DocFileExcel()
        {
            // Tạo đối tượng FileInfo
            var file = new System.IO.FileInfo(@"data/danhsach.xlsx");

            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            List<string> lines = new List<string>();
            // Tạo đối tượng ExcelPackage
            using (var package = new ExcelPackage(file))
            {
                // Lấy sheet đầu tiên trong file
                var worksheet = package.Workbook.Worksheets[1];

                // Lấy số dòng và cột của sheet
                int rows = worksheet.Dimension.End.Row;
                int cols = worksheet.Dimension.End.Column;

                var temp_ = "";

                // Duyệt từng ô trong shOfficeOpenXml.LicenseException: 'Please set the ExcelPackage.LicenseContext property. See
                for (int r = 2; r <= rows; r++)
                {
                    var maqr_cellValue = worksheet.Cells[r, 1].Value;
                    var donvi_cellValue = worksheet.Cells[r, 2].Value;
                    var hoten_cellValue = worksheet.Cells[r, 3].Value;
                    var gioitinh_cellValue = worksheet.Cells[r, 4].Value;
                    var chucvu_cellValue = worksheet.Cells[r, 5].Value;

                    nguoi nguoi = new nguoi();

                    // mã qr
                    if (maqr_cellValue != null && !string.IsNullOrWhiteSpace(maqr_cellValue.ToString()))
                    {
                        // Xử lý ô dữ liệu ở đây
                        nguoi.maqr = maqr_cellValue.ToString();
                    }
                    else
                    {
                        // Ô dữ liệu rỗng, xử lý tùy ý hoặc bỏ qua
                        nguoi.maqr = "data rỗng";
                    }

                    // đơn vị
                    if (donvi_cellValue != null && !string.IsNullOrWhiteSpace(donvi_cellValue.ToString()))
                    {
                        // Xử lý ô dữ liệu ở đây
                        nguoi.donvi = donvi_cellValue.ToString().Trim();
                        temp_ = nguoi.donvi;
                    }
                    else
                    {
                        // Ô dữ liệu rỗng, xử lý tùy ý hoặc bỏ qua
                        nguoi.donvi = temp_.ToString();
                    }

                    // họ tên
                    if (hoten_cellValue != null && !string.IsNullOrWhiteSpace(hoten_cellValue.ToString()))
                    {
                        // Xử lý ô dữ liệu ở đây
                        nguoi.hoten = hoten_cellValue.ToString().Trim();
                    }
                    else
                    {
                        // Ô dữ liệu rỗng, xử lý tùy ý hoặc bỏ qua
                        nguoi.hoten = "";
                    }

                    // giới tính
                    if (gioitinh_cellValue != null && !string.IsNullOrWhiteSpace(gioitinh_cellValue.ToString()))
                    {
                        // Xử lý ô dữ liệu ở đây
                        nguoi.gioitinh = gioitinh_cellValue.ToString();
                    }
                    else
                    {
                        // Ô dữ liệu rỗng, xử lý tùy ý hoặc bỏ qua
                        nguoi.gioitinh = "";
                    }

                    // chức vụ
                    if (chucvu_cellValue != null && !string.IsNullOrWhiteSpace(chucvu_cellValue.ToString()))
                    {
                        // Xử lý ô dữ liệu ở đây
                        nguoi.chucvu = chucvu_cellValue.ToString().Trim();
                    }
                    else
                    {
                        // Ô dữ liệu rỗng, xử lý tùy ý hoặc bỏ qua
                        nguoi.chucvu = "";
                    }

                    danh_sach_tat_ca_nguoi.Add(nguoi);

                }
                int a = 0;
            }
        }

        private void Ghi_FileExcel()
        {
            // Tạo một tệp Excel mới
            using (var package = new ExcelPackage())
            {
                // Tạo một worksheet mới
                var worksheet = package.Workbook.Worksheets.Add("data_check_in");

                // Bắt đầu thêm dữ liệu vào worksheet

                worksheet.Cells["A1"].LoadFromCollection(danh_sach_nguoi_check_in);

                // Lưu tệp Excel vào đường dẫn cụ thể
                package.SaveAs(new FileInfo(string.Format(@"result\{0}.xlsx", name_file_check_in)));
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        Boolean check_scanning = false;
        int delay_view_result = 0;

        private void timer_scanning_qrcode_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_input_qrcode.Text))
            {
                // định nghĩa để tùy biến background của app
                check_scanning = true;
                delay_view_result = 0;
                this.BackgroundImage = Image.FromFile(@"image_sys/back_scaning.png");

                label_result_qrcode.Text = txt_input_qrcode.Text;
                txt_input_qrcode.Clear();

                show_result_scaner("show");

                show_nguoi_tham_du(label_result_qrcode.Text);

                can_giua_lable_all();

                can_giua_image(pictureBox_image_chandung);
            }

            if (!txt_input_qrcode.Focused)
            {
                txt_input_qrcode.Focus();
            }

            if (check_scanning == true)
            {
                delay_view_result += 100;

                // show kết quả quét sau X giây tự chuyển về back chính
                if (delay_view_result == 10000)
                {
                    this.BackgroundImage = Image.FromFile(@"image_sys/background_chot_13_10.png");

                    show_result_scaner("hide");

                    check_scanning = false;
                    delay_view_result = 0;
                }
            }
        }

        private void show_result_scaner(string ip)
        {
            if (ip.Equals("show"))
            {
                pictureBox_image_chandung.Show();
                //label_chucvu.Show();
            }
            else
            {
                pictureBox_image_chandung.Hide();
                //label_chucvu.Hide();

                lable_hovaten.Text = "";

                this.Invalidate();
            }
        }

        private void can_giua_lable_all()
        {
            can_giua_label_trong_form(label_chucvu.Width, label_chucvu, label_chucvu.Location.Y);
            can_giua_label_trong_form(lable_hovaten.Width, lable_hovaten, lable_hovaten.Location.Y);
        }

        private void can_giua_label_trong_form(int labelWidth, System.Windows.Forms.Label label_, int ylabel)
        {
            int formWidth = this.Width;

            int xlabel = (formWidth - labelWidth) / 2;

            label_.Location = new Point(xlabel, ylabel);
        }

        private void can_giua_image(PictureBox p)
        {
            p.Location = new Point((this.Width - p.Width) / 2, ((this.Height - p.Height) / 2) + 20);
        }

        private void show_nguoi_tham_du(string qrcode_)
        {
            int cout = danh_sach_tat_ca_nguoi.Count;
            int dem = 0;
            foreach (nguoi nguoi_ in danh_sach_tat_ca_nguoi)
            {
                if (nguoi_.maqr != "")
                {
                    if (nguoi_.maqr.Equals(qrcode_))
                    {
                        // phải set text cho lable họ tên để vẽ viền và căn giữa, vì cần dùng thuộc tính width, height của lable.
                        lable_hovaten.Text = "Đ/C " + nguoi_.hoten.ToUpper();

                        label_chucvu.Text = nguoi_.chucvu.ToUpper();

                        // kiểm tra độ lớn của chức vụ nếu dài quá thì giảm size font đi
                        if (label_chucvu.Height > 80)
                        {
                            // khi chức vụ có 3 dòng, thì set lại font cho vừa
                            label_chucvu.Font = new Font("UTM HelvetIns", 15, FontStyle.Regular, GraphicsUnit.Point);
                        }
                        else
                        {
                            // khi chức vụ có 2 dòng, thì set lại font cho vừa
                            label_chucvu.Font = new Font("UTM HelvetIns", 20, FontStyle.Regular, GraphicsUnit.Point);
                        }

                        string sourceFolder = string.Format(@"data\file_anh\{0}\", nguoi_.donvi.Trim());
                        string sourcePath = Application.StartupPath + sourceFolder;

                        // Lấy danh sách tên tập tin trong thư mục
                        string[] fileNames = Directory.GetFiles(sourcePath);

                        Boolean check_no_image = true;

                        // check lần một đưa họ tên để so sánh
                        foreach (string fileName in fileNames)
                        {
                            if (File.Exists(fileName))
                            {
                                string file_name_image = Path.GetFileName(fileName).ToLower().Replace(" ", "");
                                string hoten_ = nguoi_.hoten.ToLower().Replace(" ", "");
                                int fn = file_name_image.Split(hoten_).Length;
                                if (fn > 1)
                                {
                                    this.Invalidate();

                                    pictureBox_image_chandung.Image = creat_stroke_image(fileName);

                                    check_no_image = false;

                                    nguoi temp_nguoi = new nguoi();
                                    temp_nguoi.maqr = nguoi_.maqr;
                                    temp_nguoi.donvi = nguoi_.donvi;
                                    temp_nguoi.hoten = nguoi_.hoten;
                                    temp_nguoi.gioitinh = nguoi_.gioitinh;
                                    temp_nguoi.chucvu = nguoi_.chucvu;
                                    temp_nguoi.timecheck = DateTime.Now.ToString();

                                    danh_sach_nguoi_check_in.Add(temp_nguoi);
                                    Ghi_FileExcel();

                                    break;
                                }
                            }
                        }

                        //check lại lần nữa bằng cách bỏ dấu tên người
                        if (check_no_image == true)
                        {
                            foreach (string fileName in fileNames)
                            {
                                if (File.Exists(fileName))
                                {
                                    string file_name_image_ = BoDauTiengViet(Path.GetFileName(fileName).ToLower()).Replace(" ", "");
                                    string hoten__ = BoDauTiengViet(nguoi_.hoten.ToLower()).Replace(" ", "");
                                    int fn_ = file_name_image_.Split(hoten__).Length;
                                    if (fn_ > 1)
                                    {
                                        this.Invalidate();

                                        pictureBox_image_chandung.Image = creat_stroke_image(fileName);

                                        check_no_image = false;

                                        nguoi temp_nguoi = new nguoi();
                                        temp_nguoi.maqr = nguoi_.maqr;
                                        temp_nguoi.donvi = nguoi_.donvi;
                                        temp_nguoi.hoten = nguoi_.hoten;
                                        temp_nguoi.gioitinh = nguoi_.gioitinh;
                                        temp_nguoi.chucvu = nguoi_.chucvu;
                                        temp_nguoi.timecheck = DateTime.Now.ToString();

                                        danh_sach_nguoi_check_in.Add(temp_nguoi);
                                        Ghi_FileExcel();

                                        break;
                                    }
                                }
                            }

                        }

                        // nếu tất cả check đề không có thì đưa ra không có ảnh chân dung
                        if (check_no_image == true)
                        {
                            this.Invalidate();
                            pictureBox_image_chandung.Image = Image.FromFile(@"image_sys\no_image.png");

                            nguoi temp_nguoi = new nguoi();
                            temp_nguoi.maqr = nguoi_.maqr;
                            temp_nguoi.donvi = nguoi_.donvi;
                            temp_nguoi.hoten = nguoi_.hoten;
                            temp_nguoi.gioitinh = nguoi_.gioitinh;
                            temp_nguoi.chucvu = nguoi_.chucvu;
                            temp_nguoi.timecheck = DateTime.Now.ToString();

                            danh_sach_nguoi_check_in.Add(temp_nguoi);
                            Ghi_FileExcel();
                        }

                        // dừng sau khi tìm thấy người
                        break;
                    }
                    dem++;
                }
            }

            // nếu số lần không có bằng với độ lớn của list all thì đưa ra mã qr không có trong csdl
            if (dem == cout)
            {
                lable_hovaten.Text = "Mã Qr không xác định!";
                label_chucvu.Text = "";

                this.Invalidate();

                pictureBox_image_chandung.Image = Image.FromFile(@"image_sys\no_image.png");
            }
        }

        public Bitmap creat_stroke_image(string path_image)
        {
            // Màu sắc của viền 
            Color strokeColor = Color.Navy;

            // Độ rộng của viền
            int strokeWidth = 3;

            // Tạo đối tượng Bitmap từ hình ảnh gốc
            using (Bitmap originalImage = new Bitmap(path_image))
            {
                // Tạo một Bitmap mới với kích thước bằng với hình ảnh gốc
                Bitmap imageWithStroke = new Bitmap(originalImage.Width, originalImage.Height);

                strokeWidth = do_rong_stroke_theo_size_anh(originalImage.Width);

                using (Graphics g = Graphics.FromImage(imageWithStroke))
                {
                    // Vẽ hình ảnh gốc lên Bitmap mới
                    g.DrawImage(originalImage, 0, 0, originalImage.Width, originalImage.Height);

                    // Tạo Pen với màu sắc và độ rộng cho viền
                    using (Pen strokePen = new Pen(strokeColor, strokeWidth))
                    {
                        // Đặt kiểu vẽ viền cho Pen
                        strokePen.Alignment = PenAlignment.Inset;

                        // Vẽ viền xung quanh hình ảnh
                        g.DrawRectangle(strokePen, 0, 0, imageWithStroke.Width - 1, imageWithStroke.Height - 1);
                    }
                }
                return imageWithStroke;
            }
        }

        public int do_rong_stroke_theo_size_anh(int w)
        {
            int strokeWidth = 3;
            if (w < 130)
            {
                strokeWidth = 2;
            }
            else if (w < 170)
            {
                strokeWidth = 3;
            }
            else if (w < 200)
            {
                strokeWidth = 4;
            }
            else if (w < 300)
            {
                strokeWidth = 8;
            }
            else if (w < 400)
            {
                strokeWidth = 9;
            }
            else if (w < 500)
            {
                strokeWidth = 10;
            }
            else if (w < 600)
            {
                strokeWidth = 13;
            }
            else if (w < 700)
            {
                strokeWidth = 14;
            }
            else if (w < 800)
            {
                strokeWidth = 15;
            }
            else if (w < 900)
            {
                strokeWidth = 16;
            }
            else if (w < 1000)
            {
                strokeWidth = 20;
            }
            else if (w < 1100)
            {
                strokeWidth = 21;
            }
            else if (w < 1200)
            {
                strokeWidth = 22;
            }
            else if (w < 1300)
            {
                strokeWidth = 23;
            }
            else if (w < 1400)
            {
                strokeWidth = 24;
            }
            else
            {
                strokeWidth = 26;
            }

            return strokeWidth;
        }

        static string BoDauTiengViet(string hoTen)
        {
            // Sử dụng Normalization để chuẩn hóa chuỗi Unicode
            hoTen = hoTen.Normalize(NormalizationForm.FormD);

            // Loại bỏ tất cả các ký tự không phải là ký tự Latin hoặc số
            StringBuilder sb = new StringBuilder();
            foreach (char c in hoTen)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(c);
                }
            }

            return sb.ToString();
        }

        static string BoDauCach(string chuoi)
        {
            // Sử dụng phương thức Replace để thay thế dấu cách bằng không gì
            return chuoi.Replace(" ", "");
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            // nếu nội dung scan không rỗng thì vẽ
            if (label_result_qrcode.Text != "")
            {
                // Tạo một đối tượng Graphics để vẽ
                Graphics g = e.Graphics;

                // Tạo Font cho văn bản
                Font font = new Font("UTM HelvetIns", 41, FontStyle.Regular, GraphicsUnit.Point);

                // lấy text của lable họ tên
                string text = lable_hovaten.Text;
                Color textColor = Color.Red;

                // Màu viền và độ dày của viền
                Color strokeColor = Color.White;
                int strokeWidth = 9;

                // Tính toán kích thước và vị trí cho văn bản
                SizeF textSize = g.MeasureString(text, lable_hovaten.Font);

                //int hovaten_x = (this.Width - (lable_hovaten.Width + 12)) / 2;
                //int hovaten_y = lable_hovaten.Location.Y;

                int hovaten_x = (this.Width - (int)textSize.Width) / 2;
                int hovaten_y = lable_hovaten.Location.Y;

                // Tạo một hình chữ nhật cho văn bản
                Rectangle textRect = new Rectangle(hovaten_x, hovaten_y, this.Width, lable_hovaten.Height);

                // Tạo một đường viền (outline) cho văn bản bằng cách tạo một GraphicsPath
                GraphicsPath path = new GraphicsPath();
                path.AddString(text, font.FontFamily, (int)font.Style, font.Size, textRect, StringFormat.GenericDefault);

                // Tạo một Pen cho viền
                using (Pen strokePen = new Pen(strokeColor, strokeWidth))
                {
                    strokePen.LineJoin = LineJoin.Round; // Điều này giúp viền trông mượt mà hơn

                    // Bật tính năng chống răng cưa
                    g.SmoothingMode = SmoothingMode.AntiAlias;

                    // Vẽ các đoạn đường xung quanh văn bản để tạo viền
                    g.DrawPath(strokePen, path);
                }

                // Vẽ văn bản bên trên đường viền
                g.FillPath(new SolidBrush(textColor), path);
            }
        }

    }
}