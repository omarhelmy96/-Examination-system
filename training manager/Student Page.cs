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
    public partial class Student_Page : Form
    {
        SqlProjectEntities1 ctx = new SqlProjectEntities1();
        public Student_Page()
        {
            InitializeComponent();
        }

        private void Student_Page_Load(object sender, EventArgs e)
        {
            var q = from s in ctx.Students
                    select s;
            listBox1.DataSource = null;
            listBox1.DataSource = q.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddStudent addStudent = new AddStudent();
            addStudent.ShowDialog();
            Student_Page_Load( sender,  e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var q1 = (from s in ctx.Students
                      where s.Login_ID == ((Student)listBox1.SelectedItem).Login_ID
                      select s).FirstOrDefault();
            ctx.Students.Remove(q1);
            ctx.SaveChanges();
            Student_Page_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateStudent updateStudent = new UpdateStudent();
            updateStudent.ShowDialog();
            Student_Page_Load(sender, e);
        }
    }
}
