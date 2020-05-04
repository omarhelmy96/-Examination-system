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
    public partial class UpdateStudent : Form
    {
        SqlProjectEntities1 ctx = new SqlProjectEntities1();
        public UpdateStudent()
        {
            InitializeComponent();
        }

        private void UpdateStudent_Load(object sender, EventArgs e)
        {
            var q1 = from s in ctx.Students
                    select s;
            comboBox3.DataSource = q1.ToArray();
            var q2 = from s in ctx.Intakes
                     select s;
            var q3 = from s in ctx.Tracks
                     select s;
            comboBox1.DataSource = q3.ToList();
            comboBox2.DataSource = q2.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedItem == null||comboBox2.SelectedItem == null ||comboBox3.SelectedItem == null
                || textBox1.Text == ""|| textBox2.Text == ""|| textBox3.Text == "")
            {
                MessageBox.Show("Please fill all data");
            }
            else
            {
                var q1 = (from s in ctx.Students
                          where s.Student_ID == ((Student)comboBox3.SelectedItem).Student_ID
                          select s).FirstOrDefault();
                q1.Student_Name = textBox3.Text;

                var q2 = (from s in ctx.Logins
                          where s.Login_ID == ((Student)comboBox3.SelectedItem).Login_ID
                          select s).FirstOrDefault();

                q2.User_Name = textBox1.Text;
                q2.User_Password = textBox2.Text;
                ctx.SaveChangesAsync();
                this.Close();
            }


                    
        }
    }
}
