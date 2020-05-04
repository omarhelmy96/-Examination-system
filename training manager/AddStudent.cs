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
    public partial class AddStudent : Form
    {
        SqlProjectEntities1 ctx = new SqlProjectEntities1();
        public AddStudent()
        {
            InitializeComponent();
        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            var q1 = from t in ctx.Tracks
                     select t;
            comboBox1.DataSource = q1.ToList();
            var q2 = from t in ctx.Intakes
                     select t;
            comboBox2.DataSource = q2.ToList();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ctx.Logins.Add(new Login { Acc_Type = 3, User_Name = textBox1.Text, User_Password = textBox2.Text });
            ctx.SaveChanges();
            var q3 = (from l in ctx.Logins
                      where l.User_Name == textBox1.Text
                      select l.Login_ID).FirstOrDefault();

            ctx.Students.Add(new Student { Student_Name = textBox3.Text, Track_ID = ((Track)comboBox1.SelectedValue).Track_ID, Intake_ID = ((Intake)comboBox2.SelectedValue).Intake_ID, Login_ID = q3 });
            ctx.SaveChanges();
            this.Close();
        }
    }
}
