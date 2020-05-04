using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq.Expressions;
using test.Model;

namespace test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            credentialsLabel.Hide();
            formlist.formslist.Add(this);
        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (UsernameText.Text.Length != 0 && UsernameText.Text != " " && PasswordText.Text.Length != 0 && PasswordText.Text != " ")
            {
                credentialsLabel.Hide();

                SqlProjectEntities1 context = new SqlProjectEntities1();
                var query = context.Logins.Select(s => s).Where(s=>(s.User_Name==UsernameText.Text&&s.User_Password==PasswordText.Text)).FirstOrDefault();
                if (query != null && (query.Acc_Type == 2))
                {
                    instForm ins = new instForm(query);
                    ins.FormClosed += (s, args) => this.Close();
                    ins.Show();

                }
                else if (query != null && (query.Acc_Type == 3)) {
                    StudentForm s = new StudentForm();
                    s.setvalues(query.Login_ID);
                    s.FormClosed += (st, args) => this.Close();
                    this.Hide();
                    s.Show();
                    
                }
                else if (query != null && (query.Acc_Type == 1))
                {
                    TrainingManager trainingManager = new TrainingManager();
                    trainingManager.SetValues(query.Login_ID);
                    trainingManager.FormClosed += (st, args) => this.Close();
                    this.Hide();
                    trainingManager.Show();

                }
                else
                {
                    credentialsLabel.Show();
                }
            }
            else {
                credentialsLabel.Show();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
