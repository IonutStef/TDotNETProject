using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LectorASP.Models
{
    public class TestCustomContainer
    {
        public string ID { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public int TimeToSolve { set; get; }
        public int Points { set; get; }
        public int NoQuestions { set; get; }

        //public IDNamePair RefTest { get; set; }

        public List<IDNamePair> Examinations { get; set; }
        public List<IDNamePair> SolvedTests { get; set; }
        public List<IDNamePair> Test1 { get; set; }
        public List<IDNamePair> Questions { get; set; }
        public List<IDNamePair> Chapters { get; set; }

        public TestCustomContainer() { }

        public TestCustomContainer(ProjectTSDotNETServiceReference.Test test)
        {
            this.Examinations = new List<IDNamePair>();
            this.SolvedTests = new List<IDNamePair>();
            this.Test1 = new List<IDNamePair>();
            this.Questions = new List<IDNamePair>();
            this.Chapters = new List<IDNamePair>();
            //this.RefTest = new IDNamePair();

            this.ID = test.TestId.ToString();
            this.Title = test.Title;
            this.Description = test.Description;
            //this.RefTest = new IDNamePair(test.Test2.TestId, test.Test2.Title);

            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();

            foreach (var item in test.Examinations)
            {
                this.Examinations.Add(new IDNamePair(item.ExaminationId, item.Date.ToString()));
            }

            foreach (var item in test.Chapters)
            {
                this.Chapters.Add(new IDNamePair(item.ChapterId, item.Title));
            }

            foreach (var item in test.Test1)
            {
                this.Test1.Add(new IDNamePair(item.TestId, item.Title));
            }

            foreach (var item in test.SolvedTests)
            {

                ProjectTSDotNETServiceReference.Student student = lectorSrv.GetStudent(item.StudentId, "");
                this.Examinations.Add(new IDNamePair(item.SolvedTestId, student.LastName + " " + student.FirstName, item.Punctaj));
            }


            foreach (var item in test.TestQuestions)
            {
                ProjectTSDotNETServiceReference.Question question = lectorSrv.GetQuestion(item.QuestionId, "");
                this.Questions.Add(new IDNamePair(item.TestQuestionId, question.Requirement, item.Punctaj));
            }
        }
    }
}