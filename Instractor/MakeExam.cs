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

namespace test.Instractor
{
    public partial class MakeExam : Form
    {
         int summationOfDegree=0;
        SqlProjectEntities1 context = new SqlProjectEntities1();
        Exam NewExam = new Exam();
        Instructor instructor=new Instructor() ;
        public MakeExam()
        {
            InitializeComponent();
        }
        public MakeExam(Instructor Instractor)
        {
            this.instructor = Instractor;
            InitializeComponent();
        }
        
        


        private void QuestionSelect_Click(object sender, EventArgs e)
        {
            

        }

        private void MakeExam_Load(object sender, EventArgs e)
        {
            var listOfCource = context.Courses.Where(x => x.Instructor_ID == instructor.Instructor_ID).Select(Co => Co.Course_Name);
            
            foreach (var item in listOfCource)
            {
                Course_Name.Items.Add(item);
            }
            comboBox2.Enabled = false;
            RandomQuestion.Enabled = false;
            QuestionSelect.Enabled = false;
            MakeNewExam.Enabled = false;
            panel2.Visible = false;
            
        }

        private void SaveExam_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && textBox1.Text != "" && Course_Name.Text != "")
            {
                NewExam.Exam_Date = dateTimePicker1.Value;
                NewExam.Exam_END_Time = new TimeSpan(EndTme.Value.Hour, EndTme.Value.Minute, EndTme.Value.Second);
                NewExam.Exam_Start_Time = new TimeSpan(StartTime.Value.Hour, StartTime.Value.Minute, StartTime.Value.Second);
                NewExam.Exam_Type = comboBox1.Text;
                NewExam.Instructor_ID = instructor.Instructor_ID;
                string cor = Course_Name.SelectedItem.ToString();
                var selectedCourse = context.Courses.SingleOrDefault(c => c.Course_Name == cor);
                if (int.Parse(textBox1.Text) < selectedCourse.Course_Max_Degree)
                {
                    NewExam.Exam_Total_Degree = int.Parse(textBox1.Text);
                }
                else
                {
                    MessageBox.Show("this degree greater than max degree");
                    return;
                }
                var listOfStudent = context.Students.Where(x => x.Track_ID == selectedCourse.Track_ID);
                comboBox2.DisplayMember = "Student_Name";
                comboBox2.ValueMember = "Student_ID";
                foreach (var item in listOfStudent)
                {
                    
                    //comboBox2.ValueMember = item.Student_ID;
                    comboBox2.Items.Add(item);
                }
                context.Exams.Add(NewExam);
                context.SaveChanges(); NewExam.Exam_Date = dateTimePicker1.Value;
                QuestionSelect.Enabled = true;
                RandomQuestion.Enabled = true;
                MakeNewExam.Enabled = true;
                comboBox2.Enabled = true;
                SaveExam.Enabled = false;
            }
            else
                MessageBox.Show("enter all field");
        }

        private void QuestionSelect_Click_1(object sender, EventArgs e)
        {
            SaveExam.Enabled = false;
            listView1.Items.Clear();
            selectionExampanel1.Visible = true;
            selectionExampanel1.Show();
            List<QuestionPool> Questionlist = new List<QuestionPool>();
            string co = Course_Name.Text;
            var listOfCource = context.Courses.Where(x => x.Instructor_ID == instructor.Instructor_ID && x.Course_Name == co).ToList();
            foreach (var item in listOfCource)
            {
                var Question = context.QuestionPools.Where(x => x.Course_ID == item.Course_ID).ToList();
                foreach (var Que in Question)
                {
                    Questionlist.Add(Que);
                }

            }
            foreach (var item in Questionlist)
            {
                ListViewItem listItem = new ListViewItem(item.Question);
                var CourceName = context.Courses.SingleOrDefault(x => x.Course_ID == item.Course_ID);
                listItem.SubItems.Add(item.Default_Degree.ToString());
                listItem.SubItems.Add(item.Question_ID.ToString());
                listItem.SubItems.Add(item.Q_Type.ToString());
                listView1.Items.Add(listItem);
            }
            
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Instructor_Exam_Degree instructor_Exam = new Instructor_Exam_Degree();
            if (listView1.SelectedIndices.Count == 0)
                return;
            
            if (textBox2.Text != "")
            {
                summationOfDegree += int.Parse(textBox2.Text);
                
                if (summationOfDegree < int.Parse(textBox1.Text))
                {
                    instructor_Exam.Instructor_Degree = int.Parse(textBox2.Text);
                }

                else
                {
                    summationOfDegree -= int.Parse(textBox2.Text);
                    MessageBox.Show("enough Question");
                    return;
                }
                        
            }
            else
            {
                summationOfDegree += int.Parse(listView1.SelectedItems[0].SubItems[1].Text);
                if (summationOfDegree < int.Parse(textBox1.Text))
                {
                    
                    instructor_Exam.Instructor_Degree = int.Parse(listView1.SelectedItems[0].SubItems[1].Text);

                }

                else
                {
                    summationOfDegree -= int.Parse(listView1.SelectedItems[0].SubItems[1].Text);
                    MessageBox.Show("enough Question");
                    return;
                }
            }
            instructor_Exam.Question_Pool_ID =int.Parse( listView1.SelectedItems[0].SubItems[2].Text);
            instructor_Exam.Exam_ID = context.Exams.Max(x => x.Exam_ID) ;
            context.Instructor_Exam_Degree.Add(instructor_Exam);
            context.SaveChanges();
        }

        private void MakeNewExam_Click(object sender, EventArgs e)
        {
            selectionExampanel1.Visible = false;
            SaveExam.Enabled = true;
            MakeNewExam.Enabled = false;
            QuestionSelect.Enabled = false;
            RandomQuestion.Enabled = false;
            MakeNewExam.Enabled = false;
            comboBox2.Enabled = false;
        }

        private void RandomQuestion_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
            panel2.Show();
        }

        private void RandomMakeExam_Click(object sender, EventArgs e)
        {
            if(textBox3.Text!="")
            {
                int NumberOFQuestion;
                int ExamDegree;
                bool isExamDegree = int.TryParse(textBox1.Text, out ExamDegree);
                bool isNumber = int.TryParse(textBox3.Text,out NumberOFQuestion);
                if(isNumber&& isExamDegree)
                {
                    Instructor_Exam_Degree instructor_Exam = new Instructor_Exam_Degree();
                    List<QuestionPool> Questionlist = new List<QuestionPool>();
                    string co = Course_Name.Text;
                    var listOfCource = context.Courses.Where(x => x.Instructor_ID == instructor.Instructor_ID && x.Course_Name == co).ToList();
                    foreach (var item in listOfCource)
                    {
                        var Question = context.QuestionPools.Where(x => x.Course_ID == item.Course_ID).OrderByDescending(x=>x.Default_Degree).ToList();
                        foreach (var Que in Question)
                        {
                            Questionlist.Add(Que);
                        }

                    }
                    int itrator = NumberOFQuestion;
                    for (int i = 0; i < itrator; i++)
                    {
                        foreach (var item in Questionlist)
                        {
                            if (item.Default_Degree<= (ExamDegree / NumberOFQuestion))
                            {
                                instructor_Exam.Instructor_Degree = item.Default_Degree;
                                instructor_Exam.Question_Pool_ID = item.Question_ID;
                                instructor_Exam.Exam_ID= context.Exams.Max(x => x.Exam_ID);
                                context.Instructor_Exam_Degree.Add(instructor_Exam);
                                context.SaveChanges();
                                ExamDegree = ExamDegree - (int)item.Default_Degree;
                                NumberOFQuestion -= 1;
                                Questionlist.Remove(item);
                                break;
                            }
                        }
                    }
                }
            }
            RandomMakeExam.Enabled = false;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Student_Result student_Exam = new Student_Result();
            Student std =(Student) comboBox2.SelectedItem;
            student_Exam.Student_ID = std.Student_ID;
            student_Exam.Exam_ID = context.Exams.Max(x=>x.Exam_ID);
            context.Student_Result.Add(student_Exam);
            context.SaveChanges();
        }
    }
}
