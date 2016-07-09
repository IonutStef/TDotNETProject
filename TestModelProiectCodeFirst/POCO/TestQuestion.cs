namespace TestModelProiectCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TestQuestion")]
    public partial class TestQuestion
    {
        public TestQuestion() { Duplicate = false; }
        public TestQuestion(TestQuestion tq, UnitOfWork uow, Test test)
        {
            Punctaj = 0;
            Test = test;
            Duplicate = true;
            Question = new Question(tq.Question, uow);
            uow.QuestionRepository.Insert(Question);
            //uow.Save();
        }
        [Key]
        [Column(Order = 0)]
        public Guid TestId { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid QuestionId { get; set; }

        public bool Duplicate { get; set; }
        public int Punctaj { get; set; }

        public virtual Question Question { get; set; }

        public virtual Test Test { get; set; }
    }
}
