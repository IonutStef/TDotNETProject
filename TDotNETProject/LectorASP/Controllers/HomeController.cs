using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LectorASP.Controllers
{
    public class MyViewModel
    {
        public String FirstField { get; set; }
        public List<Models.DataBaseShowCustomContainer> SecondField { get; set; }
    }


    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult DataBase()
        {
            return View();
        }

        #region FillCustomContainer

        public List<Models.DataBaseShowCustomContainer> FillCustomContainerSubject(ref ProjectTSDotNETServiceReference.ServerLectorClient server, string includeProperties = "", string constraint = null)
        {
            List<ProjectTSDotNETServiceReference.Subject> subjects = server.GetSubjects(includeProperties).ToList();
            List<Models.DataBaseShowCustomContainer> customContainerList = new List<Models.DataBaseShowCustomContainer>();

            /*
            if(constraint != null)
            {
                subjects = subjects.Where(s => !constraint.Contains(s.SubjectId.ToString())).ToList();
            }
            */
            foreach (ProjectTSDotNETServiceReference.Subject subject in subjects)
            {
                Models.DataBaseShowCustomContainer customContainer = new Models.DataBaseShowCustomContainer();
                customContainer.ID = subject.SubjectId;
                customContainer.FirsStringField = subject.Title;
                customContainer.SecondStringField = subject.Description;
                customContainerList.Add(customContainer);
            }

            return customContainerList;
        }

        public List<Models.DataBaseShowCustomContainer> FillCustomContainerChapter(ref ProjectTSDotNETServiceReference.ServerLectorClient server, string includeProperties = "", string constraint = null)
        {
            List<ProjectTSDotNETServiceReference.Chapter> chapters = server.GetChapters(includeProperties).ToList();
            List<Models.DataBaseShowCustomContainer> customContainerList = new List<Models.DataBaseShowCustomContainer>();

            if (constraint != null)
            {
                chapters = chapters.Where(s => !constraint.Contains(s.SubjectId.ToString())).ToList();
            }

            foreach (ProjectTSDotNETServiceReference.Chapter chapter in chapters)
            {
                Models.DataBaseShowCustomContainer customContainer = new Models.DataBaseShowCustomContainer();
                customContainer.ID = chapter.ChapterId;
                customContainer.FirsStringField = chapter.Title;
                customContainer.SecondStringField = chapter.SubjectId.ToString();
                customContainerList.Add(customContainer);
            }

            return customContainerList;
        }

        public List<Models.DataBaseShowCustomContainer> FillCustomContainerStudent(ref ProjectTSDotNETServiceReference.ServerLectorClient server, string includeProperties = "", string constraint = null)
        {
            List<ProjectTSDotNETServiceReference.Student> students = server.GetStudents(includeProperties).ToList();
            List<Models.DataBaseShowCustomContainer> customContainerList = new List<Models.DataBaseShowCustomContainer>();

            /*
            if (constraint != null)
            {
                students = students.Where(s => !constraint.Contains(s)).ToList();
            }
            */
            foreach (ProjectTSDotNETServiceReference.Student student in students)
            {
                Models.DataBaseShowCustomContainer customContainer = new Models.DataBaseShowCustomContainer();
                customContainer.ID = student.StudentId;
                customContainer.FirsStringField = student.FirstName + " " + student.LastName;
                if(student.Users.Count() > 0)
                    customContainer.SecondStringField = student.Users[0].Email;

                foreach(ProjectTSDotNETServiceReference.Subject subject in student.Subjects)
                {
                    customContainer.ThirdStringField += subject.Title + " - ";
                }

                if(customContainer.ThirdStringField != null)
                    customContainer.ThirdStringField = customContainer.ThirdStringField.Remove(customContainer.ThirdStringField.Count() - 3, 3);
                customContainerList.Add(customContainer);
            }

            return customContainerList;
        }

        public List<Models.DataBaseShowCustomContainer> FillCustomContainerQuestion(ref ProjectTSDotNETServiceReference.ServerLectorClient server, string includeProperties = "", string constraint = null)
        {
            List<ProjectTSDotNETServiceReference.Question> questions = server.GetQuestions(includeProperties).ToList();
            List<Models.DataBaseShowCustomContainer> customContainerList = new List<Models.DataBaseShowCustomContainer>();

            if (constraint != null)
            {
                questions = questions.Where(s => !constraint.Contains(s.ChapterId.ToString())).ToList();
            }

            foreach (ProjectTSDotNETServiceReference.Question question in questions)
            {
                if(question.Duplicate == false)
                {
                    Models.DataBaseShowCustomContainer customContainer = new Models.DataBaseShowCustomContainer();
                    customContainer.ID = question.QuestionId;
                    customContainer.FirsStringField = question.Requirement;
                    if (question.Chapter != null)
                        customContainer.SecondStringField = question.Chapter.Title;
                    customContainerList.Add(customContainer);
                }
            }

            return customContainerList;
        }

        public List<Models.DataBaseShowCustomContainer> FillCustomContainerTest(ref ProjectTSDotNETServiceReference.ServerLectorClient server, string includeProperties = "", string constraint = null)
        {
            List<ProjectTSDotNETServiceReference.Test> tests = server.GetTests(includeProperties).ToList();
            List<Models.DataBaseShowCustomContainer> customContainerList = new List<Models.DataBaseShowCustomContainer>();

            if (constraint != null)
            {
                foreach (ProjectTSDotNETServiceReference.Test test in tests)
                {
                    foreach (ProjectTSDotNETServiceReference.Chapter chapter in test.Chapters)
                        tests = tests.Where(s => !constraint.Contains(s.TestId.ToString())).ToList();
                }
            }

            foreach (ProjectTSDotNETServiceReference.Test test in tests)
            {
                if(test.Duplicate == false)
                {
                    Models.DataBaseShowCustomContainer customContainer = new Models.DataBaseShowCustomContainer();
                    customContainer.ID = test.TestId;
                    customContainer.FirsStringField = test.Title;
                    customContainer.SecondStringField = test.Description;

                    foreach (ProjectTSDotNETServiceReference.Chapter chapter in test.Chapters)
                    {
                        customContainer.ThirdStringField += chapter.Title + " - ";
                    }
                    if(customContainer.ThirdStringField != null)
                        customContainer.ThirdStringField = customContainer.ThirdStringField.Remove(customContainer.ThirdStringField.Count() - 3, 3);
                    customContainerList.Add(customContainer);
                }
                
            }

            return customContainerList;
        }

        public List<Models.DataBaseShowCustomContainer> FillCustomContainerExamination(ref ProjectTSDotNETServiceReference.ServerLectorClient server, string includeProperties = "", string constraint = null)
        {
            List<ProjectTSDotNETServiceReference.Examination> examinations = server.GetExaminations(includeProperties).ToList();
            List<Models.DataBaseShowCustomContainer> customContainerList = new List<Models.DataBaseShowCustomContainer>();

            /*
            if (constraint != null)
            {
                examinations = examinations.Where(s => !constraint.Contains(s.ExaminationId.ToString())).ToList();
            }
            */
            foreach (ProjectTSDotNETServiceReference.Examination examination in examinations)
            {
                Models.DataBaseShowCustomContainer customContainer = new Models.DataBaseShowCustomContainer();
                customContainer.ID = examination.ExaminationId;
                customContainer.FirsStringField = examination.Date.ToString();
                if(examination.Test != null)
                    customContainer.SecondStringField = examination.Test.Title;

                foreach (ProjectTSDotNETServiceReference.Student student in examination.Students)
                {
                    customContainer.ThirdStringField += student.LastName + " " + student.FirstName + " - ";
                }
                if (customContainer.ThirdStringField != null)
                    customContainer.ThirdStringField = customContainer.ThirdStringField.Remove(customContainer.ThirdStringField.Count() - 3, 3);
                customContainerList.Add(customContainer);
            }

            return customContainerList;
        }

        public List<Models.DataBaseShowCustomContainer> FillCustomContainerUser(ref ProjectTSDotNETServiceReference.ServerLectorClient server, string includeProperties = "", string constraint = null)
        {
            List<ProjectTSDotNETServiceReference.User> users = server.GetUsers(includeProperties).ToList();
            List<Models.DataBaseShowCustomContainer> customContainerList = new List<Models.DataBaseShowCustomContainer>();

            if (constraint != null)
            {
                users = users.Where(s => !constraint.Contains(s.UserId.ToString())).ToList();
            }

            foreach (ProjectTSDotNETServiceReference.User user in users)
            {
                Models.DataBaseShowCustomContainer customContainer = new Models.DataBaseShowCustomContainer();
                customContainer.ID = user.UserId;
                customContainer.FirsStringField = user.Email;
                customContainer.SecondStringField = user.Password;
                customContainerList.Add(customContainer);
            }

            return customContainerList;
        }

        public List<Models.DataBaseShowCustomContainer> FillCustomContainerSolvedTest(ref ProjectTSDotNETServiceReference.ServerLectorClient server, string includeProperties = "", string constraint = null)
        {
            List<ProjectTSDotNETServiceReference.SolvedTest> solvedTests = server.GetSolvedTests(includeProperties).ToList();
            List<Models.DataBaseShowCustomContainer> customContainerList = new List<Models.DataBaseShowCustomContainer>();

            if (constraint != null)
            {
                solvedTests = solvedTests.Where(s => !constraint.Contains(s.SolvedTestId.ToString())).ToList();
            }

            foreach (ProjectTSDotNETServiceReference.SolvedTest solvedTest in solvedTests)
            {
                Models.DataBaseShowCustomContainer customContainer = new Models.DataBaseShowCustomContainer();
                customContainer.ID = solvedTest.SolvedTestId;
                if (solvedTest.Test != null && solvedTest.Student != null)
                {
                    customContainer.FirsStringField = solvedTest.Test.Title;
                    customContainer.SecondStringField = solvedTest.Student.LastName + " " + solvedTest.Student.FirstName;
                }
                customContainerList.Add(customContainer);
            }

            return customContainerList;
        }

        #endregion

        [HttpPost]
        public ActionResult DataBase(string optionSelected)
        {
            string valueSelected = Request.Form["selectorPDB"];

            MyViewModel viewModel = new MyViewModel();
            ProjectTSDotNETServiceReference.ServerLectorClient lectorSrv = new ProjectTSDotNETServiceReference.ServerLectorClient();
            switch (valueSelected)
            {
                case "Subjects":
                    {
                        viewModel.FirstField = "Subjects";
                        viewModel.SecondField = FillCustomContainerSubject(ref lectorSrv);
                        break;
                    }
                case "Chapters":
                    {
                        viewModel.FirstField = "Chapters";
                        viewModel.SecondField = FillCustomContainerChapter(ref lectorSrv, "Subject");
                        break;
                    }
                case "Students":
                    {
                        viewModel.FirstField = "Students";
                        viewModel.SecondField = FillCustomContainerStudent(ref lectorSrv, "Subjects, Users");
                        break;
                    }
                case "Questions":
                    {
                        viewModel.FirstField = "Questions";
                        viewModel.SecondField = FillCustomContainerQuestion(ref lectorSrv, "Chapter");
                        break;
                    }
                case "Tests":
                    {
                        viewModel.FirstField = "Tests";
                        viewModel.SecondField = FillCustomContainerTest(ref lectorSrv, "Chapters");
                        break;
                    }
                case "Examinations":
                    {
                        viewModel.FirstField = "Examinations";
                        viewModel.SecondField = FillCustomContainerExamination(ref lectorSrv, "Test, Students");
                        break;
                    }
                //case "Users":
                //    {
                //          viewModel.FirstField = "Users";
                //          viewModel.SecondField = FillCustomContainerUser(ref lectorSrv);
                //          break;
                //    }
                case "SolvedTests":
                    {
                        viewModel.FirstField = "SolvedTests";
                        viewModel.SecondField = FillCustomContainerSolvedTest(ref lectorSrv, "Test, Student");
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Reset()
        {
            ProjectTSDotNETServiceReference.ServerLectorClient Server = new ProjectTSDotNETServiceReference.ServerLectorClient();

            //Server.DeleteAll();
            Server.AddAll();

            return RedirectToAction("DataBase", "Home");
        }
    }
}