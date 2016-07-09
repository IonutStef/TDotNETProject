namespace TestModelProiectCodeFirst
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ModelTestProiect : DbContext
    {
        public ModelTestProiect()
            : base("name=ModelTestProiect")
        {
        }

        public virtual DbSet<Chapter> Chapters { get; set; }
        public virtual DbSet<Examination> Examinations { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Response> Responses { get; set; }
        public virtual DbSet<SolvedTest> SolvedTests { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestQuestion> TestQuestions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chapter>()
                .HasMany(e => e.Tests)
                .WithMany(e => e.Chapters)
                .Map(m => m.ToTable("ChapterTest").MapLeftKey("ChapterId").MapRightKey("TestId"));

            modelBuilder.Entity<Examination>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Examination>()
                .HasMany(e => e.Students)
                .WithMany(e => e.Examinations)
                .Map(m => m.ToTable("StudentExamination").MapLeftKey("ExaminationId").MapRightKey("StudentId"));

            modelBuilder.Entity<Question>()
                .Property(e => e.Requirement)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Justification)
                .IsUnicode(false);

            modelBuilder.Entity<Response>()
                .Property(e => e.Answer)
                .IsUnicode(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Subjects)
                .WithMany(e => e.Students)
                .Map(m => m.ToTable("StudentSubject").MapLeftKey("StudentId").MapRightKey("SubjectId"));

            modelBuilder.Entity<Subject>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Subject>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Test>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Test>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Test>()
                .HasMany(e => e.Test1)
                .WithOptional(e => e.Test2)
                .HasForeignKey(e => e.TestReferencedId);
        }
    }
}
