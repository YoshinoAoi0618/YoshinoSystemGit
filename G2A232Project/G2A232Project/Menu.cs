using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace G2A232Project
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }
        //変数
        private Top _top;
        private Register _register;
        private Change _change;
        private Search _search;
        private Delete _delete;
        private void Menu_Load(object sender, EventArgs e)
        {
        }
        //タイトル画面へ遷移
        private void button1_Click(object sender, EventArgs e)
        {
            _top = new Top();
            _top.Show();
            this.Visible = false;
        }
        //登録画面へ遷移
        private void button2_Click(object sender, EventArgs e)
        {
            _register = new Register();
            _register.Show();
            this.Visible = false;
        }
        //変更画面へ遷移
        private void button3_Click(object sender, EventArgs e)
        {
            _change = new Change();
            _change.Show();
            this.Visible = false;
        }
        //検索画面へ遷移
        private void button4_Click(object sender, EventArgs e)
        {
            _search = new Search();
            _search.Show();
            this.Visible = false;
        }
        //削除画面へ遷移
        private void button5_Click(object sender, EventArgs e)
        {
            _delete = new Delete();
            _delete.Show();
            this.Visible = false;
        }
        //テーブル作成
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                // コネクションを開いてテーブル作成して閉じる  
                using (var con = new SQLiteConnection("Data Source=g2a232.db"))
                {
                    con.Open();
                    using (SQLiteCommand command = con.CreateCommand())
                    {
                        //　データテーブルの内容(氏名,住所,生年月日,電話番号,メールアドレス)
                        command.CommandText = "create table t_product (ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                                             "Name TEXT, Address TEXT, Birth INTEGER, Tel TEXT, Email TEXT)";
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
        //テーブル削除
        private void button7_Click(object sender, EventArgs e)
        {
            // システム終了の確認ダイアログ
            if (MessageBox.Show("削除してもよろしいですか?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
            {
                // コネクションを開いてテーブル削除して閉じる  
                using (var con = new SQLiteConnection("Data Source=g2a232.db"))
                {
                    con.Open();
                    using (SQLiteCommand command = con.CreateCommand())
                    {
                        command.CommandText =
                            "drop table t_product";
                        command.ExecuteNonQuery();
                    }
                    con.Close();
                }
            }
        }
    }
}

