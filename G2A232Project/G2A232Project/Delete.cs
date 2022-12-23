﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;

namespace G2A232Project
{
    // 削除クラス
    public partial class Delete : Form
    {
        // 変数
        private Menu menu;

        // SQL文を "const"で定数化
        // DELETE文SQL
        private const string DELETE = "DELETE FROM MenberTable WHERE ID = @Id;";


        public Delete()
        {
            InitializeComponent();
        }
        /// <summary>
        /// フォームロード中の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteLoad(object sender, EventArgs e)
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
        /// 戻る
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExitClick(object sender, EventArgs e)
        {
            menu = new Menu();
            menu.Show();
            this.Visible = false;
        }
        /// <summary>
        /// 削除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteClick(object sender, EventArgs e)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection("Data Source=G2A232.db"))
                {
                    con.Open();
                    if (MessageBox.Show("削除してもよろしいですか？", "注意", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
                    {
                        using (SQLiteTransaction trans = con.BeginTransaction())
                        {
                            SQLiteCommand cmd = con.CreateCommand();
                            // インサート
                            cmd.CommandText = DELETE;
                            // パラメータセット
                            cmd.Parameters.Add("Id", System.Data.DbType.Int64);
                            // データ削除
                            cmd.Parameters["Id"].Value = int.Parse(txt_delete.Text);
                            cmd.ExecuteNonQuery();
                            // コミット
                            trans.Commit();
                        }
                    }

                }
            }
            //条件に合わなかったら、メッセージボックスにエラー内容を表示
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// 確認
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCheckClick(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection("Data Source=G2A232.db"))
            {
                // DataTableを生成します。
                var dataTable = new DataTable();
                // SQLの実行
                var adapter = new SQLiteDataAdapter("SELECT * FROM t_product", con);
                adapter.Fill(dataTable);
                MenberTableDataView.DataSource = dataTable;
            }
        }
    }
}
