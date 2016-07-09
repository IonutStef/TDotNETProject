using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LectorASP.Models
{
    public class QuestionCustomContainer
    {
        public string ID { set; get; }
        public string Requirement { get; set; }
        public string Justification { get; set; }
        public IDNamePair Chapter { get; set; }
        public List<IDNamePair> Responses { get; set; }
        public List<IDNamePair> Tests { get; set; }

        public QuestionCustomContainer() { }
        public QuestionCustomContainer(Guid id, string requirement, string justification, ProjectTSDotNETServiceReference.Chapter chapter, IEnumerable<ProjectTSDotNETServiceReference.Response> responses, IEnumerable<ProjectTSDotNETServiceReference.TestQuestion> testQuestions)
        {
            this.ID = id.ToString();
            this.Requirement = requirement;
            this.Justification = justification;
            this.Chapter = new IDNamePair(chapter.ChapterId, chapter.Title);
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            foreach (var item in responses)
            {
                this.Responses.Add(new IDNamePair(item.ResponseId, item.Answer, (item.Correct == true ? 1 : 0)));
            }

            foreach (var item in testQuestions)
            {
                ProjectTSDotNETServiceReference.Test test = lectorSrv.GetTest(item.TestId, "");
                this.Tests.Add(new IDNamePair(item.TestQuestionId, test.Title, item.Punctaj));
            }
        }

        public QuestionCustomContainer(ProjectTSDotNETServiceReference.Question question)
        {
            this.Tests = new List<IDNamePair>();
            this.Responses = new List<IDNamePair>();
            this.ID = question.QuestionId.ToString();
            this.Requirement = question.Requirement;
            this.Justification = question.Justification;
            this.Chapter = new IDNamePair(question.Chapter.ChapterId, question.Chapter.Title);
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            foreach (var item in question.Responses)
            {
                this.Responses.Add(new IDNamePair(item.ResponseId, item.Answer, (item.Correct == true ? 1 : 0)));
            }

            foreach (var item in question.TestQuestions)
            {

                ProjectTSDotNETServiceReference.Test test = lectorSrv.GetTest(item.TestId, "");
                this.Tests.Add(new IDNamePair(item.TestQuestionId, test.Title, item.Punctaj));
            }
        }
    }
}