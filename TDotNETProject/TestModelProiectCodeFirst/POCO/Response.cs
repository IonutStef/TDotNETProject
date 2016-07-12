namespace TestModelProiectCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Response")]
    public partial class Response
    {
        public Response() { Duplicate = false; }
        public Response(Response resp, Question quest)
        {
            ResponseId = Guid.NewGuid();
            Question = quest;
            Answer = resp.Answer;
            Duplicate = true;
            Correct = false;
        }
        public Guid ResponseId { get; set; }

        public bool Duplicate { get; set; }
        [Required]
        public string Answer { get; set; }

        public bool Correct { get; set; }

        public Guid QuestionId { get; set; }

        public virtual Question Question { get; set; }
    }
}
