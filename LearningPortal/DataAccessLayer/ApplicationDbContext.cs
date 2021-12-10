using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningPortal.Domains;
using LearningPortal.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LearningPortal.DataAccessLayer
{
    public class ApplicationDbContext:IdentityDbContext<AppUserModel>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }


    }
}
