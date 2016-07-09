namespace ProjectClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;
    [Table("User")]
    [DataContract(IsReference = true)]
    public partial class User
    {
        [DataMember]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        [DataMember]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public int UserType { get; set; }

        [DataMember]
        public Guid StudentId { get; set; }

        [DataMember]
        public virtual Student Student { get; set; }
    }
}
