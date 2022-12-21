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
    public partial class Change : Form
    {
        public Change()
        {
            InitializeComponent();
        }
        private Menu _menu;

        private void Change_Load(object sender, EventArgs e)
        {
            //偶数行の背景色を変更
            changeDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue;
            //Gridの幅を列に揃える
            changeDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //行の高さをヘッダーとセルに合わせて自動調整
            changeDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //列の項目名を中央揃え
            changeDataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            //フォームを開いたら自動でデータを表示
            using (SQLiteConnection con = new SQLiteConnection("Data Source=G2A232.db"))
            {
                // DataTableを生成します。
                var dataTable = new DataTable();
                // SQLの実行
                var adapter = new SQLiteDataAdapter("SELECT * FROM t_product", con);
                adapter.Fill(dataTable);
                changeDataGridView.DataSource = dataTable;
            }
        }
        //メニューに戻る
        private void Btn_exit_Click(object sender, EventArgs e)
        {
            _menu = new Menu();
            _menu.Show();
            this.Visible = false;
        }
        //変更
        private void Btn_change_Click(object sender, EventArgs e)
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
                        //cmd.CommandText = "UPDATE t_product set Name = @Name, Address = @Address, Birth = @Birth, Tel = @Tel, Email = @Email WHERE ID = @Id;";
                        cmd.CommandText = "UPDATE t_product set Name = ?, Address = ?, Birth = ?, Tel = ?, Email = ? WHERE ID = @Id;";
                        // パラメータセット
                        cmd.Parameters.Add("Name", System.Data.DbType.String);
                        cmd.Parameters.Add("Address", System.Data.DbType.String);
                        cmd.Parameters.Add("Birth", System.Data.DbType.Int64);
                        cmd.Parameters.Add("Tel", System.Data.DbType.String);
                        cmd.Parameters.Add("Email", System.Data.DbType.String);
                        cmd.Parameters.Add("Id", System.Data.DbType.Int64);
                        // データ修正
                        cmd.Parameters["Name"].Value = txt_name.Text;
                        cmd.Parameters["Address"].Value = txt_address.Text;
                        cmd.Parameters["Birth"].Value = int.Parse(txt_birth.Text);
                        cmd.Parameters["Tel"].Value = txt_tel.Text;
                        cmd.Parameters["Email"].Value = txt_mail.Text;
                        cmd.Parameters["Id"].Value = int.Parse(txt_id.Text);
                        cmd.ExecuteNonQuery();
                        // コミット
                        trans.Commit();
                    }
                }
                //データ入力後"変更"ボタンを押したらテキストボックスをリセットする
                txt_name.ResetText();
                txt_address.ResetText();
                txt_birth.ResetText();
                txt_tel.ResetText();
                txt_mail.ResetText();
                txt_id.ResetText();
            }
            //条件に合わなかったら、メッセージボックスにエラー内容を表示
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }   
        }
        // 確認
        private void Btn_check_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=G2A232.db"))
            {
                // DataTableを生成します。
                var dataTable = new DataTable();
                // SQLの実行
                var adapter = new SQLiteDataAdapter("SELECT * FROM t_product", con);
                adapter.Fill(dataTable);
                changeDataGridView.DataSource = dataTable;
            }

        }
    }
}
