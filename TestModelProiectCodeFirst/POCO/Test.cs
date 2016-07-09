namespace TestModelProiectCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Test")]
    public partial class Test
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Test()
        {
            Examinations = new HashSet<Examination>();
            SolvedTests = new HashSet<SolvedTest>();
            Test1 = new HashSet<Test>();
            TestQuestions = new HashSet<TestQuestion>();
            Chapters = new HashSet<Chapter>();
            Duplicate = false;
        }

        public Test(Test test1, UnitOfWork uow)
        {
            Examinations = new HashSet<Examination>();
            SolvedTests = new HashSet<SolvedTest>();
            TestQuestions = new HashSet<TestQuestion>();
            Test2 = test1;
            TestId = Guid.NewGuid();
            Title = test1.Title;
            Duplicate = true;
            TimeToSolve = test1.TimeToSolve;
            Description = test1.Description;
            NoQuestions = test1.NoQuestions;
            //TestReferencedId = null;
            Punctaj = 0;
            foreach(TestQuestion tq in test1.TestQuestions)
            {
                TestQuestion tqAux = new TestQuestion(tq, uow, this);
                uow.TestQuestionRepository.Insert(tqAux);

                TestQuestions.Add(tqAux);
            }
            Chapters = test1.Chapters;
        }

        public Guid TestId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        public bool Duplicate { get; set; }
        public Guid? TestReferencedId { get; set; }

        public int TimeToSolve { get; set; }

        public int NoQuestions { get; set; }

        public int Punctaj { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Examination> Examinations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SolvedTest> SolvedTests { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Test> Test1 { get; set; }

        public virtual Test Test2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Chapter> Chapters { get; set; }
    }
}
