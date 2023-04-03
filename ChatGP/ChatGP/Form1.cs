using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace ChatGP
{
    
    public partial class Form1 : Form
    {
        private List<QAPair> qaPairs;
        string username;
        public Form1()
        {
            InitializeComponent();
            LoadQAPairs();
        }
        private void LoadQAPairs()
        {
            qaPairs = new List<QAPair>();
            qaPairs.Add(new QAPair { Question = "Как дела?", Answer = "Хорошо, спасибо!" });
            qaPairs.Add(new QAPair { Question = "Какой сегодня день?", Answer = "Сегодня " + DateTime.Today.ToString("dd.MM.yyyy") });
            qaPairs.Add(new QAPair { Question = "Как тебя зовут?", Answer = "Меня зовут Бот" });
            qaPairs.Add(new QAPair { Question = "Какие новости?", Answer = "Пока нет новостей" });
        }

        public class QAPair
        {
            public string Question { get; set; }
            public string Answer { get; set; }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            string question = txtQuestion.Text; 

            string answer = FindAnswer(question); 

            
            txtAnswer.Text = answer;
        }
        private string FindAnswer(string question)
        {
            
            QAPair qaPair = qaPairs.FirstOrDefault(qa => qa.Question.ToLower() == question.ToLower());

            if (qaPair != null)
            {
                return qaPair.Answer; 
            }
            else
            {
                return "Извините, я не понимаю вас"; 
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (IsUserRegistered())
            {
                MessageBox.Show("Добро пожаловать, " + username + "!");
                txtQuestion.Enabled = true;
                btnSend.Enabled = true;
            }
            else
            {
                MessageBox.Show("Пожалуйста, зарегистрируйтесь.");
                txtUsername.Enabled = true;
            }
        }

        private void txtQuestion_TextChanged(object sender, EventArgs e)
        {
            if (txtQuestion.Text != "")
            {
                btnSend.Enabled = true;
            }
            else
            {
                btnSend.Enabled = false;
            }
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text != "")
            {
                btnRegister.Enabled = true;
            }
            else
            {
                btnRegister.Enabled = false;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            username = txtUsername.Text;
            SaveUsernameToFile();
            MessageBox.Show("Пользователь зарегистрирован");
            txtQuestion.Enabled = true;
            btnSend.Enabled = true;
        }
        private void SaveUsernameToFile()
        {
            string filePath = "username.txt";
            File.WriteAllText(filePath, username);
        }

        private bool IsUserRegistered()
        {
            string filePath = "username.txt";
            if (File.Exists(filePath))
            {
                username = File.ReadAllText(filePath);
                if (username != "")
                {
                    return true;
                }
            }
            return false;
        }
        
    }
}
