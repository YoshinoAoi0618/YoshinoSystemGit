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
    public partial class Delete : Form
    {
        private Menu _menu;
        public Delete()
        {
            InitializeComponent();
        }
        private void Delete_Load(object sender, EventArgs e)
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
        //戻る
        private void button2_Click(object sender, EventArgs e)
        {
            _menu = new Menu();
            _menu.Show();
            this.Visible = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection("Data Source=G2A232.db"))
                {
                    con.Open();
                    if(MessageBox.Show("削除してもよろしいですか？","注意",MessageBoxButtons.YesNo,MessageBoxIcon.Hand)==DialogResult.Yes)
                    {
                        using (SQLiteTransaction trans = con.BeginTransaction())
                        {
                            SQLiteCommand cmd = con.CreateCommand();
                            // インサート
                            cmd.CommandText = "DELETE FROM t_product WHERE ID = @Id;";
                            // パラメータセット
                            cmd.Parameters.Add("Id", System.Data.DbType.Int64);
                            // データ削除
                            cmd.Parameters["Id"].Value = int.Parse(textBox6.Text);
                            cmd.ExecuteNonQuery();
                            // コミット
                            trans.Commit();
                        }
                    }
                    
                }
            }
            //条件に合わなかったら、メッセージボックスにエラー内容を表示
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
        //確認
        private void button1_Click(object sender, EventArgs e)
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
    }
}
