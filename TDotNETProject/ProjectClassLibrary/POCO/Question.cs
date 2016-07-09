namespace ProjectClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Runtime.Serialization;
    [Table("Question")]
    [DataContract(IsReference = true)]
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
            Justification = "";
            Duplicate = true;
            Chapter = quest.Chapter;
            Requirement = quest.Requirement;
            List<Response> responses = uow.ResponseRepository.Get(r => r.QuestionId == quest.QuestionId).ToList();
            foreach (Response resp in responses)
            {
                Response respAux = new Response(resp, this);
                uow.ResponseRepository.Insert(respAux);
                //Responses.Add(respAux);
            }
            //uow.Save();
        }

        [DataMember]
        public Guid QuestionId { get; set; }

        [DataMember]
        [Required]
        public string Requirement { get; set; }

        [DataMember]
        public string Justification { get; set; }

        [DataMember]
        public Guid ChapterId { get; set; }

        [DataMember]
        public bool? Duplicate { get; set; }

        [DataMember]
        public virtual Chapter Chapter { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Response> Responses { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
    }
}
