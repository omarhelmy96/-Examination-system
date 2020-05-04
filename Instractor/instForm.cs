using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test;
using test.Instractor;
using test.Model;

namespace test
{
    public partial class instForm : Form
    {
        SqlProjectEntities1 context = new SqlProjectEntities1();
        public Login InstractorLogin { get; set; }
        public Instructor Instructor { get; set; }
        QuestionPool NewQuestion = new QuestionPool();
        MCQ_Multi_Ans multi_AnsOpject;
        List<MCQuestion> MCQuestion = new List<MCQuestion>();
        List<string> AllWordsChoices = new List<string>();

        public instForm()
        {
            formlist.formslist.Add(this);
            InitializeComponent();
        }
        public instForm(Login instractorLogin)
        {
            formlist.formslist.Add(this);
            this.InstractorLogin = instractorLogin;
            this.Instructor = context.Instructors.SingleOrDefault(x => x.Login_ID == InstractorLogin.Login_ID);
            InitializeComponent();
            
        }
        
        private void instForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (CloseCancel() == false)
            {
                e.Cancel = true;
            }
        }
        public static bool CloseCancel()
        {
            const string message = "Are you sure that you would like to Exit the program";
            const string caption = "close program";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                return true;
            else
                return false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            formlist.formslist[1].Show();
        }

        private void QuestionOperation_Click(object sender, EventArgs e)
        {
            panel1.Hide();
            UpdatePanel2.Show();
        }
        #region AddQuestion
        private void Add_Question_Click(object sender, EventArgs e)
        {
            MakeQuestionInPoolPanal.Show();
            var listOfCource = context.Courses.Where(x => x.Instructor_ID == Instructor.Instructor_ID).Select(Co => Co.Course_Name);
            foreach (var item in listOfCource)
            {
                Course_Name.Items.Add(item);
            }
            Add_Question.Enabled = false;
            UpdateQuestion.Enabled = false;
            Delete.Enabled = false;
            Save_Question.Enabled = false;
            Cancel_Question.Enabled = false;
        }

        private void SaveQuestion_Click(object sender, EventArgs e)
        {

            if (Question.Text != " " && Course_Name.SelectedItem != null && Type.SelectedItem != null && DefaltDegree.Text != "")
            {
                try
                {
                    NewQuestion.Default_Degree = int.Parse(DefaltDegree.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                string cor = Course_Name.SelectedItem.ToString();
                NewQuestion.Question = Question.Text;

                var selectedCourse = context.Courses.SingleOrDefault(c => c.Course_Name == cor);
                NewQuestion.Course_ID = selectedCourse.Course_ID;
                if (Type.SelectedItem.ToString()=="Text")
                {
                    NewQuestion.Q_Type ="2";
                }
                else if (Type.SelectedItem.ToString() == "multiple choice")
                {
                    NewQuestion.Q_Type = "3";
                }
                else if(Type.SelectedItem.ToString() == "True & False")
                {
                    NewQuestion.Q_Type = "1";

                }
                if (Type.SelectedItem.ToString() == "Text")
                {
                    CorrectAnswerTextPanal.Show();
                }
                else if (Type.SelectedItem.ToString() == "multiple choice")
                {
                    MakeQuestionInPoolPanal.Visible = true;
                    CorrectAnswerTextPanal.Show();
                    CorrectAnswerTextPanal.Visible = true;
                    TrueAndFalseAnsPanel4.Visible = true;

                    panel7.Visible = true;
                    button6.Visible = true;
                    panel7.Show();

                }
                else
                {
                    MakeQuestionInPoolPanal.Visible = true;
                    CorrectAnswerTextPanal.Show();
                    TrueAndFalseAnsPanel4.Visible = true;
                    TrueAndFalseAns.Visible = true;
                    TrueAndFalseAnsPanel4.Show();
                }
            }
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            NewQuestion = null;
            MakeQuestionInPoolPanal.Visible = false;
            TrueAndFalseAnsPanel4.Visible = false;
            CorrectAnsMultichoisePanal.Visible = false;
            CorrectAnswerTextPanal.Visible = false;
            panel7.Visible = false;
            Add_Question.Enabled = true;
            UpdateQuestion.Enabled = true;
            Delete.Enabled = true;
            Save_Question.Enabled = true;
            Cancel_Question.Enabled = true;

        }

        

        private void SaveCorrectTextAnswer_Click(object sender, EventArgs e)
        {
            bool flage = true;

            if (NewQuestion.Q_Type == "Text")
            {
                context.QuestionPools.Add(NewQuestion);
                context.SaveChanges();
                TextQuestion textQuestion = new TextQuestion();
                textQuestion.Question_Poll_ID = context.QuestionPools.Max(Q => Q.Question_ID);
                textQuestion.Text_Correct_Ans = TextCorrectAns.Text;
                context.TextQuestions.Add(textQuestion);
                context.SaveChanges();
            }
            else if (NewQuestion.Q_Type == "multiple choice")
            {
                MakeQuestionInPoolPanal.Visible = true;
                CorrectAnswerTextPanal.Show();
                CorrectAnswerTextPanal.Visible = true;
                TrueAndFalseAnsPanel4.Visible = true;
                panel7.Visible = true;
                button6.Visible = false;
                CorrectAnsMultichoisePanal.Visible = true;
                CorrectAnsMultichoisePanal.Show();

                foreach (var item in AllWordsChoices)
                {
                    checkedListBox1.Items.Add(item);
                }
                flage = false;
            }
            else
            {
                context.QuestionPools.Add(NewQuestion);
                context.SaveChanges();
                TureFalseQuestion TrueFalseQuestion = new TureFalseQuestion();
                TrueFalseQuestion.Question_Pool_ID = context.QuestionPools.Max(Q => Q.Question_ID);
                TrueFalseQuestion.TF_Correct_Ans = TrueAndFalseAns.SelectedItem.ToString() == "true" ? true : false;
                context.TureFalseQuestions.Add(TrueFalseQuestion);
                context.SaveChanges();
            }

            if (flage)
            {
                CorrectAnswerTextPanal.Visible = false;
                MakeQuestionInPoolPanal.Visible = false;
                Add_Question.Enabled = true;
                UpdateQuestion.Enabled = true;
                Delete.Enabled = true;
                Save_Question.Enabled = true;
                Cancel_Question.Enabled = true;
            }

        }

        private void InsertCorectAnswerMulti_Click(object sender, EventArgs e)
        {

            var allAnswers = checkedListBox1.CheckedItems;
            foreach (var item in allAnswers)
            {
                MCQuestion.Add(new MCQuestion()
                {
                    Question_Pool_ID = context.QuestionPools.Max(x => x.Question_ID) + 1,
                    MCQ_Correct_Ans = item.ToString()
                });

            }
        }

        private void InsertAllChoices_Click(object sender, EventArgs e)
        {
            AllWordsChoices.Add(WordsOption.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void SaveCorrectMultiAnswer_Click(object sender, EventArgs e)
        {
            context.QuestionPools.Add(NewQuestion);
            context.SaveChanges();
            foreach (var item in MCQuestion)
            {
                context.MCQuestions.Add(item);
                context.SaveChanges();
            }
            foreach (var item in AllWordsChoices)
            {
                multi_AnsOpject = new MCQ_Multi_Ans()
                {
                    MCQ_ID = context.QuestionPools.Max(x => x.Question_ID),
                    MCQ_Multi_Answar = item
                };
                context.MCQ_Multi_Ans.Add(multi_AnsOpject);
                context.SaveChanges();
            }
            CorrectAnswerTextPanal.Visible = false;
            MakeQuestionInPoolPanal.Visible = false;
            Add_Question.Enabled = true;
            UpdateQuestion.Enabled = true;
            Delete.Enabled = true;
            Save_Question.Enabled = true;
            Cancel_Question.Enabled = true;
        }
        #endregion

        #region UpdateQuestion
        private void UpdateQuestion_Click(object sender, EventArgs e)
        {
            
            if(listView1.SelectedItems.Count==1)
            {
                Updatepanel3.Visible = true;
                Updatepanel3.Show();
                Question_Text_Update.Text = listView1.SelectedItems[0].SubItems[0].Text;
                Default_Degree_Update.Text = listView1.SelectedItems[0].SubItems[2].Text;
                Cource_Name_Update.Text = listView1.SelectedItems[0].SubItems[1].Text;
                string type_Question = listView1.SelectedItems[0].SubItems[4].Text ;
                ListViewItem listItem;
                switch (type_Question)
                {
                    
                    case "1":
                        listView2.Visible = true;
                        listView2.Show();
                        int Question_Pool_ID = int.Parse(listView1.SelectedItems[0].SubItems[3].Text);
                        var TrueFalse_Q = context.TureFalseQuestions.SingleOrDefault(x => x.Question_Pool_ID == Question_Pool_ID /*Question_Pool_ID*/);
                        listItem = new ListViewItem(TrueFalse_Q.TF_Correct_Ans.ToString());
                        listItem.SubItems.Add(TrueFalse_Q.TF_Q_ID.ToString());
                        listView2.Items.Add(listItem);
                        
                        break;
                    case "2":
                        listView2.Visible = true;
                        listView2.Show();
                        int TxtQueId = int.Parse(listView1.SelectedItems[0].SubItems[3].Text);
                        var Text_Q = context.TextQuestions.SingleOrDefault(x => x.Question_Poll_ID == TxtQueId);
                        listItem = new ListViewItem(Text_Q.Text_Correct_Ans.ToString());
                        listItem.SubItems.Add(Text_Q.Text_Q_ID.ToString());
                        listView2.Items.Add(listItem);
                        
                        break;
                    case "3":
                        listView2.Visible = true;
                        listView2.Show();
                        listView3.Visible = true;
                        listView3.Show();
                        int MultiQueId = int.Parse(listView1.SelectedItems[0].SubItems[3].Text);
                        var textAnswer = context.MCQuestions.Where(x => x.Question_Pool_ID == MultiQueId).ToList();
                        foreach (var item in textAnswer)
                        {
                            listItem = new ListViewItem(item.MCQ_Correct_Ans.ToString());
                            listItem.SubItems.Add(item.MCQ_Q_ID.ToString());
                            listView2.Items.Add(listItem);
                        }
                        int McqIdQue = int.Parse(listView1.SelectedItems[0].SubItems[3].Text);
                        var textOption = context.MCQ_Multi_Ans.Where(x => x.MCQ_ID == McqIdQue).ToList();
                        foreach (var item in textOption)
                        {
                            ListViewItem listItem1 = new ListViewItem(item.MCQ_Multi_Answar.ToString());
                            listItem1.SubItems.Add(item.MCQ_Multi_Ans_ID.ToString());
                            listView3.Items.Add(listItem1);
                        }
                        
                        
                        break;
                }
                if (listView2.SelectedItems.Count == 1)
                {
                    Answer_Update.Text= listView2.SelectedItems[0].SubItems[0].Text;
                    Option_Update.Text = listView2.SelectedItems[0].SubItems[1].Text;
                }
                listView1.Enabled = false;
                
                UpdateQuestion.Enabled = false;
                Delete.Enabled = false;
                Save_Question.Enabled = false;
                Cancel_Question.Enabled = false;
            }
            else
            {
                MessageBox.Show("pleaze select one item");
            }
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.SelectedIndices.Count == 0)
                return;

            Answer_Update.Text = listView2.SelectedItems[0].SubItems[0].Text;
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView3.SelectedIndices.Count == 0)
                return;
            Option_Update.Text = listView3.SelectedItems[0].SubItems[0].Text;
        }

        private void CancelChanges_Click(object sender, EventArgs e)
        {
            listView1.Enabled = true;
            Updatepanel3.Visible = false;
            UpdateQuestion.Enabled = true;
            Delete.Enabled = true;
            Save_Question.Enabled = true;
            Cancel_Question.Enabled = true;
        }

        private void SaveChanges_Click(object sender, EventArgs e)
        {
            int Question_Pool_ID = int.Parse(listView1.SelectedItems[0].SubItems[3].Text);
            QuestionPool question = context.QuestionPools.SingleOrDefault(x => x.Question_ID == Question_Pool_ID);
            question.Question = Question_Text_Update.Text;
            question.Default_Degree = int.Parse(Default_Degree_Update.Text);
            context.SaveChanges();
            string Type_Que = question.Q_Type;
            switch (Type_Que)
            {
                case "1":
                    if (listView2.SelectedIndices.Count == 0)
                        return;

                    int True_False_ID = int.Parse(listView2.SelectedItems[0].SubItems[1].Text);
                    TureFalseQuestion Tr_Fa_Ques = context.TureFalseQuestions.SingleOrDefault(x => x.TF_Q_ID == True_False_ID);
                    Tr_Fa_Ques.TF_Correct_Ans = Answer_Update.Text == "true" ? true : false;
                    context.SaveChanges();
                    break;
                case "2":
                    if (listView2.SelectedIndices.Count == 0)
                        return;
                    int Text_Q_Ans = int.Parse(listView2.SelectedItems[0].SubItems[1].Text);
                    TextQuestion Text_Ques = context.TextQuestions.SingleOrDefault(x => x.Text_Q_ID == Text_Q_Ans);
                    Text_Ques.Text_Correct_Ans = Answer_Update.Text;
                    context.SaveChanges();
                    break;
                case "3":
                    if (listView2.SelectedIndices.Count != 0)
                    {
                        int Multi_Q_Ans = int.Parse(listView2.SelectedItems[0].SubItems[1].Text);
                        MCQuestion Multi_Ques = context.MCQuestions.SingleOrDefault(x => x.MCQ_Q_ID == Multi_Q_Ans);
                        Multi_Ques.MCQ_Correct_Ans = Answer_Update.Text;
                        context.SaveChanges();
                    }

                    if (listView3.SelectedIndices.Count != 0)
                    {
                        int Multi_Q_Option = int.Parse(listView3.SelectedItems[0].SubItems[1].Text);
                        MCQ_Multi_Ans Multi_Option = context.MCQ_Multi_Ans.SingleOrDefault(x => x.MCQ_Multi_Ans_ID == Multi_Q_Option);
                        Multi_Option.MCQ_Multi_Answar = Option_Update.Text;
                        context.SaveChanges();
                    }

                    break;
                default:
                    break;
            }
            MessageBox.Show(" true Operation");
        }
        #endregion
        #region Show all cources
        private void instForm_Load(object sender, EventArgs e)
        {
            List<QuestionPool> Questionlist = new List<QuestionPool>();
            var listOfCource = context.Courses.Where(x => x.Instructor_ID == Instructor.Instructor_ID).ToList();
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
                listItem.SubItems.Add(CourceName.Course_Name);
                listItem.SubItems.Add(item.Default_Degree.ToString());
                listItem.SubItems.Add(item.Question_ID.ToString());
                listItem.SubItems.Add(item.Q_Type.ToString());
                listView1.Items.Add(listItem);
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            
        }



        #endregion

        private void Make_Exam_Click(object sender, EventArgs e)
        {
            Instructor= context.Instructors.SingleOrDefault(x => x.Login_ID == InstractorLogin.Login_ID);

            MakeExam ins = new MakeExam(Instructor);
            ins.FormClosed += (s, args) => this.Close();
            this.Hide();
            ins.Show();
        }

        private void Delete_Click(object sender, EventArgs e)
        {

            if (listView1.SelectedItems.Count == 1)
            {
                int McqIdQue = int.Parse(listView1.SelectedItems[0].SubItems[3].Text);
                QuestionPool question = context.QuestionPools.SingleOrDefault(x=>x.Question_ID== McqIdQue);
                context.QuestionPools.Remove(question);
                context.SaveChanges();
            }
            else
            {
                MessageBox.Show("pleaze select one item");
            }
        }

        private void TrueAndFalseAns_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
