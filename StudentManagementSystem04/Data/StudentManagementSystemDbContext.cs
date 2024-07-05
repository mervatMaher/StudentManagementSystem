using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem04.Model;

namespace StudentManagementSystem04.Data
{
    public class StudentManagementSystemDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Lecture> Lectures { get; set; }
        public DbSet<UniProject> UniProjects { get; set; }
        public DbSet<PersonalProject> PersonalProjects { get; set; }
        public DbSet<Model.Task> Tasks { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<AttachedImage> AttachedImages { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<StudentLecture> StudentLectures { get; set; }
        public DbSet<StudentUniProject> StudentUniProjects { get; set; }
        public StudentManagementSystemDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().HasMany(e => e.Likes).WithOne(e => e.User).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<ApplicationUser>().HasMany(e => e.comments).WithOne(e => e.User).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<ApplicationUser>().HasMany(e => e.Subjects).WithOne(e => e.User).OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ApplicationUser>().Ignore(e => e.Email);

            modelBuilder.Entity<Student>().HasOne(e => e.User);
            modelBuilder.Entity<Student>().HasMany(e => e.Subjects).WithMany(e => e.Students);
            modelBuilder.Entity<Student>().Property(e => e.CoverPhoto).IsRequired(false);


            modelBuilder.Entity<Student>().HasOne(e => e.Level).WithMany(e => e.Students).OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<Student>().HasMany(e => e.PersonalProjects).WithOne(e => e.Student).OnDelete(DeleteBehavior.ClientCascade);

            //modelBuilder.Entity<Student>().HasMany(e => e.UniProjects).WithMany(e => e.Students);

            modelBuilder.Entity<Lecture>().HasMany(e => e.Students).WithMany(e => e.lectures).UsingEntity<StudentLecture>(
               r => r.HasOne<Student>().WithMany().HasForeignKey(e => e.StudentId).OnDelete(DeleteBehavior.ClientCascade),
               l => l.HasOne<Lecture>().WithMany().HasForeignKey(e => e.LectureId).OnDelete(DeleteBehavior.ClientCascade));

            modelBuilder.Entity<Level>().HasMany(e => e.ApplicationUsers).WithMany(e => e.Levels);


            modelBuilder.Entity<StudentUniProject>()
              .HasKey(sup => new { sup.StudentId, sup.UniProjectId });
            modelBuilder.Entity<StudentUniProject>()
              .HasOne(sup => sup.Student)
              .WithMany(s => s.StudentUniProjects)
              .HasForeignKey(sup => sup.StudentId);

            modelBuilder.Entity<StudentUniProject>()
                .HasOne(sup => sup.UniProject)
                .WithMany(u => u.StudentUniProjects)
                .HasForeignKey(sup => sup.UniProjectId);


            modelBuilder.Entity<UniProject>().HasMany(e => e.Likes).WithOne(e => e.UniProject).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<UniProject>().HasMany(e => e.Comments).WithOne(e => e.UniProject).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<UniProject>().HasMany(e => e.AttachedImages).WithOne(e => e.UniProject).OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<PersonalProject>().HasMany(e => e.Likes).WithOne(e => e.PersonalProject).OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<PersonalProject>().HasMany(e => e.Comments).WithOne(e => e.PersonalProject).OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}
