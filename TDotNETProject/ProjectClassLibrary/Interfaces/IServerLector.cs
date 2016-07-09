using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace ProjectClassLibrary
{
    //[ServiceContract(CallbackContract = typeof(IServerLectorCallBack))]
    [ServiceContract]
    public interface IServerLector
    {
        //[OperationContract]
        //bool LogIn(string username, string password);
        //[OperationContract]
        //int ProductRegister(int a, int b);

        //[OperationContract(IsOneWay = true)]
        //void CallFromClient();
        #region Resetare Baza de date - Proiect 1

        [OperationContract(IsOneWay = true)]
        void TestAddSubject();
        [OperationContract(IsOneWay = true)]
        void PrintSubjects();
        [OperationContract(IsOneWay = true)]
        void TestAddChapter();
        [OperationContract(IsOneWay = true)]
        void PrintChapters();
        [OperationContract(IsOneWay = true)]
        void TestAddStudentUser();
        [OperationContract(IsOneWay = true)]
        void PrintStudents();
        [OperationContract(IsOneWay = true)]
        void TestAddQuestion();
        [OperationContract(IsOneWay = true)]
        void PrintQuestions();
        [OperationContract(IsOneWay = true)]
        void TestAddTest();
        [OperationContract(IsOneWay = true)]
        void PrintTests();
        [OperationContract(IsOneWay = true)]
        void TestAddExamination();
        [OperationContract(IsOneWay = true)]
        void PrintExaminations();
        [OperationContract(IsOneWay = true)]
        void TestAddSolvedTest();
        [OperationContract(IsOneWay = true)]
        void PrintSolvedTests();
        [OperationContract(IsOneWay = true)]
        void DeleteAll();
        [OperationContract(IsOneWay = true)]
        void ModifyRemoveCreateQuestion(Question question);
        [OperationContract(IsOneWay = true)]
        void ModifyRemoveCreateTest(Test test);
        [OperationContract]
        int AddOpp(int a, int b);
        [OperationContract(IsOneWay = true)]
        void AddAll();

        #endregion

        [OperationContract]
        List<Subject> GetSubjects(string includeProperties = "");
        [OperationContract]
        List<Chapter> GetChapters(string includeProperties = "");
        [OperationContract]
        List<Test> GetTests(string includeProperties = "");
        [OperationContract]
        List<Student> GetStudents(string includeProperties = "");
        [OperationContract]
        List<Question> GetQuestions(string includeProperties = "");
        [OperationContract]
        List<Examination> GetExaminations(string includeProperties = "");
        [OperationContract]
        List<User> GetUsers(string includeProperties = "");
        [OperationContract]
        List<TestQuestion> GetTestQuestions(string includeProperties = "");
        [OperationContract]
        List<SolvedTest> GetSolvedTests(string includeProperties = "");


        [OperationContract]
        Subject GetSubject(Guid subjectID, string includeProperties = "");
        [OperationContract]
        Chapter GetChapter(Guid chapterID, string includeProperties = "");
        [OperationContract]
        Test GetTest(Guid testID, string includeProperties = "");
        [OperationContract]
        Student GetStudent(Guid studentID, string includeProperties = "");
        [OperationContract]
        Question GetQuestion(Guid questionID, string includeProperties = "");
        [OperationContract]
        Examination GetExamination(Guid examinationID, string includeProperties = "");
        [OperationContract]
        User GetUser(Guid userID, string includeProperties = "");
        [OperationContract]
        TestQuestion GetTestQuestion(Guid testQuestionID, string includeProperties = "");
        [OperationContract]
        SolvedTest GetSolvedTest(Guid solvedTestID, string includeProperties = "");

        [OperationContract]
        void AddQuestion(Question question, List<Response> responses);

        [OperationContract]
        void AddTest(Test test, List<TestQuestion> testQuestions, List<string> chaptersID);
    }

    public interface IServerLectorCallBack
    {
        [OperationContract(IsOneWay = true)]
        void NotifyTestAdded();


        [OperationContract(IsOneWay = true)]
        void CalledByServer();
    }
}
