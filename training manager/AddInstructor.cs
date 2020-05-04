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
    public partial class AddInstructor : Form
    {
        SqlProjectEntities1 ctx = new SqlProjectEntities1();
        private int _id;

        public AddInstructor()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ctx.Logins.Add(new Login { User_Name = textBox1.Text, Acc_Type = 2, User_Password = textBox2.Text });
            ctx.SaveChanges();
            var LoginID = (from l in ctx.Logins
                          where l.User_Name == textBox1.Text
                          select l.Login_ID).FirstOrDefault();
            ctx.Instructors.Add(new Instructor { Instructor_Name = textBox3.Text, Manager_ID = _id, Login_ID = LoginID,flag=true });
            ctx.SaveChanges();
            
            this.Close();
        }

        internal void SetValues(int id)
        {
            _id = id;
        }

        private void AddInstructor_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
