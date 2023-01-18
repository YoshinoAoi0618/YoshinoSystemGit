using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;

namespace G2A232Project
{
    // 登録クラス
    public partial class Register : Form
    {
        //変数
        private Menu menu;
        // SQL文を "const"で定数化
        // INSERT文SQL
        private const string REGISTER_INSERT = "INSERT INTO MenberTable(name, address, birth, tel, email)" +
            " VALUES (@Name, @Address, @Birth, @Tel, @Email)";
        // SELECT文SQL
        private const string REGISTER_SELECT = "SELECT * FROM MenberTable";

        public Register()
        {
            InitializeComponent();
        }
        /// <summary>
        /// フォームロード中の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterLoad(object sender, EventArgs e)
        {
            //偶数行の背景色を変更
            MenberTableDataView.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue;
            //Gridの幅を列に揃える
            MenberTableDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //行の高さをヘッダーとセルに合わせて自動調整
            MenberTableDataView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //列の項目名を中央揃え
            MenberTableDataView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRegisterClick(object sender, EventArgs e)
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
                        cmd.CommandText = REGISTER_INSERT;
                        // パラメータセット
                        cmd.Parameters.Add("Name",DbType.String);
                        cmd.Parameters.Add("Address",DbType.String);
                        cmd.Parameters.Add("Birth",DbType.Int64);
                        cmd.Parameters.Add("Tel",DbType.String);
                        cmd.Parameters.Add("Email",DbType.String);

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
                    var adapter = new SQLiteDataAdapter(REGISTER_SELECT, con);
                    adapter.Fill(dataTable);
                    MenberTableDataView.DataSource = dataTable;
                }
            }
            //条件に合わなかったら、メッセージボックスにエラー内容を表示
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
    }
}
