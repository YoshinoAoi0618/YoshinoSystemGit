using System;
using System.Windows.Forms;

namespace G2A232Project
{
    // トップページクラス
    public partial class Top : Form
    {
        //変数
        private Menu menu;

        public Top()
        {
            InitializeComponent();
        }
        private void Form1Load(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// 会員登録管理画面に遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStartClick(object sender, EventArgs e)
        {

            // _menu変数にMenuを格納して
            menu = new Menu();
            // 遷移したいForm.Show();
            menu.Show();
            this.Visible = false;
        }

        /// <summary>
        /// 終了ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEndClick(object sender, EventArgs e)
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
