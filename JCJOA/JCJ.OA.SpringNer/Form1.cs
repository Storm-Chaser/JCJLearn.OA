using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JCJ.OA.SpringNer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            IUserInfoService userInfoService = (IUserInfoService)ctx.GetObject("UserInfoService");
            MessageBox.Show(userInfoService.ShowMsg());
        }
    }
}
