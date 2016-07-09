using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LectorASP.Controllers
{
    public class LectorEditController : Controller
    {
        // GET: LectorModify
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult StartEdit(string toEdit)
        {
            switch(toEdit)
            {
                case "Subjects":
                    {
                        return RedirectToAction("EditSubject");
                    }
                case "Chapters":
                    {
                        return RedirectToAction("EditChapter");
                    }
                case "Students":
                    {
                        return RedirectToAction("EditStudent");
                    }
                case "Questions":
                    {
                        return RedirectToAction("EditQuestion");
                    }
                case "SolvedTests":
                    {
                        return RedirectToAction("EditSolvedTest");
                    }
                case "Examinations":
                    {
                        return RedirectToAction("EditExamination");
                    }
                case "Tests":
                    {
                        return RedirectToAction("EditTest");
                    }
                case "Users":
                    {
                        return RedirectToAction("EditUser");
                    }
                case "TestQuestions":
                    {
                        return RedirectToAction("EditTestQuestion");
                    }
                case "null":
                    {
                        return View();
                    }
            }

            return View();
        }



        public ActionResult EditSubject()
        {
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            List<ProjectTSDotNETServiceReference.Subject> subjects = lectorSrv.GetSubjects("").ToList();
            return View(subjects);
        }

        public ActionResult SubjectEntitySelected(string ID)
        {
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            ProjectTSDotNETServiceReference.Subject subject = lectorSrv.GetSubject(new Guid(ID), "Students, Chapters");

            LectorASP.Models.SubjectCustomContainer customSubject = new Models.SubjectCustomContainer(subject);
            return Json(customSubject, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EditSubject(ProjectTSDotNETServiceReference.Subject entity)
        {
            return View();
        }



        public ActionResult EditChapter()
        {
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            List<ProjectTSDotNETServiceReference.Chapter> chapters = lectorSrv.GetChapters("").ToList();
            return View(chapters);
        }

        [HttpPut]
        public ActionResult EditChapter(ProjectTSDotNETServiceReference.Chapter entity)
        {
            return View();
        }



        public ActionResult EditStudent()
        {
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            List<ProjectTSDotNETServiceReference.Student> students = lectorSrv.GetStudents("").ToList();
            return View(students);
        }

        [HttpPut]
        public ActionResult EditStudent(ProjectTSDotNETServiceReference.Student entity)
        {
            return View();
        }



        public ActionResult EditQuestion()
        {

            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();

            List<ProjectTSDotNETServiceReference.Question> questions = lectorSrv.GetQuestions("").ToList();
            questions = questions.Where(q => q.Duplicate == false).ToList();

            List<ProjectTSDotNETServiceReference.Chapter> chapters = new List<ProjectTSDotNETServiceReference.Chapter>();
            chapters = lectorSrv.GetChapters("").ToList();
            List<Models.IDNamePair> chaptersCustom = new List<Models.IDNamePair>();

            foreach(ProjectTSDotNETServiceReference.Chapter chapter in chapters)
            {
                chaptersCustom.Add(new Models.IDNamePair(chapter.ChapterId, chapter.Title));
            }
            ViewBag.Chapters = chaptersCustom;

            Dictionary<string, List<ProjectTSDotNETServiceReference.Question>> questionsGrouped = new Dictionary<string, List<ProjectTSDotNETServiceReference.Question>>();
            foreach(ProjectTSDotNETServiceReference.Chapter chapter in chapters)
            {
                List<ProjectTSDotNETServiceReference.Question> questionsTemp = new List<ProjectTSDotNETServiceReference.Question>();
                questionsTemp = questions.Where(q => q.ChapterId == chapter.ChapterId).ToList();
                if(questionsTemp == null)
                {
                    continue;
                }
                else if(questionsTemp.Count == 0)
                {
                    continue;
                }
                questionsGrouped.Add(chapter.Title, questionsTemp);
            }

            return View(questionsGrouped);
        }

        public ActionResult QuestionEntitySelected(string ID)
        {
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            ProjectTSDotNETServiceReference.Question question = lectorSrv.GetQuestion(new Guid(ID), "Chapter, Responses, TestQuestions");

            LectorASP.Models.QuestionCustomContainer customQuestion = new Models.QuestionCustomContainer(question);
            return Json(customQuestion, JsonRequestBehavior.AllowGet);

           
        }
        [HttpPost]
        public ActionResult EditQuestion(ProjectTSDotNETServiceReference.Question entity)
        {
            var req = Request.Form["questionRequirement"];
            var just = Request.Form["questionJustification"];
            var chapters = Request.Form["questionChapter"];
            var responses = Request.Form.GetValues("questionResponses");

            ProjectTSDotNETServiceReference.Question quest = new ProjectTSDotNETServiceReference.Question();

            quest.Requirement = req;
            quest.QuestionId = Guid.NewGuid();
            quest.Justification = just;
            quest.ChapterId = new Guid(chapters);

            List<ProjectTSDotNETServiceReference.Response> Responses = new List<ProjectTSDotNETServiceReference.Response>();

            foreach(var response in responses)
            {
                var splitted = response.ToString().Split('^');
                ProjectTSDotNETServiceReference.Response Resp = new ProjectTSDotNETServiceReference.Response();
                Resp.Answer = splitted[0];
                Resp.Correct = Int32.Parse(splitted[1]) == 1 ? true : false;
                Guid respGuid = Guid.NewGuid();
                Resp.ResponseId = respGuid;
                Responses.Add(Resp);
            }

            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            
            lectorSrv.AddQuestion(quest, Responses.ToArray());
            return RedirectToAction("EditQuestion", "LectorEdit");
        }



        public ActionResult EditTest()
        {
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            List<ProjectTSDotNETServiceReference.Test> tests = lectorSrv.GetTests("").ToList();
            tests = tests.Where(t => t.Duplicate == false).ToList();
            return View(tests);
        }


        public ActionResult TestnEntitySelected(string ID)
        {
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            ProjectTSDotNETServiceReference.Test test = lectorSrv.GetTest(new Guid(ID), "Examinations, Chapters, Test1, TestQuestions");

            LectorASP.Models.TestCustomContainer customTest = new Models.TestCustomContainer(test);
            return Json(customTest, JsonRequestBehavior.AllowGet);
        }

        [HttpPut]
        public ActionResult EditTest(ProjectTSDotNETServiceReference.Test entity)
        {
            return View();
        }



        public ActionResult EditExamination()
        {
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            List<ProjectTSDotNETServiceReference.Examination> examinations = lectorSrv.GetExaminations("").ToList();
            return View(examinations);
        }

        [HttpPut]
        public ActionResult EditExamination(ProjectTSDotNETServiceReference.Examination entity)
        {
            return View();
        }


        public ActionResult EditUser()
        {
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            List<ProjectTSDotNETServiceReference.User> users = lectorSrv.GetUsers("").ToList();
            return View(users);
        }

        [HttpPut]
        public ActionResult EditUser(ProjectTSDotNETServiceReference.User entity)
        {
            return View();
        }



        public ActionResult EditSolvedTest()
        {
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            List<ProjectTSDotNETServiceReference.SolvedTest> solvedTests = lectorSrv.GetSolvedTests("Student, Test").ToList();
            return View(solvedTests);
        }

        [HttpPut]
        public ActionResult EditSolvedTest(ProjectTSDotNETServiceReference.SolvedTest entity)
        {
            return View();
        }


        /*
        public ActionResult EditTestQuestion()
        {
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            List<ProjectTSDotNETServiceReference.TestQuestion> testQuestions = lectorSrv.GetTestQuestions("Test, Question").ToList();
            testQuesions = testQuestions.Where(tq => tq.Duplicate == false);
            return View(testQuestions);
        }

        [HttpPut]
        public ActionResult EditTestQuestion(ProjectTSDotNETServiceReference.TestQuestion entity)
        {
            return View();
        }
        */
    }
}