﻿
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
namespace VoThanhTrong_2121110273_DoAnWindowsForms
{
    public partial class Form1 : Form
    {
        private const int MaxLoginAttempts = 3;
        private int loginAttempts = 0;
        private EmployeeBLL EmployeeDAL = new EmployeeBLL();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Đoạn này để ngăn chặn từ đầu ( Nếu không có đoạn này thì khi nhập quá 3 lần thì lần 4 có thể chấp nhận và ngược lại nó sẽ dừng thực thi )
            if (loginAttempts >= MaxLoginAttempts)
            {
                MessageBox.Show("Số lần đăng nhập vượt quá giới hạn. Vui lòng thử lại sau.");
                return; // Dừng thực thi ở đây
            }
            //thiết lập câu truy vấn
            bool isValidLogin = EmployeeDAL.ValidateLogin(txtUsername.Text, txtPassword.Text);
            if (isValidLogin)
            {
                labelError.Visible = false;//Ẩn câu thông báo lỗi
                Dashboard dash = new Dashboard();//Tạo đối tượng DashBoard 
                this.Hide();//Hide có nghĩa là nó ẩn nhưng vẫn còn dữ liệu khi chạy , còn close là tắt luôn
                dash.Show();//Show ra 
                
            }
            else
            {
                loginAttempts++; // Tăng số lần đăng nhập thất bại

                if (loginAttempts >= MaxLoginAttempts)
                {
                    MessageBox.Show("Số lần đăng nhập vượt quá giới hạn. Vui lòng thử lại sau.");
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng. Vui lòng thử lại.");
                }


                labelError.Visible = true;//Hiện câu thông báo lỗi nếu nhập sai thông tin
                txtPassword.Clear();
            }
        }
    }
}
