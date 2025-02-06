using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Week5.Domain;

namespace Week5.Infrastructure
{
    public class Week5DbContext : DbContext
    {
        public Week5DbContext(DbContextOptions<Week5DbContext> options) : base(options) { }

        // กำหนด DbSet สำหรับตารางที่คุณมี เช่น
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

            // กำหนดให้ ScoreID เป็น Primary Key
            modelBuilder.Entity<BehaviorScore>()
                .HasKey(b => b.ScoreID);  // กำหนด ScoreID เป็น Primary Key

            modelBuilder.Entity<BehaviorScore>()
                .HasOne(b => b.Student)
                .WithMany(s => s.BehaviorScore)
                .HasForeignKey(b => b.StudentID);
        }

    }
}
