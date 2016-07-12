namespace ProjectClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;
    [Table("TestQuestion")]
    [DataContract(IsReference = true)]
    public partial class TestQuestion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TestQuestion()
        {
            TestQuestion1 = new HashSet<TestQuestion>();
        }

        public TestQuestion(TestQuestion tq, UnitOfWork uow, Test test)
        {
            Punctaj = 0;
            Test = test;
            this.TestQuestionId = Guid.NewGuid();
            Duplicate = true;
            TestQuestion2 = tq;
            Question quest = uow.QuestionRepository.GetByID(tq.QuestionId);
            this.Question = new Question(quest, uow);
            //this.Test = test;
            uow.QuestionRepository.Insert(Question);
            //uow.Save();
        }

        [DataMember]
        public Guid TestId { get; set; }

        [DataMember]
        public Guid QuestionId { get; set; }

        [DataMember]
        public int Punctaj { get; set; }

        [DataMember]
        public bool? Duplicate { get; set; }

        [DataMember]
        public Guid TestQuestionId { get; set; }

        [DataMember]
        public Guid? TestQuestionReferencedId { get; set; }

        [DataMember]
        public virtual Question Question { get; set; }

        [DataMember]
        public virtual Test Test { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestQuestion> TestQuestion1 { get; set; }

        [DataMember]
        public virtual TestQuestion TestQuestion2 { get; set; }
    }
}
