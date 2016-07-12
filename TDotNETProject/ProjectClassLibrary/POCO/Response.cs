namespace ProjectClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;
    [Table("Response")]
    [DataContract(IsReference = true)]
    public partial class Response
    {
        public Response() { }
        public Response(Response resp, Question quest)
        {
            ResponseId = Guid.NewGuid();
            Question = quest;
            Answer = resp.Answer;
            Duplicate = true;
            Correct = false;
        }

        [DataMember]
        public Guid ResponseId { get; set; }

        [DataMember]
        [Required]
        public string Answer { get; set; }

        [DataMember]
        public bool Correct { get; set; }

        [DataMember]
        public Guid QuestionId { get; set; }

        [DataMember]
        public bool? Duplicate { get; set; }

        [DataMember]
        public virtual Question Question { get; set; }
    }
}
