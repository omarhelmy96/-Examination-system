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
    public partial class TrainingManager : Form
    {
        SqlProjectEntities1 context = new SqlProjectEntities1();
        public TrainingManager()
        {
            InitializeComponent();
        }
        public int id;
        public void SetValues(int login_ID)
        {
            
            var NameQuery = (from t in context.TrainingManagers
                             where t.Login_ID == login_ID
                             select new { n = t.Training_Mang_Name, b = t.Branch_ID }).FirstOrDefault();
            TRName.Text = NameQuery.n;

            var BranchQuery = (from b in context.Branches
                               where b.Branch_ID == NameQuery.b
                               select b.Branch_Name).FirstOrDefault();
            TRBranch.Text = BranchQuery;
            
            var InstructorList = (from i in context.Instructors
                                  where i.flag == true
                                 select i.Instructor_Name).ToArray();
            listBox1.DataSource = null;
            listBox1.DataSource = InstructorList;

            var DepartmentList = from d in context.Departments
                                 select d.Department_Name;
            listBox2.DataSource = DepartmentList.ToArray();

            id = login_ID;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddInstructor addInstructor = new AddInstructor();
            addInstructor.SetValues(id);
            addInstructor.ShowDialog();
            SetValues(id);
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateInstractor updateInstractor = new UpdateInstractor();
            updateInstractor.ShowDialog();
            SetValues(id);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Please select instructor");
            }
            else
            {
                var q = (from i in context.Instructors
                        where i.Instructor_ID == ((Instructor)listBox1.SelectedItem).Instructor_ID
                        select i).FirstOrDefault();
                q.flag = false;
                context.SaveChanges();
                SetValues(id);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddDepatemnt addDepatemnt = new AddDepatemnt();
            addDepatemnt.ShowDialog();
            SetValues(id);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            UpdateDepartment update = new UpdateDepartment();
            update.ShowDialog();
            SetValues(id);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            context.Departments.Remove(((Department)listBox2.SelectedItem));
            context.SaveChanges();
            SetValues(id);
        }

        int DepId;

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            var q = from t in context.Tracks
                    where t.Department_ID == ((Department)listBox2.SelectedItem).Department_ID
                    select t;
            listBox3.DataSource = q.ToList();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            AddTrack addTrack = new AddTrack();
            addTrack.ShowDialog();
            SetValues(id);
        }

        private void TrainingManager_Load(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            UpdateTrack updateTrack = new UpdateTrack();
            updateTrack.ShowDialog();
            SetValues(id);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            context.Tracks.Remove(((Track)listBox3.SelectedItem));
            context.SaveChanges();
            SetValues(id);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Student_Page student_Page = new Student_Page();
            student_Page.FormClosed += (st, args) => this.Close();
            this.Hide();
            student_Page.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                context.Intakes.Add(new Intake { Intake_No =int.Parse(textBox1.Text) });
                context.SaveChanges();
            }
            else
            {
                MessageBox.Show("Please Enter Intake number");
            }
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var q = from c in context.Courses
                    where c.Track_ID == ((Track)listBox3.SelectedItem).Track_ID
                    select c;
            listBox4.DataSource = q.ToList();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var q = (from c in context.Courses
                     where c.Course_ID == ((Cours)listBox4.SelectedItem).Course_ID
                     select c).FirstOrDefault();
            context.Courses.Remove(q);
            context.SaveChanges();
            SetValues(id);
        }

        private void button14_Click(object sender, EventArgs e)
        {
            AddCourse addCourse = new AddCourse();
            addCourse.ShowDialog();
            SetValues(id);
        }
    }
}
