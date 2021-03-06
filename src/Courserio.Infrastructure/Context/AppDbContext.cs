using Microsoft.EntityFrameworkCore;
using Courserio.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courserio.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<Course> Courses { get; set; }
        //public DbSet<Tag> Tags { get; set; }
        //public DbSet<Section> Sections { get; set; }
        //public DbSet<Chapter> Chapters { get; set; }
        //public DbSet<Question> Questions { get; set; }
        //public DbSet<Answer> Answers { get; set; }
        //public DbSet<RoleApplication> RoleApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        
    }
}
