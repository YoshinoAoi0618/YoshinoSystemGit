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
        public Register()
        {
            InitializeComponent();
        }
        private Menu _menu;
        private void Register_Load(object sender, EventArgs e)
        {
        }
        //登録
        private void button3_Click(object sender, EventArgs e)
        {
            
        }
        //戻る
        private void button2_Click(object sender, EventArgs e)
        {
            _menu = new Menu();
            _menu.Show();
            this.Enabled = false;
        }
        //確認
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
