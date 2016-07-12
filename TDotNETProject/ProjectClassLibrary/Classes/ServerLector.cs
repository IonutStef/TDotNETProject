﻿
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace ProjectClassLibrary
{
    public class ServerLector : IServerLector
    {

        //private static List<IServerLectorCallBack> _callbackList = new List<IServerLectorCallBack>();
        //private static int RegisteredLectors { get; set; }

        public ServerLector() { }
        UnitOfWork uow = new UnitOfWork();

        //public int ProductRegister(int a, int b)
        //{
        //    int c = a * b;
        //    IServerLectorCallBack registeredLector = OperationContext.Current.GetCallbackChannel<IServerLectorCallBack>();

        //    if (!_callbackList.Contains(registeredLector))
        //    {
        //        _callbackList.Add(registeredLector);
        //        RegisteredLectors++;
        //        Console.WriteLine("New Lector: {0} ({1})", registeredLector.ToString(), RegisteredLectors);
        //    }

        //    return c;
        //}

        //public void CallFromClient()
        //{
        //    _callbackList.ForEach(
        //       delegate (IServerLectorCallBack callback)
        //       {
        //           if (((ICommunicationObject)callback).State ==
        //           CommunicationState.Opened)
        //           {
        //               callback.CalledByServer();
        //           }
        //           else
        //           {
        //               _callbackList.Remove(callback);
        //           }
        //       });
        //}

        //public bool LogIn(string username, string password)
        //{
        //    bool logged = false;

        //    UnitOfWork uow = new UnitOfWork();

        //    List<User> usr = uow.UserRepository.Get(user => user.Email == username && user.Password == password).ToList();

        //    if(usr.Count == 1)
        //    {
        //        logged = true;

        //        IServerLectorCallBack registeredUser = OperationContext.Current.GetCallbackChannel<IServerLectorCallBack>();

        //        if (!_callbackList.Contains(registeredUser))
        //        {
        //            _callbackList.Add(registeredUser);
        //            RegisteredLectors++;
        //        }

        //    }

        //    return logged;
        //}


        #region DBReset smallFunctions
        public void TestAddSubject()
        {
            UnitOfWork uow = new UnitOfWork();

            Subject subj = new Subject();

            Guid subjId = Guid.NewGuid();
            subj.SubjectId = subjId;

            string title = "Topici Speciale DotNET";
            subj.Title = title;

            try
            {
                uow.SubjectRepository.Insert(subj);
                uow.Save();
                uow.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
            }
        }
        public void PrintSubjects()
        {
            UnitOfWork uow = new UnitOfWork();
            List<Subject> subjectsArr = uow.SubjectRepository.Get().ToList();

            foreach (Subject subj in subjectsArr)
            {
                Console.WriteLine("Subject: {0}\n\tDescription: {1}", subj.Title, subj.Description);
            }
            uow.Dispose();
        }

        public void TestAddChapter()
        {
            UnitOfWork uow = new UnitOfWork();

            List<Subject> subjectsArr = uow.SubjectRepository.Get(subject => subject.Title == "Topici Speciale DotNET").ToList();

            Subject subj = subjectsArr[0];

            Chapter chapter1 = new Chapter();
            Guid chapterId = Guid.NewGuid();
            String title = "EF";
            chapter1.ChapterId = chapterId;
            chapter1.Title = title;
            chapter1.Subject = subj;
            chapter1.SubjectId = subj.SubjectId;


            Chapter chapter2 = new Chapter();
            chapterId = Guid.NewGuid();
            title = "WPF";
            chapter2.ChapterId = chapterId;
            chapter2.Title = title;
            chapter2.Subject = subj;
            chapter2.SubjectId = subj.SubjectId;

            Chapter chapter3 = new Chapter();
            chapterId = Guid.NewGuid();
            title = "WCF";
            chapter3.ChapterId = chapterId;
            chapter3.Title = title;
            chapter3.Subject = subj;
            chapter3.SubjectId = subj.SubjectId;

            Chapter chapter4 = new Chapter();
            chapterId = Guid.NewGuid();
            title = "ASP";
            chapter4.ChapterId = chapterId;
            chapter4.Title = title;
            chapter4.Subject = subj;
            chapter4.SubjectId = subj.SubjectId;



            try
            {
                uow.ChapterRepository.Insert(chapter1);
                uow.ChapterRepository.Insert(chapter2);
                uow.ChapterRepository.Insert(chapter3);
                uow.ChapterRepository.Insert(chapter4);
                //uow.ChapterRepository.Insert(chapter2);
                uow.Save();
                uow.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + "\n" + e.StackTrace);
            }
        }
        public void PrintChapters()
        {
            UnitOfWork uow = new UnitOfWork();
            List<Chapter> chaptersArr = uow.ChapterRepository.Get().ToList();

            foreach (Chapter chapter in chaptersArr)
            {
                Console.WriteLine("Chapter: {0}  -- Subject: {1}", chapter.Title, chapter.Subject.Title);
            }
            uow.Dispose();
        }

        public void TestAddStudentUser()
        {
            UnitOfWork uow = new UnitOfWork();

            List<Subject> subjectsArr = uow.SubjectRepository.Get(subject => subject.Title == "Topici Speciale DotNET").ToList();
            Subject subj = subjectsArr[0];

            // First Student = Lector
            User usr1 = new User();
            Student stud1 = new Student();
            Guid usrId = Guid.NewGuid();
            Guid studId = Guid.NewGuid();

            int type = 0;
            string firstName = "Lector";
            string lastName = "Lector";
            string email = "lector@lector.com";
            string pass = "lector";

            stud1.FirstName = firstName;
            stud1.LastName = lastName;
            stud1.StudentId = studId;
            stud1.Subjects.Add(subj);

            usr1.Email = email;
            usr1.Password = pass;
            usr1.UserType = type;
            usr1.UserId = usrId;
            usr1.StudentId = studId;
            usr1.Student = stud1;



            // Second Student = Student
            User usr2 = new User();
            Student stud2 = new Student();
            usrId = Guid.NewGuid();
            studId = Guid.NewGuid();


            type = 1;
            firstName = "Student";
            lastName = "Student";
            email = "student@student.com";
            pass = "student";


            stud2.FirstName = firstName;
            stud2.LastName = lastName;
            stud2.StudentId = studId;
            stud2.Subjects.Add(subj);


            usr2.Email = email;
            usr2.Password = pass;
            usr2.UserType = type;
            usr2.UserId = usrId;
            usr2.StudentId = studId;
            usr2.Student = stud2;



            // Third Student = Anonim
            User usr3 = new User();
            Student stud3 = new Student();
            usrId = Guid.NewGuid();
            studId = Guid.NewGuid();

            type = 2;
            firstName = "Anonim";
            lastName = "Anonim";
            email = "anonim@anonim.com";
            pass = "anonim";


            stud3.FirstName = firstName;
            stud3.LastName = lastName;
            stud3.StudentId = studId;

            usr3.Email = email;
            usr3.Password = pass;
            usr3.UserType = type;
            usr3.UserId = usrId;
            usr3.StudentId = studId;
            usr3.Student = stud3;


            User usr4 = new User();
            Student stud4 = new Student();
            usrId = Guid.NewGuid();
            studId = Guid.NewGuid();


            type = 1;
            firstName = "Student1";
            lastName = "Student1";
            email = "student1@student.com";
            pass = "student";


            stud4.FirstName = firstName;
            stud4.LastName = lastName;
            stud4.StudentId = studId;
            stud4.Subjects.Add(subj);


            usr4.Email = email;
            usr4.Password = pass;
            usr4.UserType = type;
            usr4.UserId = usrId;
            usr4.StudentId = studId;
            usr4.Student = stud4;


            User usr5 = new User();
            Student stud5 = new Student();
            usrId = Guid.NewGuid();
            studId = Guid.NewGuid();


            type = 1;
            firstName = "Student2";
            lastName = "Student2";
            email = "student2@student.com";
            pass = "student";


            stud5.FirstName = firstName;
            stud5.LastName = lastName;
            stud5.StudentId = studId;
            stud5.Subjects.Add(subj);


            usr5.Email = email;
            usr5.Password = pass;
            usr5.UserType = type;
            usr5.UserId = usrId;
            usr5.StudentId = studId;
            usr5.Student = stud5;


            User usr6 = new User();
            Student stud6 = new Student();
            usrId = Guid.NewGuid();
            studId = Guid.NewGuid();


            type = 1;
            firstName = "Student3";
            lastName = "Student3";
            email = "student3@student.com";
            pass = "student";


            stud6.FirstName = firstName;
            stud6.LastName = lastName;
            stud6.StudentId = studId;
            stud6.Subjects.Add(subj);


            usr6.Email = email;
            usr6.Password = pass;
            usr6.UserType = type;
            usr6.UserId = usrId;
            usr6.StudentId = studId;
            usr6.Student = stud6;


            uow.UserRepository.Insert(usr1);
            uow.StudentRepository.Insert(stud1);
            uow.UserRepository.Insert(usr2);
            uow.StudentRepository.Insert(stud2);
            uow.UserRepository.Insert(usr3);
            uow.StudentRepository.Insert(stud3);
            uow.UserRepository.Insert(usr4);
            uow.StudentRepository.Insert(stud4);
            uow.UserRepository.Insert(usr5);
            uow.StudentRepository.Insert(stud5);
            uow.UserRepository.Insert(usr6);
            uow.StudentRepository.Insert(stud6);

            try
            {
                uow.Save();
                uow.Dispose();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public void PrintStudents()
        {
            UnitOfWork uow = new UnitOfWork();
            List<Student> studArr = uow.StudentRepository.Get().ToList();

            foreach (Student stud in studArr)
            {
                string tip;
                switch (stud.Users.ElementAt(0).UserType)
                {
                    case 0: { tip = "lector"; break; }
                    case 1: { tip = "student"; break; }
                    case 2: { tip = "anonim"; break; }
                    default: { tip = ""; break; }
                }
                Console.WriteLine("Nume: {0}\nPrenume: {1}\nEmail: {2}\nTip: {3}\nSubjects: ", stud.LastName, stud.FirstName, stud.Users.ElementAt(0).Email, tip);

                foreach (Subject subj in stud.Subjects)
                {
                    Console.WriteLine(subj.Title);
                }
                Console.WriteLine("\n\n\n");
            }

            uow.Dispose();
        }



        public void AddQuestion(Question question, List<Response> responses)
        {
            UnitOfWork uow = new UnitOfWork();
            question.Chapter = GetChapter(question.ChapterId);
            question.Chapter = null;
            uow.QuestionRepository.Insert(question);

            foreach (Response response in responses)
            {
                response.Question = question;
                uow.ResponseRepository.Insert(response);
            }

            try
            {
                uow.Save();
                uow.Dispose();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }


        public void AddTest(Test test, List<TestQuestion> testQuestions, List<string> chaptersID)
        {
            UnitOfWork uow = new UnitOfWork();
            List<Chapter> chaptersArr = new List<Chapter>();

            chaptersArr = uow.ChapterRepository.Get(chapter => chaptersID.Contains(chapter.ChapterId.ToString()), null, "").ToList();

            foreach (Chapter chapter in chaptersArr)
            {
                test.Chapters.Add(chapter);
                uow.TestRepository.Insert(test);
            }
            
            foreach (TestQuestion testQ in testQuestions)
            {
                Question quest = GetQuestion(testQ.QuestionId);
                testQ.Question = quest;
                testQ.Test = test;
                test.Punctaj += testQ.Punctaj;
                uow.TestQuestionRepository.Insert(testQ);
            }

            try
            {
                uow.Save();
                uow.Dispose();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }


        public void TestAddQuestion()
        {
            UnitOfWork uow = new UnitOfWork();

            using (StreamReader r = new StreamReader("L:\\Facultate\\TDotNET\\Proiect\\TestModelProiectCodeFirst\\ProjectClassLibrary\\question.json"))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var item in array)
                {
                    foreach (var item2 in item.First)
                    {
                        Question quest = new Question();

                        string chapterName = item2.chapter;
                        Guid questId = Guid.NewGuid();
                        List<Chapter> chapterArr = uow.ChapterRepository.Get(chapter => chapter.Title == chapterName).ToList();

                        quest.Chapter = chapterArr[0];
                        quest.QuestionId = questId;
                        quest.Requirement = item2.requirement;
                        quest.Justification = item2.justification;
                        uow.QuestionRepository.Insert(quest);

                        foreach (var item3 in item2.answers)
                        {
                            Response resp = new Response();
                            Guid respId = Guid.NewGuid();
                            resp.ResponseId = respId;
                            resp.Question = quest;
                            string ans = item3.answer;
                            resp.Answer = ans;
                            resp.Correct = item3.correct;
                            uow.ResponseRepository.Insert(resp);
                        }
                    }
                }
            }
            try
            {
                uow.Save();
                uow.Dispose();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public void PrintQuestions()
        {
            UnitOfWork uow = new UnitOfWork();

            List<Question> questionArr = uow.QuestionRepository.Get().ToList();

            foreach (Question quest in questionArr)
            {
                Console.WriteLine("Chaper: {0}\nRequirement: {1}\nAnswers:", quest.Chapter.Title, quest.Requirement);
                foreach (Response resp in quest.Responses)
                {
                    Console.WriteLine("\tAnswer: {0}\n\tCorrect: {1}", resp.Answer, resp.Correct);
                }
                Console.WriteLine("Justification: {0}", quest.Justification);
            }

            uow.Dispose();
        }
        public void PrintQuestion(Question quest)
        {
            Console.WriteLine("Chaper: {0}\nRequirement: {1}\nAnswers:", quest.Chapter.Title, quest.Requirement);
            foreach (Response resp in quest.Responses)
            {
                Console.WriteLine("\tAnswer: {0}\n\tCorrect: {1}", resp.Answer, resp.Correct);
            }
        }

        public void TestAddTest()
        {
            UnitOfWork uow = new UnitOfWork();
            Guid testId;
            string testTitle;
            int testTTS = 60;
            List<Chapter> chaptersArr;
            string chaptersTitle;
            
            #region Test1
            Test test1 = new Test();
            testId = Guid.NewGuid();
            testTitle = "EF + WCF";

            test1.TestId = testId;
            test1.Title = testTitle;
            test1.TimeToSolve = testTTS;
            test1.NoQuestions = 6;
            chaptersTitle = "EF WCF";

            chaptersArr = uow.ChapterRepository.Get(chapter => chaptersTitle.Contains(chapter.Title), null, "Questions").ToList();
            foreach (Chapter chapter in chaptersArr)
            {
                test1.Chapters.Add(chapter);
                int j = 0;
                for (int i = 0; i < test1.NoQuestions / 2 && j < test1.NoQuestions; i++, j++)
                {
                    TestQuestion testQuest = new TestQuestion();

                    testQuest.TestQuestionId = Guid.NewGuid();
                    testQuest.Question = chapter.Questions.ElementAt(i);
                    testQuest.Test = test1;
                    testQuest.Punctaj = 10;
                    test1.Punctaj += testQuest.Punctaj;
                    uow.TestQuestionRepository.Insert(testQuest);
                }
                uow.TestRepository.Insert(test1);
            }
            #endregion

            #region Test2
            Test test2 = new Test();
            testId = Guid.NewGuid();
            testTitle = "WPF + WCF";

            test2.TestId = testId;
            test2.Title = testTitle;
            test2.TimeToSolve = testTTS;
            test2.NoQuestions = 6;
            chaptersTitle = "WPF WCF";

            chaptersArr = uow.ChapterRepository.Get(chapter => chaptersTitle.Contains(chapter.Title), null, "Questions").ToList();
            foreach (Chapter chapter in chaptersArr)
            {
                test2.Chapters.Add(chapter);
                int j = 0;
                for (int i = 0; i < test2.NoQuestions / 2 && j < test2.NoQuestions; i++, j++)
                {
                    TestQuestion testQuest = new TestQuestion();

                    testQuest.TestQuestionId = Guid.NewGuid();
                    testQuest.Question = chapter.Questions.ElementAt(i);
                    testQuest.Test = test2;
                    testQuest.Punctaj = 10;
                    test2.Punctaj += testQuest.Punctaj;
                    uow.TestQuestionRepository.Insert(testQuest);
                }
                uow.TestRepository.Insert(test2);
            }
            #endregion

            #region Test3
            Test test3 = new Test();
            testId = Guid.NewGuid();
            testTitle = "WPF + ASP";

            test3.TestId = testId;
            test3.Title = testTitle;
            test3.TimeToSolve = testTTS;
            test3.NoQuestions = 6;
            chaptersTitle = "WPF ASP";

            chaptersArr = uow.ChapterRepository.Get(chapter => chaptersTitle.Contains(chapter.Title), null, "Questions").ToList();
            foreach (Chapter chapter in chaptersArr)
            {
                test3.Chapters.Add(chapter);
                int j = 0;
                for (int i = 0; i < test3.NoQuestions / 2 && j < test3.NoQuestions; i++, j++)
                {
                    TestQuestion testQuest = new TestQuestion();
                    testQuest.TestQuestionId = Guid.NewGuid();
                    testQuest.Question = chapter.Questions.ElementAt(i);
                    testQuest.Test = test3;
                    testQuest.Punctaj = 10;
                    test3.Punctaj += testQuest.Punctaj;
                    uow.TestQuestionRepository.Insert(testQuest);
                }
                uow.TestRepository.Insert(test3);
            }
            #endregion

            try
            {
                uow.Save();
                uow.Dispose();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public void PrintTests()
        {
            UnitOfWork uow = new UnitOfWork();

            List<Test> testArr = uow.TestRepository.Get().ToList();

            foreach (Test test in testArr)
            {
                Console.Write("Capitole: ");
                foreach (Chapter chapter in test.Chapters)
                {
                    Console.Write(chapter.Title + " ");
                }
                if (test.TestReferencedId != null)
                    Console.WriteLine("Referencing: {0}", test.TestReferencedId.ToString());
                Console.WriteLine("\nTitle: {0}\nPunctaj: {1}\nTime to solve: {2}\nNumber of Questions: {3}\nQuestions:", test.Title, test.Punctaj, test.TimeToSolve, test.NoQuestions);
                foreach (TestQuestion tq in test.TestQuestions)
                {
                    Console.WriteLine("Puncte: {0}", tq.Punctaj);
                    PrintQuestion(tq.Question);
                }
                Console.WriteLine("\n\n\n");
            }

            uow.Dispose();
        }
        public void PrintTest(Test test)
        {
            Console.Write("Capitole: ");
            foreach (Chapter chapter in test.Chapters)
            {
                Console.Write(chapter.Title + " ");
            }
            Console.WriteLine("\nTitle: {0}\nPunctaj: {1}\nTime to solve: {2}\nNumber of Questions: {3}\nQuestions:", test.Title, test.Punctaj, test.TimeToSolve, test.NoQuestions);
            foreach (TestQuestion tq in test.TestQuestions)
            {
                Console.WriteLine("Puncte: {0}", tq.Punctaj);
                PrintQuestion(tq.Question);
            }
        }

        public void TestAddExamination()
        {
            UnitOfWork uow = new UnitOfWork();
            Guid examId = Guid.NewGuid();
            string studentsEmail = "student1@student.com student2@student.com";
            
            List<User> usrArr = uow.UserRepository.Get(user => studentsEmail.Contains(user.Email)).ToList();
            string studentsID = "";
            foreach (User user in usrArr)
            {
                studentsID += user.StudentId.ToString();
                studentsID += " ";
            }
            List<Student> studArr = uow.StudentRepository.Get(student => studentsID.Contains(student.StudentId.ToString())).ToList();
            List<Test> testArr = uow.TestRepository.Get(test => test.Title == "EF + WCF").ToList();
            Examination exam1 = new Examination();
            exam1.Date = new DateTime(2016, 8, 8);
            exam1.Test = testArr[0];
            exam1.ExaminationId = examId;

            foreach (Student stud in studArr)
            {
                exam1.Students.Add(stud);
            }


            examId = Guid.NewGuid();
            studentsEmail = "student@student.com student3@student.com";
            usrArr = uow.UserRepository.Get(user => studentsEmail.Contains(user.Email)).ToList();
            studentsID = "";
            foreach (User user in usrArr)
            {
                studentsID += user.StudentId.ToString();
                studentsID += " ";
            }
            studArr = uow.StudentRepository.Get(student => studentsID.Contains(student.StudentId.ToString())).ToList();
            testArr = uow.TestRepository.Get(test => test.Title == "WPF + ASP").ToList();
            Examination exam2 = new Examination();
            exam2.Date = new DateTime(2016, 8, 9);
            exam2.Test = testArr[0];
            exam2.ExaminationId = examId;

            foreach (Student stud in studArr)
            {
                exam2.Students.Add(stud);
            }


            uow.ExaminationRepository.Insert(exam1);
            uow.ExaminationRepository.Insert(exam2);


            try
            {
                uow.Save();
                uow.Dispose();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
        public void PrintExaminations()
        {
            UnitOfWork uow = new UnitOfWork();

            List<Examination> examArr = uow.ExaminationRepository.Get().ToList();

            foreach (Examination exam in examArr)
            {
                Console.WriteLine("Examination:\nTest Name: {0}\nDate: {1}\nStudents:", exam.Test.Title, exam.Date.ToString());
                foreach (Student stud in exam.Students)
                {
                    Console.WriteLine("\tName: {0} {1}", stud.FirstName, stud.LastName);
                }
            }
            uow.Dispose();
        }

        public void TestAddSolvedTest()
        {
            UnitOfWork uow = new UnitOfWork();

            List<Test> testArr = uow.TestRepository.Get(test => test.Title == "EF + WCF" && test.TestReferencedId == null, null, "TestQuestions, Chapters").ToList();

            List<Student> studArr = uow.StudentRepository.Get(stud => stud.FirstName == "Student1", null, "Users").ToList();
            
            Test test2 = new Test(testArr[0], uow);
            uow.TestRepository.Insert(test2);

            SolvedTest st = new SolvedTest();
            st.Pending = studArr[0].Users.ElementAt(0).UserType == 0 ? false : true;
            st.Punctaj = test2.Punctaj;
            st.SolvedTestId = Guid.NewGuid();
            st.Student = studArr[0];
            st.Test = test2;
            st.Time = new TimeSpan(4000);

            uow.SolvedTestRepository.Insert(st);
            try
            {
                uow.Save();
                uow.Dispose();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "\n\n" + ex.InnerException + "\n\n" + ex.StackTrace);
            }

        }
        public void PrintSolvedTests()
        {
            UnitOfWork uow = new UnitOfWork();

            List<SolvedTest> solvedTestArr = uow.SolvedTestRepository.Get().ToList();

            foreach (SolvedTest solvedTest in solvedTestArr)
            {
                Console.WriteLine("Test Title: {0}\n\tPoints: {1}\n\tSolved in: {2}\n\tBy Student: {3} {4}   -   Email: {5}", solvedTest.Test.Title, solvedTest.Punctaj, solvedTest.Time.ToString(), solvedTest.Student.LastName, solvedTest.Student.FirstName, solvedTest.Student.Users.ElementAt(0).Email);
            }

            uow.Dispose();
        }

        public void DeleteAll()
        {
            UnitOfWork uow = new UnitOfWork();

            List<Subject> subjArr = uow.SubjectRepository.Get().ToList();
            foreach (Subject subj in subjArr)
            {
                uow.SubjectRepository.Delete(subj);
            }

            List<Student> studArr = uow.StudentRepository.Get().ToList();
            foreach (Student stud in studArr)
            {
                uow.StudentRepository.Delete(stud);
            }


            List<TestQuestion> testQuestionArr = uow.TestQuestionRepository.Get(test => test.TestQuestionReferencedId != null).ToList();
            foreach (TestQuestion testQuestion in testQuestionArr)
            {
                testQuestion.TestQuestionReferencedId = null;
                uow.TestQuestionRepository.Update(testQuestion);
            }
            uow.Save();

            testQuestionArr = uow.TestQuestionRepository.Get().ToList();
            foreach (TestQuestion testQuestion in testQuestionArr)
            {
                uow.TestQuestionRepository.Delete(testQuestion);
            }

            List<Test> testArr = uow.TestRepository.Get(test => test.TestReferencedId != null).ToList();
            foreach (Test test in testArr)
            {
                test.TestReferencedId = null;
                uow.TestRepository.Update(test);
            }
            uow.Save();

            testArr = uow.TestRepository.Get().ToList();
            foreach (Test test in testArr)
            {
                uow.TestRepository.Delete(test);
            }

            List<Question> questArr = uow.QuestionRepository.Get().ToList();
            foreach (Question quest in questArr)
            {
                uow.QuestionRepository.Delete(quest);
            }

            Console.WriteLine("Baza de date a fost golita");

            try
            {
                uow.Save();
                uow.Dispose();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void AddAll()
        {
            TestAddSubject();
            TestAddChapter();
            TestAddStudentUser();
            TestAddQuestion();
            TestAddTest();
            TestAddExamination();
            TestAddSolvedTest();
        }
        #endregion

        #region Modify - Delete - Create
        public void ModifyRemoveCreateQuestion(Question question)
        {
            UnitOfWork uow = new UnitOfWork();
            if(question.Requirement == " ")
            {
                //Remove
                uow.QuestionRepository.Delete(question.QuestionId);
            }
            else
            {
                //Create - Update

                Question quest = uow.QuestionRepository.GetByID(question.QuestionId);
                if(quest == null)
                {
                    //Create
                    uow.QuestionRepository.Insert(question);
                }
                else
                {
                    //Update
                    uow.QuestionRepository.Update(question, question.QuestionId);
                }
            }

            try
            {
                uow.Save();
                uow.Dispose();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
            }
        }
        public void ModifyRemoveCreateTest(Test test)
        {
            UnitOfWork uow = new UnitOfWork();
            if (test.Title == " ")
            {
                //Remove
                uow.TestRepository.Delete(test.TestId);
            }
            else
            {
                //Create - Update

                Test tst = uow.TestRepository.GetByID(test.TestId);
                if (tst == null)
                {
                    //Create
                    uow.TestRepository.Insert(test);
                }
                else
                {
                    //Update
                    uow.TestRepository.Update(test);
                }
            }


            try
            {
                uow.Save();
                uow.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
            }
        }
        #endregion

        #region Get Liste de entitati

        public List<Subject> GetSubjects(string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            List<Subject> subjectsArr = uow.SubjectRepository.Get(null, null, includeProperties).ToList();
            uow.Dispose();

            return subjectsArr;
        }
        public List<Chapter> GetChapters(string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            List<Chapter> chaptersArr = uow.ChapterRepository.Get(null, null, includeProperties).ToList();
            uow.Dispose();

            return chaptersArr;
        }
        public List<Test> GetTests(string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            List<Test> testArr = uow.TestRepository.Get(null, null, includeProperties).ToList();
            uow.Dispose();

            return testArr;
        }
        public List<Student> GetStudents(string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            List<Student> studentsArr = uow.StudentRepository.Get(null, null, includeProperties).ToList();
            uow.Dispose();

            return studentsArr;
        }
        public List<Question> GetQuestions(string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            List<Question> questionsArr = uow.QuestionRepository.Get(null, null, includeProperties).ToList();
            uow.Dispose();

            return questionsArr;
        }
        public List<Examination> GetExaminations(string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            List<Examination> examinationsArr = uow.ExaminationRepository.Get(null, null, includeProperties).ToList();
            uow.Dispose();

            return examinationsArr;
        }
        public List<User> GetUsers(string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            List<User> usersArr = uow.UserRepository.Get(null, null, includeProperties).ToList();
            uow.Dispose();

            return usersArr;
        }
        public List<SolvedTest> GetSolvedTests(string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            List<SolvedTest> solvedTestsArr = uow.SolvedTestRepository.Get(null, null, includeProperties).ToList();
            uow.Dispose();

            return solvedTestsArr;
        }
        public List<TestQuestion> GetTestQuestions(string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            List<TestQuestion> testQuestionsArr = uow.TestQuestionRepository.Get(null, null, includeProperties).ToList();
            uow.Dispose();

            return testQuestionsArr;
        }

        #endregion


        #region Get Entitate

        public Subject GetSubject(Guid subjectID, string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            Subject subject = uow.SubjectRepository.Get(subj => subj.SubjectId == subjectID, null, includeProperties).ToList()[0];
            uow.Dispose();

            return subject;
        }
        public Chapter GetChapter(Guid chapterID, string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            Chapter chapter = uow.ChapterRepository.Get(chap => chap.ChapterId == chapterID, null, includeProperties).ToList()[0];
            uow.Dispose();

            return chapter;
        }
        public Test GetTest(Guid testID, string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            Test test = uow.TestRepository.Get(tst => tst.TestId == testID, null, includeProperties).ToList()[0];
            uow.Dispose();

            return test;
        }
        public Student GetStudent(Guid studentID, string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            Student student = uow.StudentRepository.Get(stud => stud.StudentId == studentID, null, includeProperties).ToList()[0];
            uow.Dispose();

            return student;
        }
        public Question GetQuestion(Guid questionID, string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            Question question = uow.QuestionRepository.Get(quest => quest.QuestionId == questionID, null, includeProperties).ToList()[0];
            uow.Dispose();

            return question;
        }
        public Examination GetExamination(Guid examinationID, string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            Examination examination = uow.ExaminationRepository.Get(exam => exam.ExaminationId == examinationID, null, includeProperties).ToList()[0];
            uow.Dispose();

            return examination;
        }
        public User GetUser(Guid userID, string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            User user = uow.UserRepository.Get(usr => usr.UserId == userID, null, includeProperties).ToList()[0];
            uow.Dispose();

            return user;
        }
        public SolvedTest GetSolvedTest(Guid solvedTestID, string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            SolvedTest solvedTest = uow.SolvedTestRepository.Get(st => st.SolvedTestId == solvedTestID, null, includeProperties).ToList()[0];
            uow.Dispose();

            return solvedTest;
        }
        public TestQuestion GetTestQuestion(Guid testQuestionID, string includeProperties = "")
        {
            UnitOfWork uow = new UnitOfWork();
            TestQuestion testQuestion = uow.TestQuestionRepository.Get(st => st.TestQuestionId== testQuestionID, null, includeProperties).ToList()[0];
            uow.Dispose();

            return testQuestion;
        }

        #endregion
        public int AddOpp(int a, int b)
        {
            return a + b;
        }
    }
}
