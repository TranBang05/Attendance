using Microsoft.OData.Edm.Vocabularies;
using Microsoft.EntityFrameworkCore;
using System;

namespace AttendanceApi.Models
{
    public class AttendanceContext : DbContext
    {
        public AttendanceContext()
        {
        }

        public AttendanceContext(DbContextOptions<AttendanceContext> options)
           : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<StudentSchedule> StudentSchedules { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<StudentRole> StudentRoles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conStr = "server=LAPTOP-0LAB0G9K\\TXB_SQLLAP;database=Attendance ;user=sa;user=sa;password=123;TrustServerCertificate=true";
                optionsBuilder.UseSqlServer(conStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>()
                .HasKey(sc => sc.Id);
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.StudentCourses)
                .HasForeignKey(sc => sc.StudentId);
            modelBuilder.Entity<StudentCourse>()
                .HasOne(sc => sc.Course)
                .WithMany(c => c.StudentCourses)
                .HasForeignKey(sc => sc.CourseId);


            modelBuilder.Entity<StudentSchedule>()
                .HasKey(ss => ss.Id);
            modelBuilder.Entity<StudentSchedule>()
                .HasOne(ss => ss.Student)
                .WithMany(s => s.StudentSchedules)
                .HasForeignKey(ss => ss.StudentId);
            modelBuilder.Entity<StudentSchedule>()
                .HasOne(ss => ss.Schedule)
                .WithMany(s => s.StudentSchedules)
                .HasForeignKey(ss => ss.ScheduleId);



            modelBuilder.Entity<Course>()
                .HasOne(c => c.Subject)
                .WithMany(s => s.Courses)
                .HasForeignKey(c => c.SubjectId);



            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Course)
                .WithMany(c => c.Schedules)
                .HasForeignKey(s => s.CourseId);

            modelBuilder.Entity<Schedule>()
                .HasOne(s => s.Teacher)
                .WithMany(t => t.Schedules)
                .HasForeignKey(s => s.TeacherId);


            modelBuilder.Entity<Subject>()
                .HasKey(s => s.Id);
            Seeding(modelBuilder);

            modelBuilder.Entity<StudentRole>()
                 .HasKey(sc => sc.Id);

            modelBuilder.Entity<StudentRole>()
                .HasOne(sr => sr.Student)
                .WithMany(s => s.StudentRoles)
                .HasForeignKey(sr => sr.StudentId);

            modelBuilder.Entity<StudentRole>()
                .HasOne(sr => sr.Role)
                .WithMany(r => r.StudentRoles)
                .HasForeignKey(sr => sr.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasKey(sc => sc.Id);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);
        }
        private void Seeding(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Subject>().HasData(
                new Subject { Id = 1, Code = "PRN231", Name = "Building Cross-Platform Back-End Application With .NET", NumberSlot = 20 },
                new Subject { Id = 2, Code = "EXE121", Name = "Experiential Entrepreneurship 2", NumberSlot = 20 },
                new Subject { Id = 3, Code = "PRM231", Name = "Mobile Programming", NumberSlot = 20 },
                new Subject { Id = 4, Code = "MLN111", Name = "Philosophy of Marxism – Leninism", NumberSlot = 20 },
                new Subject { Id = 5, Code = "WDU102", Name = "UI/UX Design", NumberSlot = 20 }
            );


            modelBuilder.Entity<Teacher>().HasData(
                new Teacher { Id = 1, Name = "ChiLp" },
                new Teacher { Id = 2, Name = "AnhLH75" },
                new Teacher { Id = 3, Name = "LoanBT7" },
                new Teacher { Id = 4, Name = "Khuyendtv" },
                new Teacher { Id = 5, Name = "TienTd" }
            );


            modelBuilder.Entity<Course>().HasData(
                new Course { Id = 1, Code = "PRN231_SE1705", Name = "Building Cross-Platform Back-End ", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3), SubjectId = 1 },
                new Course { Id = 2, Code = "PRN231_SE1706", Name = "Building Cross-Platform Back-End ", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(3), SubjectId = 1 }

            );



            modelBuilder.Entity<Student>().HasData(
                new Student { Id = 1, Name = "Tran Xuan Bang", Email = "bangtx@example.com", Password = "bang0501@bb", Mssv = "HE163986" },
                new Student { Id = 2, Name = "Nguyen Van Tien", Email = "tiennv@example.com", Password = "1223", Mssv = "HE163087" },
                new Student { Id = 3, Name = "Vu Gia Huy", Email = "huyvg@example.com", Password = "111", Mssv = "HE163188" },
                new Student { Id = 4, Name = "Nguyen Huy Long", Email = "longnh@example.com", Password = "111", Mssv = "HE163389" },
                new Student { Id = 5, Name = "Do Ngoc Duc", Email = "ducdn@example.com", Password = "111", Mssv = "SHE163966" }
            );

            modelBuilder.Entity<Schedule>().HasData(
                new Schedule { Id = 1, Slot = "A24", Date = DateTime.Now, CourseId = 1, TeacherId = 1 },
                new Schedule { Id = 2, Slot = "A35", Date = DateTime.Now.AddDays(1), CourseId = 2, TeacherId = 1 }

            );


            modelBuilder.Entity<StudentCourse>().HasData(
                new StudentCourse { Id = 1, StudentId = 1, CourseId = 1 },
                new StudentCourse { Id = 2, StudentId = 2, CourseId = 1 },
                new StudentCourse { Id = 3, StudentId = 3, CourseId = 1 },
                new StudentCourse { Id = 4, StudentId = 4, CourseId = 2 },
                new StudentCourse { Id = 5, StudentId = 5, CourseId = 2 }
            );


            modelBuilder.Entity<StudentSchedule>().HasData(
                new StudentSchedule { Id = 1, StudentId = 1, ScheduleId = 1, Status = AttendanceStatus.Present },
                new StudentSchedule { Id = 2, StudentId = 2, ScheduleId = 1, Status = AttendanceStatus.Absent },
                new StudentSchedule { Id = 3, StudentId = 3, ScheduleId = 1, Status = AttendanceStatus.Present },
                new StudentSchedule { Id = 4, StudentId = 4, ScheduleId = 2, Status = AttendanceStatus.Absent },
                new StudentSchedule { Id = 5, StudentId = 5, ScheduleId = 2, Status = AttendanceStatus.Absent }
                );

            modelBuilder.Entity<Role>().HasData(
               new Role { Id = 1, Name = "Admin" },
               new Role { Id = 2, Name = "Teacher" },
               new Role { Id = 3, Name = "Student" }
            );

            modelBuilder.Entity<Permission>().HasData(
                new Permission { Id = 1, Name = "View" },
                new Permission { Id = 2, Name = "Edit" },
                new Permission { Id = 3, Name = "Create" },
                new Permission { Id = 4, Name = "Delete" }
            );

            modelBuilder.Entity<StudentRole>().HasData(
                new StudentRole { Id = 1, StudentId = 1, RoleId = 1 },
                new StudentRole { Id = 2, StudentId = 2, RoleId = 2 },
                new StudentRole { Id = 3, StudentId = 3, RoleId = 3 },
                new StudentRole { Id = 4, StudentId = 4, RoleId = 3 },
                new StudentRole { Id = 5, StudentId = 5, RoleId = 3 }
            );

            modelBuilder.Entity<RolePermission>().HasData(
                new RolePermission { Id = 1, RoleId = 1, PermissionId = 1 },
                new RolePermission { Id = 2, RoleId = 1, PermissionId = 2 },
                new RolePermission { Id = 3, RoleId = 1, PermissionId = 3 },
                new RolePermission { Id = 4, RoleId = 1, PermissionId = 4 },
                new RolePermission { Id = 5, RoleId = 2, PermissionId = 1 },
                new RolePermission { Id = 6, RoleId = 2, PermissionId = 3 }
            );

        }
    
            
    }
}
