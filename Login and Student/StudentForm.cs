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
    public partial class StudentForm : Form
    {
        SqlProjectEntities1 context = new SqlProjectEntities1();
        Student stu = new Student();
        public StudentForm()
        {
            InitializeComponent();
            listView1.Hide();
            formlist.formslist.Add(this);
        }
        public void setvalues(int id) {
            var query = context.Students.Select(s => s).Where(s => s.Login_ID == id).FirstOrDefault();
            stu = query;
            NameLabel.Text = stu.Student_Name;
        }
        private void StudentForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listView1.Show();
            listView1.Items.Clear();
            var query = context.Student_Result.Select(s =>s);
            
            foreach (var item in query)
            {
              string[] st = {examName(item.Exam_ID),item.Student_Result1.ToString()};
                if (st[0]!=null && st[1]!=null)
                {
                    var listviewItem = new ListViewItem(st);
                    listView1.Items.Add(listviewItem);
                }
                
                
            }



        }
        public string examName(int? id) {
            var query = from c in context.Courses
                        join qbool in context.QuestionPools on c.Course_ID equals qbool.Course_ID
                        join qbexam in context.Instructor_Exam_Degree on qbool.Question_ID equals qbexam.Question_Pool_ID
                        where qbexam.Exam_ID == id
                        select new { cs=c.Course_Name};
            
            foreach (var item in query)
            {
                return item.cs;
            }
            return null;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            formlist.formslist[0].Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            ExamForm ex = new ExamForm();
            this.Hide();
            ex.setvalues(stu);
            ex.Show();
            ex.FormClosed += (s, args) => this.Close();
        }
    }
}
