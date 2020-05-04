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
    public partial class AddTrack : Form
    {
        SqlProjectEntities1 ctx = new SqlProjectEntities1();
        public AddTrack()
        {
            InitializeComponent();
        }

        private void AddTrack_Load(object sender, EventArgs e)
        {
            var DepartmentList = from d in ctx.Departments
                                 select d;
            comboBox1.DataSource = DepartmentList.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ctx.Tracks.Add(new Track { Track_Name = textBox1.Text, Department_ID = ((Department)comboBox1.SelectedItem).Department_ID });
            ctx.SaveChanges();
            this.Close();
        }
    }
}
