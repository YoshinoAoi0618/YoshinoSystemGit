using System;
using System.Windows.Forms;
using System.Data.SQLite;

namespace G2A232Project
{
    // メニュークラス
    public partial class Menu : Form
    {
        //変数
        private Top top;
        private Register register;
        private Change change;
        private Search search;
        private Delete delete;
        // SQL文を "const"で定数化
        // テーブル作成SQL
        private const string CREATE_TABLE = "create table MenberTable (ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
            "" + "Name TEXT, Address TEXT, Birth INTEGER, Tel TEXT, Email TEXT)";
        // テーブル削除SQL
        private const string dropTable = "drop table MenberTable";


        public Menu()
        {
            InitializeComponent();
        }
        private void MenuLoad(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// タイトル画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExitClick(object sender, EventArgs e)
        {
            top = new Top();
            top.Show();
            this.Visible = false;
        }
        /// <summary>
        /// 登録画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRegisterClick(object sender, EventArgs e)
        {
            register = new Register();
            register.Show();
            this.Visible = false;
        }
        /// <summary>
        /// 変更画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChangeClick(object sender, EventArgs e)
        {
            change = new Change();
            change.Show();
            this.Visible = false;
        }
        /// <summary>
        /// 検索画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSerachClick(object sender, EventArgs e)
        {
            search = new Search();
            search.Show();
            this.Visible = false;
        }
        /// <summary>
        /// 削除画面へ遷移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteClick(object sender, EventArgs e)
        {
            delete = new Delete();
            delete.Show();
            this.Visible = false;
        }
        /// <summary>
        /// テーブル作成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreatTbClick(object sender, EventArgs e)
        {
            try
            {
                // コネクションを開いてテーブル作成して閉じる  
                using (SQLiteConnection con = new SQLiteConnection("Data Source=g2a232.db"))
                {
                    con.Open();
                    using (SQLiteCommand command = con.CreateCommand())
                    {
                        //　データテーブルの内容(氏名,住所,生年月日,電話番号,メールアドレス)
                        command.CommandText = CREATE_TABLE;
                        command.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                // 例外が出た場合はエラーメッセージを表示
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// テーブル削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteTbClick(object sender, EventArgs e)
        {
            // システム終了の確認ダイアログ
            if (MessageBox.Show("削除してもよろしいですか?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.No)
            {
                return;
            }
            else
            {
                // コネクションを開いてテーブル削除して閉じる  
                using (SQLiteConnection con = new SQLiteConnection("Data Source=g2a232.db"))
                {
                    con.Open();
                    using (SQLiteCommand command = con.CreateCommand())
                    {
                        command.CommandText = dropTable;
                        command.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }

        }
    }
}

