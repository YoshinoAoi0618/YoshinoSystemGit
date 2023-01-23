using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;

namespace G2A232Project
{
    // 変更クラス
    public partial class Change : Form
    {
        // 変数
        private Menu menu;

        // SQL文を "const"で定数化
        // UPDATE文SQL
        private const string CHANGE_UPDATE = "UPDATE MenberTable set Name = ?, Address = ?, Birth = ?, Tel = ?, Email = ? WHERE ID = @Id;";
        // SELECT文SQL
        private const string CHANGE_SELECT = "SELECT * FROM MenberTable";

        public Change()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// フォームロード中の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeLoad(object sender, EventArgs e)
        {
            //偶数行の背景色を変更
            MenberTableDataView.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue;
            //Gridの幅を列に揃える
            MenberTableDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //行の高さをヘッダーとセルに合わせて自動調整
            MenberTableDataView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //列の項目名を中央揃え
            MenberTableDataView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //フォームを開いたら自動でデータを表示
            disPlay();
        }
        /// <summary>
        /// メニューに戻る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExitClick(object sender, EventArgs e)
        {
            menu = new Menu();
            menu.Show();
            this.Visible = false;
        }
        /// <summary>
        /// 変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChangeClick(object sender, EventArgs e)
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
                        cmd.CommandText = CHANGE_UPDATE;
                        // パラメータセット
                        cmd.Parameters.Add("Name",DbType.String);
                        cmd.Parameters.Add("Address",DbType.String);
                        cmd.Parameters.Add("Birth",DbType.Int64);
                        cmd.Parameters.Add("Tel",DbType.String);
                        cmd.Parameters.Add("Email",DbType.String);
                        cmd.Parameters.Add("Id",DbType.Int64);
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
                disPlay();
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
        /// <summary>
        /// データベースの情報を表示させる
        /// </summary>
        private void disPlay()
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=G2A232.db"))
            {
                // DataTableを生成します。
                DataTable dt = new DataTable();
                // SQLの実行
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(CHANGE_SELECT, con);
                adapter.Fill(dt);
                MenberTableDataView.DataSource = dt;
            }
        }
    }
}
