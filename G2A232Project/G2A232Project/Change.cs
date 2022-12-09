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
            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue;
            //Gridの幅を列に揃える
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //行の高さをヘッダーとセルに合わせて自動調整
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //列の項目名を中央揃え
            dataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        //メニューに戻る
        private void button1_Click(object sender, EventArgs e)
        {
            _menu = new Menu();
            _menu.Show();
            this.Visible = false;
        }
        //確認
        private void button2_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=G2A232.db"))
            {
                // DataTableを生成します。
                var dataTable = new DataTable();
                // SQLの実行
                var adapter = new SQLiteDataAdapter("SELECT * FROM t_product", con);
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
        }
        //変更
        private void button3_Click(object sender, EventArgs e)
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
                        cmd.CommandText = "UPDATE t_product set Name = @Name, Address = @Address, Birth = @Birth, Tel = @Tel, Email = @Email WHERE ID = @Id;";
                        // パラメータセット
                        cmd.Parameters.Add("Name", System.Data.DbType.String);
                        cmd.Parameters.Add("Address", System.Data.DbType.String);
                        cmd.Parameters.Add("Birth", System.Data.DbType.Int64);
                        cmd.Parameters.Add("Tel", System.Data.DbType.String);
                        cmd.Parameters.Add("Email", System.Data.DbType.String);
                        cmd.Parameters.Add("Id", System.Data.DbType.Int64);
                        // データ修正
                        cmd.Parameters["Name"].Value = textBox1.Text;
                        cmd.Parameters["Address"].Value = textBox2.Text;
                        cmd.Parameters["Birth"].Value = int.Parse(textBox3.Text);
                        cmd.Parameters["Tel"].Value = textBox4.Text;
                        cmd.Parameters["Email"].Value = textBox5.Text;
                        cmd.Parameters["Id"].Value = int.Parse(textBox6.Text);
                        cmd.ExecuteNonQuery();
                        // コミット
                        trans.Commit();
                    }
                }
                textBox1.ResetText();
                textBox2.ResetText();
                textBox3.ResetText();
                textBox4.ResetText();
                textBox5.ResetText();
                textBox6.ResetText();
            }
            //条件に合わなかったら、メッセージボックスにエラー内容を表示
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
        
    }
}
