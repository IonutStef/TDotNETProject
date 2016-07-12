using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProjectClassLibrary;

namespace LectorWF
{
    public partial class MainForm : Form
    {
        ServerLectorClient Server;
        List<Panel> Panels;

        public MainForm()
        {
            InitializeComponent();
            Server = new ServerLectorClient();


            this.Panels = new System.Collections.Generic.List<System.Windows.Forms.Panel>();

            Panels.Add(subjectPanel);
            Panels.Add(chapterPanel);
            Panels.Add(studentPanel);
            Panels.Add(userPanel);
            Panels.Add(questionPanel);
            Panels.Add(testPanel);
            Panels.Add(solvedTestPanel);
            Panels.Add(examinationPanel);

            foreach (System.Windows.Forms.Panel panel in Panels)
            {
                panel.Visible = false;
            }
        }
      

        #region Fill Panels
        private void FillSubjectPanel(DataGridViewRow row)
        {
            Subject subj = (Subject)row.DataBoundItem;
            subj = Server.GetSubject(subj.SubjectId, "Chapters, Students");
            titleTB.Text = subj.Title;

            subjectTitleTB.Text = subj.Title;
            subjectDescriptionTB.Text = subj.Description;
            subjectChaptersCB.DataSource = subj.Chapters;
            subjectChaptersCB.DisplayMember = "Title";
            subjectStudentsCB.DataSource = subj.Students;
            subjectStudentsCB.DisplayMember = "FirstName";
        }

        private void FillChapterPanel(DataGridViewRow row)
        {
            Chapter chapter = (Chapter)row.DataBoundItem;
            chapter = Server.GetChapter(chapter.ChapterId, "Subject, Tests, Questions");
            titleTB.Text = chapter.Title;

            chapterTitleTB.Text = chapter.Title;
            chapterQuestionsCB.DataSource = chapter.Questions.Where(q => q.Duplicate != true).ToList();
            chapterQuestionsCB.DisplayMember = "Requirement";
            chapterSubjectCB.DataSource = new List<Subject>() { chapter.Subject };
            chapterSubjectCB.DisplayMember = "Title";
            chapterTestsCB.DataSource = chapter.Tests.Where(t => t.TestReferencedId == null).ToList();
            chapterTestsCB.DisplayMember = "Title";

        }

        private void FillStudentPanel(DataGridViewRow row)
        {
            Student stud = (Student)row.DataBoundItem;
            stud = Server.GetStudent(stud.StudentId, "Users, Subjects, Examinations, SolvedTests");
            titleTB.Text = stud.FirstName + " " + stud.LastName;

            studentNameTB.Text = stud.LastName;
            studentFNameTB.Text = stud.FirstName;
            studentEmailTB.Text = stud.Users[0].Email;
            studentUserCB.DataSource = stud.Users;
            studentUserCB.DisplayMember = "Email";
            studentSubjectsCB.DataSource = stud.Subjects;
            studentSubjectsCB.DisplayMember = "Title";
            studentExaminationsCB.DataSource = stud.Examinations;
            studentExaminationsCB.DisplayMember = "Date";
            studentSolvedTestsCB.DataSource = stud.SolvedTests;
            studentSolvedTestsCB.DisplayMember = "TestId";
        }

        private void FillUserPanel(DataGridViewRow row)
        {
            User user = (User)row.DataBoundItem;
            user = Server.GetUser(user.UserId, "Student");
            titleTB.Text = user.Email;

            userEmailTB.Text = user.Email;
            userPasswordTB.Text = user.Password;
            userTypeCB.DataSource = new List<String> () { user.UserType == 0 ? "Anonim" : user.UserType == 1 ? "Lector" : "Student" };
            userStudentsCB.DataSource = new List<Student>() { user.Student };
            userStudentsCB.DisplayMember = "FirstName";

        }

        private void FillTesttPanel(DataGridViewRow row)
        {
            Test test = (Test)row.DataBoundItem;
            test = Server.GetTest(test.TestId, "Chapters, Examinations, SolvedTests, TestQuestions");
            titleTB.Text = test.Title;
            testIdTB.Text = test.TestId.ToString();

            testTitleTB.Text = test.Title;
            testDescriptionTB.Text = test.Description;
            testTTSTB.Text = test.TimeToSolve.ToString();
            testNoQuestionsTB.Text = test.NoQuestions.ToString();
            testPunctajTB.Text = test.Punctaj.ToString();

            testChaptersCB.DataSource = test.Chapters;
            testChaptersCB.DisplayMember = "Title";
            testExaminationCB.DataSource = test.Examinations;
            testExaminationCB.DisplayMember = "Date";
            List<Test> solvedTests = Server.GetTests().ToList();
            solvedTests = solvedTests.Where(st => st.TestReferencedId == test.TestId).ToList();
            testSTCB.DataSource = solvedTests;
            testSTCB.DisplayMember = "TestId";


            List<Guid> questionsID = new List<Guid>();

            foreach (TestQuestion ts in test.TestQuestions)
            {
                questionsID.Add(ts.QuestionId);
            }

            try
            {
                List<Question> questions = Server.GetQuestions("TestQuestions").ToList();
                List<Question> quests = new List<Question>();
                quests = questions.Where(q => questionsID.Contains(q.QuestionId)).ToList();
                testQuestionsCB.DataSource = quests;
                testQuestionsCB.DisplayMember = "Requirement";
            }
            catch (Exception e)
            {
                titleTB.Text = e.Message + "\n" + e.InnerException;
            }
        }

        private void FillQuestionPanel(DataGridViewRow row)
        {
            Question quest = (Question)row.DataBoundItem;
            quest = Server.GetQuestion(quest.QuestionId, "Chapter, TestQuestions, Responses");
            titleTB.Text = quest.Requirement;
            questionRequirementTB.Text = quest.Requirement;

            questionIDTB.Text = quest.QuestionId.ToString();
            questionJustificationTB.Text = quest.Justification;
            questionResponsesCB.DataSource = quest.Responses;
            questionResponsesCB.DisplayMember = "Answer";

            questionChapterCB.DataSource = new List<Chapter>() { quest.Chapter };
            questionChapterCB.DisplayMember = "Title";

            List<Guid> testsID = new List<Guid>();

            foreach (TestQuestion ts in quest.TestQuestions)
            {
                testsID.Add(ts.TestId);
            }

            List<Test> tests = Server.GetTests("TestQuestions").ToList();
            List<Test> tst = new List<Test>();
            tst = tests.Where(q => testsID.Contains(q.TestId)).ToList();
            questionTestsCB.DataSource = tst;
            questionTestsCB.DisplayMember = "Title";
        }

        private void FillExaminationPanel(DataGridViewRow row)
        {
            Examination exam = (Examination)row.DataBoundItem;
            exam = Server.GetExamination(exam.ExaminationId, "Students, Test");
            titleTB.Text = exam.Date.ToString();

            examinationDescriptionTB.Text = exam.Description;
            examinationDateTB.Text = exam.Date.ToString();

            examinationStudentsCB.DataSource = exam.Students;
            examinationStudentsCB.DisplayMember = "FirstName";

            examinationTestCB.DataSource = new List<Test>() { exam.Test };
            examinationTestCB.DisplayMember = "Title";

        }

        private void FillSolvedTestPanel(DataGridViewRow row)
        {
            SolvedTest solvedTest = (SolvedTest)row.DataBoundItem;
            solvedTest = Server.GetSolvedTest(solvedTest.SolvedTestId, "Student");
            titleTB.Text = solvedTest.SolvedTestId.ToString();
            solvedTestIDTB.Text = solvedTest.SolvedTestId.ToString();

            Test initTest = Server.GetTest(solvedTest.TestId, "TestQuestions");
            Test test = Server.GetTest(new Guid(initTest.TestReferencedId.ToString()), "TestQuestions");
            solvedTestSolverTB.Text = solvedTest.Student.FirstName + " " + solvedTest.Student.LastName;

            solvedTestTestTB.Text = test.TestId.ToString();
            solvedTestTestMarkTB.Text = solvedTest.Punctaj + "/" + test.Punctaj;

            List<Guid> questionsID = new List<Guid>();

            foreach (TestQuestion ts in initTest.TestQuestions)
            {
                questionsID.Add(ts.QuestionId);
            }

            List<Question> questions = Server.GetQuestions().ToList();
            List<Question> quests = new List<Question>();
            quests = questions.Where(q => questionsID.Contains(q.QuestionId)).ToList();
            solvedTestQuestionsCB.DataSource = quests;
            solvedTestQuestionsCB.DisplayMember = "Requirement";
        }
        #endregion

        #region Fill DataGridView
        private void FillSubjectsDataGridView()
        {
            List<Subject> subjects = Server.GetSubjects().ToList();
            //subjectTabDataGridView.AutoGenerateColumns = false;
            itemListDGV.DataSource = subjects;

            for (int i = 0; i < itemListDGV.ColumnCount; i++)
            {
                if (itemListDGV.Columns[i].HeaderText != "Title")
                {
                    itemListDGV.Columns[i].Visible = false;
                }
            }

            HidePanels();
            subjectPanel.Visible = true;
        }
        private void FillChaptersDataGridView()
        {
            List<Chapter> chapters = Server.GetChapters().ToList();
            itemListDGV.DataSource = chapters;

            for (int i = 0; i < itemListDGV.ColumnCount; i++)
            {
                if (itemListDGV.Columns[i].HeaderText != "Title")
                {
                    itemListDGV.Columns[i].Visible = false;
                }
            }

            HidePanels();
            chapterPanel.Visible = true;
        }

        private void FillStudentsDataGridView()
        {
            List<Student> students = Server.GetStudents().ToList();
            itemListDGV.DataSource = students;

            for (int i = 0; i < itemListDGV.ColumnCount; i++)
            {
                if (itemListDGV.Columns[i].HeaderText != "FirstName")
                {
                    itemListDGV.Columns[i].Visible = false;
                }
            }

            HidePanels();
            studentPanel.Visible = true;
        }

        private void FillUsersDataGridView()
        {
            List<User> users = Server.GetUsers().ToList();
            itemListDGV.DataSource = users;

            for (int i = 0; i < itemListDGV.ColumnCount; i++)
            {
                if (itemListDGV.Columns[i].HeaderText != "Email")
                {
                    itemListDGV.Columns[i].Visible = false;
                }
            }

            HidePanels();
            userPanel.Visible = true;
        }

        private void FillQuestionsDataGridView()
        {
            List<Question> quest = Server.GetQuestions().ToList();
            quest = quest.Where(q => q.Duplicate != true).ToList();
            itemListDGV.DataSource = quest;

            for (int i = 0; i < itemListDGV.ColumnCount; i++)
            {
                if (itemListDGV.Columns[i].HeaderText != "Requirement")
                {
                    itemListDGV.Columns[i].Visible = false;
                }
            }

            HidePanels();
            questionPanel.Visible = true;
        }

        private void FillTestsDataGridView()
        {
            List<Test> tests = Server.GetTests().ToList();
            tests = tests.Where(t => t.Duplicate != true).ToList();
            itemListDGV.DataSource = tests;

            for (int i = 0; i < itemListDGV.ColumnCount; i++)
            {
                if (itemListDGV.Columns[i].HeaderText != "Title")
                {
                    itemListDGV.Columns[i].Visible = false;
                }
            }

            HidePanels();
            testPanel.Visible = true;
        }

        private void FillExaminationsDataGridView()
        {
            List<Examination> exam = Server.GetExaminations().ToList();
            itemListDGV.DataSource = exam;

            for (int i = 0; i < itemListDGV.ColumnCount; i++)
            {
                if (itemListDGV.Columns[i].HeaderText != "Date")
                {
                    itemListDGV.Columns[i].Visible = false;
                }
            }

            HidePanels();
            examinationPanel.Visible = true;
        }

        private void FillSolvedTestsDataGridView()
        {
            List<SolvedTest> solvedTest = Server.GetSolvedTests().ToList();
            
            itemListDGV.DataSource = solvedTest;
            foreach (SolvedTest st in solvedTest)
            {
                for (int i = 0; i < itemListDGV.ColumnCount; i++)
                {
                    if (itemListDGV.Columns[i].HeaderText != "StudentId")
                    {
                        itemListDGV.Columns[i].Visible = false;
                    }
                }
            }

            HidePanels();
            solvedTestPanel.Visible = true;
        }
        #endregion

        #region Metode Acces la baza de date
        /// <summary>
        /// Metoda care populeaza primul ComboBoc cu sectiunile (Tabelele) din baza de date.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PopulateComboBox(object sender, EventArgs e)
        {
            itemSelector.DataSource = new List<string> { "", "Subjects", "Chapters", "Students", "Examinations", "Tests", "Questions", "SolvedTests", "Users" };
        }

        /// <summary>
        /// Event care se activeaza atunci cand se alege o alta sectiune.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChangeItem(object sender, EventArgs e)
        {
            switch (itemSelector.SelectedValue.ToString())
            {
                case "Subjects":
                    {
                        FillSubjectsDataGridView();
                        sectionLB.Text = "Subjects";
                        break;
                    }
                case "Chapters":
                    {
                        FillChaptersDataGridView();
                        sectionLB.Text = "Chapters";
                        break;
                    }
                case "Students":
                    {
                        FillStudentsDataGridView();
                        sectionLB.Text = "Students";
                        break;
                    }
                case "Questions":
                    {
                        FillQuestionsDataGridView();
                        sectionLB.Text = "Questions";
                        break;
                    }
                case "Tests":
                    {
                        FillTestsDataGridView();
                        sectionLB.Text = "Tests";
                        break;
                    }
                case "Examinations":
                    {
                        FillExaminationsDataGridView();
                        sectionLB.Text = "Examinations";
                        break;
                    }
                case "SolvedTests":
                    {
                        FillSolvedTestsDataGridView();
                        sectionLB.Text = "Solved Tests";
                        break;
                    }
                case "Users":
                    {
                        FillUsersDataGridView();
                        sectionLB.Text = "Users";
                        break;
                    }
                case "":
                    {
                        HidePanels();
                        saveBT.Enabled = false;
                        titleTB.Text = null;
                        sectionLB.Text = "";
                        itemListDGV.DataSource = null;
                        break;
                    }
                default: break;
            }
        }

        /// <summary>
        /// Face tranzitia de la un panel la altul.
        /// </summary>
        private void HidePanels()
        {
            foreach(Panel panel in this.Panels)
            {
                panel.Visible = false;
            }
        }
        
        /// <summary>
        /// Metoda care se activeaza atunci cand facem click pe un item din DataGridView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.itemListDGV.Rows[e.RowIndex];
                switch (itemSelector.SelectedValue.ToString())
                {
                    case "Subjects":
                        {
                            FillSubjectPanel(row);
                            break;
                        }
                    case "Chapters":
                        {
                            FillChapterPanel(row);
                            break;
                        }
                    case "Students":
                        {
                            FillStudentPanel(row);
                            break;
                        }
                    case "Questions":
                        {
                            FillQuestionPanel(row);
                            break;
                        }
                    case "Tests":
                        {
                            FillTesttPanel(row);
                            break;
                        }
                    case "Examinations":
                        {
                            FillExaminationPanel(row);
                            break;
                        }
                    case "SolvedTests":
                        {
                            FillSolvedTestPanel(row);
                            break;
                        }
                    case "Users":
                        {
                            FillUserPanel(row);
                            break;
                        }
                    case "":
                        {
                            saveBT.Enabled = false;
                            HidePanels();
                            itemListDGV.DataSource = null;
                            break;
                        }
                    default: break;
                }
            }
            
        }
        #endregion

        #region Evenimente ComboBox
        private void solvedTestResponsesGB_Enter(object sender, EventArgs e)
        {

        }

        private void testSolovedTestsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Test st = (Test)testSTCB.SelectedItem;
            testSTPunctajTB.Text = st.Punctaj.ToString();
        }

        private void testQuestionsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Question quest = (Question)testQuestionsCB.SelectedItem;

            List<TestQuestion> ts = quest.TestQuestions.Where(t => t.TestId == new Guid(testIdTB.Text)).ToList();
            testQuestionsPunctajTB.Text = ts[0].Punctaj.ToString();
        }

        private void questionResponsesCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (questionResponsesCB.Text != "")
            {
                var lb = (sender as ListBox);
                Response resp =
                                 (Response)lb.SelectedItems[lb.SelectedItems.Count - 1];
                //Response resp = (Response)questionResponsesCB.SelectedItem;
                if (resp.Correct)
                {
                    questionResponseTrueCHB.Checked = true;
                }
                else
                {
                    questionResponseTrueCHB.Checked = false;
                }

                questionResponsesTB.Text = resp.Answer;
            }
            else
            {
                questionResponsesTB.Text = "";
            }
        }

        private void solvedTestQuestionsCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            Question quest = (Question)solvedTestQuestionsCB.SelectedValue;
            Question question = Server.GetQuestion(quest.QuestionId, "TestQuestions");
            //List<TestQuestion> ts = quest.TestQuestions.ToList();
            solvedTestQuestionJustTB.Text = quest.Justification;
            if (question.TestQuestions.Count() != 0)
            {
                TestQuestion ts = question.TestQuestions[0];

                //solvedTestTestQuestionRefTB.Text = quest.ReferencedQuestionId.ToString();

                solvedTestQuestionMarkTB.Text = ts.Punctaj.ToString();
                
            }
        }
        #endregion

        #region Add - Remove - Modify Buttons
        private void resetButton_click(object sender, EventArgs e)
        {
            Server.DeleteAll();
            Server.AddAll();
        }

        private void createButton_Click(object sender, EventArgs e)
        {
            Server.AddAll();
        }

        private void removeButton_click(object sender, EventArgs e)
        {
            if (!sectionLB.Text.Equals("Questions"))
            {
                MessageBox.Show("Remove other items than Questions, not implemented.");
            }
            else
            {
                Question quest = Server.GetQuestion(new Guid(questionIDTB.Text));
                quest.Requirement = " "; //Deleting
                Server.ModifyRemoveCreateQuestion(quest);
            }
        }

        private void modifyBT_Click(object sender, EventArgs e)
        {
            saveBT.Enabled = true;

            foreach (TextBox c in this.questionPanel.Controls.OfType<TextBox>())
            {
                c.ReadOnly = false;
            }

            switch (sectionLB.Text)
            {
                case "Questions":
                    {
                        int rowIndex = itemListDGV.CurrentCell.RowIndex;
                        Question quest = (Question)this.itemListDGV.Rows[rowIndex].DataBoundItem;
                        PrepareModifyQuestion(quest);
                        //Server.ModifyRemoveCreateQuestion(quest);
                        break;
                    }
                default: { break; }
            }

        }

        private void saveBT_Click(object sender, EventArgs e)
        {
            switch(sectionLB.Text)
            {
                case "Questions":
                    {
                        int rowIndex = itemListDGV.CurrentCell.RowIndex;
                        Question quest = (Question)this.itemListDGV.Rows[rowIndex].DataBoundItem;
                        PrepareSaveQuestion(quest);
                        break;
                    }
                case "Tests":
                    {
                        int rowIndex = itemListDGV.CurrentCell.RowIndex;
                        Test test = (Test)this.itemListDGV.Rows[rowIndex].DataBoundItem;
                        Server.ModifyRemoveCreateTest(test);
                        break;
                    }
                default: { break; }
            }

            foreach (TextBox c in this.questionPanel.Controls.OfType<TextBox>())
            {
                c.ReadOnly = true;
            }
            saveBT.Enabled = false;
        }
        #endregion

        #region Metode Modificare - Salvare
        private void PrepareModifyQuestion(Question question)
        {
            List<Chapter> chapters = Server.GetChapters().ToList();
            questionChapterCB.DataSource = chapters;
            questionChapterCB.DisplayMember = "Title";
            int indexChapter = 0;
            for (int i = 0; i < chapters.Count; i++)
            {
                if (chapters[i].ChapterId == question.ChapterId)
                {
                    indexChapter = i;
                    break;
                }
            }
            questionChapterCB.SetSelected(indexChapter, true);
        }

        private void PrepareSaveQuestion(Question quest)
        {
            if (questionRequirementTB.Text != quest.Requirement)
            {
                quest.Requirement = questionRequirementTB.Text;
            }

            if (questionJustificationTB.Text != quest.Justification)
            {
                quest.Justification = questionJustificationTB.Text;
            }
            Chapter chapter = (Chapter)questionChapterCB.SelectedItem;

            if (quest.ChapterId != chapter.ChapterId)
            {
                quest.Chapter = null;
                quest.Chapter = chapter;
            }

            Server.ModifyRemoveCreateQuestion(quest);
        }


        private void PrepareModifyTest(Test test)
        {

        }

        private void PrepareSaveTest(Test test)
        {

        }
        #endregion
    }
}
