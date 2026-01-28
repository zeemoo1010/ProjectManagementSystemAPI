using ProjectManagement.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Persistence.Context
{
    public class ApplicationDbContext
    {
        public ApplicationDbContext(DbContext<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskItem> TaskItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
