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
    public partial class UpdateInstractor : Form
    {
        SqlProjectEntities1 ctx = new SqlProjectEntities1();
        public UpdateInstractor()
        {
            InitializeComponent();
        }
        IQueryable<Instructor> Instructors;
        IQueryable<Cours> cours;
        private void UpdateInstractor_Load(object sender, EventArgs e)
        {
            Instructors = from i in ctx.Instructors
                          where i.flag == true
                          select i;

            comboBox1.Items.AddRange(Instructors.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Please Select Instructor");

            }
            else
            {
                var ins = (from i in ctx.Instructors
                           where i.Instructor_ID == ((Instructor)comboBox1.SelectedItem).Instructor_ID
                           select i).FirstOrDefault();
                var log = (from l in ctx.Logins
                           where l.Login_ID == ins.Login_ID
                           select l).FirstOrDefault();
                log.User_Name = textBox1.Text;
                log.User_Password = textBox2.Text;
                ins.Instructor_Name = textBox3.Text;
                var cou = (from c in ctx.Courses
                           where c.Instructor_ID == ins.Instructor_ID
                           
                             select c).FirstOrDefault();
                if(cou!=null)
                cou.Course_Name = ((Cours)comboBox2.SelectedItem).Course_Name;
                else
                {
                    ctx.Courses.Add(new Cours
                    {
                        Course_Name = ((Cours)comboBox2.SelectedItem).Course_Name,
                        Course_Desc = "asd",
                        Course_Max_Degree = 100,
                        Course_Min_Degree = 50,
                        Instructor_ID = ins.Instructor_ID,
                        Track_ID = 8
                    });
                }
                
                ctx.SaveChanges();
                this.Close();
                


            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cours = from c in ctx.Courses
                    //where c.Instructor_ID == ((Instructor)comboBox1.SelectedItem).Instructor_ID
                    select c;
            comboBox2.Items.Clear();
            comboBox2.Items.AddRange(cours.ToArray());
        }
    }
}
