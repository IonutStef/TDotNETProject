namespace ProjectClassLibrary
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.Serialization;
    [Table("Chapter")]
    [DataContract(IsReference = true)]
    public partial class Chapter
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Chapter()
        {
            Questions = new HashSet<Question>();
            Tests = new HashSet<Test>();
        }

        [DataMember]
        public Guid ChapterId { get; set; }

        [DataMember]
        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [DataMember]
        public Guid SubjectId { get; set; }

        [DataMember]
        public virtual Subject Subject { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Test> Tests { get; set; }
    }
}
