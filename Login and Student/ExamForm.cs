using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test.Model;

namespace test
{
    public partial class ExamForm : Form
    {
        int examid = -1;
        int count=0;
        int question_ID = 0;
        Exam ex = new Exam();
        Student stud = new Student();
        List<int?> questions;
        Dictionary<int, int> Question_type = new Dictionary<int, int>();
        Dictionary<int?, string> question_answer = new Dictionary<int?, string>();
        Dictionary<int, Dictionary<int?, string>> mcq_answer = new Dictionary<int, Dictionary<int?, string>>();
        int mcq_answer_count = 0;
        SqlProjectEntities1 context = new SqlProjectEntities1();
        public ExamForm()
        {

            InitializeComponent();
            TextQuestionPanel.Hide();
            TrueFalsePanel.Hide();
            McqPanel.Hide();
            button3.Hide();
            
        }
        public void setvalues(Student stu) {
            //var query = from s in context.Student_Result
            //            where s.Student_ID==stu.Student_ID
            //            select (s.Exam_ID);
            stud = stu;
            //the right one commented for testing purposes only
            var query =
                
                (from s in context.Students
                join sr in context.Student_Result on s.Student_ID equals sr.Student_ID
                join ex in context.Exams on sr.Exam_ID equals ex.Exam_ID
                
                select ex);
            
            foreach (var item in query)
            {
                if (item.Exam_Start_Time.Value.TotalSeconds <=DateTime.Now.TimeOfDay.TotalSeconds && item.Exam_END_Time.Value.TotalSeconds>DateTime.Now.TimeOfDay.TotalSeconds && item.Exam_Date == DateTime.Now.Date)
                {
                    examid = item.Exam_ID;
                    ex.Exam_Start_Time = item.Exam_Start_Time;
                    ex.Exam_END_Time = item.Exam_END_Time;
                    
                }
            }
            


            if (examid >-1)
            {
                questions = (from q in context.Instructor_Exam_Degree
                         where q.Exam_ID == examid
                         select (q.Question_Pool_ID)).ToList();
                
                assign_question();
            }
            else
            {
                timer1.Enabled = false;
                MessageBox.Show("you have no exams ATM");

                formlist.formslist[1].Show();
             
            }
        }

        private void ExamForm_Load(object sender, EventArgs e)
        {
            if (examid==-1)
            {
                this.Close();
            }
            else
            {
                timer1.Enabled = true;
                timer1.Interval = 1000;
            }
            
            previous_button.Enabled = false;
        }

        public void assign_question()
        {
            int or = (int)questions[count];
            var query = context.QuestionPools.Where(q => q.Question_ID == or).FirstOrDefault();
            count++;
            if (query != null)
            {
                //trueorfalse
                if (int.Parse(query.Q_Type) == 1)
                {
                    question_ID = query.Question_ID;
                    if (!Question_type.Keys.Contains(question_ID))
                    {
                        Question_type.Add(question_ID, int.Parse(query.Q_Type));
                        radioButton1.Checked = true;
                    }
                    else fillanswers();
                    TrueFalsePanel.Show();
                    TextQuestionPanel.Show();
                    QuestionLabel.Text = query.Question;
                    
                    McqPanel.Hide();
                }//textquestion
                else if (int.Parse(query.Q_Type) == 2)
                {
                    question_ID = query.Question_ID;
                    if (!Question_type.Keys.Contains(question_ID))
                        Question_type.Add(question_ID, int.Parse(query.Q_Type));
                    else fillanswers();
                    TextQuestionPanel.Show();
                    QuestionLabel.Text = query.Question;

                    TrueFalsePanel.Hide();
                    McqPanel.Hide();
                }//mcq question
                else if (int.Parse(query.Q_Type) == 3)
                {
                    
                    question_ID = query.Question_ID;
                    if (!Question_type.Keys.Contains(question_ID))
                    {
                        Question_type.Add(question_ID, int.Parse(query.Q_Type));
                        ShowMcqAnswers(query.Question_ID);
                    }
                    else
                    {
                        ShowMcqAnswers(query.Question_ID);
                        fillanswers();
                    }
                    TextQuestionPanel.Show();
                    TrueFalsePanel.Show();
                    McqPanel.Show();
                    QuestionLabel.Text = query.Question;
                    
                }
            }
        }

        public void ShowMcqAnswers(int id) {
            checkedListBox1.Items.Clear();
            var query = (from ans in context.MCQ_Multi_Ans
                         where ans.MCQ_ID==id
                         select ans.MCQ_Multi_Answar).ToList();
            foreach (var item in query)
            {
                checkedListBox1.Items.Add(item);
            }
        }
        //Nextbutton
        private void button1_Click(object sender, EventArgs e)
        {
            if (Question_type[question_ID] == 1)
            {
                question_answer[question_ID] = radioButton1.Checked.ToString();
               
            }
            else if (Question_type[question_ID] == 2)
            {
                question_answer[question_ID] = TextQuestionAnswer.Text;
                TextQuestionAnswer.Clear();
            }
            else if (Question_type[question_ID] == 3)
            {
                   
                foreach (var item in checkedListBox1.CheckedItems)
                {
                    Dictionary<int?, string> mcq_quset_answer = new Dictionary<int?, string>();
                    mcq_quset_answer[question_ID] = item.ToString();
                    mcq_answer.Add(mcq_answer_count, mcq_quset_answer);
                    mcq_answer_count++;
                    
                }
             
                checkedListBox1.Items.Clear();
            }
          
            McqPanel.Hide();
            TextQuestionPanel.Hide();
            TrueFalsePanel.Hide();
          
            if (count < questions.Count) {
                assign_question();
            }
            else
            {
                button3.Show();
            }

            previous_button.Enabled = true;

        }
        
        private void save()
        {
            foreach (var item in question_answer)
            {
                Student_Question_Degree st = new Student_Question_Degree();

                var question_pool_exam_question_id = context.Instructor_Exam_Degree.Where(q => q.Question_Pool_ID == item.Key).FirstOrDefault();
                st.question_pool_exam_id = question_pool_exam_question_id.Question_Pool_Exam_ID;
                context.Student_Question_Degree.Add(st);
                context.SaveChanges();
                Student_Answer stans = new Student_Answer();

                stans.Students_ID = stud.Student_ID;
                stans.Student_Answer1 = item.Value;
                stans.Student_Question_Degree_ID = st.Student_Q_Degree_ID;
                context.Student_Answer.Add(stans);
                context.SaveChanges();

            }
            foreach (var item in mcq_answer)
            {
                Student_Question_Degree st = new Student_Question_Degree();
                foreach (var item2 in item.Value)
                {
                    var question_pool_exam_question_id = context.Instructor_Exam_Degree.Where(q => q.Question_Pool_ID == item2.Key).FirstOrDefault();
                    st.question_pool_exam_id = question_pool_exam_question_id.Question_Pool_Exam_ID;
                    context.Student_Question_Degree.Add(st);
                    context.SaveChanges();
                    Student_Answer stans = new Student_Answer();

                    stans.Students_ID = stud.Student_ID;
                    stans.Student_Answer1 = item2.Value;
                    stans.Student_Question_Degree_ID = st.Student_Q_Degree_ID;
                    context.Student_Answer.Add(stans);
                    context.SaveChanges();
                }
               

            }

        }

        private void Submit(object sender, EventArgs e)
        {
            const string message = "are you sure you want to submit the answers ?";
            const string caption = "submit answers";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                MessageBox.Show("Submitted Successfully");
                save();
                this.Hide();
                formlist.formslist[1].Show();
            }
        }

        private void previous_button_Click(object sender, EventArgs e)
        {
            if (count>1)
            {
                count -= 2;
                if (Question_type[question_ID] == 1)
                {
                    
                    question_answer[question_ID] = radioButton1.Checked.ToString();
                }
                else if (Question_type[question_ID] == 2)
                {
                    
                    question_answer[question_ID] = TextQuestionAnswer.Text;
                }
                else if (Question_type[question_ID] == 3)
                {
                    foreach (var item in checkedListBox1.CheckedItems)
                    {
                        Dictionary<int?, string> mcq_quset_answer = new Dictionary<int?, string>();
                        mcq_quset_answer[question_ID] = item.ToString();
                        mcq_answer.Add(mcq_answer_count, mcq_quset_answer);
                        mcq_answer_count++;

                    }
                }
                assign_question();
                
                fillanswers();
            }
            else
            {
                previous_button.Enabled = false;
            }
            

        }
        private void fillanswers() {
            if (Question_type.Keys.Contains(question_ID))
            {
                if (Question_type[question_ID]==1)
                {
                    radioButton1.Checked =bool.Parse(question_answer[question_ID]);
                    radioButton2.Checked = !radioButton1.Checked;
                }
                else if (Question_type[question_ID] == 2)
                {
                    TextQuestionAnswer.Text = question_answer[question_ID];
                }
                else if (Question_type[question_ID] == 3)
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        checkedListBox1.SetItemCheckState(i, CheckState.Unchecked);
                    }
                    foreach (var item in mcq_answer)
                    {
                        foreach (var item2 in item.Value)
                        {
                            if (question_ID == item2.Key)
                            {
                                
                                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                                {
                                    if (checkedListBox1.Items[i].ToString() == item2.Value)
                                    {
                                        checkedListBox1.SetItemCheckState(i, CheckState.Checked);
                                       
                                    }
                                    

                                }

                            }
                        }
                        
                    }
                 
                }
            }
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double x = ex.Exam_END_Time.Value.TotalSeconds - DateTime.Now.TimeOfDay.TotalSeconds;
            if ((int)x==120)
            {
                MessageBox.Show("2 minutes remaining");
            }
            else if (x<=0)
            {
                save();
                this.Hide();
                formlist.formslist[1].Show();
                timer1.Enabled = false;
            }
            label1.Text = (DateTime.Now.TimeOfDay.Hours).ToString()+":"+(DateTime.Now.TimeOfDay.Minutes).ToString() +":"+ (DateTime.Now.TimeOfDay.Seconds).ToString();
        }
    }
}
