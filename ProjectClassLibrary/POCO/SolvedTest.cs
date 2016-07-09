namespace ProjectClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;
    [Table("SolvedTest")]
    [DataContract(IsReference = true)]
    public partial class SolvedTest
    {
        [DataMember]
        public Guid SolvedTestId { get; set; }

        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public Guid TestId { get; set; }

        [DataMember]
        public TimeSpan Time { get; set; }

        [DataMember]
        public int Punctaj { get; set; }

        [DataMember]
        public bool? Pending { get; set; }

        [DataMember]
        public virtual Student Student { get; set; }

        [DataMember]
        public virtual Test Test { get; set; }
    }
}
