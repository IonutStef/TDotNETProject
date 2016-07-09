namespace TestModelProiectCodeFirst
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SolvedTest")]
    public partial class SolvedTest
    {
        public Guid SolvedTestId { get; set; }

        public Guid StudentId { get; set; }

        public Guid TestId { get; set; }

        public TimeSpan Time { get; set; }

        public int Punctaj { get; set; }

        public bool? Pending { get; set; }

        public virtual Student Student { get; set; }

        public virtual Test Test { get; set; }
    }
}
