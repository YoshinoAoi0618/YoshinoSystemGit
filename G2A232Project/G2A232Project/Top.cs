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
        //変数
        private Menu _menu;


        public Top()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
        }
        // 会員登録管理画面に遷移
        private void Btn_start_Click_1(object sender, EventArgs e)
        {

            // _menu変数にMenuを格納して
            _menu = new Menu();
            // 遷移したいForm.Show();
            _menu.Show();
            this.Visible = false;
        }

        //終了ボタン
        private void Btn_end_Click_1(object sender, EventArgs e)
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
