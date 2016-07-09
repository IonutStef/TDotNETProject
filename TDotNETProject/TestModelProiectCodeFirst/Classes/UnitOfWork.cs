using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModelProiectCodeFirst
{
    public class UnitOfWork : IDisposable
    {
        private ModelTestProiect context = new ModelTestProiect();
        private GenericRepository<Chapter>      chapterRepository;
        private GenericRepository<Subject>      subjectRepository;
        private GenericRepository<Test>         testRepository;
        private GenericRepository<Question>     questionRepository;
        private GenericRepository<TestQuestion> testQuestionRepository;
        private GenericRepository<SolvedTest>   solvedTestRepository;
        private GenericRepository<Examination>  examinationRepository;
        private GenericRepository<Student>      studentRepository;
        private GenericRepository<User>         userRepository;
        private GenericRepository<Response>     responseRepository;
        //unitOfWork.DepartmentRepository.Insert()
        public GenericRepository<Chapter> ChapterRepository
        {
            get
            {

                if (this.chapterRepository == null)
                {
                    this.chapterRepository = new GenericRepository<Chapter>(context);
                }
                return chapterRepository;
            }
        }

        public GenericRepository<Subject> SubjectRepository
        {
            get
            {

                if (this.subjectRepository == null)
                {
                    this.subjectRepository = new GenericRepository<Subject>(context);
                }
                return subjectRepository;
            }
        }

        public GenericRepository<Test> TestRepository
        {
            get
            {
                if (this.testRepository == null)
                {
                    this.testRepository = new GenericRepository<Test>(context);
                }
                return testRepository;
            }
        }

        public GenericRepository<SolvedTest> SolvedTestRepository
        {
            get
            {
                if (this.solvedTestRepository == null)
                {
                    this.solvedTestRepository = new GenericRepository<SolvedTest>(context);
                }
                return solvedTestRepository;
            }
        }

        public GenericRepository<Examination> ExaminationRepository
        {
            get
            {
                if (this.examinationRepository == null)
                {
                    this.examinationRepository = new GenericRepository<Examination>(context);
                }
                return examinationRepository;
            }
        }

        public GenericRepository<Question> QuestionRepository
        {
            get
            {

                if (this.questionRepository == null)
                {
                    this.questionRepository = new GenericRepository<Question>(context);
                }
                return questionRepository;
            }
        }

        public GenericRepository<TestQuestion> TestQuestionRepository
        {
            get
            {

                if (this.testQuestionRepository == null)
                {
                    this.testQuestionRepository = new GenericRepository<TestQuestion>(context);
                }
                return testQuestionRepository;
            }
        }

        public GenericRepository<Student> StudentRepository
        {
            get
            {

                if (this.studentRepository == null)
                {
                    this.studentRepository = new GenericRepository<Student>(context);
                }
                return studentRepository;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get
            {

                if (this.userRepository == null)
                {
                    this.userRepository = new GenericRepository<User>(context);
                }
                return userRepository;
            }
        }

        public GenericRepository<Response> ResponseRepository
        {
            get
            {

                if (this.responseRepository == null)
                {
                    this.responseRepository = new GenericRepository<Response>(context);
                }
                return responseRepository;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
