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
    public partial class Register : Form
    {
        //変数
        private Menu _menu;

        public Register()
        {
            InitializeComponent();
        }

        private void Register_Load(object sender, EventArgs e)
        {
            //偶数行の背景色を変更
            register_DataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue;
            //Gridの幅を列に揃える
            register_DataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //行の高さをヘッダーとセルに合わせて自動調整
            register_DataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //列の項目名を中央揃え
            register_DataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
            //登録
            private void Btn_register_Click(object sender,System.Windows.Forms.KeyPressEventArgs e)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection("Data Source=G2A232.db"))
                {
                    con.Open();
                    using (SQLiteTransaction trans = con.BeginTransaction())
                    {
                        SQLiteCommand cmd = con.CreateCommand();
                        // インサート
                        cmd.CommandText = "INSERT INTO t_product (name, address, birth, tel, email) VALUES (@Name, @Address, @Birth, @Tel, @Email)";
                        // パラメータセット
                        cmd.Parameters.Add("Name", System.Data.DbType.String);
                        cmd.Parameters.Add("Address", System.Data.DbType.String);
                        cmd.Parameters.Add("Birth", System.Data.DbType.Int64);
                        cmd.Parameters.Add("Tel", System.Data.DbType.String);
                        cmd.Parameters.Add("Email", System.Data.DbType.String);

                        // データ追加
                        cmd.Parameters["Name"].Value = txt_name.Text;
                        cmd.Parameters["Address"].Value = txt_address.Text;
                        cmd.Parameters["Birth"].Value = int.Parse(txt_birth.Text);
                        cmd.Parameters["Tel"].Value = txt_tel.Text;
                        cmd.Parameters["Email"].Value = txt_mail.Text;
                        cmd.ExecuteNonQuery();
                        // コミット
                        trans.Commit();
                    }
                }
                //データ入力後"登録"ボタンを押したらテキストボックスをリセットする
                txt_name.ResetText();
                txt_address.ResetText();
                txt_birth.ResetText();
                txt_tel.ResetText();
                txt_mail.ResetText();

                //登録したらデータグリッドに表示
                using (SQLiteConnection con = new SQLiteConnection("Data Source=G2A232.db"))
                {
                    // DataTableを生成します。
                    var dataTable = new DataTable();
                    // SQLの実行
                    var adapter = new SQLiteDataAdapter("SELECT * FROM t_product", con);
                    adapter.Fill(dataTable);
                    register_DataGridView.DataSource = dataTable;
                }
            }
            //条件に合わなかったら、メッセージボックスにエラー内容を表示
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        //戻る
        private void Btn_exit_Click(object sender, EventArgs e)
        {
            _menu = new Menu();
            _menu.Show();
            this.Visible = false;
        }
    }
}
