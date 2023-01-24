using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;

namespace G2A232Project
{
    // 検索クラス
    public partial class Search : Form
    {
        // 変数
        private Menu menu;

        // インサート文を "const"で定数化
        // SELECT文SQL
        private const string SEARCH_SELECT= "SELECT * FROM MenberTable WHERE Id= ";

        public Search()
        {
            InitializeComponent();
        }
        /// <summary>
        /// フォームをロード中の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchLoad(object sender, EventArgs e)
        {
            //偶数行の背景色を変更
            MenberTableDataView.AlternatingRowsDefaultCellStyle.BackColor = Color.SkyBlue;
            //Gridの幅を列に揃える
            MenberTableDataView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //行の高さをヘッダーとセルに合わせて自動調整
            MenberTableDataView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            //列の項目名を中央揃え
            MenberTableDataView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            //デフォルトだと空のセルが追加されるので、それを表示させない
            MenberTableDataView.AllowUserToAddRows = false;
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSearchClick(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection("Data Source=G2A232.db"))
                {
                    // DataTableを生成します。
                    DataTable dt = new DataTable();
                    // SQLの実行
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(SEARCH_SELECT + txt_search.Text, con);
                    adapter.Fill(dt);
                    MenberTableDataView.DataSource = dt;
                }
                txt_search.ResetText();
            }
            //条件に合わなかったら、メッセージボックスにエラー内容を表示
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //データ入力後"検索"ボタンを押したらテキストボックスをリセットする
            txt_search.ResetText();
        }
        /// <summary>
        /// 戻る
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
