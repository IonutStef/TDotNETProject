namespace TestModelProiectCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Question")]
    public partial class Question
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Question()
        {
            Responses = new HashSet<Response>();
            TestQuestions = new HashSet<TestQuestion>();
            Duplicate = false;
        }

        public Question(Question quest, UnitOfWork uow)
        {
            TestQuestions = new HashSet<TestQuestion>();
            Responses = new HashSet<Response>();
            QuestionId = Guid.NewGuid();
            ReferencedQuestionId = new Guid(quest.QuestionId.ToString());
            Justification = "";
            Duplicate = true;
            Chapter = quest.Chapter;
            Requirement = quest.Requirement;

            foreach(Response resp in quest.Responses)
            {
                Response respAux = new Response(resp, this);
                uow.ResponseRepository.Insert(respAux);
                //Responses.Add(respAux);
            }
            //uow.Save();
        }

        public Guid QuestionId { get; set; }

        public bool Duplicate { get; set; }
        [Required]
        public string Requirement { get; set; }
        
        public Guid? ReferencedQuestionId { get; set; }

        public string Justification { get; set; }

        public Guid ChapterId { get; set; }

        public virtual Chapter Chapter { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Response> Responses { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
    }
}
