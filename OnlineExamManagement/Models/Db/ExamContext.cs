using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineExamManagement.Models.Db
{
    public partial class ExamContext : DbContext
    {
        //public ExamContext()
        //{
        //}

        //public ExamContext(DbContextOptions<ExamContext> options)
        //    : base(options)
        //{
        //}

        public virtual DbSet<Answers> Answers { get; set; }
        public virtual DbSet<Itempictures> Itempictures { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<Topics> Topics { get; set; }
        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Exam;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answers>(entity =>
            {
                entity.HasKey(e => e.AnswerSerial);

                entity.Property(e => e.AnswerSerial)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Answer)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Correct)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.QuestionNo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.QuestionNoNavigation)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.QuestionNo)
                    .HasConstraintName("FK__Answers__Questio__3A81B327");
            });
            modelBuilder.Entity<Itempictures>(entity =>
            {
                entity.HasKey(e => new { e.StudentId, e.Slno });
            });

            modelBuilder.Entity<Questions>(entity =>
            {
                entity.HasKey(e => e.QuestionNo);

                entity.Property(e => e.QuestionNo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TopicsCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.TopicsCodeNavigation)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TopicsCode)
                    .HasConstraintName("FK__Questions__Topic__37A5467C");
            });


            modelBuilder.Entity<Questions>(entity =>
            {
                entity.HasKey(e => e.QuestionNo);

                entity.Property(e => e.QuestionNo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TopicsCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.TopicsCodeNavigation)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.TopicsCode)
                    .HasConstraintName("FK__Questions__Topic__37A5467C");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.StudentId);

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.Class)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Father)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Picture).HasMaxLength(100);

                entity.Property(e => e.StudentName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.HasKey(e => e.SubjectCode);

                entity.Property(e => e.SubjectCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Subjects)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__Subjects__Studen__2C3393D0");
            });

            modelBuilder.Entity<Topics>(entity =>
            {
                entity.HasKey(e => e.TopicsCode);

                entity.Property(e => e.TopicsCode)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.SubjectCode)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.TopicsName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.SubjectCodeNavigation)
                    .WithMany(p => p.Topics)
                    .HasForeignKey(d => d.SubjectCode)
                    .HasConstraintName("FK__Topics__SubjectC__34C8D9D1");
            });
        }
    }
}
