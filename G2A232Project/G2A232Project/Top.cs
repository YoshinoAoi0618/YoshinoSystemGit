using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G2A232Project
{
    public partial class Top : Form
    {
        public Top()
        {
            InitializeComponent();
        }
        //変数
        private Menu _menu;

        private void Form1_Load(object sender, EventArgs e)
        {
        }
        // 会員登録管理画面に遷移
        private void button1_Click_1(object sender, EventArgs e)
        {

            // f2変数にForm2を格納して
            _menu = new Menu();
            // 遷移したいForm.Show();
            _menu.Show();
            this.Visible = false;
        }

        //終了ボタン
        private void button2_Click_1(object sender, EventArgs e)
        {
            // システム終了の確認ダイアログ
            if (MessageBox.Show("終了してもよろしいですか?", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // アプリケーション終了
                Application.Exit();
            }
        }
    }
}
