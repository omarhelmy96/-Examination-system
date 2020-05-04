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
    public partial class UpdateDepartment : Form
    {
        SqlProjectEntities1 ctx = new SqlProjectEntities1();
        public UpdateDepartment()
        {
            InitializeComponent();
        }

        int q;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
             q = ((Department)comboBox1.SelectedItem).Department_ID;
        }

        private void UpdateDepartment_Load(object sender, EventArgs e)
        {
            var qq = from d in ctx.Departments
                    select d;
            comboBox1.Items.AddRange(qq.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var query = (from d in ctx.Departments
                         where d.Department_ID == q
                         select d).FirstOrDefault();
            query.Department_Name = textBox1.Text;
            ctx.SaveChanges();
            this.Close();
        }
    }
}
