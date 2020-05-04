using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test.Model;
namespace test
{
    public partial class AddDepatemnt : Form
    {
        SqlProjectEntities1 ctx = new SqlProjectEntities1();
        public AddDepatemnt()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ctx.Departments.Add(new Department { Department_Name = textBox1.Text });
            ctx.SaveChanges();
            this.Close();
        }
    }
}
