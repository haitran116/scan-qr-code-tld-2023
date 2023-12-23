namespace Qr_Code_TLD_1213T10
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            txt_input_qrcode = new TextBox();
            label_result_qrcode = new Label();
            timer_scanning_qrcode = new System.Windows.Forms.Timer(components);
            lable_hovaten = new Label();
            label_chucvu = new Label();
            pictureBox_image_chandung = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox_image_chandung).BeginInit();
            SuspendLayout();
            // 
            // txt_input_qrcode
            // 
            txt_input_qrcode.Location = new Point(640, 1000);
            txt_input_qrcode.Name = "txt_input_qrcode";
            txt_input_qrcode.Size = new Size(100, 23);
            txt_input_qrcode.TabIndex = 5;
            // 
            // label_result_qrcode
            // 
            label_result_qrcode.AutoSize = true;
            label_result_qrcode.BackColor = Color.Transparent;
            label_result_qrcode.Font = new Font("Tahoma", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            label_result_qrcode.ForeColor = Color.Azure;
            label_result_qrcode.Location = new Point(360, 900);
            label_result_qrcode.Name = "label_result_qrcode";
            label_result_qrcode.Size = new Size(35, 16);
            label_result_qrcode.TabIndex = 6;
            label_result_qrcode.Text = "1001";
            // 
            // timer_scanning_qrcode
            // 
            timer_scanning_qrcode.Tick += timer_scanning_qrcode_Tick;
            // 
            // lable_hovaten
            // 
            lable_hovaten.AutoSize = true;
            lable_hovaten.BackColor = Color.Transparent;
            lable_hovaten.Font = new Font("UTM HelvetIns", 30F, FontStyle.Regular, GraphicsUnit.Point);
            lable_hovaten.ForeColor = Color.Indigo;
            lable_hovaten.Location = new Point(30, 612);
            lable_hovaten.Name = "lable_hovaten";
            lable_hovaten.Size = new Size(435, 59);
            lable_hovaten.TabIndex = 3;
            lable_hovaten.Text = "Đ/C BÙI THỊ THANH GIANG";
            // 
            // label_chucvu
            // 
            label_chucvu.AutoSize = true;
            label_chucvu.BackColor = Color.Transparent;
            label_chucvu.Font = new Font("UTM HelvetIns", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            label_chucvu.ForeColor = Color.Snow;
            label_chucvu.Location = new Point(-4, 758);
            label_chucvu.Name = "label_chucvu";
            label_chucvu.Size = new Size(550, 80);
            label_chucvu.TabIndex = 4;
            label_chucvu.Text = "ỦY VIÊN BAN THƯỜNG VỤ, TRƯỞNG BAN NỮ CÔNG\r\nLIÊN ĐOÀN LAO ĐỘNG THÀNH PHỐ HÀ NỘI";
            label_chucvu.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox_image_chandung
            // 
            pictureBox_image_chandung.BackColor = Color.Transparent;
            pictureBox_image_chandung.Location = new Point(142, 298);
            pictureBox_image_chandung.Name = "pictureBox_image_chandung";
            pictureBox_image_chandung.Size = new Size(218, 227);
            pictureBox_image_chandung.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox_image_chandung.TabIndex = 0;
            pictureBox_image_chandung.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = (Image)resources.GetObject("$this.BackgroundImage");
            BackgroundImageLayout = ImageLayout.Stretch;
            ClientSize = new Size(512, 768);
            Controls.Add(label_chucvu);
            Controls.Add(pictureBox_image_chandung);
            Controls.Add(lable_hovaten);
            Controls.Add(label_result_qrcode);
            Controls.Add(txt_input_qrcode);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Load += Form1_Load;
            Paint += Form1_Paint;
            KeyDown += Form1_KeyDown;
            ((System.ComponentModel.ISupportInitialize)pictureBox_image_chandung).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txt_input_qrcode;
        private Label label_result_qrcode;
        private System.Windows.Forms.Timer timer_scanning_qrcode;
        private Label lable_hovaten;
        private Label label_chucvu;
        private PictureBox pictureBox_image_chandung;
    }
}