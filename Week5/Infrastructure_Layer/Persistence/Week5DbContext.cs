using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Week5.Domain_Layer.Entity;

namespace Week5.Infrastructure
{
    public class Week5DbContext : DbContext
    {
        public Week5DbContext(DbContextOptions<Week5DbContext> options) : base(options) 
        {
            if (Database.GetConnectionString() == null)
            {
                throw new InvalidOperationException("❌ Connection String is not set in Week5DbContext.");
            }
            Console.WriteLine($"✅ Week5DbContext Connected: {Database.GetConnectionString()}");
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Professor> Professor { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<BehaviorScore> BehaviorScore { get; set; }
        public DbSet<Major> Major { get; set; }
        public DbSet<StudentClass> StudentClass { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StudentClass>()
                .HasKey(sc => new { sc.StudentID, sc.ClassID });

            modelBuilder.Entity<StudentClass>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentClass)
                .HasForeignKey(sc => sc.StudentID);

            modelBuilder.Entity<StudentClass>()
                .HasOne(sc => sc.Class)
                .WithMany(c => c.StudentClass)
                .HasForeignKey(sc => sc.ClassID);

            modelBuilder.Entity<BehaviorScore>()
                .HasKey(b => b.ScoreID);

            modelBuilder.Entity<BehaviorScore>()
                .HasOne(b => b.Student)
                .WithMany(s => s.BehaviorScore)
                .HasForeignKey(b => b.StudentID);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Major)
                .WithMany(m => m.Students)
                .HasForeignKey(s => s.MajorID);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Professor)
                .WithMany(p => p.Student)
                .HasForeignKey(s => s.ProfessorID);

            modelBuilder.Entity<Student>()
            .HasMany(s => s.BehaviorScore)
            .WithOne(bs => bs.Student)
            .HasForeignKey(bs => bs.StudentID)
            .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
