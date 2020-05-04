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
    public partial class UpdateTrack : Form
    {
        SqlProjectEntities1 ctx = new SqlProjectEntities1();
        public UpdateTrack()
        {
            InitializeComponent();
        }

        private void UpdateTrack_Load(object sender, EventArgs e)
        {
            var DepartmentList = from d in ctx.Departments
                                 select d;
            comboBox1.DataSource = DepartmentList.ToArray();

            var TrackList = from t in ctx.Tracks
                                 select t;
            comboBox2.DataSource = TrackList.ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var q = (from t in ctx.Tracks
                     where t.Department_ID == ((Department)comboBox1.SelectedItem).Department_ID
                     && t.Track_ID == ((Track)comboBox2.SelectedItem).Track_ID
                     select t).FirstOrDefault();
            q.Track_Name = textBox1.Text;
            ctx.SaveChanges();
            this.Close();
        }
    }
}
